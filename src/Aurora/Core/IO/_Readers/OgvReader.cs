using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Aurora.Utilities;
using Microsoft.Xna.Framework.Media;
using ReLogic.Content;
using ReLogic.Content.Readers;
using ReLogic.Utilities;

// Code written and provided by @Mirsario at GitHub:
// https://github.com/Mirsario/TerrariaOverhaul/blob/dev/Core/VideoPlayback/OgvReader.cs
namespace Aurora.Core.IO;

[Autoload(false)]
public sealed class OgvReader : IAssetReader, ILoadable
{
	/// <summary>
	///     The file extension associated with this asset reader's assets.
	/// </summary>
	public const string Extension = ".ogv";
	
	private static readonly Theorafile.tf_callbacks Callbacks = new() {
		read_func = ReadFunction,
		seek_func = SeekFunction,
		close_func = CloseFunction
	};

	private static readonly Dictionary<IntPtr, UnmanagedMemoryStream> Streams = [];

	async ValueTask<T> IAssetReader.FromStream<T>(Stream stream, MainThreadCreationContext mainThreadCtx) where T : class {
		if (typeof(T) != typeof(Video)) {
			throw AssetLoadException.FromInvalidReader<OgvReader, T>();
		}

		await mainThreadCtx;

		var video = BuildVideo(stream);

		return video as T;
	}

	void ILoadable.Load(Mod mod) {
		var collection = Main.instance.Services.Get<AssetReaderCollection>();

		collection.TryRegisterReader(new OgvReader(), Extension);
	}

	void ILoadable.Unload() {
		var collection = Main.instance.Services.Get<AssetReaderCollection>();

		collection.TryRemoveReader(Extension);
	}

	private static unsafe Video BuildVideo(Stream stream) {
		using var memoryStream = new MemoryStream();

		stream.CopyTo(memoryStream);

		var numBytes = (int)memoryStream.Position;
		var dataPtr = Marshal.AllocHGlobal(numBytes);
		
		var unmanagedStream = new UnmanagedMemoryStream((byte*)dataPtr, numBytes, numBytes, FileAccess.ReadWrite);

		memoryStream.Seek(0, SeekOrigin.Begin);
		memoryStream.CopyTo(unmanagedStream, numBytes);
		unmanagedStream.Seek(0L, SeekOrigin.Begin);

		Streams[dataPtr] = unmanagedStream;

		var video = (Video)FormatterServices.GetUninitializedObject(typeof(Video));

		var result = Theorafile.tf_open_callbacks(dataPtr, out var theoraPtr, Callbacks);

		if (result != 0) {
			throw new InvalidOperationException($"Theorafile returned code '{result}' when trying to load data.");
		}

		Theorafile.tf_videoinfo(theoraPtr, out var yWidth, out var yHeight, out var fps, out var fmt);

		int uvWidth;
		int uvHeight;

		if (fmt == Theorafile.th_pixel_fmt.TH_PF_420) {
			uvWidth = yWidth / 2;
			uvHeight = yHeight / 2;
		}
		else if (fmt == Theorafile.th_pixel_fmt.TH_PF_422) {
			uvWidth = yWidth / 2;
			uvHeight = yHeight;
		}
		else if (fmt == Theorafile.th_pixel_fmt.TH_PF_444) {
			uvWidth = yWidth;
			uvHeight = yHeight;
		}
		else {
			throw new NotSupportedException("Unrecognized YUV format!");
		}

		video.GraphicsDevice = Main.graphics.GraphicsDevice;
		
		video.needsDurationHack = true;
		
		video.theora = theoraPtr;
		
		video.yWidth = yWidth;
		video.yHeight = yHeight;
		
		video.uvWidth = uvWidth;
		video.uvHeight = uvHeight;
		
		video.fps = fps;
		
		video.Duration = TimeSpan.MaxValue;

		return video;
	}

	private static unsafe IntPtr ReadFunction(IntPtr ptr, IntPtr size, IntPtr nmemb, IntPtr dataSource) {
		if (!Streams.TryGetValue(dataSource, out var stream)) {
			return IntPtr.Zero;
		}

		var numBytes = (int)(nmemb * size);
		var span = new Span<byte>((void*)ptr, numBytes);
		var numRead = stream.Read(span);

		return numRead;
	}

	private static int SeekFunction(IntPtr dataSource, long offset, Theorafile.SeekWhence whence) {
		if (!Streams.TryGetValue(dataSource, out var stream)) {
			return 0;
		}

		var seekOrigin = whence switch {
			Theorafile.SeekWhence.TF_SEEK_SET => SeekOrigin.Begin,
			Theorafile.SeekWhence.TF_SEEK_CUR => SeekOrigin.Current,
			Theorafile.SeekWhence.TF_SEEK_END => SeekOrigin.End,
			_ => throw new InvalidDataException($"{nameof(Theorafile.SeekWhence)} value made no sense")
		};
		
		var newPosition = stream.Seek(offset, seekOrigin);

		return (int)newPosition;
	}

	private static int CloseFunction(IntPtr dataSource) {
		if (!Streams.Remove(dataSource, out var stream)) {
			return 0;
		}

		stream.Dispose();
		
		Marshal.FreeHGlobal(dataSource);

		return 1;
	}
}

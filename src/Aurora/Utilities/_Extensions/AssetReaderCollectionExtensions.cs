using System.Linq;
using ReLogic.Content;
using ReLogic.Content.Readers;

namespace Aurora.Utilities;

/// <summary>
///		Provides <see cref="AssetReaderCollection"/> extensions.
/// </summary>
public static class AssetReaderCollectionExtensions
{
	public static bool TryRegisterReader<T>(this AssetReaderCollection collection, T reader, string extension) where T : IAssetReader {
		if (collection.TryGetReader(extension, out _)) {
			return false;
		}
		
		collection.RegisterReader(reader, extension);

		return true;
	}
	
	public static bool TryRemoveReader(this AssetReaderCollection collection, string extension) {
		if (!collection.TryGetReader(extension, out _)) {
			return false;
		}

		collection._readersByExtension.Remove(extension);
		collection._extensions = collection._readersByExtension.Keys.ToArray();

		return true;
	}
}

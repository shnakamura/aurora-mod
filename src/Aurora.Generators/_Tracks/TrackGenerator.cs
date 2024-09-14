using System.IO;
using System.Text;
using Hjson;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;

namespace Aurora.Generators
{
	[Generator(LanguageNames.CSharp)]
	public sealed class TrackGenerator : IIncrementalGenerator
	{
		/// <summary>
		///     The file extension associated with this generator.
		/// </summary>
		public const string Extension = ".track";

		public void Initialize(IncrementalGeneratorInitializationContext initializationContext) {
			var files = initializationContext.AdditionalTextsProvider.Where(file => Path.GetExtension(file.Path) == Extension);

			var contents = files.Select(
				(text, token) => (
					Name: Path.GetFileNameWithoutExtension(text.Path),
					Text: text.GetText(token).ToString())
			);

			initializationContext.RegisterSourceOutput(
				contents,
				(sourceContext, content) => {
					var json = HjsonValue.Parse(content.Text).ToString(Stringify.Plain);
					var track = JsonConvert.DeserializeObject<Track>(json);

					sourceContext.AddSource($"{content.Name}.g.cs", BuildTrack(content.Name, in track));
				}
			);
		}

		private static string BuildTrack(string name, in Track track) {
			var builder = new StringBuilder();

			builder.Append("[");

			for (var i = 0; i < track.Signals.Length; i++) {
				builder.Append('"');
				builder.Append(track.Signals[i]);
				builder.Append('"');

				if (i >= track.Signals.Length - 1) {
					continue;
				}
				
				builder.Append(',');
				builder.Append(' ');
			}

			builder.Append("]");
			
			return $@"using Terraria.Audio;
using ReLogic.Utilities;

namespace Aurora.Common.Ambience;

public sealed class {name} : ITrack
{{
	public SoundStyle Sound {{ get; init; }} = new(""{track.SoundStyleData.SoundPath}"", SoundType.Ambient) {{
		Volume = 0.8f,
		IsLooped = true
	}};

	public string[] Signals {{ get; init; }} = {builder};

	public float StepIn {{ get; init; }} = {track.StepIn}f;

	public float StepOut {{ get; init; }} = {track.StepOut}f;

	public float Volume {{
		get => _volume;
		set => _volume = MathHelper.Clamp(value, 0f, 1f);
	}}

	private float _volume;

	public SlotId Slot {{ get; set; }}

	public {name}() {{ }}
}}";
		}
	}
}

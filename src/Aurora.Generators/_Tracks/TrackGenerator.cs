using System.IO;
using Microsoft.CodeAnalysis;

namespace Aurora.Generators
{
	[Generator(LanguageNames.CSharp)]
	public sealed class TrackGenerator : IIncrementalGenerator
	{
		/// <summary>
		///		The file extension associated with this generator.
		/// </summary>
		public const string Extension = ".track";
		
		public void Initialize(IncrementalGeneratorInitializationContext context) {
			var files = context.AdditionalTextsProvider.Where(file => Path.GetExtension(file.Path) == Extension);
			
			var contents = files.Select((text, token) => (
				Name: Path.GetFileNameWithoutExtension(text.Path), 
				Text: text.GetText(token).ToString())
			);
			
			context.RegisterSourceOutput(contents,
				(source, content) => {
					source.AddSource($"{content.Name}.g.cs", BuildTrack(content.Name, default));	
				}
			);
		}

		private static string BuildTrack(string name, in Track track) {
			return $@"using Terraria.Audio;
using ReLogic.Utilities;

namespace Aurora.Common.Ambience;

public sealed class {name} : ITrack
{{	
	public SoundStyle Sound {{ get; init; }} = new(""{track.Data.SoundPath}"", SoundType.Ambient) {{
		Volume = 0.8f
	}};

	public string[] Signals {{ get; init; }} = [];

	public float StepIn {{ get; init; }} = 0.05f;

	public float StepOut {{ get; init; }} = 0.05f;

	public float Volume {{
		get => volume;
		set => volume = MathHelper.Clamp(value, 0f, 1f);
	}}	

	private float volume;

	public SlotId Slot {{ get; set; }}

	public {name}() {{ }}
}}";
		}
	}
}

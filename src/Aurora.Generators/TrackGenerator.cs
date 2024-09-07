using System;
using System.IO;
using Hjson;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;

namespace Aurora.Generators
{
	[Generator(LanguageNames.CSharp)]
	public sealed class TrackGenerator : IIncrementalGenerator
	{
		public const string Extension = ".ambience";

		public void Initialize(IncrementalGeneratorInitializationContext context) {
			var files = context.AdditionalTextsProvider.Where(file => Path.GetExtension(file.Path) == Extension);
			
			var contents = files.Select((text, token) => (
				Name: Path.GetFileNameWithoutExtension(text.Path), 
				Text: text.GetText(token).ToString())
			);
			
			context.RegisterSourceOutput(contents,
				(source, content) => {
					source.AddSource(content.Name, BuildTrack(content.Name));	
				}
			);
		}

		private static string BuildTrack(string name) {
			return $@"using ReLogic.Utilities;
using Terraria.Audio;

namespace Aurora.Common.Ambience;

public struct {name} : IAmbienceTrack
{{
	public AmbienceTrackSoundData SoundData {{ get; set; }} = new() {{
		SoundPath = ""Bingus""
	}};

	public string[] Signals {{ get; set; }} = [""Bingus""];

	public float Volume {{ 
        get => volume;
        set => volume = MathHelper.Clamp(value, 0f, 1f);
	}}

	private float volume;

    public SlotId Slot {{ get; set; }} = SlotId.Invalid;

    public float StepIn {{ get; set; }} = 0.05f;

    public float StepOut {{ get; set; }} = 0.05f;


	public {name}() {{ }}
}}";
		}
	}
}

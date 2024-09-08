using System;
using System.IO;
using System.Text;
using Microsoft.CodeAnalysis;

namespace Aurora.Generators
{
	[Generator(LanguageNames.CSharp)]
	public sealed class FootstepGenerator : IIncrementalGenerator
	{
		public const string Extension = ".footstep";

		public void Initialize(IncrementalGeneratorInitializationContext context) {
			var files = context.AdditionalTextsProvider.Where(file => Path.GetExtension(file.Path) == Extension);
			
			var contents = files.Select((text, token) => (
				Name: Path.GetFileNameWithoutExtension(text.Path), 
				Text: text.GetText(token).ToString())
			);
			
			context.RegisterSourceOutput(contents,
				(source, content) => {
					source.AddSource($"{content.Name}.g.cs", BuildFootstep(content.Name));	
				}
			);
		}

		private static string BuildFootstep(string name) {
			return $@"namespace Aurora.Common.Footsteps;

public sealed class {name} : IFootstep
{{
	public FootstepSoundData SoundData {{ get; set; }} = new() {{
		SoundPath = """",
		Variants = 1
	}};

	public string Material {{ get; set; }} = """";

	public {name}() {{ }}
}}";
		}
	}
}

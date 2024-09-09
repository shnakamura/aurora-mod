﻿using System;
using System.IO;
using System.Text;
using Microsoft.CodeAnalysis;

namespace Aurora.Generators
{
	[Generator(LanguageNames.CSharp)]
	public sealed class FootstepGenerator : IIncrementalGenerator
	{
		/// <summary>
		///		The file extension associated with this generator.
		/// </summary>
		public const string Extension = ".footstep";
		
		public void Initialize(IncrementalGeneratorInitializationContext initializationContext) {
			var files = initializationContext.AdditionalTextsProvider.Where(file => Path.GetExtension(file.Path) == Extension);
			
			var contents = files.Select((text, token) => (
				Name: Path.GetFileNameWithoutExtension(text.Path), 
				Text: text.GetText(token).ToString())
			);
			
			initializationContext.RegisterSourceOutput(contents,
				(sourceContext, content) => {
					var footstep = new Footstep();
					
					sourceContext.AddSource($"{content.Name}.g.cs", BuildFootstep(content.Name, in footstep));	
				}
			);
		}

		private static string BuildFootstep(string name, in Footstep footstep) {
			return $@"using Terraria.Audio;

namespace Aurora.Common.Ambience;

public sealed class {name} : IFootstep
{{
	public SoundStyle Sound {{ get; init; }} = new(""{footstep.Data.SoundPath}"", {footstep.Data.Variants}, SoundType.Ambient) {{
		Volume = 0.2f,
		SoundLimitBehavior = SoundLimitBehavior.ReplaceOldest
	}};

	public string Material {{ get; init; }} = ""{footstep.Material}"";

	public {name}() {{ }}
}}";
		}
	}
}

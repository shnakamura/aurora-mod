using System;
using System.IO;
using Microsoft.CodeAnalysis;

namespace Aurora.Generators
{
	[Generator(LanguageNames.CSharp)]
	public sealed class FootstepGenerator : IIncrementalGenerator
	{
		public const string Extension = ".footstep";

		public void Initialize(IncrementalGeneratorInitializationContext context) {
			var files = context.AdditionalTextsProvider.Where(file => Path.GetExtension(file.Path) == Extension);
			var contents = files.Select((text, token) => (Name: Path.GetFileNameWithoutExtension(text.Path), Text: text.GetText(token).ToString()));

		}
	}
}

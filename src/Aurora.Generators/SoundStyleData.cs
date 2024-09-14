using Newtonsoft.Json;

namespace Aurora.Generators
{
	public struct SoundStyleData
	{
		[JsonRequired]
		public string SoundPath;

		public int Variants;
	}
}

using Newtonsoft.Json;

namespace Aurora.Generators
{
	public struct Footstep
	{
		[JsonRequired]
		public SoundStyleData SoundStyleData;

		[JsonRequired]
		public string Material;
	}
}

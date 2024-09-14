using Newtonsoft.Json;

namespace Aurora.Generators
{
	public class Track
	{
		[JsonRequired]
		public SoundStyleData SoundStyleData;

		[JsonRequired]
		public string[] Signals;

		public float StepIn = 0.05f;

		public float StepOut = 0.05f;
	}
}

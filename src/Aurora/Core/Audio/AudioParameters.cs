namespace Aurora.Core.Audio;

public struct AudioParameters
{
	/// <summary>
	///		The intensity of the low pass filter. Ranges from <c>0f</c> to <c>1f</c>.
	/// </summary>
	public float LowPass {
		get => _lowPass;
		set => _lowPass = MathHelper.Clamp(value, 0f, 1f);
	}

    private float _lowPass;
}

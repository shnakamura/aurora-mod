namespace Aurora.Core.Audio;

public struct AudioParameters
{
	public float LowPass {
		get => _lowPass;
		set => _lowPass = MathHelper.Clamp(value, 0f, 1f);
	}
	
    private float _lowPass;
}

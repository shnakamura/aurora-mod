using Terraria.Graphics.CameraModifiers;

namespace Aurora.Common.Camera;

[Autoload(Side = ModSide.Client)]
public sealed class SmoothingCameraModifier : ICameraModifier, ILoadable
{
	string ICameraModifier.UniqueIdentity { get; } = $"{nameof(Aurora)}:{nameof(SmoothingCameraModifier)}";
	
	bool ICameraModifier.Finished { get; } = false;

	private Vector2 Offset {
		get => offset;
		set => offset = Vector2.Clamp(value, new Vector2(-16f), new Vector2(16f));
	}
	
	private Vector2 offset;
	
	void ICameraModifier.Update(ref CameraInfo info) {
		Offset = Vector2.SmoothStep(Offset, Main.LocalPlayer.velocity, 0.3f);
		
		info.CameraPosition += Offset;
	}
	
	void ILoadable.Load(Mod mod) {
		Main.instance.CameraModifiers.Add(this);
	}
	
	void ILoadable.Unload() { }
}

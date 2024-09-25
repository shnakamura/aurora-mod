namespace Aurora.Core.EC;

public abstract class Component
{
	public AuroraEntity Entity { get; set; }

	public virtual void Update() { }

	public virtual void Render() { }
}

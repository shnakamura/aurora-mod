namespace Aurora.Core.EC;

public abstract class Component : IComponent
{
	public Entity Parent { get; }

	public virtual void Update() { }

	public virtual void Render() { }
}

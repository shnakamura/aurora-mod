namespace Aurora.Core.EC;

public abstract class Component : IComponent
{
	public IEntity Entity { get; set; }

	public virtual void Update() { }

	public virtual void Render() { }
}

namespace Aurora.Core.EC;

public abstract class Component : IComponent
{
	/// <summary>
	///		The entity this component is attached to.
	/// </summary>
	public Entity Entity { get; }

	/// <summary>
	///		Called every update cycle for this component's logic.
	/// </summary>
	public virtual void Update() { }

	/// <summary>
	///		Called every update cycle for this component's rendering logic.
	/// </summary>
	public virtual void Render() { }
}

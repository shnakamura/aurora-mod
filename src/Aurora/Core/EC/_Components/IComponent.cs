namespace Aurora.Core.EC;

public interface IComponent
{
	Entity Entity { get; }

	void Update();

	void Render();
}

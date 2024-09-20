namespace Aurora.Core.EC;

public interface IComponent
{
	Entity Parent { get; }

	void Update();

	void Render();
}

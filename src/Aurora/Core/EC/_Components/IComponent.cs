namespace Aurora.Core.EC;

public interface IComponent
{
	IEntity Entity { get; set; }

	void Update();

	void Render();
}

namespace Aurora.Core.EC;

public interface IEntity
{
	int Id { get; }

	bool Active { get; set; }

	T Get<T>() where T : IComponent;

	T Set<T>(T? value) where T : IComponent;

	bool Has<T>() where T : IComponent;

	bool Remove<T>() where T : IComponent;
}

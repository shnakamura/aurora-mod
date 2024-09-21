namespace Aurora.Core.EC;

public interface IEntity
{
	int Id { get; }

	bool Active { get; set; }

	T Get<T>() where T : class, IComponent;

	T Set<T>(T value) where T : class, IComponent;

	bool Has<T>() where T : class, IComponent;

	bool Remove<T>() where T : class, IComponent;
}

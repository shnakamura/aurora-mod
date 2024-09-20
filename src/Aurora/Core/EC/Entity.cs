using System.Collections.Generic;

namespace Aurora.Core.EC;

public struct Entity : IEntity
{
	public int Id { get; }

	public bool Active {
		get => EntitySystem.GetActive(Id);
		set => EntitySystem.SetActive(Id, value);
	}

	internal Entity(int id) {
		Id = id;
	}

	public T Get<T>() where T : IComponent {
		return ComponentSystem.Get<T>(Id);
	}

	public T Set<T>(T? value) where T : IComponent {
		return ComponentSystem.Set(Id, value);
	}

	public bool Has<T>() where T : IComponent {
		return ComponentSystem.Has<T>(Id);
	}

	public bool Remove<T>() where T : IComponent {
		return ComponentSystem.Remove<T>(Id);
	}
}

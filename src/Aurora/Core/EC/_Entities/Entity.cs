namespace Aurora.Core.EC;

public struct Entity : IEntity
{
	/// <summary>
	///		The unique identifier of this entity.
	/// </summary>
	public int Id { get; }

	/// <summary>
	///		Whether this entity is active or not.
	/// </summary>
	public bool Active {
		get => EntitySystem.GetActive(Id);
		set => EntitySystem.SetActive(Id, value);
	}

	internal Entity(int id) {
		Id = id;
	}

	/// <summary>
	///		Retrieves a component of the specified type from this entity.
	/// </summary>
	/// <typeparam name="T">The type of the component to retrieve.</typeparam>
	/// <returns>The instance of the component if found; otherwise, <c>null</c>.</returns>
	public T Get<T>() where T : class, IComponent {
		return ComponentSystem.Get<T>(Id);
	}

	/// <summary>
	///		Sets the value of a component of the specified type to this entity.
	/// </summary>
	/// <param name="value">The value of the component to set.</param>
	/// <typeparam name="T">The type of the component to set.</typeparam>
	/// <returns>The assigned component instance.</returns>
	public T Set<T>(T? value) where T : class, IComponent {
		value.Entity = this;

		return ComponentSystem.Set(Id, value);
	}

	/// <summary>
	///		Checks whether this entity has a component of the specified type or not.
	/// </summary>
	/// <typeparam name="T">The type of component to check.</typeparam>
	/// <returns><c>true</c> if the component was found; otherwise, <c>false</c>.</returns>
	public bool Has<T>() where T : class, IComponent {
		return ComponentSystem.Has<T>(Id);
	}

	/// <summary>
	///		Attempts to remove a component of the specified type from this entity.
	/// </summary>
	/// <typeparam name="T">The type of the component to remove.</typeparam>
	/// <returns><c>true</c> if the component was successfully removed; otherwise, <c>false</c>.</returns>
	public bool Remove<T>() where T : class, IComponent {
		return ComponentSystem.Remove<T>(Id);
	}
}

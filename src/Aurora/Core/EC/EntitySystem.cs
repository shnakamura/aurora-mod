using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Aurora.Core.EC;

public sealed class EntitySystem : ModSystem
{
	private static readonly List<Entity> Entities = [];

	/// <summary>
	///		Creates a new instance of an entity.
	/// </summary>
	/// <param name="activate">Whether to activate the entity instance or not.</param>
	/// <returns>The created entity instance.</returns>
	public static Entity Create(bool activate = true) {
		var entity = new Entity();

		if (activate) {
			Entities.Add(entity);
		}

		return entity;
	}

	/// <summary>
	///		Removes an instance of an entity.
	/// </summary>
	/// <param name="entity">The entity to remove.</param>
	/// <returns><c>true</c> if the entity was successfully removed; otherwise, <c>false</c>.</returns>
	public static bool Remove(Entity entity) {
		return Entities.Remove(entity);
	}
}

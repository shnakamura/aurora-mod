using System.Collections.Concurrent;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Aurora.Core.EC;

public sealed class EntitySystem : ModSystem
{
	private static readonly List<int> ActiveEntityIds = [];
	private static readonly List<int> InactiveEntityIds = [];

	private static readonly ConcurrentBag<int> FreeEntityIds = [];

	private static int NextEntityId;

	/// <summary>
	///		Creates a new instance of an entity.
	/// </summary>
	/// <param name="activate">Whether to activate the entity instance or not.</param>
	/// <returns>The created entity instance.</returns>
	public static Entity Create(bool activate) {
		int id;

		if (!FreeEntityIds.TryTake(out id)) {
			id = NextEntityId++;
		}

		if (activate) {
			ActiveEntityIds.Add(id);
		}

		return new Entity(id);
	}

	/// <summary>
	///		Removes an instance of an entity from its unique identifier.
	/// </summary>
	/// <param name="entityId">The identity of the entity to remove.</param>
	/// <returns><c>true</c> if the entity was successfully removed; otherwise, <c>false</c>.</returns>
	public static bool Remove(int id) {
		if (id < 0) {
			return false;
		}

		ActiveEntityIds.Remove(id);
		InactiveEntityIds.Remove(id);

		FreeEntityIds.Add(id);

		return true;
	}

	internal static bool GetActive(int entityId) {
		if (entityId < 0) {
			return false;
		}

		return ActiveEntityIds.Contains(entityId);
	}

	internal static void SetActive(int entityId, bool value) {
		if (entityId < 0) {
			return;
		}

		if (value) {
			ActiveEntityIds.Add(entityId);
			InactiveEntityIds.Remove(entityId);
			return;
		}

		ActiveEntityIds.Remove(entityId);
		InactiveEntityIds.Add(entityId);
	}
}

using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Aurora.Core.EC;

public sealed class EntitySystem : ModSystem
{
	private sealed class Transform(Vector2 position, Vector2 scale, float rotation = 0f) : IComponent
	{
		public Vector2 Position = position;

		public Vector2 Scale = scale;

		public float Rotation = rotation;

		public override string ToString() {
			return $"Position: {Position} @ Scale: {Scale} @ Rotation: {Rotation}";
		}

		void IComponent.Update() { }

		void IComponent.RenderUpdate() { }
	}

	private static readonly List<Entity> Entities = [];

	private static Entity entity;

	public override void OnWorldLoad() {
		base.OnWorldLoad();

		entity = EntitySystem.Create();
	}

	public override void PostUpdateWorld() {
		base.PostUpdateWorld();

		entity.Set(new Transform(new Vector2(500f), Vector2.One));

		if (Main.keyState.IsKeyDown(Keys.F) && !Main.oldKeyState.IsKeyDown(Keys.F)) {
			entity.Remove<Transform>();
		}

		Main.NewText(entity.Has<Transform>() + " @ " + entity.Get<Transform>());
	}

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

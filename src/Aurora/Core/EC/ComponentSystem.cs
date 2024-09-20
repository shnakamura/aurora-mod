using Aurora.Utilities;

namespace Aurora.Core.EC;

public sealed class ComponentSystem : ModSystem
{
	private static class ComponentData<T> where T : IComponent
	{
		public static readonly int Id = componentTypeCount++;
		public static readonly int Mask = 1 << Id;

		public static long[] Flags = [];

		public static T?[] Components = [];

		static ComponentData() {
			OnUpdate += OnUpdateEvent;
			OnRender += OnRenderEvent;
		}

		private static void OnUpdateEvent() {
			for (var i = 0; i < Components.Length; i++) {
				var component = Components[i];

				if (!component.Parent.Active) {
					continue;
				}

				component.Update();
			}
		}

		private static void OnRenderEvent() {
			for (var i = 0; i < Components.Length; i++) {
				var component = Components[i];

				if (!component.Parent.Active) {
					continue;
				}

				component.Render();
			}
		}
	}

	private static int componentTypeCount;

	private static event Action? OnUpdate;
	private static event Action? OnRender;

	public override void Load() {
		base.Load();

		On_Main.DrawProjectiles += DrawProjectilesHook;
	}

	public override void PostUpdateWorld() {
		base.PostUpdateWorld();

		OnUpdate?.Invoke();
	}

	public static bool Has<T>(int entityId) where T : IComponent {
		if (entityId < 0 || entityId >= ComponentData<T>.Components.Length) {
			return false;
		}

		return (ComponentData<T>.Flags[entityId] & ComponentData<T>.Mask) != 0;
	}

	public static T Get<T>(int entityId) where T : IComponent {
		if (entityId < 0 || entityId >= ComponentData<T>.Components.Length) {
			return default;
		}

		return ComponentData<T>.Components[entityId];
	}

	public static T Set<T>(int entityId, T? value) where T : IComponent {
		if (entityId >= ComponentData<T>.Components.Length) {
			var newSize = Math.Max(1, ComponentData<T>.Components.Length);

			while (newSize <= entityId) {
				newSize *= 2;
			}

			Array.Resize(ref ComponentData<T>.Components, newSize);
		}

		if (entityId >= ComponentData<T>.Flags.Length) {
			var newSize = Math.Max(1, ComponentData<T>.Flags.Length);

			while (newSize <= entityId) {
				newSize *= 2;
			}

			Array.Resize(ref ComponentData<T>.Flags, newSize);
		}

		ComponentData<T>.Components[entityId] = value;
		ComponentData<T>.Flags[entityId] |= ComponentData<T>.Mask;

		return ComponentData<T>.Components[entityId];
	}

	public static bool Remove<T>(int entityId) where T : IComponent {
		if (entityId < 0 || entityId >= ComponentData<T>.Components.Length) {
			return false;
		}

		ComponentData<T>.Components[entityId] = default;
		ComponentData<T>.Flags[entityId] &= ~ComponentData<T>.Mask;

		return true;
	}

	private static void DrawProjectilesHook(On_Main.orig_DrawProjectiles orig, Main self) {
		orig(self);

		OnRender?.Invoke();
	}
}

// This file is placed outside of the common 'Utilities/Extensions' scope for the sake of convenience when using components.
namespace Aurora.Common.NPCs.Components;

/// <summary>
///     Provides basic extensions that interface with <see cref="NPCComponent"/>.
/// </summary>
public static class NPCComponentExtensions
{
    /// <summary>
    ///     Attempts to enable a specified component on an NPC.
    /// </summary>
    /// <param name="npc">The NPC on which the component is to be enabled.</param>
    /// <param name="initializer">An optional delegate to initialize the component after it has been enabled.</param>
    /// <typeparam name="T">The type of the component to enable, which must inherit from <see cref="ProjectileComponent"/>.</typeparam>
    /// <returns><c>true</c> if the component was successfully enabled; otherwise, <c>false</c>.</returns>
    public static bool TryEnableComponent<T>(this NPC npc, Action<T>? initializer = null) where T : NPCComponent {
        var hasComponent = npc.TryGetGlobalNPC(out T? component);
        
        if (!hasComponent) {
            return false;
        }

        component.Enabled = true;

        initializer?.Invoke(component);

        return true;
    }
}

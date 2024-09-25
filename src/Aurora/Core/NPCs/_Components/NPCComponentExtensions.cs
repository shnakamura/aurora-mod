// This file is placed outside of the common 'Utilities/Extensions' scope for the sake of convenience when using components.

namespace Aurora.Core.NPCs;

/// <summary>
///     Provides <see cref="NPC"/> extensions that interface with <see cref="NPCComponent" />.
/// </summary>
public static class NPCComponentExtensions
{
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

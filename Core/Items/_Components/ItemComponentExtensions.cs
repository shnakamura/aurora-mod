// This file is placed outside of the common 'Utilities/Extensions' scope for the sake of convenience when using components.

namespace Aurora.Core.Items;

/// <summary>
///     Provides basic extensions that interface with <see cref="ItemComponent" />.
/// </summary>
public static class ItemComponentExtensions
{
    /// <summary>
    ///     Attempts to enable a specified component on an item.
    /// </summary>
    /// <param name="item">The item on which the component is to be enabled.</param>
    /// <param name="initializer">An optional delegate to initialize the component after it has been enabled.</param>
    /// <typeparam name="T">The type of the component to enable, which must inherit from <see cref="ItemComponent" />.</typeparam>
    /// <returns><c>true</c> if the component was successfully enabled; otherwise, <c>false</c>.</returns>
    public static bool TryEnableComponent<T>(this Item item, Action<T>? initializer = null) where T : ItemComponent {
        var hasComponent = item.TryGetGlobalItem(out T? component);

        if (!hasComponent) {
            return false;
        }

        component.Enabled = true;

        initializer?.Invoke(component);

        return true;
    }
}

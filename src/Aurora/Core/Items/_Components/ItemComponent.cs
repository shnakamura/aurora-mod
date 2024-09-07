namespace Aurora.Core.Items;

public abstract class ItemComponent : GlobalItem
{
    public sealed override bool InstancePerEntity { get; } = true;

    /// <summary>
    ///     Whether the component is enabled or not.
    /// </summary>
    public bool Enabled { get; set; }
}

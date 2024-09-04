using Aurora.Content.Gores;
using Aurora.Core.Items;

namespace Aurora.Common.Items;

public sealed class Shotgun : GlobalItem
{
    public override bool AppliesToEntity(Item entity, bool lateInstantiation) {
        return entity.type == ItemID.Shotgun;
    }

    public override void SetDefaults(Item entity) {
        base.SetDefaults(entity);

        entity.TryEnableComponent<ItemBulletCasings>(
            static c => {
                c.Data.Type = ModContent.GoreType<ShellCasing>();
                c.Data.Amount = 2;
            }
        );
    }
}

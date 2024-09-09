using Aurora.Common.Effects;
using Aurora.Content.Gores;
using Aurora.Core.Items;

namespace Aurora.Common.Items;

public sealed class QuadBarrelShotgun : GlobalItem
{
    public override bool AppliesToEntity(Item entity, bool lateInstantiation) {
        return entity.type == ItemID.QuadBarrelShotgun;
    }

    public override void SetDefaults(Item entity) {
        base.SetDefaults(entity);

        entity.TryEnableComponent<ItemBulletCasings>(
            static c => {
                c.Data.Type = ModContent.GoreType<ShellCasing>();
                c.Data.Amount = 4;
            }
        );
    }
}

using Aurora.Content.Gores;
using Aurora.Core.Items;

namespace Aurora.Common.Items;

public sealed class FlintlockPistol : GlobalItem
{
    public override bool AppliesToEntity(Item entity, bool lateInstantiation) {
        return entity.type == ItemID.FlintlockPistol;
    }

    public override void SetDefaults(Item entity) {
        base.SetDefaults(entity);

        entity.TryEnableComponent<ItemBulletCasings>(static c => { c.Data.Type = ModContent.GoreType<BulletCasing>(); });
    }
}

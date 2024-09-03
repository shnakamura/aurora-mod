using Aurora.Content.Gores;
using Aurora.Core.Items;

namespace Aurora.Common.Items;

public sealed class CoinGun : GlobalItem
{
    public override bool AppliesToEntity(Item entity, bool lateInstantiation) {
        return entity.type == ItemID.CoinGun;
    }

    public override void SetDefaults(Item entity) {
        base.SetDefaults(entity);

        entity.TryEnableComponent<ItemBulletCasings>(static c => { c.Data.Type = ModContent.GoreType<BulletCasing>(); });
    }
}

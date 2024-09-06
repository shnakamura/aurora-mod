using Aurora.Content.Gores;
using Aurora.Core.Items;

namespace Aurora.Common.Items;

public sealed class Gatligator : GlobalItem
{
    public override bool AppliesToEntity(Item entity, bool lateInstantiation) {
        return entity.type == ItemID.Gatligator;
    }

    public override void SetDefaults(Item entity) {
        entity.TryEnableComponent<ItemBulletCasings>(static c => { c.Data.Type = ModContent.GoreType<BulletCasing>(); });
    }
}

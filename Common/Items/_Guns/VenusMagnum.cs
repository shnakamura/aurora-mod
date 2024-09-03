using Aurora.Content.Gores;
using Aurora.Core.Items;

namespace Aurora.Common.Items;

public sealed class VenusMagnum : GlobalItem
{
    public override bool AppliesToEntity(Item entity, bool lateInstantiation) {
        return entity.type == ItemID.VenusMagnum;
    }

    public override void SetDefaults(Item entity) {
        base.SetDefaults(entity);
        
        entity.TryEnableComponent<ItemBulletCasings>(static c => { c.Data.Type = ModContent.GoreType<BulletCasing>(); });
    }
}

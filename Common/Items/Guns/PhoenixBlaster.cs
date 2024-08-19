using Aurora.Common.Items.Components;
using Aurora.Common.Items.Effects;
using Aurora.Content.Gores;

namespace Aurora.Common.Items.Guns;

public sealed class PhoenixBlaster : GlobalItem
{
    public override bool AppliesToEntity(Item entity, bool lateInstantiation) {
        return entity.type == ItemID.PhoenixBlaster;
    }

    public override void SetDefaults(Item entity) {
        base.SetDefaults(entity);

        entity.TryEnableComponent<ItemBulletCasings>(c => { c.Data.Type = ModContent.GoreType<BulletCasing>(); });
    }
}

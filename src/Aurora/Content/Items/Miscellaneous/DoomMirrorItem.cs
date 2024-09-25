using Aurora.Common.Behavior;
using Aurora.Common.Movement;
using Aurora.Core.EC;
using Aurora.Core.Graphics;
using Aurora.Core.Physics;

namespace Aurora.Content.Items.Miscellaneous;

public class DoomMirrorItem : ModItem
{
    public override void SetDefaults() {
        base.SetDefaults();

        Item.maxStack = Item.CommonMaxStack;

        Item.consumable = true;

        Item.width = 36;
        Item.height = 34;

        Item.useTime = 25;
        Item.useAnimation = 25;
        Item.useStyle = ItemUseStyleID.HoldUp;

        Item.rare = ItemRarityID.Blue;
    }

    public override bool CanUseItem(Player player) {
	    return player.lastDeathPostion != Vector2.Zero;
    }

    public override bool? UseItem(Player player) {
	    for (var i = 0; i < 20; i++) {
		    var entity = EntitySystem.Create(true);

		    entity.Set(new Transform(player.Center, new Vector2(Main.rand.NextFloat(0.4f, 1f))));

		    entity.Set(
			    new Velocity(
				    Main.rand.NextFloat(-1f, 1f),
				    Main.rand.NextFloat(-1f, 1f)
			    )
		    );

		    entity.Set(
			    new LinearCircularMotion(
				    Main.rand.NextFloat(0.05f, 0.2f),
				    Main.rand.NextFloat(2f, 4f),
				    Main.rand.NextBool() ? 1 : -1
			    )
		    );

		    entity.Set(new OpacityTimeLeft());

		    entity.Set(new Duration(120));

		    var texture = Mod.Assets.Request<Texture2D>("Assets/Textures/Particles/DoomMirror");

		    var info = new SpriteBatchRenderInfo(
			    texture,
			    Color.Red,
			    null,
			    texture.Size() / 2f
		    );

		    entity.Set(new PixellatedTextureRenderer(info));
	    }

	    player.Teleport(player.lastDeathPostion - player.Size / 2f, TeleportationStyleID.DebugTeleport);
	    player.velocity = Vector2.Zero;

	    for (var i = 0; i < 20; i++) {
		    var entity = EntitySystem.Create(true);

		    entity.Set(new Transform(player.Center, new Vector2(Main.rand.NextFloat(0.4f, 1f))));

		    entity.Set(
			    new Velocity(
					Main.rand.NextFloat(-1f, 1f),
					Main.rand.NextFloat(-1f, 1f)
			    )
			);

		    entity.Set(
			    new LinearCircularMotion(
				    Main.rand.NextFloat(0.05f, 0.2f),
				    Main.rand.NextFloat(2f, 4f),
				    Main.rand.NextBool() ? 1 : -1
			    )
		    );

		    entity.Set(new OpacityTimeLeft());

		    entity.Set(new Duration(120));

		    var texture = Mod.Assets.Request<Texture2D>("Assets/Textures/Particles/DoomMirror");

		    var info = new SpriteBatchRenderInfo(
				texture,
				Color.Red,
				null,
				texture.Size() / 2f
			);

		    entity.Set(new PixellatedTextureRenderer(info));
	    }

	    return true;
    }
}

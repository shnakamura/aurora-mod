using Terraria.DataStructures;
using Terraria.ModLoader.IO;

namespace Aurora.Content.Items.Consumables;

public class DoomMirrorItem : ModItem
{
    private sealed class DoomMirrorPlayerImpl : ModPlayer
    {
        /// <summary>
        ///     The position of this player's last death.
        /// </summary>
        public Vector2? DeathPosition { get; private set; }
        
        /// <summary>
        ///     Whether this player is teleporting to its last death position or not.
        /// </summary>
        public bool Teleporting { get; set; }
        
        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource) {
            base.Kill(damage, hitDirection, pvp, damageSource);

            DeathPosition = Player.position;
        }
        
        public override void LoadData(TagCompound tag) {
            base.LoadData(tag);

            tag["deathPosition"] = DeathPosition;
        }

        public override void SaveData(TagCompound tag) {
            base.SaveData(tag);

            DeathPosition = tag.Get<Vector2>("deathPosition");
        }
    }
    
    [Autoload(Side = ModSide.Client)]
    private sealed class DoomMirrorSystemImpl : ModSystem
    {
        public override void Load() {
            base.Load();
            
            On_Main.DrawInfernoRings += DrawInfernoRingsHook;
        }

        private void DrawInfernoRingsHook(On_Main.orig_DrawInfernoRings orig, Main self) {
            orig(self);

            var player = Main.LocalPlayer;
        }
    }

    public override void SetDefaults() {
        base.SetDefaults();

        Item.maxStack = Item.CommonMaxStack;

        Item.consumable = true;

        Item.width = 36;
        Item.height = 34;

        Item.useTime = 25;
        Item.useAnimation = 25;
        Item.useStyle = ItemUseStyleID.HoldUp;
    }

    public override bool? UseItem(Player player) {
        if (!player.TryGetModPlayer(out DoomMirrorPlayerImpl modPlayer)) {
            return false;
        }

        modPlayer.Teleporting = true;
        
        return true;
    }
}

using Terraria.DataStructures;
using Terraria.ModLoader.IO;

namespace Aurora.Content.Items.Consumables;

public class DoomMirror : ModItem
{
    private sealed class DoomMirrorPlayerImpl : ModPlayer
    {
        /// <summary>
        ///     The position of this player's last death.
        /// </summary>
        public Vector2 DeathPosition {
            get => deathPosition;
            set {
                deathPosition = value;

                HasDeathPosition = true;
            }
        }

        private Vector2 deathPosition;
        
        /// <summary>
        ///     Whether this player has a last death position or not.
        /// </summary>
        public bool HasDeathPosition { get; private set; }
        
        public override void LoadData(TagCompound tag) {
            base.LoadData(tag);

            tag["deathPosition"] = DeathPosition;
        }

        public override void SaveData(TagCompound tag) {
            base.SaveData(tag);

            DeathPosition = tag.Get<Vector2>("deathPosition");
        }
        
        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource) {
            base.Kill(damage, hitDirection, pvp, damageSource);

            DeathPosition = Player.position;
        }
    }
    
    // TODO: VFX and functionality.
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
}

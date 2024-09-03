using Terraria.GameContent;
using Terraria.GameContent.Events;

namespace Aurora.Common.NPCs;

/// <summary>
///     Provides rendering of party hats for slimes during a  party.
/// </summary>
[Autoload(Side = ModSide.Client)]
public sealed class NPCSlimeParty : GlobalNPC
{
    public enum HatColor
    {
        Pink = 16,
        Cyan = 17,
        Purple = 18,
        White = 19
    }
    
    /// <summary>
    ///     The color of the NPC's hat.
    /// </summary>
    public HatColor Color { get; set; }
    
    public override bool InstancePerEntity { get; } = true;
    
    public override bool AppliesToEntity(NPC entity, bool lateInstantiation) {
        return entity.type == NPCID.BlueSlime;
    }

    public override void SetDefaults(NPC entity) {
        base.SetDefaults(entity);

        Color = Main.rand.Next(Enum.GetValues<HatColor>());
    }

    public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor) {
        base.PostDraw(npc, spriteBatch, screenPos, drawColor);

        if (!BirthdayParty.PartyIsUp) {
            return;
        }
        
        var texture = TextureAssets.Extra[ExtrasID.TownNPCHats].Value;
        var frame = texture.Frame(20, 1, (byte)Color % 20);
        var origin = frame.Size() / 2f;

        var hatOffset = new Vector2(0f, -14f);
        var npcOffset = new Vector2(0f, npc.gfxOffY);

        var position = npc.Center - screenPos - hatOffset - npcOffset;
        
        var effects = npc.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
        
        spriteBatch.Draw(
            texture, 
            position,
            frame,
            drawColor,
            npc.rotation,
            origin,
            npc.scale,
            effects,
            0f
        );
    }
}

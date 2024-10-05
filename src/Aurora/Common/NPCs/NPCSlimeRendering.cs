using Terraria.GameContent;

namespace Aurora.Common.NPCs;

public sealed class NPCSlimeRendering : GlobalNPC
{
	private const float StretchSpeed = 0.05f;
	private const float MaxStretch = 1.5f;

	private Vector2 scale = Vector2.One;

	public override bool InstancePerEntity { get; } = true;

	public override bool AppliesToEntity(NPC entity, bool lateInstantiation) {
		return entity.type == NPCID.BlueSlime;
	}

	public override void AI(NPC npc) {
		base.AI(npc);

		if (npc.velocity.Y < 0f) {
			scale.Y = MathHelper.Clamp(scale.Y + StretchSpeed, 1f, MaxStretch);
			scale.X = MathHelper.Clamp(scale.X - StretchSpeed, 0.8f, 1f);
		}
		else if (npc.velocity.Y > 0f) {
			scale.Y = MathHelper.Clamp(scale.Y - StretchSpeed * 0.5f, 1f, MaxStretch);
			scale.X = MathHelper.Clamp(scale.X + StretchSpeed * 0.5f, 0.8f, 1f);
		}
		else {
			scale.Y = MathHelper.Lerp(scale.Y, 1f, StretchSpeed);
			scale.X = MathHelper.Lerp(scale.X, 1f, StretchSpeed);
		}

		npc.rotation = npc.rotation.AngleLerp(npc.velocity.X * 0.05f, 0.3f);
	}

	public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor) {
		var texture = TextureAssets.Npc[npc.type].Value;

		var drawOffset = new Vector2(0f, npc.gfxOffY - npc.frame.Height / 2f);
		var drawPosition = npc.Center - screenPos + drawOffset;

		// Don't account for ModNPC offsets since this is only applied to vanilla NPCs.
		var origin = new Vector2(npc.frame.Width / 2f, 0f);

		var effects = npc.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

		Main.EntitySpriteDraw(
			texture,
			drawPosition,
			npc.frame,
			npc.GetShimmerColor(npc.color),
			npc.rotation,
			origin,
			scale,
			effects
		);

		return false;
	}
}

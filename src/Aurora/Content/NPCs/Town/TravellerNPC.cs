namespace Aurora.Content.NPCs.Town;

public class TravellerNPC : ModNPC
{
	public override string Texture { get; } = "Terraria/Images/NPC_" + NPCID.Guide;

	public override void SetDefaults() {
		base.SetDefaults();

		NPC.townNPC = true;
	}
}

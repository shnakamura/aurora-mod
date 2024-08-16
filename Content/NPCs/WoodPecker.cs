using Aurora.Common.NPCs.Components;
using Aurora.Common.NPCs.Effects;

namespace Aurora.Content.NPCs;

public class WoodPecker : ModNPC
{
    public override void SetStaticDefaults() {
        base.SetStaticDefaults();

        Main.npcFrameCount[Type] = 5;
    }

    public override void SetDefaults() {
        base.SetDefaults();
        
        NPC.CloneDefaults(NPCID.BirdRed);

        AIType = NPCID.BirdRed;
        AnimationType = NPCID.BirdRed;

        NPC.TryEnableComponent<NPCDeathEffects>(
            c => {
                c.AddGore($"{nameof(Aurora)}/{Name}0");
                c.AddGore($"{nameof(Aurora)}/{Name}1", 2);
                
                c.AddDust(DustID.Blood, 5);
            }
        );
    }
    
    public override float SpawnChance(NPCSpawnInfo spawnInfo) {
        return spawnInfo.Player.ZoneForest && Main.dayTime ? 0.1f : 0f;
    }
}

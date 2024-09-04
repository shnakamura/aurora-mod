namespace Aurora.Common.Items;

public sealed class PlayerWildWarriorSet : ModPlayer
{
    public const int MaxCooldown = 30 * 60;
    
    public int Cooldown {
        get => _cooldown;
        set => _cooldown = (int)MathHelper.Clamp(value, 0, MaxCooldown);
    }

    private int _cooldown = MaxCooldown;

    public bool CanDodge => Cooldown == 0;

    public override void PostUpdateMiscEffects() {
        base.PostUpdateMiscEffects();

        Cooldown--;
    }
}

using Aurora.Utilities;

namespace Aurora.Common.Ambience;

public static class SignalFlags
{
    [SignalUpdater]
    public static bool Submerged(in SignalContext context) {
        return context.Player.IsDrowning();
    }
}

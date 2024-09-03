using Aurora.Utilities;

namespace Aurora.Common.Ambience._Signals;

public static class SignalFlags
{
    [SignalUpdater]
    public static bool Submerged(in SignalContext context) {
        return context.Player.IsDrowning();
    }
}

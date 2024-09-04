using Aurora.Utilities;

namespace Aurora.Common.Ambience;

public static class SignalFlags
{
    [SignalUpdater]
    public static bool Underwater(in SignalContext context) {
        return context.Player.IsUnderwater();
    }
}

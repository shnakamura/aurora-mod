using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;
using Terraria.ModLoader.Core;

namespace Aurora.Common.Ambience;

/// <summary>
///		Handles the registration and updating of player signals that should be used for parsing
///		player flags as serializable content.
/// </summary>
[Autoload(Side = ModSide.Client)]
[UsedImplicitly(ImplicitUseKindFlags.InstantiatedWithFixedConstructorSignature)]
public sealed class SignalsSystem : ModSystem
{
    public delegate bool SignalUpdaterCallback(in SignalContext context);

    private static Dictionary<string, bool>? flagsByName = new();
    private static Dictionary<string, SignalUpdaterCallback?>? callbacksByName = new();

    public override void Load() {
        base.Load();

        foreach (var type in AssemblyManager.GetLoadableTypes(Mod.Code)) {
            foreach (var method in type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)) {
                var attribute = method.GetCustomAttribute<SignalUpdaterAttribute>();

                if (attribute == null) {
                    continue;
                }

                var callback = method.CreateDelegate<SignalUpdaterCallback>();
                var name = attribute.Name ?? method.Name;

                RegisterUpdater(name, callback);
            }
        }

        RegisterUpdater("Forest", static (in SignalContext context) => context.Player.ZonePurity);
        RegisterUpdater("Day", static (in SignalContext _) => Main.dayTime);
        RegisterUpdater("Night", static (in SignalContext _) => !Main.dayTime);
    }

    public override void Unload() {
        base.Unload();

        flagsByName?.Clear();
        flagsByName = null;

        callbacksByName?.Clear();
        callbacksByName = null;
    }
    
    public override void PostUpdatePlayers() {
        base.PostUpdatePlayers();
        
        foreach (var (name, updater) in callbacksByName) {
            flagsByName[name] = updater?.Invoke(in SignalContext.Default) ?? false;
        }
    }

    /// <summary>
    ///		Checks if a signal is active.
    /// </summary>
    /// <param name="name">The name of the signal to check.</param>
    /// <returns><c>true</c> if the signal was found and is active; otherwise, <c>false</c>.</returns>
    public static bool GetSignal(string name) {
        return flagsByName[name];
    }

    /// <summary>
    ///     Checks if any of the specified signals are active.
    /// </summary>
    /// <param name="names">The names of signals to check.</param>
    /// <returns><c>true</c> if any of the specified signals were found and are active; otherwise, <c>false</c>.</returns>
    public static bool GetSignal(params string[] names) {
        var success = false;

        for (var i = 0; i < names.Length; i++) {
            if (GetSignal(names[i])) {
                success = true;
                break;
            }
        }

        return success;
    }

    /// <summary>
    ///		Registers a new signal updater.
    /// </summary>
    /// <param name="name">The name of the signal to register.</param>
    /// <param name="callback">The callback of the signal to register.</param>
    public static void RegisterUpdater(string name, SignalUpdaterCallback? callback) {
        callbacksByName[name] = callback;
    }
}

using System.Collections.Generic;
using System.Reflection;
using Terraria.ModLoader.Core;

namespace Aurora.Common.Ambience;

[Autoload(Side = ModSide.Client)]
public sealed class SignalsSystem : ModSystem
{
    public delegate bool SignalUpdater(in SignalContext context);

    public static Dictionary<string, bool> Flags = new();
    public static Dictionary<string, SignalUpdater?> Updaters = new();
    
    public override void Load() {
        base.Load();

        foreach (var type in AssemblyManager.GetLoadableTypes(Mod.Code)) {
            foreach (var method in type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)) {
                var attribute = method.GetCustomAttribute<SignalUpdaterAttribute>();

                if (attribute == null) {
                    continue;
                }

                var callback = method.CreateDelegate<SignalUpdater>();
                var name = attribute.Name ?? method.Name;

                RegisterUpdater(name, callback);
            }
        }
        
        RegisterUpdater("Purity", static (in SignalContext context) => context.Player.ZonePurity);
        RegisterUpdater("Forest", static (in SignalContext context) => context.Player.ZonePurity);
        RegisterUpdater("Day", static (in SignalContext _) => Main.dayTime);
        RegisterUpdater("Night", static (in SignalContext _) => !Main.dayTime);
    }

    public override void Unload() {
        base.Unload();
        
        Flags?.Clear();
        Flags = null;
        
        Updaters?.Clear();
        Updaters = null;
    }

    public override void PostUpdatePlayers() {
        base.PostUpdatePlayers();

        var context = new SignalContext {
            Player = Main.LocalPlayer
        };

        foreach (var (name, updater) in Updaters) {
            Flags[name] = updater?.Invoke(in context) ?? false;
        }
    }

    public static bool GetSignal(string name) {
        return Flags[name];
    }
    
    public static bool GetSignal(params string[] names) {
        var success = false;

        for (int i = 0; i < names.Length; i++) {
            if (GetSignal(names[i])) {
                success = true;
                break;
            }
        }

        return success;
    }

    public static void RegisterUpdater(string name, SignalUpdater? updater) {
        Updaters[name] = updater;
    }
}

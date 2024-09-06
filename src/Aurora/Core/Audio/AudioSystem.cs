using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using ReLogic.Utilities;
using Terraria.Audio;

namespace Aurora.Core.Audio;

/// <summary>
///     Handles the registration and updating of <see cref="AudioModifier" /> instances.
/// </summary>
[Autoload(Side = ModSide.Client)]
public sealed class AudioSystem : ModSystem
{
    private static readonly SoundStyle[] IgnoredSoundStyles = {
        SoundID.MenuClose,
        SoundID.MenuOpen,
        SoundID.MenuTick,
        SoundID.Chat,
        SoundID.Grab
    };

    private static List<ActiveSound?>? sounds = new();
    private static List<AudioModifier>? modifiers = new();

    private static AudioParameters parameters;

    public override void Load() {
        base.Load();

        On_SoundPlayer.Play_Inner += PlayInnerHook;
    }

    public override void Unload() {
        base.Unload();

        sounds?.Clear();
        sounds = null;

        modifiers?.Clear();
        modifiers = null;
    }

    public override void PostUpdateEverything() {
        base.PostUpdateEverything();

        UpdateModifiers();
        UpdateSounds();
    }

    /// <summary>
    ///     Adds an audio modifier to the current audio parameters.
    /// </summary>
    /// <remarks>
    ///     If a modifier with the same identifier already exists, this will override its values.
    /// </remarks>
    /// <param name="identifier">The identifier of the modifier.</param>
    /// <param name="duration">The duration of the modifier in ticks.</param>
    /// <param name="callback">The callback to the modifier's execution.</param>
    public static void AddModifier(string identifier, int duration, AudioModifier.ModifierCallback? callback) {
        var index = modifiers.FindIndex(modifier => modifier.Identifier == identifier);

        if (index == -1) {
            modifiers.Add(new AudioModifier(identifier, duration, callback));
            return;
        }

        var modifier = modifiers[index];

        modifier.TimeLeft = Math.Max(modifier.TimeLeft, duration);
        modifier.TimeMax = Math.Max(modifier.TimeMax, duration);
        modifier.Callback = callback;

        modifiers[index] = modifier;
    }

    private static void ApplyParameters(SoundEffectInstance instance, in AudioParameters parameters) {
        if (instance?.IsDisposed == true) {
            return;
        }

        var filters = ModContent.GetContent<IAudioFilter>();

        foreach (var filter in filters) {
            filter.Apply(instance, in parameters);
        }
    }

    private static void UpdateModifiers() {
        var newParameters = new AudioParameters();

        for (var i = 0; i < modifiers.Count; i++) {
            var modifier = modifiers[i];

            if (modifier.TimeLeft-- <= 0) {
                modifiers.RemoveAt(i--);
                continue;
            }

            modifier.Callback?.Invoke(ref newParameters, modifier.TimeLeft / (float)modifier.TimeMax);

            modifiers[i] = modifier;
        }

        parameters = newParameters;
    }

    private static void UpdateSounds() {
        for (var i = 0; i < sounds.Count; i++) {
            var sound = sounds[i];

            if (!sound.IsPlaying) {
                sounds.RemoveAt(i--);
                continue;
            }

            ApplyParameters(sound.Sound, parameters);
        }
    }

    private static SlotId PlayInnerHook(
        On_SoundPlayer.orig_Play_Inner orig,
        SoundPlayer self,
        ref SoundStyle style,
        Vector2? position,
        SoundUpdateCallback updateCallback
    ) {
        var slot = orig(self, ref style, position, updateCallback);

        var isSoundIgnored = false;

        foreach (var ignoredStyle in IgnoredSoundStyles) {
            if (style == ignoredStyle) {
                isSoundIgnored = true;
                break;
            }
        }

        var isSoundActive = SoundEngine.TryGetActiveSound(slot, out var sound);
        var isSoundDisposed = sound?.Sound?.IsDisposed == true;

        if (isSoundActive && isSoundActive && !isSoundDisposed) {
            sounds.Add(sound);
        }

        return slot;
    }
}

using Aurora.Core.IO;
using Aurora.Utilities;
using ReLogic.Content;
using ReLogic.Content.Sources;
using ReLogic.Utilities;

namespace Aurora;

/// <summary>
///     Aurora's <see cref="Mod" /> implementation.
/// </summary>
public sealed class Aurora : Mod
{
    /// <summary>
    ///     Aurora's <see cref="Mod" /> instance.
    /// </summary>
    public static Aurora Instance => ModContent.GetInstance<Aurora>();

    public override IContentSource CreateDefaultContentSource() {
        RegisterAssetReaders();

        return base.CreateDefaultContentSource();
    }

    public override void Unload() {
        base.Unload();

        RemoveAssetReaders();
    }

    private static void RegisterAssetReaders() {
        if (Main.dedServ) {
            return;
        }

        var collection = Main.instance.Services.Get<AssetReaderCollection>();

        collection.TryRegisterReader(new AmbienceTrackReader(), AmbienceTrackReader.Extension);
    }

    private static void RemoveAssetReaders() {
        if (Main.dedServ) {
            return;
        }

        var collection = Main.instance.Services.Get<AssetReaderCollection>();

        collection.TryRemoveReader(AmbienceTrackReader.Extension);
    }
}

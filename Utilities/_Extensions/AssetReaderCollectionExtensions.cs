using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ReLogic.Content;
using ReLogic.Content.Readers;

namespace Aurora.Utilities;

/// <summary>
///     Provides <see cref="AssetReaderCollection" /> extensions.
/// </summary>
public static class AssetReaderCollectionExtensions
{
    private static readonly FieldInfo? ReadersByExtensionField;
    private static readonly FieldInfo? ExtensionsField;

    static AssetReaderCollectionExtensions() {
        const BindingFlags Flags = BindingFlags.Instance | BindingFlags.NonPublic;

        ExtensionsField = typeof(AssetReaderCollection).GetField("_extensions", Flags);

        if (ExtensionsField == null) {
            throw new MissingMemberException(nameof(AssetReaderCollection), "_extensions");
        }

        ReadersByExtensionField = typeof(AssetReaderCollection).GetField("_readersByExtension", Flags);

        if (ReadersByExtensionField == null) {
            throw new MissingMemberException(nameof(AssetReaderCollection), "_readersByExtension");
        }
    }

    /// <summary>
    ///     Attempts to register an asset reader for a specific file extension.
    /// </summary>
    /// <param name="collection">The collection of asset readers to register with.</param>
    /// <param name="reader">The asset reader to register.</param>
    /// <param name="extension">The file extension associated with the reader.</param>
    /// <returns><see langword="true" /> if the reader was successfully registered; otherwise, <see langword="false" />.</returns>
    public static bool TryRegisterReader(this AssetReaderCollection collection, IAssetReader reader, string extension) {
        if (collection.TryGetReader(extension, out var existing) || existing == reader) {
            return false;
        }

        collection.RegisterReader(reader, extension);

#if DEBUG
        Aurora.Instance.Logger.Debug($"Asset reader successfully registered: {reader.GetType().Name} with extension: {extension}");
#endif

        return true;
    }

    /// <summary>
    ///     Attempts to remove an asset reader associated with a specific file extension.
    /// </summary>
    /// <param name="collection">The collection of asset readers to remove from.</param>
    /// <param name="extension">The file extension associated with the reader.</param>
    /// <returns><see langword="true" /> if the reader was successfully removed; otherwise, <see langword="false>.</returns>
    public static bool TryRemoveReader(this AssetReaderCollection collection, string extension) {
        var dictionary = ReadersByExtensionField.GetValue(collection);
        var array = ExtensionsField.GetValue(collection);

        if (dictionary is not Dictionary<string, IAssetReader> readers || array is not string[]) {
            return false;
        }

        readers.Remove(extension);

        ExtensionsField.SetValue(collection, readers.Keys.ToArray());

#if DEBUG
        Aurora.Instance.Logger.Debug($"Asset reader extension successfully removed: {extension}");
#endif

        return true;
    }
}

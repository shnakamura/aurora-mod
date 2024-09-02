using System.Collections.Generic;
using System.Reflection;
using ReLogic.Content;
using ReLogic.Content.Readers;

namespace Aurora.Utilities;

/// <summary>
///     Provides <see cref="AssetReaderCollection"/> extensions.
/// </summary>
public static class AssetReaderCollectionExtensions
{
    private static readonly FieldInfo? ReadersByExtensionField = typeof(AssetReaderCollection)
        .GetField("_readersByExtension", BindingFlags.Instance | BindingFlags.NonPublic);
    
    private static readonly FieldInfo? ExtensionsField = typeof(AssetReaderCollection)
        .GetField("_extensions", BindingFlags.Instance | BindingFlags.NonPublic);
    
    public static bool TryRegisterReader(this AssetReaderCollection collection, IAssetReader reader, params string[] extensions) {
        var allowedExtensions = new string[extensions.Length];
        
        for (int i = 0; i < extensions.Length; i++) {
            if (collection.TryGetReader(extensions[i], out _)) {
                continue;
            }

            allowedExtensions[i] = extensions[i];
        }

        if (allowedExtensions.Length <= 0) {
            return false;
        }

        for (int i = 0; i < allowedExtensions.Length; i++) {
            collection.RegisterReader(reader, allowedExtensions);
        }
        
        return true;
    }

    public static bool TryRemoveReader(this AssetReaderCollection collection, string extension) {
        var dictionary = ReadersByExtensionField.GetValue(collection);
        var array = ExtensionsField.GetValue(collection);

        if (dictionary is not Dictionary<string, IAssetReader> readers || array is not string[]) {
            return false;
        }

        readers.Remove(extension);
        
        ExtensionsField.SetValue(collection, readers.Keys);
        
        return true;
    }
}

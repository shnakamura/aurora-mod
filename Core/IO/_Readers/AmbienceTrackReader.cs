using System.IO;
using System.Text;
using System.Threading.Tasks;
using Aurora.Common.Ambience;
using Aurora.Utilities;
using Hjson;
using Newtonsoft.Json;
using ReLogic.Content;
using ReLogic.Content.Readers;
using ReLogic.Utilities;

namespace Aurora.Core.IO;

public sealed class AmbienceTrackReader : IAssetReader
{
    /// <summary>
    ///     The file extension that this asset reader supports.
    /// </summary>
    public const string Extension = ".ambience";

    public async ValueTask<T> FromStream<T>(Stream stream, MainThreadCreationContext context) where T : class {
        if (typeof(T) != typeof(AmbienceTrack)) {
            throw AssetLoadException.FromInvalidReader<AmbienceTrack, T>();
        }

        await context;
        
        using var reader = new StreamReader(stream, Encoding.UTF8);

        var hjson = reader.ReadToEnd();
        var json = HjsonValue.Parse(hjson).ToString(Stringify.Plain);

        var prefab = JsonConvert.DeserializeObject<T>(json);
        
        return (T)prefab;
    }
}
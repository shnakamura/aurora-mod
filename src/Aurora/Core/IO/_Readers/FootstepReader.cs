using System.IO;
using System.Text;
using System.Threading.Tasks;
using Aurora.Common;
using Aurora.Common.Ambience;
using Hjson;
using Newtonsoft.Json;
using ReLogic.Content;
using ReLogic.Content.Readers;

namespace Aurora.Core.IO;

public sealed class FootstepReader : IAssetReader
{
    /// <summary>
    ///     The file extension that this asset reader supports.
    /// </summary>
    public const string Extension = ".footstep";

    public async ValueTask<T> FromStream<T>(Stream stream, MainThreadCreationContext context) where T : class {
        if (typeof(T) != typeof(Footstep)) {
            throw AssetLoadException.FromInvalidReader<Footstep, T>();
        }

        await context;
        
        using var reader = new StreamReader(stream, Encoding.UTF8);

        var hjson = reader.ReadToEnd();
        var json = HjsonValue.Parse(hjson).ToString(Stringify.Plain);

        var prefab = JsonConvert.DeserializeObject<T>(json);

        return (T)prefab;
    }
}

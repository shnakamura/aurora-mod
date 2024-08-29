using System.IO;
using System.Text;
using System.Threading.Tasks;
using Hjson;
using Newtonsoft.Json;
using ReLogic.Content;
using ReLogic.Content.Readers;

namespace Aurora.Common.IO;

public sealed class ProjectilePrefabReader : IAssetReader
{
    public async ValueTask<T> FromStream<T>(Stream stream, MainThreadCreationContext context) where T : class {
        if (typeof(T) != typeof(ProjectilePrefab)) {
            throw AssetLoadException.FromInvalidReader<ProjectilePrefabReader, T>();
        }

        using var reader = new StreamReader(stream, Encoding.UTF8);

        var hjson = reader.ReadToEnd();
        var json = HjsonValue.Parse(hjson).ToString(Stringify.Plain);

        var prefab = JsonConvert.DeserializeObject<T>(json);

        return (T)prefab;
    }
}

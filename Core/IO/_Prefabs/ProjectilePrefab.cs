using Aurora.Common.Projectiles.Components;
using Newtonsoft.Json;

namespace Aurora.Common.IO;

public readonly struct ProjectilePrefab
{
    [JsonRequired]
    public readonly ProjectileComponent[] Components;
}

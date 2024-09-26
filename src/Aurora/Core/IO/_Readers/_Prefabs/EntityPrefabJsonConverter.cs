using Newtonsoft.Json;

namespace Aurora.Core.IO;

public sealed class EntityPrefabJsonConverter : JsonConverter
{
	public override bool CanConvert(Type objectType) {
		throw new NotImplementedException();
	}

	public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer) {
		throw new NotImplementedException();
	}

	public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer) {
		throw new NotImplementedException();
	}
}

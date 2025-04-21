using System.Text.Json;
using System.Text.Json.Nodes;

namespace Qfx.Cinema.Web.Extensions
{
    public static class JsonExtension
    {
        public static bool IsValidJsonObject(this string response)
        {
            if (!string.IsNullOrWhiteSpace(response))
            {
                return JsonNode.Parse(response) != null;
            }
            return false;
        }
        public static T? DeserializeAnonymousType<T>(this string json, T anonymousTypeObject, JsonSerializerOptions? options = default)
            => JsonSerializer.Deserialize<T>(json, options);

        public static ValueTask<TValue?> DeserializeAnonymousTypeAsync<TValue>(Stream stream, TValue anonymousTypeObject, JsonSerializerOptions? options = default, CancellationToken cancellationToken = default)
            => JsonSerializer.DeserializeAsync<TValue>(stream, options, cancellationToken); // Method to deserialize from a stream added for completeness
    }
}

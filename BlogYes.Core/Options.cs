using System.Text.Json;

namespace BlogYes.Core
{
    public static class Options
    {
        public static JsonSerializerOptions JsonSerializerOptions { get; set; } = new JsonSerializerOptions
        {
            AllowTrailingCommas = true,
            PropertyNameCaseInsensitive = false,
        };

    }
}

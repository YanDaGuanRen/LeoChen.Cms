using System.Text.Json.Serialization;

namespace NewLife.Cube.Models;

public class UEditorFileItem
{
    [JsonPropertyName("url")] public string Url { get; set; }

    [JsonPropertyName("mtime")] public long Mtime { get; set; }
}
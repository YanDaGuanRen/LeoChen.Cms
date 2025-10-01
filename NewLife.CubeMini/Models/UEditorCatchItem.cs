using System.Text.Json.Serialization;

namespace NewLife.Cube.Models;

public class UEditorCatchItem
{
    [JsonPropertyName("state")] public string State { get; set; }

    [JsonPropertyName("url")] public string Url { get; set; }

    [JsonPropertyName("size")] public long Size { get; set; }

    [JsonPropertyName("title")] public string Title { get; set; }

    [JsonPropertyName("original")] public string Original { get; set; }

    [JsonPropertyName("source")] public string Source { get; set; }
}
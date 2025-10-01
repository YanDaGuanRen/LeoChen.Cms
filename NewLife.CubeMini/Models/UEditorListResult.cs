using System.Text.Json.Serialization;

namespace NewLife.Cube.Models;

public class UEditorListResult
{
    [JsonPropertyName("state")] public string State { get; set; }

    [JsonPropertyName("list")] public UEditorFileItem[] List { get; set; }

    [JsonPropertyName("start")] public int Start { get; set; }

    [JsonPropertyName("total")] public int Total { get; set; }
}
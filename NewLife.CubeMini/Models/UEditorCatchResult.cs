using System.Text.Json.Serialization;

namespace NewLife.Cube.Models;

public class UEditorCatchResult
{
    [JsonPropertyName("state")] public string State { get; set; }

    [JsonPropertyName("list")] public UEditorCatchItem[] List { get; set; }
}
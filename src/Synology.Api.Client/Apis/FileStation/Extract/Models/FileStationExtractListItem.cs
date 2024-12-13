using System.Text.Json.Serialization;

namespace Synology.Api.Client.Apis.FileStation.Extract.Models;

public class FileStationExtractListItem
{
    [JsonPropertyName("item_id")]
    public int ItemId { get; set; }

    public string? Name { get; set; }

    public decimal Size { get; set; }

    [JsonPropertyName("pack_size")]
    public decimal PackSize { get; set; }

    public string? Mtime { get; set; }

    public string? Path { get; set; }

    [JsonPropertyName("is_dir")]
    public bool IsDir { get; set; }
}
using System.Text.Json.Serialization;

namespace CMVMD.Shared.Models;

public class FileDto
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    [JsonPropertyName("fileName")]
    public string FileName { get; set; } = default!;
    [JsonPropertyName("fileUrl")]
    public string FileUrl { get; set; } = default!;
}

using System.Text.Json.Serialization;

namespace VTBlog.WebApp.Models
{
    public class UploadResponse
    {
        [JsonPropertyName("path")] public string Path { get; set; }
    }
}

using Newtonsoft.Json;

namespace PhotoFlip
{
    public class SettingsModel
    {
        [JsonProperty("path")]
        public string Path { get; set; }
    }
}
using Newtonsoft.Json;

namespace TranslationTool
{
    [JsonObject]
    public struct Sentence
    {
        // Meta
        [JsonProperty("id")] public int Id;
        [JsonProperty("translated", DefaultValueHandling = DefaultValueHandling.Ignore)] public bool Translated;
        // Data
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)] public string Name;
        [JsonProperty("names", NullValueHandling = NullValueHandling.Ignore)] public string[] Names;
        [JsonProperty("message")] public string Message;
    }
}
using Newtonsoft.Json;

namespace SAB.System
{
    public struct BotTokenDeserialize
    {
        [JsonProperty("Token")]
        public string Token { get; private set; }

        [JsonProperty("Prefix")]
        public string[] Prefix { get; private set; }
    }
}

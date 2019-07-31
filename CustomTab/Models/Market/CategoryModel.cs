using System;
using Newtonsoft.Json;

namespace CustomTab.Models.Market
{
    public class CategoryModel
    {
        [JsonIgnore]
        public bool IsLast { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }
}

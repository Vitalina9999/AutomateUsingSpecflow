using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace UnitTestProjectSpecFlow.Json
{
   public class CRUDResponceJson
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("job")]
        public string Job { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }
    }
}

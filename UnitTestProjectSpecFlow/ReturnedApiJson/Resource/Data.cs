using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace UnitTestProjectSpecFlow.Json.Resource
{
    public class Data
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("year")]
        public string Year { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }
        
        [JsonProperty("pantone_value")]
        public string PantoneValue { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace UnitTestProjectSpecFlow.Json
{
   public class CrudResponseJson
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        //todo: refactor duplicate
        public string FirstName { get; set; }

        [JsonProperty("job")]
        public string Job { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public string UpdatedAt { get; set; }
    }
}
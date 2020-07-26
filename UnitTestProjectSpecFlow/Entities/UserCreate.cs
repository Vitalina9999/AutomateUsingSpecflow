using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestProjectSpecFlow.Entities
{
  public class UserCreate
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("job")]
        public string Job { get; set; }
    }
}

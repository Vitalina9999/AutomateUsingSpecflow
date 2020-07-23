using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using UnitTestProjectSpecFlow.Steps;
using Data = UnitTestProjectSpecFlow.Json.Resource.Data;

namespace UnitTestProjectSpecFlow.Features
{
    public class ResourceJson
    {
        [JsonProperty("data")]
        public Data Data { get; set; }
        
        [JsonProperty("ad")]
        public Ad Ad { get; set; }
    }
}

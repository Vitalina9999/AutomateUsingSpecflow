using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using UnitTestProjectSpecFlow.Steps;

namespace UnitTestProjectSpecFlow
{
    public class JsonResponce
    {
        [JsonProperty("data")]
        public Data Data { get; set; }

        [JsonProperty("ad")]
        public Ad Ad { get; set; }
    }
}

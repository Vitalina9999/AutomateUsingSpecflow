using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using UnitTestProjectSpecFlow.Steps;

namespace UnitTestProjectSpecFlow
{
    public class JsonResponse
    {
        [JsonProperty("data")]
        public Data Data { get; set; }

        [JsonProperty("support")]
        public Support Support { get; set; }
    }
}
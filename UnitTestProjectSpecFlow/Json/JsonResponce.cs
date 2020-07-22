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

        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("per_page")]
        public int PerPage { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("total_pages")]
        public int TotalPages { get; set; }
    }
}

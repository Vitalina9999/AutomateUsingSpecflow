using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using UnitTestProjectSpecFlow.Steps;

namespace UnitTestProjectSpecFlow.Json
{
    public class MultiplyJsonResponce
    {
        public List<UserInfoDataResponce> Data { get; set; }
        public List<UserInfoFullResponce> JsonResponceList { get; set; }

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

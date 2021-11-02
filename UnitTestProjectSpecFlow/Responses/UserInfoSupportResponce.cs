using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestProjectSpecFlow.Steps
{
    public class UserInfoSupportResponce
    {
        [JsonProperty("company")]
        public string Company { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
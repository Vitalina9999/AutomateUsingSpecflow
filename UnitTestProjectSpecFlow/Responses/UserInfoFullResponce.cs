using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using UnitTestProjectSpecFlow.Steps;

namespace UnitTestProjectSpecFlow
{
    public class UserInfoFullResponce
    {
        [JsonProperty("data")]
        public UserInfoDataResponce Data { get; set; }

        [JsonProperty("support")]
        public UserInfoSupportResponce Support { get; set; }
    }
}
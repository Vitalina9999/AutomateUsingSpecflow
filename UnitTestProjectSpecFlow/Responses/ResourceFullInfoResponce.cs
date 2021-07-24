using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using UnitTestProjectSpecFlow.Steps;
using ResourceDataResponce = UnitTestProjectSpecFlow.Json.Resource.ResourceDataResponce;

namespace UnitTestProjectSpecFlow.Features
{
    public class ResourceFullInfoResponce
    {
        [JsonProperty("data")]
        public ResourceDataResponce Data { get; set; }
        
        [JsonProperty("support")]
        public UserInfoSupportResponce Support { get; set; }
    }
}

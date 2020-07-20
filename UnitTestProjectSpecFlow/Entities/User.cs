using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using UnitTestProjectSpecFlow.Steps;

namespace UnitTestProjectSpecFlow.Entities
{
    public class User
    {
        [JsonProperty("data")]
        public Data Data { get; set; }

        [JsonProperty("ad")]
        public Ad Ad { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Avatar { get; set; }
    }
}

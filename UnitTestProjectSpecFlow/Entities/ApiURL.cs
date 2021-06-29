using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestProjectSpecFlow.Entities
{
    public class ApiUrl
    {
        public const string host = "https://reqres.in";
        public const string loginUrl = ApiUrl.host+"/api/login";
        public const string registerUrl = ApiUrl.host + "/api/register";
        public const string usersUrl = ApiUrl.host + "/api/users";
        public const string resourceUrl = ApiUrl.host + "/api/unknown";
    }
}
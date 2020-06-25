using System.Net;
using Google.Protobuf.WellKnownTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using TechTalk.SpecFlow;

namespace UnitTestProjectSpecFlow.Steps
{
    [Binding]
    public class LoginUserSteps
    {
        public RestClient restClient = new RestClient();
        public string email;
        public string password;
        private IRestResponse response = null;

        [Given(@"A correct email")]
        public void GivenACorrectEmail()
        {
            email = "eve.holt@reqres.in";
        }

        [Given(@"An incorrect email")]
        public void GivenAnInCorrectEmail()
        {
            email = "ewewewewewe";
        }

        [Given(@"an incorrect password")]
        public void GivenAnInCorrectPassword()
        {
            password = "dfgdfgdfgdfgfd";
        }

        [Given(@"a correct password")]
        public void GivenAnCorrectPassword()
        {
            password = "cityslicka";
        }

        [When(@"I send request")]
        public void WhenISendRequest()
        {
            string baseURL = "https://reqres.in";
            string apiURL = baseURL + "/" + "api/login";

            RestRequest restRequest = new RestRequest(apiURL, RestSharp.Method.POST);

            restRequest.AddHeader("Accept", "application/json");
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.AddParameter("email", email);
            restRequest.AddParameter("password", password);

            response = restClient.Execute(restRequest);
            //string content = response.Content;

            //ResponceLoginUser deserialize = JsonConvert.DeserializeObject<ResponceLoginUser>(content);
            //return deserialize.token;

        }

        [Then(@"User has response (.*) and token")]
        public void ThenUserHasResponse200AndToken()
        {
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(response.Content);
        }

        
        [Then(@"data is valid")]
        public void ThenDataIsValid()
        {
            string content = response.Content;

            ResponceLoginUser deserialize = JsonConvert.DeserializeObject<ResponceLoginUser>(content);
            Assert.IsNotNull(deserialize);
            Assert.AreEqual("dffdfdf", deserialize.token);
        }


        [Then(@"User has response (.*) and token")]
        public void ThenUserHasResponse400AndToken()
        {
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }
    }
}

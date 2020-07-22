using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using TechTalk.SpecFlow;
using UnitTestProjectSpecFlow.Entities;

namespace UnitTestProjectSpecFlow.Steps
{
    [Binding]
    public class LoginUserSteps
    {
        public RestClient _restClient = new RestClient();
        public User _user = new User();
       
        private IRestResponse response;

        //Context-Injection Sharing-Data-between-Bindings
  // private readonly SingleUserSteps _singleUserSteps;

        [Given(@"a correct email")]
        public void GivenACorrectEmail()
        {
            _user.Email = "eve.holt@reqres.in";
        }

        [Given(@"an incorrect email")]
        public void GivenAnInCorrectEmail()
        {
            _user.Email = "ewewewewewe";
        }

        [Given(@"an incorrect password")]
        public void GivenAnInCorrectPassword()
        {
            _user.Password = "dfgdfgdfgdfgfd";
        }

        [Given(@"a correct password")]
        public void GivenAnCorrectPassword()
        {
            _user.Password = "cityslicka";
        }

        [Given(@"missing password")]
        public void GivenMissingPassword()
        {
            _user.Password = null;
        }

        [When(@"I send request")]
        public void WhenISendRequest()
        {
            string baseURL = "https://reqres.in";
            string apiURL = baseURL + "/" + "api/login";

            RestRequest restRequest = new RestRequest(apiURL, RestSharp.Method.POST);

            restRequest.AddHeader("Accept", "application/json");
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.AddParameter("email", _user.Email);
            restRequest.AddParameter("password", _user.Password);

            response = _restClient.Execute(restRequest);
        }

        [Then(@"the _user should be returned in the responce")]
        public void ThenTheUserShouldBeReturnedInTheResponce(User user)
        {
            // Assert.IsNotNull<OkObjectResult>(_user.);
        }

        [Then(@"the response status code is (.*)")]
        public void ThenTheResponseStatusCodeIs(int statusCode)
        {
            if (statusCode == 200) //"string statusCode 200 OK"
            {
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            }

            else if (statusCode == 400) //" string statusCode 404 Not Found"
            {
                Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }

        [Then(@"the token is NotNull")]
        public void ThenTheTokenIsNotNull()
        {
            Assert.IsNotNull(response.Content.Contains("token"));
        }

        [Then(@"data is valid")]
        public void ThenDataIsValid()
        {
            string content = response.Content;
            ResponceLoginUser deserialize = JsonConvert.DeserializeObject<ResponceLoginUser>(content);
            Assert.IsNotNull(deserialize);
        }

        [When(@"I send request without password")]
        public void WhenISendRequestWithoutPassword()
        {
            string baseURL = "https://reqres.in";
            string apiURL = baseURL + "/" + "api/login";

            RestRequest restRequest = new RestRequest(apiURL, RestSharp.Method.POST);

            restRequest.AddHeader("Accept", "application/json");
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.AddParameter("email", _user.Email);

            response = _restClient.Execute(restRequest);
        }



    }
}

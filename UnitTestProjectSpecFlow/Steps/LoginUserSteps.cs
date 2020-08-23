using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using TechTalk.SpecFlow;
using UnitTestProjectSpecFlow.Entities;
using TechTalk.SpecFlow.Assist;

namespace UnitTestProjectSpecFlow.Steps
{
    [Binding]
    public class LoginUserSteps
    {
        public RestClient _restClient = new RestClient();
        public User _user = new User();

        private IRestResponse response;


        //Context-Injection Sharing-Data-between-Bindings
        private readonly ApiURL _apiUrl;
        private string _loginUrl;
        public LoginUserSteps(ApiURL apiUrl)
        {
            _apiUrl = apiUrl;
            _loginUrl = apiUrl.loginUrl;
        }


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
            RestRequest restRequest = new RestRequest(_loginUrl, RestSharp.Method.POST);

            restRequest.AddHeader("Accept", "application/json");
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.AddParameter("email", _user.Email);
            restRequest.AddParameter("password", _user.Password);

            response = _restClient.Execute(restRequest);
        }

        [Then(@"the _user should be returned in the response")]
        public void ThenTheUserShouldBeReturnedInTheResponse(User user)
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
            LoginResponceJson deserialize = JsonConvert.DeserializeObject<LoginResponceJson>(content);
            Assert.IsNotNull(deserialize.Token);
        }

        [When(@"I send request without password")]
        public void WhenISendRequestWithoutPassword()
        {
            //apiUrl.loginUrl = loginUrl;
            RestRequest restRequest = new RestRequest(_loginUrl, RestSharp.Method.POST);

            restRequest.AddHeader("Accept", "application/json");
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.AddParameter("email", _user.Email);

            response = _restClient.Execute(restRequest);
        }
    }
}
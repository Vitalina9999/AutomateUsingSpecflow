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
        private RestClient _restClient = new RestClient();
        private UserInfo _user = new UserInfo();
        private IRestResponse _response;

        [Given(@"Correct credentials")]
        public void GivenCorrectCredentials(Table table)
        {
            var account = table.CreateInstance<UserInfo>();
            _user.Email = account.Email;
            _user.Password = account.Password;
        }

        [Given(@"I entered the incorrect data into the login form")]
        public void GivenIEnteredTheIncorrectDataIntoTheLoginForm(Table table)
        {
            var account = table.CreateInstance<UserInfo>();
            _user.Email = account.Email;
            _user.Password = account.Password;
        }

        [Given(@"I entered only email into the login form")]
        public void GivenIEnteredOnlyEmailIntoTheLoginForm(Table table)
        {
            var account = table.CreateInstance<UserInfo>();
            _user.Email = account.Email;
        }

        [Then(@"Login request is successful")]
        public void ThenLoginRequestIsSuccessful()
        {
            RestRequest restRequest = new RestRequest(ApiUrl.loginUrl, Method.POST);

            restRequest.AddHeader("Accept", "application/json");
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.AddParameter("email", _user.Email);
            restRequest.AddParameter("password", _user.Password);

            _response = _restClient.Execute(restRequest);

            Assert.AreEqual(HttpStatusCode.OK, _response.StatusCode);
        }

        [Then(@"Login request is unsuccessful")]
        public void ThenLoginRequestIsUnsuccessful()
        {
            RestRequest restRequest = new RestRequest(ApiUrl.loginUrl, Method.POST);

            restRequest.AddHeader("Accept", "application/json");
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.AddParameter("email", _user.Email);
            restRequest.AddParameter("password", _user.Password);

            _response = _restClient.Execute(restRequest);

            Assert.AreEqual(HttpStatusCode.BadRequest, _response.StatusCode);
        }
        
        [Then(@"the token is NotNull")]
        public void ThenTheTokenIsNotNull()
        {
            Assert.IsNotNull(_response.Content.Contains("token"));
        }

        [Then(@"data is valid")]
        public void ThenDataIsValid()
        {
            string content = _response.Content;
            LoginResponce deserialize = JsonConvert.DeserializeObject<LoginResponce>(content);
            Assert.IsNotNull(deserialize.Token);
        }
    }
}
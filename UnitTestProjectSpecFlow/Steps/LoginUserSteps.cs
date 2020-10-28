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
        private User _user = new User();
        public IRestResponse _response;
        
        [Given(@"I entered the following data into the login form:")]
        public void GivenIEnteredTheFollowingDataIntoTheLoginForm(Table table)
        {
            var account = table.CreateInstance<User>();
            _user.Email = account.Email;
            _user.Password = account.Password;
        }

        [Given(@"I entered the incorrect data into the login form")]
        public void GivenIEnteredTheIncorrectDataIntoTheLoginForm(Table table)
        {
            var account = table.CreateInstance<User>();
            _user.Email = account.Email;
            _user.Password = account.Password;
        }
        [Given(@"I entered only email into the login form")]
        public void GivenIEnteredOnlyEmailIntoTheLoginForm(Table table)
        {
            var account = table.CreateInstance<User>();
            _user.Email = account.Email;
        }
    
        [When(@"I send request")]
        public void WhenISendRequest()
        {
            RestRequest restRequest = new RestRequest(ApiURL.loginUrl, RestSharp.Method.POST);

            restRequest.AddHeader("Accept", "application/json");
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.AddParameter("email", _user.Email);
            restRequest.AddParameter("password", _user.Password);
            _response = _restClient.Execute(restRequest);
        }

       [Then(@"the response is BadRequest")]
        public void ThenTheResponseIsBadRequest()
        {
            Assert.AreEqual(HttpStatusCode.BadRequest, _response.StatusCode);
        }

      [Then(@"the token is NotNull")]
        public void ThenTheTokenIsNotNull()
        {
            Assert.IsNotNull(_response.Content.Contains("token"));
        }
        
        [Then(@"the result status code should be Ok")]
        public void ThenTheResultStatusCodeShouldBeOk()
        {
            Assert.AreEqual(HttpStatusCode.OK, _response.StatusCode);
        }
        
        [Then(@"data is valid")]
        public void ThenDataIsValid()
        {
            string content = _response.Content;
            LoginResponceJson deserialize = JsonConvert.DeserializeObject<LoginResponceJson>(content);
            Assert.IsNotNull(deserialize.Token);
        }

        [When(@"I send request without password")]
        public void WhenISendRequestWithoutPassword()
        {
           RestRequest restRequest = new RestRequest(ApiURL.loginUrl, RestSharp.Method.POST);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.AddParameter("email", _user.Email);
            _response = _restClient.Execute(restRequest);
        }
    }
}
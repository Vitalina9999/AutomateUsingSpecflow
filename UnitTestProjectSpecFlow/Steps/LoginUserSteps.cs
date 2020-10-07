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
        private IRestResponse response;
        
        //Context-Injection Sharing-Data-between-Bindings
        private readonly string _loginUrl;
        public LoginUserSteps(ApiURL apiUrl)
        {
            _loginUrl = apiUrl.loginUrl;
        }
        
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
            RestRequest restRequest = new RestRequest(_loginUrl, RestSharp.Method.POST);

            restRequest.AddHeader("Accept", "application/json");
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.AddParameter("email", _user.Email);
            restRequest.AddParameter("password", _user.Password);

            response = _restClient.Execute(restRequest);
        }

       [Then(@"the response is BadRequest")]
        public void ThenTheResponseIsBadRequest()
        {
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        //[Then(@"the response status code is (.*)")]
        //public void ThenTheResponseStatusCodeIs(int statusCode)
        //{
        //    if (statusCode == 200) //"string statusCode 200 OK"
        //    {
        //        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        //    }

        //    else if (statusCode == 400) //" string statusCode 404 Not Found"
        //    {
        //        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        //    }
        //}

        [Then(@"the token is NotNull")]
        public void ThenTheTokenIsNotNull()
        {
            Assert.IsNotNull(response.Content.Contains("token"));
        }
        
        [Then(@"the result status code should be Ok")]
        public void ThenTheResultStatusCodeShouldBeOk()
        {
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
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
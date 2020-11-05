using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Google.Protobuf.WellKnownTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using UnitTestProjectSpecFlow.Entities;
using UnitTestProjectSpecFlow.Json;

namespace UnitTestProjectSpecFlow.Steps
{
    [Binding]
    public sealed class RegisterUserSteps
    {
        private RestClient _restClient = new RestClient();
        private User _user = new User();
        private IRestResponse _response;

        [Given(@"I have entered credentials")]
        public void GivenIHaveEnteredCredentials(Table table)
        {
            var account = table.CreateInstance<User>();
            _user.Email = account.Email;
            _user.Password = account.Password;
        }

        [Given(@"I have entered only email")]
        public void GivenIHaveEnteredOnlyEmail(Table table)
        {
            var account = table.CreateInstance<User>();
            _user.Email = account.Email;
        }

        [Then(@"the response should provide id and token")]
        public void ThenTheResponseShouldProvideIdAndToken()
        {
            RestRequest restRequest = new RestRequest(ApiURL.registerUrl, RestSharp.Method.POST);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.AddParameter("email", _user.Email);
            restRequest.AddParameter("password", _user.Password);
            _response = _restClient.Execute(restRequest);

            RegisterResponseJson deserialize = JsonConvert.DeserializeObject<RegisterResponseJson>(_response.Content);
            Assert.IsNotNull(deserialize.Id);
            Assert.IsNotNull(deserialize.Token);
            Assert.AreEqual(HttpStatusCode.OK, _response.StatusCode);
        }

        [Then(@"the response has an error")]
        public void ThenTheResponseHasAnError()
        {
            RestRequest restRequest = new RestRequest(ApiURL.registerUrl, RestSharp.Method.POST);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.AddParameter("email", _user.Email);
            restRequest.AddParameter("password", _user.Password);
            _response = _restClient.Execute(restRequest);

            RegisterResponseJson deserialize = JsonConvert.DeserializeObject<RegisterResponseJson>(_response.Content);
            Assert.AreEqual(deserialize.Error, "Missing password");
            Assert.AreEqual(HttpStatusCode.BadRequest, _response.StatusCode);
        }
    }
}

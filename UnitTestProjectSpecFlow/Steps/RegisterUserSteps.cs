using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using TechTalk.SpecFlow;
using UnitTestProjectSpecFlow.Entities;
using UnitTestProjectSpecFlow.Json;

namespace UnitTestProjectSpecFlow.Steps
{
    [Binding]
    public sealed class RegisterUserSteps
    {
        public RestClient _restClient = new RestClient();
        public User _user = new User();

        private IRestResponse _response;

        [Given(@"I have entered credentials")]
        public void GivenIHaveEnteredCredentials()
        {
            _user.Email = "eve.holt@reqres.in";
            _user.Password = "pistol";
        }

        [When(@"I sent request")]
        public void WhenISentRequest()
        {
            string baseURL = "https://reqres.in";
            string apiURL = baseURL + "/" + "api/register";

            RestRequest restRequest = new RestRequest(apiURL, RestSharp.Method.POST);

            restRequest.AddHeader("Accept", "application/json");
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.AddParameter("email", _user.Email);
            restRequest.AddParameter("password", _user.Password);
            _response = _restClient.Execute(restRequest);
        }

        [Then(@"the response should provide id and token")]
        public void ThenTheResponseShouldProvideIdAndToken()
        {
            RegisterResponceJson deserialize = JsonConvert.DeserializeObject<RegisterResponceJson>(_response.Content);

            Assert.IsNotNull(deserialize.Id);
            Assert.IsNotNull(deserialize.Token);
        }
        [Then(@"the register response status code should be OK")]
        public void ThenTheRegisterResponceStatusCodeShouldBeOK()
        {
            Assert.AreEqual(HttpStatusCode.OK, _response.StatusCode);
        }
        [Given(@"I have entered only email")]
        public void GivenIHaveEnteredOnlyEmail()
        {
            _user.Email = "eve.holt@reqres.in";
        }

        [Then(@"the response has an error")]
        public void ThenTheResponseHasAnError()
        {
            RegisterResponceJson deserialize = JsonConvert.DeserializeObject<RegisterResponceJson>(_response.Content);
            Assert.AreEqual(deserialize.Error, "Missing password");
        }

        [Then(@"the register response status code should be BadRequest")]
        public void ThenTheRegisterResponceStatusCodeShouldBeBadRequest()
        {
            Assert.AreEqual(HttpStatusCode.BadRequest, _response.StatusCode);

        }
    }
}

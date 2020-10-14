using System;
using System.Net;
using System.Threading;
using TechTalk.SpecFlow;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using UnitTestProjectSpecFlow.Entities;
using Flurl;

namespace UnitTestProjectSpecFlow.Steps
{
    [Binding]
    public class DelayRequestSteps
    {
        public RestClient _restClient = new RestClient();
        private readonly Url _usersUrl;
        private IRestResponse _response;
        public DelayRequestSteps(ApiURL apiUrl)
        {
            _usersUrl = apiUrl.usersUrl;
        }

        [When(@"send response with delay (.*) secs")]
        public void WhenSendResponseWithDelaySecs(int delaySecs)
        {
            Url usersPageUrl = _usersUrl.SetQueryParam("delay", delaySecs);

            CancellationTokenSource source = new CancellationTokenSource();
            Task.Delay(3000, source.Token);

            RestRequest restRequest = new RestRequest(usersPageUrl);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.RequestFormat = DataFormat.Json;
            _response = _restClient.Execute(restRequest);
        }

        [Then(@"the result status code is Ok")]
        public void ThenTheResultStatusCodeIsOk()
        {
            Assert.AreEqual(HttpStatusCode.OK, _response.StatusCode);
        }
    }
}
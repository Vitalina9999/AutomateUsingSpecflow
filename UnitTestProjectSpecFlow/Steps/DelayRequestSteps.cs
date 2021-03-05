using System;
using System.Diagnostics;
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
        private RestClient _restClient = new RestClient();
        private IRestResponse _response;
        private int _delaySecs;

        [When(@"Get user info request is sent with delay (.*) sec")]
        public void WhenGetUserInfoRequestIsSent(int delaySecs)
        {
            Url usersPageUrl = ApiUrl.usersUrl.SetQueryParam("delay", delaySecs);

            RestRequest restRequest = new RestRequest(usersPageUrl);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.RequestFormat = DataFormat.Json;

            Stopwatch sw = new Stopwatch();
            sw.Start();
            _response = _restClient.Execute(restRequest);
            sw.Stop();

            Assert.AreEqual(HttpStatusCode.OK, _response.StatusCode);

            _delaySecs = sw.Elapsed.Seconds;
        }

        [Then(@"Response is delayed for a (.*) secs")]
        public void ThenResponseIsDelayed(int delaySecs)
        {
            Assert.AreEqual(delaySecs, _delaySecs);
        }
    }
}
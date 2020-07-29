using System;
using System.Net;
using System.Threading;
using TechTalk.SpecFlow;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using UnitTestProjectSpecFlow.Entities;

namespace UnitTestProjectSpecFlow.Steps
{
    [Binding]
    public class DelayRequestSteps
    {
        public RestClient _restClient = new RestClient();
        
        private IRestResponse response;

        [When(@"send response with delay")]
        public void WhenSendResponseWithDelay()
        {
            CancellationTokenSource source = new CancellationTokenSource();

            string baseURL = "https://reqres.in";
            string apiURL = baseURL + "/" + "api/users?delay=3";
            Task.Delay(3000, source.Token);
            RestRequest restRequest = new RestRequest(apiURL);

            restRequest.AddHeader("Accept", "application/json");
            restRequest.RequestFormat = DataFormat.Json;

            response = _restClient.Execute(restRequest);
        }

        [Then(@"the result status code is Ok")]
        public void ThenTheResultStatusCodeIsOk()
        {
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}

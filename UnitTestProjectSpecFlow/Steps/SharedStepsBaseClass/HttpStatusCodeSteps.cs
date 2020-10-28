using System;
using System.Net;
using System.Threading;
using TechTalk.SpecFlow;
using System.Threading.Tasks;
using RestSharp;
using UnitTestProjectSpecFlow.Entities;
using Flurl;
using NUnit.Framework;
using UnitTestProjectSpecFlow.Steps;

namespace UnitTestProjectSpecFlow.Features
{
    [Binding]
    public class HttpStatusCodeSteps
    {
        //Context-Injection Sharing-Data-between-Bindings
        private readonly IRestResponse _response;

        public HttpStatusCodeSteps(DelayRequestSteps delay)
        {
            _response = delay._response;
        }
        public HttpStatusCodeSteps(LoginUserSteps response)
        {
            _response = response._response;
        }

        [Then(@"status code is Ok")]
        public void ThenStatusCodeIsOk()
        {
            Assert.AreEqual(HttpStatusCode.OK, _response.StatusCode);
        }
    }
}
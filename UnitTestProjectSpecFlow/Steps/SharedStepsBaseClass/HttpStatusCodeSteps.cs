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

        public HttpStatusCodeSteps(DelayRequestSteps response)
        {
            _response = response._response;
        }
        public HttpStatusCodeSteps(LoginUserSteps response)
        {
            _response = response._response;
        }
        public HttpStatusCodeSteps(UserCRUDSteps response)
        {
            _response = response._response;
        }

        public HttpStatusCodeSteps(SingleUserSteps response)
        {
            _response = response._response;
        }

        [Then(@"status code is Ok")]
        public void ThenStatusCodeIsOk()
        {
            Assert.AreEqual(HttpStatusCode.OK, _response.StatusCode);
        }
        
        [Then(@"status code is BadRequest")]
        public void ThenStatusCodeIsBadRequest()
        {
            Assert.AreEqual(HttpStatusCode.BadRequest, _response.StatusCode);
        }

        [Then(@"status code is Created")]
        public void TheStatusCodeIsCreated()
        {
            Assert.AreEqual(HttpStatusCode.Created, _response.StatusCode);
        }

        [Then(@"status code is No Content")]
        public void ThenStatusCodeIsNoContent()
        {
            Assert.AreEqual(HttpStatusCode.NoContent, _response.StatusCode);
        }

        [Then(@"status code is Not Found")]
        public void ThenStatusCodeIsNotFound() //404
        {
            Assert.AreEqual(HttpStatusCode.NotFound, _response.StatusCode);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using TechTalk.SpecFlow;
using UnitTestProjectSpecFlow.Features;
using UnitTestProjectSpecFlow.Json;

namespace UnitTestProjectSpecFlow.Steps
{
    [Binding]
    public class ResourceSteps
    {
        ResourceJson _resource = new ResourceJson();
        public RestClient _restClient = new RestClient();
        private IRestResponse _response;
        private string _baseURL = "https://reqres.in";

        [Given(@"resource Id")]
        public void GivenResourceId()
        {
            int resourceId = 2;
        }

        [When(@"I sent resourse request")]
        public void WhenISentResourceRequest()
        {
            string apiURL = _baseURL + "/" + "api/unknown" + "/" + 2;

            RestRequest restRequest = new RestRequest(apiURL);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.RequestFormat = DataFormat.Json;

            _response = _restClient.Execute(restRequest);
        }

        [When(@"I sent resource request with url")]
        public void WhenISentResourceRequestWithUrl()
        {
            string apiURL = _baseURL + "/" + "api/unknown";

            RestRequest restRequest = new RestRequest(apiURL);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.RequestFormat = DataFormat.Json;

            _response = _restClient.Execute(restRequest);
        }

        [When(@"I sent resource request with unknown url")]
        public void WhenISentResourceRequestWithUnknownUrl()
        {
           string apiURL = _baseURL + "/" + "api/unknown" + "/" + 255555;

            RestRequest restRequest = new RestRequest(apiURL);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.RequestFormat = DataFormat.Json;

            _response = _restClient.Execute(restRequest);
        }
        
        [Then(@"the response should provide list of data resource")]
        public void ThenTheResponseShouldProvideListOfDataResource()
        {
            MultiplyJsonResponce deserialize = JsonConvert.DeserializeObject<MultiplyJsonResponce>(_response.Content);
            Assert.IsNotNull(deserialize.Page);
            Assert.IsNotNull(deserialize.PerPage);
            Assert.IsNotNull(deserialize.Total);
            Assert.IsNotNull(deserialize.TotalPages);
        }

        [Then(@"the response should provide data resource")]
        public void ThenTheResponseShouldProvideDataResource()
        {
            ResourceJson deserialize = JsonConvert.DeserializeObject<ResourceJson>(_response.Content);
            Assert.IsNotNull(deserialize.Data);
            Assert.IsNotNull(deserialize.Data.Id);

            Assert.IsNotNull(deserialize.Ad);
        }

        [Then(@"the resource response status code should be OK")]
        public void ThenTheResourseResponceStatusCodeShouldBeOK()
        {
            Assert.AreEqual(HttpStatusCode.OK, _response.StatusCode);
        }

        [Then(@"the resource response status code should be Not found")]
        public void ThenTheResourceResponseStatusCodeShouldBeNotFound()
        {
            Assert.AreEqual(HttpStatusCode.NotFound, _response.StatusCode);
        }






    }
}

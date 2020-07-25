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
    public class Resource
    {
        ResourceJson _resource = new ResourceJson();
        public RestClient _restClient = new RestClient();
        private IRestResponse _response;


        [Given(@"resource Id")]
        public void GivenResourceId()
        {
            int resourceId = 2;
        }

        [When(@"I sent resource request")]
        public void WhenISentResourceRequest()
        {
            string baseURL = "https://reqres.in";
            string apiURL = baseURL + "/" + "api/unknown" + "/" + 2;

            RestRequest restRequest = new RestRequest(apiURL);

            restRequest.AddHeader("Accept", "application/json");
            restRequest.RequestFormat = DataFormat.Json;

            _response = _restClient.Execute(restRequest);
        }

        [When(@"I sent resource request with url")]
        public void WhenISentResourceRequestWithUrl()
        {
            string baseURL = "https://reqres.in";
            string apiURL = baseURL + "/" + "api/unknown";

            RestRequest restRequest = new RestRequest(apiURL);

            restRequest.AddHeader("Accept", "application/json");
            restRequest.RequestFormat = DataFormat.Json;

            _response = _restClient.Execute(restRequest);
        }

        [When(@"I sent resource request with unknown url")]
        public void WhenISentResourceRequestWithUnknownUrl()
        {
            string baseURL = "https://reqres.in";
            string apiURL = baseURL + "/" + "api/unknown" + "/" + 255555;

            RestRequest restRequest = new RestRequest(apiURL);

            restRequest.AddHeader("Accept", "application/json");
            restRequest.RequestFormat = DataFormat.Json;

            _response = _restClient.Execute(restRequest);
        }
        
        [Then(@"the responce should provide list of data resource")]
        public void ThenTheResponceShouldProvideListOfDataResource()
        {
            MultiplyJsonResponce deserialize = JsonConvert.DeserializeObject<MultiplyJsonResponce>(_response.Content);
            Assert.IsNotNull(deserialize.Page);
            Assert.IsNotNull(deserialize.PerPage);
            Assert.IsNotNull(deserialize.Total);
            Assert.IsNotNull(deserialize.TotalPages);
        }

        [Then(@"the responce should provide data resource")]
        public void ThenTheResponceShouldProvideDataResource()
        {
            ResourceJson deserialize = JsonConvert.DeserializeObject<ResourceJson>(_response.Content);
            Assert.IsNotNull(deserialize.Data);
            Assert.IsNotNull(deserialize.Data.Id);

            Assert.IsNotNull(deserialize.Ad);
        }

        [Then(@"the resource responce status code should be OK")]
        public void ThenTheResourceResponceStatusCodeShouldBeOK()
        {
            Assert.AreEqual(HttpStatusCode.OK, _response.StatusCode);
        }

        [Then(@"the resource responce status code should be Not found")]
        public void ThenTheResourceResponceStatusCodeShouldBeNotFound()
        {
            Assert.AreEqual(HttpStatusCode.NotFound, _response.StatusCode);
        }

    }
}

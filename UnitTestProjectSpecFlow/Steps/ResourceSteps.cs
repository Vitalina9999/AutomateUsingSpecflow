using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Flurl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using UnitTestProjectSpecFlow.Entities;
using UnitTestProjectSpecFlow.Features;
using UnitTestProjectSpecFlow.Json;

namespace UnitTestProjectSpecFlow.Steps
{
    [Binding]
    public class ResourceSteps
    {
        public RestClient _restClient = new RestClient();
        public IRestResponse _response;
        Resource _resource = new Resource();

        [Given(@"resource number")]
        public void GivenResourceNumber(Table table)
        {
            Resource account = table.CreateInstance<Resource>();
            _resource.Number = account.Number;
        }

        [When(@"I sent resource request")]
        public void WhenISentResourceRequest()
        {
            Url userIdUrl = Url.Combine(ApiURL.resourceUrl, "/", _resource.Number.ToString());

            RestRequest restRequest = new RestRequest(userIdUrl);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.RequestFormat = DataFormat.Json;
            _response = _restClient.Execute(restRequest);
        }

        [When(@"GetResources request is sent")]
        public void WhenGetResourcesIsSent()
        {
            RestRequest restRequest = new RestRequest(ApiURL.resourceUrl);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.RequestFormat = DataFormat.Json;
            _response = _restClient.Execute(restRequest);
        }

        [When(@"I sent resource request with unknown url")]
        public void WhenISentResourceRequestWithUnknownUrl()
        {
            Url userIdUrl = Url.Combine(ApiURL.resourceUrl, "/", _resource.Number.ToString());

            RestRequest restRequest = new RestRequest(userIdUrl);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.RequestFormat = DataFormat.Json;
            _response = _restClient.Execute(restRequest);
        }

        [Then(@"Resources is received")]
        public void ThenResourcesIsReceived()
        {
            Assert.AreEqual(HttpStatusCode.OK, _response.StatusCode);

            MultiplyJsonResponce deserialize = JsonConvert.DeserializeObject<MultiplyJsonResponce>(_response.Content);
            Assert.IsNotNull(deserialize.Page);
            Assert.IsNotNull(deserialize.PerPage);
            Assert.IsNotNull(deserialize.Total);
            Assert.IsNotNull(deserialize.TotalPages);

            Assert.IsNotNull(deserialize.Data);
            Assert.IsTrue(deserialize.Data.Any());

            //Data resource = deserialize.Data.FirstOrDefault();


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
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
        private IRestResponse _response;
        Resource _resource = new Resource();

        private readonly Url _resourceUrl;

        public ResourceSteps(ApiURL apiUrl)
        {
            _resourceUrl = apiUrl.resourceUrl;
        }

        [Given(@"resource number")]
        public void GivenResourceNumber(Table table)
        {
            Resource account = table.CreateInstance<Resource>();
            _resource.Number = account.Number;
        }

        [When(@"I sent resource request")]
        public void WhenISentResourceRequest()
        {
            Url userIdUrl = Url.Combine(_resourceUrl, "/", _resource.Number.ToString());

            RestRequest restRequest = new RestRequest(userIdUrl);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.RequestFormat = DataFormat.Json;
            _response = _restClient.Execute(restRequest);
        }

        [When(@"I sent resource request with url")]
        public void WhenISentResourceRequestWithUrl()
        {
            RestRequest restRequest = new RestRequest(_resourceUrl);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.RequestFormat = DataFormat.Json;
            _response = _restClient.Execute(restRequest);
        }

        [When(@"I sent resource request with unknown url")]
        public void WhenISentResourceRequestWithUnknownUrl()
        {
            Url userIdUrl = Url.Combine(_resourceUrl, "/", _resource.Number.ToString());

            RestRequest restRequest = new RestRequest(userIdUrl);
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
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
        private RestClient _restClient = new RestClient();
        private IRestResponse _response;
        private Resource _resource = new Resource();

        [Given(@"resource number")]
        public void GivenResourceNumber(Table table)
        {
            Resource account = table.CreateInstance<Resource>();
            _resource.Number = account.Number;
        }

        [Then(@"Resource is Not Found")]
        public void ThenResourceIsNotFound()
        {
            Url userIdUrl = Url.Combine(ApiUrl.resourceUrl, "/", _resource.Number.ToString());

            RestRequest restRequest = new RestRequest(userIdUrl);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.RequestFormat = DataFormat.Json;
            _response = _restClient.Execute(restRequest);
            Assert.AreEqual(HttpStatusCode.NotFound, _response.StatusCode);
        }

        [Then(@"Resources is received")]
        public void ThenResourcesIsReceived()
        {
            RestRequest restRequest = new RestRequest(ApiUrl.resourceUrl);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.RequestFormat = DataFormat.Json;
            _response = _restClient.Execute(restRequest);

            Assert.AreEqual(HttpStatusCode.OK, _response.StatusCode);

            MultiplyJsonResponce deserialize = JsonConvert.DeserializeObject<MultiplyJsonResponce>(_response.Content);
            Assert.IsNotNull(deserialize.Page);
            Assert.IsNotNull(deserialize.PerPage);
            Assert.IsNotNull(deserialize.Total);
            Assert.IsNotNull(deserialize.TotalPages);

            Assert.IsNotNull(deserialize.Data);
            Assert.IsTrue(deserialize.Data.Any());

            UserInfoDataResponce resource = deserialize.Data.FirstOrDefault();
            Assert.IsNotNull(resource.Id);

        }

        [Then(@"Resource provide company info")]
        public void ThenResourceProvideCompanyInfo()
        {
            Url userIdUrl = Url.Combine(ApiUrl.resourceUrl, "/", _resource.Number.ToString());

            RestRequest restRequest = new RestRequest(userIdUrl);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.RequestFormat = DataFormat.Json;
            _response = _restClient.Execute(restRequest);

            ResourceInfoFullResponce data = JsonConvert.DeserializeObject<ResourceInfoFullResponce>(_response.Content);
            Assert.IsNotNull(data.Data);
            Assert.IsNotNull(data.Data.Id);
            Assert.IsNotNull(data.Support);
            Assert.AreEqual(HttpStatusCode.OK, _response.StatusCode);
        }

        [Then(@"the resource response status code should be Not found")]
        public void ThenTheResourceResponseStatusCodeShouldBeNotFound()
        {
            Assert.AreEqual(HttpStatusCode.NotFound, _response.StatusCode);
        }
    }
}
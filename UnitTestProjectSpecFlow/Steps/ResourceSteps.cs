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

namespace UnitTestProjectSpecFlow.Steps
{
    [Binding]
    public class Resource
    {
        ResourceJson _resource = new ResourceJson();
        public RestClient _restClient = new RestClient();
        private IRestResponse response;

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
           
            response = _restClient.Execute(restRequest);
        }

        [Then(@"the responce should provide data resource")]
        public void ThenTheResponceShouldProvideDataResource()
        {
            //ResourceJson deserialize = JsonConvert.DeserializeObject<ResourceJson>(_resource.Data);
            //Assert.IsNotNull(deserialize);
        }

        [Then(@"the resource responce status code should be OK")]
        public void ThenTheResourceResponceStatusCodeShouldBeOK()
        {
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode); 
        }


    }
}

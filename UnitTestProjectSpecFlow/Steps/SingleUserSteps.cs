using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using TechTalk.SpecFlow;
using UnitTestProjectSpecFlow.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace UnitTestProjectSpecFlow.Steps
{
    [Binding]
    public class SingleUserSteps
    {
        public RestClient _restClient = new RestClient();
        public User _user = new User();
        private IRestResponse _response = null;
        
        [Given(@"user Id")]
        public void GivenUserId()
        {
            _user.Id = 2;
        }

        [Given(@"I have sent user id")]
        public void GivenIHaveSentUserId()
        {
            string baseURL = "https://reqres.in";
            string apiURL = baseURL + "/" + "api/users/2";

            RestRequest restRequest = new RestRequest(apiURL);

            restRequest.AddHeader("Accept", "application/json");
            restRequest.RequestFormat = DataFormat.Json;
            
            _response = _restClient.Execute(restRequest);
        }
        
        [Then(@"the result full of data")]
        public void ThenTheResultFullOfData() 
        {
           JsonResponce deserialize = JsonConvert.DeserializeObject<JsonResponce>(_response.Content);
            
            Assert.IsNotNull(deserialize.Data.Id);
            Assert.IsNotNull(deserialize.Data.Email);
            Assert.IsNotNull(deserialize.Data.FirstName);
            Assert.IsNotNull(deserialize.Data.LastName);
        }
        [Then(@"the status code should be OK")]
        public void ThenTheStatusCodeShouldBeOK()
        {
            Assert.AreEqual(HttpStatusCode.OK, _response.StatusCode);
        }



    }
}

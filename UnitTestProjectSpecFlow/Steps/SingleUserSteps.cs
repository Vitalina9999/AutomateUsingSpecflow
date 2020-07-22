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
        public RestClient restClient = new RestClient();
        public User user = new User();
        private IRestResponse response = null;
        //public IRestResponse IRestResponse
        //{
        //    get { return response; }
        //    set { response = value; }
        //}

        [Given(@"user Id")]
        public void GivenUserId()
        {
            user.Id = 2;
        }

        [Given(@"I have sent user id")]
        public void GivenIHaveSentUserId()
        {
            string baseURL = "https://reqres.in";
            string apiURL = baseURL + "/" + "api/users/2";

            RestRequest restRequest = new RestRequest(apiURL);

            restRequest.AddHeader("Accept", "application/json");
            restRequest.RequestFormat = DataFormat.Json;
            
            response = restClient.Execute(restRequest);
        }
        
        [Then(@"the result full of data")] // DATA in process
        public void ThenTheResultFullOfData() // need to transfer responce to another ;ass
        {
            JsonResponce deserialize = JsonConvert.DeserializeObject<JsonResponce>(response.Content);
            
            Assert.IsNotNull(deserialize.Data.Id);
            Assert.IsNotNull(deserialize.Data.Email);
            Assert.IsNotNull(deserialize.Data.FirstName);
            Assert.IsNotNull(deserialize.Data.LastName);
        }
    }
}

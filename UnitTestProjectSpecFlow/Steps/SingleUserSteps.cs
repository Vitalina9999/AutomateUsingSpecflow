using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using TechTalk.SpecFlow;
using UnitTestProjectSpecFlow.Entities;
using Newtonsoft.Json;

namespace UnitTestProjectSpecFlow.Steps
{
    [Binding]
    public class SingleUserSteps
    {
        public RestClient restClient = new RestClient();
        public User user = new User();
        private IRestResponse response = null;

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
        public void ThenTheResultFullOfData()
        {
            string content = response.Content; //
            ResponceLoginUser deserialize = JsonConvert.DeserializeObject<ResponceLoginUser>(content);
            Assert.IsNotNull(deserialize);
           // Assert.IsNotNull(response.Content.Contains("id"));
           // Assert.IsNotNull(response.Content.Contains("email"));
            //Assert.IsNotNull(response.Content.Contains("first_name"));
           // Assert.IsNotNull(response.Content.Contains("last_name"));
        }
    }
}

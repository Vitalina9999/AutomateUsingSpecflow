using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using UnitTestProjectSpecFlow.Entities;
using UnitTestProjectSpecFlow.Json;

namespace UnitTestProjectSpecFlow.Steps
{
    [Binding]
    public class SingleUserSteps
    {
        public RestClient _restClient = new RestClient();
        public User _user = new User();
        private IRestResponse _response;
        public Pages _pages = new Pages();

        private readonly string _usersUrl;
        
        public SingleUserSteps(ApiURL apiUrl)
        {
            _usersUrl = apiUrl.usersUrl;
        }

        [Given(@"user Id")]
        public void GivenUserId(Table table)
        {
            User account = table.CreateInstance<User>();
            _user.Id = account.Id;
        }

        [Given(@"page number")]
        public void GivenPageNumber(Table table)
        {
            Pages pagesList = table.CreateInstance<Pages>();
            _pages.Page = pagesList.Page;
        }

        [Given(@"I have sent user Id")]
        public void GivenIHaveSentUserId()
        {
            string userIdUrl = String.Concat(_usersUrl, "/", _user.Id);
            RestRequest restRequest = new RestRequest(userIdUrl);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.RequestFormat = DataFormat.Json;

            _response = _restClient.Execute(restRequest);
        }

        [Given(@"I have sent request with page number")]
        public void GivenIHaveSentRequestWithPageNumber()
        {
            // UriBuilder https://stackoverflow.com/questions/20164298/how-to-build-a-url
            string usersPageUrl = String.Concat(_usersUrl, "?", _pages.Page);

            RestRequest restRequest = new RestRequest(usersPageUrl);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.RequestFormat = DataFormat.Json;

            _response = _restClient.Execute(restRequest);
        }

        [Then(@"the result user list with full of data")]
        public void ThenTheResultUserListWithFullOfData()
        {
            MultiplyJsonResponce deserialize = JsonConvert.DeserializeObject<MultiplyJsonResponce>(_response.Content);
            Assert.IsNotNull(deserialize.Page);
            Assert.IsNotNull(deserialize.PerPage);
            Assert.IsNotNull(deserialize.Total);
            Assert.IsNotNull(deserialize.TotalPages);
        }

        [Then(@"the result full of data")]
        public void ThenTheResultFullOfData()
        {
            JsonResponse deserialize = JsonConvert.DeserializeObject<JsonResponse>(_response.Content);

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

        [Then(@"the status code should be Not Found")]
        public void ThenTheStatusCodeShouldBeNotFound() //404
        {
            Assert.AreEqual(HttpStatusCode.NotFound, _response.StatusCode);
        }
    }
}
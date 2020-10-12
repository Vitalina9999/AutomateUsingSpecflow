﻿using System;
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

        private readonly string _usersUrl;
        public SingleUserSteps(ApiURL apiUrl)
        {
            _usersUrl = apiUrl.usersUrl;
        }


        [Given(@"unexisted user Id")]
        public void GivenUnexistedUserId()
        {
            _user.Id = 244444444;
        }
        [Given(@"user Id")]
        public void GivenUserId(Table table)
        {
            var account = table.CreateInstance<User>();
            _user.Id = account.Id;
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
            int page = 2;
            string baseURL = "https://reqres.in";
            string apiURL = baseURL + "/" + "api/users?" + "page=" + page;

            RestRequest restRequest = new RestRequest(apiURL);

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
            // Assert.IsNotNull(deserialize.JsonResponceList); // null?
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

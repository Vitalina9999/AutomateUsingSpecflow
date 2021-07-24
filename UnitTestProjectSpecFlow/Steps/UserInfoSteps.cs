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
using Flurl;

namespace UnitTestProjectSpecFlow.Steps
{
    [Binding]
    public class UserInfoSteps
    {
        private RestClient _restClient = new RestClient();
        private UserInfo _user = new UserInfo();
        private IRestResponse _response;
        private UserPage _pages = new UserPage();

        [Given(@"user Id")]
        public void GivenUserId(Table table)
        {
            UserInfo account = table.CreateInstance<UserInfo>();
            _user.Id = account.Id;
        }

        [Given(@"page number")]
        public void GivenPageNumber(Table table)
        {
            UserPage pagesList = table.CreateInstance<UserPage>();
            _pages.Page = pagesList.Page;
        }

        [Then(@"User info is received")]
        public void ThenUserInfoIsExist()
        {
            Url userIdUrl = Url.Combine(ApiUrl.usersUrl, "/", _user.Id.ToString());

            RestRequest restRequest = new RestRequest(userIdUrl);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.RequestFormat = DataFormat.Json;

            _response = _restClient.Execute(restRequest);

            Assert.AreEqual(HttpStatusCode.OK, _response.StatusCode);
            UserInfoFullResponce deserialize = JsonConvert.DeserializeObject<UserInfoFullResponce>(_response.Content);

            Assert.IsNotNull(deserialize.Data.Id);
            Assert.IsNotNull(deserialize.Data.Email);
            Assert.IsNotNull(deserialize.Data.FirstName);
            Assert.IsNotNull(deserialize.Data.LastName);
        }

        [Then(@"User info is Not Found")]
        public void ThenUserInfoIsNotFound()
        {
            Url userIdUrl = Url.Combine(ApiUrl.usersUrl, "/", _user.Id.ToString());

            RestRequest restRequest = new RestRequest(userIdUrl);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.RequestFormat = DataFormat.Json;

            _response = _restClient.Execute(restRequest);

            UserInfoFullResponce deserialize = JsonConvert.DeserializeObject<UserInfoFullResponce>(_response.Content);

            Assert.AreEqual(HttpStatusCode.NotFound, _response.StatusCode);
        }

        [Then(@"Users info is received")]
        public void ThenUsersInfoIsReceived()
        {
            Url usersPageUrl = ApiUrl.usersUrl.SetQueryParam("page", _pages.Page);

            RestRequest restRequest = new RestRequest(usersPageUrl);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.RequestFormat = DataFormat.Json;

            _response = _restClient.Execute(restRequest);

            MultiplyJsonResponce deserialize = JsonConvert.DeserializeObject<MultiplyJsonResponce>(_response.Content);
            Assert.IsNotNull(deserialize.Page);
            Assert.IsNotNull(deserialize.PerPage);
            Assert.IsNotNull(deserialize.Total);
            Assert.IsNotNull(deserialize.TotalPages);
        }
    }
}
using System;
using System.Net;
using Flurl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using UnitTestProjectSpecFlow.Entities;
using UnitTestProjectSpecFlow.Json;

namespace UnitTestProjectSpecFlow.Features
{
    [Binding]
    public class UserCRUDSteps
    {
        private User _user = new User();
        private RestClient _restClient = new RestClient();
        private IRestResponse _response;

        [Given(@"user with name and job")]
        public void GivenUserWithNameAndJob(Table table)
        {
            User account = table.CreateInstance<User>();
            _user.FirstName = account.FirstName;
            _user.Job = account.Job;
        }

        [Given(@"user id")]
        public void GivenUserId(Table table)
        {
            User account = table.CreateInstance<User>();
            _user.Id = account.Id;
        }

       [Then(@"user is created")]
        public void ThenUserIsCreated()
        {
            RestRequest restRequest = new RestRequest(ApiURL.usersUrl, RestSharp.Method.POST);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.AddParameter("name", _user.FirstName);
            restRequest.AddParameter("job", _user.Job);

            _response = _restClient.Execute(restRequest);
            string content = _response.Content;
            CRUDResponseJson deserialize = JsonConvert.DeserializeObject<CRUDResponseJson>(content);
            Assert.IsNotNull(deserialize.Id);
            Assert.IsNotNull(deserialize.Name);
            Assert.IsNotNull(deserialize.CreatedAt);
            Assert.IsNotNull(deserialize.Job);
            Assert.AreEqual(HttpStatusCode.Created, _response.StatusCode);
        }


        [Then(@"user is deleted")]
        public void ThenUserIsDeleted()
        {
            RestRequest restRequest = new RestRequest(ApiURL.usersUrl, RestSharp.Method.DELETE);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.AddParameter("id", _user.Id);
            _response = _restClient.Execute(restRequest);
            Assert.AreEqual(HttpStatusCode.NoContent, _response.StatusCode);
        }
        
      
       [Then(@"user is updated")]
        public void ThenUserIsUpdated()
        {
            RestRequest restRequest = new RestRequest(ApiURL.usersUrl, RestSharp.Method.PUT);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.AddParameter("id", _user.Id);
            _response = _restClient.Execute(restRequest);
            string content = _response.Content;
            CRUDResponseJson deserialize = JsonConvert.DeserializeObject<CRUDResponseJson>(content);
            //Assert.IsNotNull(deserialize.Name);
            Assert.IsNotNull(deserialize.UpdatedAt);
            //Assert.IsNotNull(deserialize.Job);
            Assert.AreEqual(HttpStatusCode.OK, _response.StatusCode);
        }

        [Then(@"user's job is updated")]
        public void ThenUserSJobIsUpdated()
        {
            Url userIdUrl = Url.Combine(ApiURL.usersUrl, "/", _user.Id.ToString());

            RestRequest restRequest = new RestRequest(userIdUrl, RestSharp.Method.PATCH);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.AddParameter("name", _user.FirstName);
            restRequest.AddParameter("job", _user.Job);
            _response = _restClient.Execute(restRequest);

            string content = _response.Content;
            CRUDResponseJson deserialize = JsonConvert.DeserializeObject<CRUDResponseJson>(content);
           Assert.IsNotNull(deserialize.UpdatedAt);
           Assert.AreEqual(HttpStatusCode.OK, _response.StatusCode);
        }

    }
}
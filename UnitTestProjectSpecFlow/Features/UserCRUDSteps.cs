using System;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using TechTalk.SpecFlow;
using UnitTestProjectSpecFlow.Entities;
using UnitTestProjectSpecFlow.Json;

namespace UnitTestProjectSpecFlow.Features
{
    [Binding]
    public class UserCRUDSteps
    {
        public UserCreate _userCreate = new UserCreate();
        public RestClient _restClient = new RestClient();
       // public User _user = new User();
        private IRestResponse _response = null;
       
        [Given(@"name and job")]
        public void GivenNameAndJob()
        {
            RandomName();
            RandomJob();
        }

        [When(@"I send request with method Post")]
        public void WhenISendRequestWithMethodPost()
        {
            string baseURL = "https://reqres.in";
            string apiURL = baseURL + "/" + "api/users";

            RestRequest restRequest = new RestRequest(apiURL, RestSharp.Method.POST);

            restRequest.AddHeader("Accept", "application/json");
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.AddParameter("name", _userCreate.Name);
            restRequest.AddParameter("job", _userCreate.Job);

            _response = _restClient.Execute(restRequest);
        }

        
        [Then(@"the result should contains name, job, id, createdAt")]
        public void ThenTheResultShouldContainsNameJobIdCreatedAt()
        {
            string content = _response.Content;
            CRUDResponceJson deserialize = JsonConvert.DeserializeObject<CRUDResponceJson>(content);
            Assert.IsNotNull(deserialize.Id);
            Assert.IsNotNull(deserialize.Name);
            Assert.IsNotNull(deserialize.CreatedAt);
            Assert.IsNotNull(deserialize.Job);
        }

        [Then(@"the status code should be Created")]
        public void ThenTheStatusCodeShouldBeCreated()
        {
            Assert.AreEqual(HttpStatusCode.Created, _response.StatusCode);
        }

        [Given(@"existed user id")]
        public void GivenExistedUserId()
        {
            int id = 2;
        }

        [When(@"I send request with method Delete")]
        public void WhenISendRequestWithMethodDelete()
        {
            string baseURL = "https://reqres.in";
            string apiURL = baseURL + "/" + "api/users";

            RestRequest restRequest = new RestRequest(apiURL, RestSharp.Method.DELETE);

            restRequest.AddHeader("Accept", "application/json");
            restRequest.RequestFormat = DataFormat.Json;
            //restRequest.AddParameter("id", _userCreate.Id);
            restRequest.AddParameter("id", 2);

            _response = _restClient.Execute(restRequest);
        }

        [Then(@"the status code should be No Content")]
        public void ThenTheStatusCodeShouldBeNoContent()
        {
            Assert.AreEqual(HttpStatusCode.NoContent, _response.StatusCode);

        }

        private string RandomName()
        {
            Random rnd = new Random();
            int randomNumber = rnd.Next(1, 1000);
            string resultName = string.Concat("Vitalina" + randomNumber);
            _userCreate.Name = resultName;
            return resultName;
        }
        private string RandomJob()
        {
            Random rnd = new Random();
            int randomNumber = rnd.Next(1, 1000);
            string resultJob = string.Concat("job" + randomNumber);
            _userCreate.Job = resultJob;
            return resultJob;
        }
    }
}

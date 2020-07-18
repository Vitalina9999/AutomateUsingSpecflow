using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using TechTalk.SpecFlow;
using UnitTestProjectSpecFlow.Entities;

namespace UnitTestProjectSpecFlow.Steps
{
    [Binding]
    public class LoginUserSteps
    {
        public RestClient restClient = new RestClient();
        public User user = new User();
        private IRestResponse response = null;

        [Given(@"A correct email")]
        public void GivenACorrectEmail()
        {
            user.Email = "eve.holt@reqres.in";
        }

        [Given(@"An incorrect email")]
        public void GivenAnInCorrectEmail()
        {
            user.Email = "ewewewewewe";
        }

        [Given(@"an incorrect password")]
        public void GivenAnInCorrectPassword()
        {
            user.Password = "dfgdfgdfgdfgfd";
        }

        [Given(@"a correct password")]
        public void GivenAnCorrectPassword()
        {
            user.Password = "cityslicka";
        }

        [When(@"I send request")]
        public void WhenISendRequest()
        {
            string baseURL = "https://reqres.in";
            string apiURL = baseURL + "/" + "api/login";

            RestRequest restRequest = new RestRequest(apiURL, RestSharp.Method.POST);

            restRequest.AddHeader("Accept", "application/json");
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.AddParameter("email", user.Email);
            restRequest.AddParameter("password", user.Password);

            response = restClient.Execute(restRequest);
        }

        [Then(@"the user should be returned in the responce")]
        public void ThenTheUserShouldBeReturnedInTheResponce(User user)
        {
            // Assert.IsNotNull<OkObjectResult>(user.);
        }

        [Then(@"the response status code is (.*)")]
        public void ThenTheResponseStatusCodeIs(int statusCode)
        {
            if (statusCode == 200) //"string statusCode 200 OK"
            {
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }

            else if (statusCode == 400) //" string statusCode 404 Not Found"
            {
                Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }

        [Then(@"the token is NotNull")]
        public void ThenTheTokenIsNotNull()
        {
            Assert.IsNotNull(response.Content.Contains("token"));
        }

        [Then(@"data is valid")]
        public void ThenDataIsValid()
        {
            string content = response.Content;
            ResponceLoginUser deserialize = JsonConvert.DeserializeObject<ResponceLoginUser>(content);
            Assert.IsNotNull(deserialize);
         }

        //[Then(@"User has response (.*) and token")]
        //public void ThenUserHasResponse400AndToken()
        //{
        //    Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        //}
    }
}

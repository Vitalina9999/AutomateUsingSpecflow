using System;
using TechTalk.SpecFlow;
using UnitTestProjectSpecFlow.Entities;

namespace UnitTestProjectSpecFlow.Steps
{
    [Binding]
    public class SingleUserSteps
    {
        public User user = new User();

        [Given(@"user Id")]
        public void GivenUserId()
        {
            user.Id = 2;
        }
        
        [Given(@"I have sent user id")]
        public void GivenIHaveSentUserId()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"the result full of data")]
        public void ThenTheResultFullOfData()
        {
            ScenarioContext.Current.Pending();
        }
    }
}

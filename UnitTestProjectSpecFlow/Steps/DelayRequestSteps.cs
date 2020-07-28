using System;
using TechTalk.SpecFlow;

namespace UnitTestProjectSpecFlow.Steps
{
    [Binding]
    public class DelayRequestSteps
    {
        [Given(@"list of users")]
        public void GivenListOfUsers()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"send response with delay")]
        public void WhenSendResponseWithDelay()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"the result status code is Ok")]
        public void ThenTheResultStatusCodeIsOk()
        {
            ScenarioContext.Current.Pending();
        }
    }
}

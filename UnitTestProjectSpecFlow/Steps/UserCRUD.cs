using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace UnitTestProjectSpecFlow.Steps
{
    public class UserCRUD
    {
        [Given(@"name and job")]
        public string GivenNameAndJob(string resultName, string resultJob)
        {
            string name = resultName;
            string job = resultJob;
            return name;

        }

        [When(@"I send request with method Post")]
        public void WhenISendRequestWithMethodPost()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the result should contains name, job, id, createdAt")]
        public void ThenTheResultShouldContainsNameJobIdCreatedAt()
        {
            ScenarioContext.Current.Pending();
        }


        [Then(@"the status code should be Created")]
        public void ThenTheStatusCodeShouldBeCreated()
        {
            ScenarioContext.Current.Pending();
        }

        private string RandomName()
        {
            Random rnd = new Random();
            int randomNumber = rnd.Next(1, 1000);
            string resultName = string.Concat("Vitalina" + randomNumber);
            
            return resultName;
        }
        private string RandomJob()
        {
            Random rnd = new Random();
            int randomNumber = rnd.Next(1, 1000);
            string resultJob = string.Concat("job" + randomNumber);

            return resultJob;
        }
    }
}

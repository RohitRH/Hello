using System;
using TechTalk.SpecFlow;
using FirstApi.Services;
using FirstApi.Models;
using Xunit;
using TechTalk.SpecFlow.Assist;

namespace XUnitTestProject2
{
    [Binding]
    public class SpecFlowFeature2Steps
    {
        private UsersInterface uad = new UsersRepository();
        private UsersData users ;
        int response = 0;

        [Given(@"I Create a New User")]
        public void GivenICreateANewUser(Table table)
        {
            users = table.CreateInstance<UsersData>();
            response = uad.AddUsers(users);
        }

        


      /*  [Given(@"I Create a New User \(Rohit,rohit@gvg\.com,vgvgvgg,(.*)\)")]
        public void GivenICreateANewUserRohitRohitGvg_ComVgvgvgg(Table table)
        {
            //users = new UsersData() { Name = Name, Email = Email, Password = Password };
            users = table.CreateInstance<UsersData>();
            response = uad.AddUsers(users);
        }*/


        [Given(@"ModelState is Correct")]
        public void GivenModelStateIsCorrect()
        {
            Assert.NotNull(users.Name);
            Assert.NotNull(users.Email);
            Assert.NotNull(users.Password);
        }
        
       /* [Then(@"The Api should return ok (.*)")]
        public void ThenTheApiShouldReturnOk(int p0)
        {
            Assert.Equal(response,200);
        }*/

        [Then(@"The Api should return ok")]
        public void ThenTheApiShouldReturnOk()
        {
            Assert.Equal(response, 200);
        }

    }
}

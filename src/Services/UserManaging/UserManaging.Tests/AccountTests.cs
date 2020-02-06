using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using UserManaging.API;
using UserManaging.CQRS.Commands.Create;
using Xunit;

namespace UserManaging.Tests
{

    public class AccountTests : IClassFixture<UserManagingApplicationFactory<Startup>>
    {
        private readonly HttpClient client;
        public AccountTests(UserManagingApplicationFactory<Startup> factory)
        {
            client = factory.CreateClient();
        }


        [Fact]
        public async Task A_CreateUserAndAccount_UserAcocuntInserted_Return201StatusCode()
        {
            //Arrange
            CreateUserCommand createUserCommand = new CreateUserCommand
            {
                Email = "test3@example.com",
                FirstName = "Paul",
                LastName = "Walker",
                Expenses = 300,
                Salary = 500
            };
            CreateAccountCommand createAccountCommand = new CreateAccountCommand
            {
                Balance = 1000,
                Type = "ZipMoney"
            };

            // Act
            var userContent = TestHelpers.CreateHttpContent(createUserCommand);
            var userResponse = await TestHelpers.MakePostRequest(client, "/api/v1/users", userContent);
            var userResponseObject = await TestHelpers.DeserializeResponse<CreateUserResponse>(userResponse);

            createAccountCommand.UserId = userResponseObject.UserId;
            var accountContent = TestHelpers.CreateHttpContent(createAccountCommand);
            var accountResponse = await TestHelpers.MakePostRequest(client, "/api/v1/accounts", accountContent);
            var accountResponseObject = await TestHelpers.DeserializeResponse<CreateAccountResponse>(accountResponse);

            //Assert
            Assert.Equal(HttpStatusCode.Created, userResponse.StatusCode);
            Assert.Equal(HttpStatusCode.Created, accountResponse.StatusCode);

            Assert.Equal(createAccountCommand.Type, accountResponseObject.Type);
            Assert.Equal(createAccountCommand.UserId, accountResponseObject.UserId);

        }


    }
}

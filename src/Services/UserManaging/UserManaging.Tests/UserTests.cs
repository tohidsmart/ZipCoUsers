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

    public class UserTests : IClassFixture<UserManagingApplicationFactory<Startup>>
    {
        private readonly HttpClient client;
        public UserTests(UserManagingApplicationFactory<Startup> factory)
        {
            client = factory.CreateClient();
        }

        [Fact]
        public async Task A_CreateUser_UserInserted_Return201StatusCode()
        {
            //Arrange
            CreateUserCommand createUserCommand = new CreateUserCommand
            {
                Email = "test2@example.com",
                FirstName = "John",
                LastName = "McCarthy",
                Expenses = 100,
                Salary = 200
            };
            var content = TestHelpers.CreateHttpContent(createUserCommand);

            // Act
            var response = await TestHelpers.MakePostRequest(client, "/api/v1/users", content);
            var responseObject = await TestHelpers.DeserializeResponse<CreateUserResponse>(response);

            //Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal(responseObject.FirstName, createUserCommand.FirstName);

        }


        [Fact]
        public async Task B_CreateUser_UserSalaryZeroExpensesZero_Return400BadRequestStatusCode()
        {
            //Arrange
            CreateUserCommand createUserCommand = new CreateUserCommand
            {
                Email = "InvalidEmail",
                FirstName = "",
                LastName = "",
                Expenses = 0,
                Salary = 0
            };

            // Act
            var userContent = TestHelpers.CreateHttpContent(createUserCommand);
            var userResponse = await TestHelpers.MakePostRequest(client, "/api/v1/users", userContent);
            var userResponseObject = await userResponse.Content.ReadAsStringAsync();


            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, userResponse.StatusCode);

            Assert.Contains("Salary must be a positive number", JsonConvert.DeserializeObject(userResponseObject).ToString());
            Assert.Contains("Expenses must be a positive number", JsonConvert.DeserializeObject(userResponseObject).ToString());
            Assert.Contains("Email is not in the right format", JsonConvert.DeserializeObject(userResponseObject).ToString());
            Assert.Contains("First name can not be null or empty", JsonConvert.DeserializeObject(userResponseObject).ToString());
            Assert.Contains("Last name can not be null or empty", JsonConvert.DeserializeObject(userResponseObject).ToString());

        }

        [Fact]
        public async Task C_CreateTwoUsers_WithSameEmailAddress_ReturnBadRequest()
        {
            //Arrange
            CreateUserCommand createUserACommand = new CreateUserCommand
            {
                Email = "UserA@example.com",
                FirstName = "First",
                LastName = "User",
                Expenses = 400,
                Salary = 600
            };

            CreateUserCommand createUserBCommand = new CreateUserCommand
            {
                Email = "UserA@example.com",
                FirstName = "Second",
                LastName = "User",
                Expenses = 100,
                Salary = 400
            };

            // Act
            var userAContent = TestHelpers.CreateHttpContent(createUserACommand);
            var userAResponse = await TestHelpers.MakePostRequest(client, "/api/v1/users", userAContent);

            var userBContent = TestHelpers.CreateHttpContent(createUserBCommand);
            var userBResponse = await TestHelpers.MakePostRequest(client, "/api/v1/users", userBContent);
            var userBResponseObject = await userBResponse.Content.ReadAsStringAsync();

            //Assert
            Assert.Equal(HttpStatusCode.Created, userAResponse.StatusCode);

            Assert.Equal(HttpStatusCode.BadRequest, userBResponse.StatusCode);
            Assert.Contains("'UNIQUE constraint failed: Users.Email'", userBResponseObject);



        }

    }
}

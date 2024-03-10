using appDefinitions;
using appDefinitions.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApiProject
{
    [Route("api/newuser")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        private readonly ILogger<UserManagementController> logger;
        private readonly IUserManagement userManagement;
        private readonly IDbService dbService;

        public UserManagementController(ILogger<UserManagementController> logger, IUserManagement userManagement, IDbService dbService)
        {
            this.logger = logger;
            this.userManagement = userManagement;
            this.dbService = dbService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewUserRequest([FromBody] NewUser user)
        {
            this.logger.LogInformation("Create New User Request Started");
            // Validate new user is some way...

            //I can't imagine this is th best way to do this...
            var requestIp = HttpContext.Connection.RemoteIpAddress.ToString();

            var createdUser =  await userManagement.CreateNewUser(new BasicNewUserInfo(user.FirstName, user.LastName, user.Mobile, user.Email, user.DateOfBirth, user.AddressLine1, user.AddressLine2, user.City, user.Postcode, user.State, user.Country,  requestIp));
            await dbService.Users.AddNewUser(createdUser);
            this.logger.LogInformation("Create New User Request:Success");
            return new OkObjectResult(new { user = createdUser });
        }


        [HttpGet("allusers")]
        public async Task<IActionResult> GetAllUsers()
        {
            this.logger.LogInformation("List all Users");

            var users = await userManagement.AllUsers();

            this.logger.LogInformation("List all UsersLSuccess");
            return Ok();

        }

    }
}

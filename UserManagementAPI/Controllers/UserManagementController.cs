using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;
using UserManagementAPI.Services;

namespace UserManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserManagementController : ControllerBase
    {
        private readonly IUserService _userManager;

        public UserManagementController(IUserService userManager)
        {
            _userManager = userManager;
        }

        [HttpDelete("users/delete/{id}")]
        public IResult DeleteUser(int id)
        {
            return _userManager.DeleteUser(id);
        }

        [HttpGet("users")]
        public IResult GetAllUsers()
        {
            return _userManager.GetAllUsers();
        }

        [HttpGet("users/{id}")]
        public IResult GetUserById(int id)
        {
            return _userManager.GetUserById(id);
        }

        [HttpPost("users/new")]
        public IResult PostNewUser(UserData user)
        {
            return _userManager.AddUser(user);
        }

        [HttpPut("users/update/{id}")]
        public IResult UpdateUser(int id, UserData user)
        {
            return _userManager.UpdateUser(id, user);
        }
    }
}
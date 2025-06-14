using Microsoft.AspNetCore.Http.HttpResults;
using UserManagementAPI.Models;
using UserManagementAPI.Validators;

namespace UserManagementAPI.Services
{
    public class UserManager : IUserService
    {
        private readonly List<UserData> _users;

        public UserManager(List<UserData> users)
        {
            _users = users ?? throw new ArgumentNullException(nameof(users), "User list cannot be null");
        }

        public IResult AddUser(UserData user)
        {
            var validationResult = UserValidator.Validate(user, _users, isUpdate: false);
            if (validationResult is not Ok<string>)
                return validationResult;

            _users.Add(user);
            return TypedResults.Ok("User added successfully");
        }

        public IResult DeleteUser(int id)
        {
            if (id < 1 || id > _users.Count)
            {
                return TypedResults.BadRequest("Invalid user ID");
            }
            _users.RemoveAt(id - 1);
            return TypedResults.NoContent();
        }

        public IResult GetAllUsers()
        {
            return TypedResults.Ok(_users);
        }

        public IResult GetUserById(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return TypedResults.NotFound("User not found");
            }
            return TypedResults.Ok(user);
        }

        public IResult UpdateUser(int id, UserData user)
        {
            var validationResult = UserValidator.Validate(user, _users, isUpdate: true);
            if (validationResult is not Ok<string>)
                return validationResult;

            var index = _users.FindIndex(u => u.Id == id);
            if (index == -1) return TypedResults.NotFound("User not found");
            
            _users[index] = user;
            return TypedResults.Ok($"User updated successfully:{_users[index]}");
        }
    }
}

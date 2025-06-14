using UserManagementAPI.Models;
using System.Text.RegularExpressions;

namespace UserManagementAPI.Validators
{
    public class  UserValidator
    {
        public static IResult Validate(UserData user, IEnumerable<UserData> existingUsers, bool isUpdate = false)
        {
            if (user == null)
                return TypedResults.BadRequest("User data cannot be null.");
            if (string.IsNullOrWhiteSpace(user.Name) || user.Name.Length < 2 || user.Name.Length > 50)
                return TypedResults.BadRequest("User name must be between 2 and 50 characters.");
            if (string.IsNullOrWhiteSpace(user.Email))
                return TypedResults.BadRequest("User email cannot be empty.");
            if (!Regex.IsMatch(user.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                return TypedResults.BadRequest("User email format is invalid.");
            if (!isUpdate && existingUsers != null)
            {
                if (user.Id <= 0)
                    return TypedResults.BadRequest("User ID must be greater than 0.");
                if (existingUsers.Any(u => u.Id == user.Id))
                    return TypedResults.BadRequest("User ID must be unique.");
                if (existingUsers.Any(u => u.Email == user.Email))
                    return TypedResults.BadRequest("User email must be unique.");
            }

            return TypedResults.Ok("User data is valid.");
        }
    }
}
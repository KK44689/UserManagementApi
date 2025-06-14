using UserManagementAPI.Models;

namespace UserManagementAPI.Services
{
    public interface IUserService
    {
        IResult DeleteUser(int id);
        IResult GetAllUsers();
        IResult GetUserById(int id);
        IResult AddUser(UserData user);
        IResult UpdateUser(int id, UserData user);
    }
}

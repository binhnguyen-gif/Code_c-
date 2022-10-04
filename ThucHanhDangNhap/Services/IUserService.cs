using ThucHanhDangNhap.Dtos.Auth;
using ThucHanhDangNhap.Dtos.Users;
using ThucHanhDangNhap.Models;

namespace ThucHanhDangNhap.Services;

public interface IUserService
{
    User CreateUser(UserDto userDto);
    User UpdateUserByUsername(UserDto userDto);
    void DeleteUser(string username);
    List<User> FindAllUser();
    string Login(LoginDto loginDto);
    void ChangeUserStatus(ChangeStatusDto changeStatusDto);
}
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ThucHanhDangNhap.Constants;
using ThucHanhDangNhap.DBContext;
using ThucHanhDangNhap.Dtos.Auth;
using ThucHanhDangNhap.Dtos.Users;
using ThucHanhDangNhap.ExceptionCustom;
using ThucHanhDangNhap.Models;
using ThucHanhDangNhap.Utils;

namespace ThucHanhDangNhap.Services.Implement;

public class UserServiceImpl : IUserService
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    public UserServiceImpl(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public User CreateUser(UserDto userDto)
    {
        if (_context.Users.Any(user => user.Username == userDto.Username))
        {
            throw new FriendlyException($"Username: {userDto.Username} đã tồn tại");
        }

        if (_context.Users.Any(user => user.Email == userDto.Email))
        {
            throw new FriendlyException($"Email: {userDto.Email} đã được sử dụng");
        }

        var user = new User()
        {
            Username = userDto.Username,
            Email = userDto.Email,
            Password = CommonUtils.CreateMD5(userDto.Password),
            Phone = userDto.Phone,
            UserType = userDto.UserType,
            CustomerId = userDto.CustomerId
        };
        _context.Users.Add(user);
        _context.SaveChanges();
        return user;
    }

    public User UpdateUserByUsername(UserDto userDto)
    {
        var user = _context.Users.FirstOrDefault(user => user.Username == userDto.Username);
        if (user == null)
        {
            throw new FriendlyException($"Username: {userDto.Username} không tồn tại");
        }

        if (!user.Email.Equals(userDto.Email) && _context.Users.All(u => u.Email == userDto.Email))
        {
            throw new FriendlyException($"Email: {userDto.Email} đã được sử dụng");
        }

        user.Email = userDto.Email;
        user.Password = CommonUtils.CreateMD5(userDto.Password);
        user.Phone = userDto.Phone;
        user.UserType = userDto.UserType;

        _context.SaveChanges();
        return user;
    }

    public void DeleteUser(string username)
    {
        var userFind = _context.Users.FirstOrDefault(user => user.Username == username);
        if (userFind == null)
        {
            throw new FriendlyException($"Username: {username} không tồn tại");
        }

        _context.Users.Remove(userFind);
        _context.SaveChanges();
    }

    public List<User> FindAllUser()
    {
        return _context.Users.ToList();
    }

    public string Login(LoginDto input)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == input.Username);
        if (user == null)
        {
            throw new FriendlyException($"Username: {input.Username} không tồn tại");
        }

        if (user.UserStatus.Equals(UserStatus.Blocked))
        {
            throw new FriendlyException("Tài khoản đã bị khóa");
        }

        if (CommonUtils.CreateMD5(input.Password) != user.Password)
        {
            throw new FriendlyException("Sai mật khẩu");
        }

        var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
        var claims = new List<Claim>()
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Name, user.Username),
            new(CustomClaimType.UserType, user.UserType.ToString()),
            new(CustomClaimType.UserStatus, user.UserStatus.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
        //expires: DateTime.Now.AddSeconds(_configuration.GetValue<int>("JWT:Expires")),
            expires: DateTime.UtcNow.AddHours(1),
            claims: claims,
            signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256)
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public void ChangeUserStatus(ChangeStatusDto input)
    {
        var userFind = _context.Users.FirstOrDefault(user => user.Username == input.Username);
        if (userFind == null)
        {
            throw new FriendlyException($"Username: {input.Username} không tồn tại");
        }

        userFind.UserStatus = input.UserStatus;
        _context.SaveChanges();
    }
}
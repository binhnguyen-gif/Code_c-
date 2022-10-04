using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThucHanhDangNhap.Constants;
using ThucHanhDangNhap.Dtos.Auth;
using ThucHanhDangNhap.Dtos.Users;
using ThucHanhDangNhap.Filters;
using ThucHanhDangNhap.Services;

namespace ThucHanhDangNhap.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    
    [HttpPost]
    [Route("create-user")]
    public IActionResult CreateUser([FromForm] UserDto userDto)
    {
        try
        {
            var user = _userService.CreateUser(userDto);
            return StatusCode(StatusCodes.Status200OK, user);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
    
    
    [HttpPut]
    [Route("update-user")]
    public IActionResult UpdateUser([FromForm] UserDto userDto)
    {
        try
        {
            var user = _userService.UpdateUserByUsername(userDto);
            return StatusCode(StatusCodes.Status200OK, user);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
    
    [HttpDelete]
    [Route("delete-user")]
    public IActionResult DeleteUser(string username)
    {
        try
        {
            _userService.DeleteUser(username);
            return StatusCode(StatusCodes.Status200OK, username);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
    
    //[Authorize]
    [AuthorizationFilter(UserTypes.Admin)]
    [HttpDelete]
    [Route("find-all")]
    public IActionResult FindAllUser()
    {
        try
        {
            var users = _userService.FindAllUser();
            return StatusCode(StatusCodes.Status200OK, users);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
    
    [HttpPost]
    [Route("login")]
    public IActionResult Login(LoginDto input)
    {
        try
        {
            var token = _userService.Login(input);
            return StatusCode(StatusCodes.Status200OK, new { token });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    
    //[Authorize]
    //[AuthorizationFilter(UserTypes.Admin)]
    [HttpPost]
    [Route("status")]
    public IActionResult ChangeUserStatus([FromForm] ChangeStatusDto input)
    {
        try
        {
            _userService.ChangeUserStatus(input);
            return StatusCode(StatusCodes.Status200OK);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    
    
}
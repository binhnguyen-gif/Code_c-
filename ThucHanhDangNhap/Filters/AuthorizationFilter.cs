using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ThucHanhDangNhap.Constants;
using System;

namespace ThucHanhDangNhap.Filters;

public class AuthorizationFilter : Attribute, IAuthorizationFilter
{
    private readonly int[] _userTypes;

    public AuthorizationFilter(params int[] userTypes)
    {
        _userTypes = userTypes;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;
        var claims = user.Claims.ToList();
        Console.WriteLine(claims);

        var userTypeClaim = claims.FirstOrDefault(c => c.Type == CustomClaimType.UserType);
        if (userTypeClaim != null)
        {
            var userType = int.Parse(userTypeClaim.Value);
            if (!_userTypes.Contains(userType))
            {
                context.Result = new UnauthorizedObjectResult(new { message = $"UserType = {userType}" });
            }
        }
        else
        {
            context.Result = new UnauthorizedObjectResult(new { message = $"Không có quyền" });
        }
    }
}
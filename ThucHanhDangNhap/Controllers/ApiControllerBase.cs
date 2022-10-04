using Microsoft.AspNetCore.Mvc;
using ThucHanhDangNhap.ExceptionCustom;

namespace ThucHanhDangNhap.Controllers;

public class ApiControllerBase : ControllerBase
{
    private ILogger _logger;

    public ApiControllerBase(ILogger logger)
    {
        _logger = logger;
    }

    public IActionResult ReturnException(Exception ex)
    {
        if (ex is FriendlyException friendlyException)
        {
            return StatusCode(StatusCodes.Status400BadRequest, new ExceptionBody()
            {
                Message = friendlyException.Message
            });
        }
        _logger.LogError(ex, ex.Message);
        return StatusCode(StatusCodes.Status500InternalServerError, new ExceptionBody()
        {
            Message = ex.Message
        });
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThucHanhDangNhap.Constants;
using ThucHanhDangNhap.Dtos.Customers;
using ThucHanhDangNhap.Filters;
using ThucHanhDangNhap.Services;

namespace ThucHanhDangNhap.Controllers;

[Authorize]
[AuthorizationFilter(UserTypes.Admin)]
[ApiController]
[Route("api/[controller]")]
public class CustomerController : Controller
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }
    
    [HttpPost]
    [Route("create-customer")]
    public IActionResult CreateCustomer([FromForm] CustomerDto customerDto)
    {
        try
        {
            var customer = _customerService.CreateCustomer(customerDto);
            return StatusCode(StatusCodes.Status200OK, customer);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
    
    [HttpPut]
    [Route("update-customer/{id:int}")]
    public IActionResult UpdateCustomer(int id, [FromForm] CustomerDto customerDto)
    {
        try
        {
            var customer = _customerService.UpdateCustomer(id, customerDto);
            return StatusCode(StatusCodes.Status200OK, customer);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
    
    [HttpDelete]
    [Route("delete-customer/{id:int}")]
    public IActionResult DeleteCustomer(int id)
    {
        try
        {
            _customerService.DeleteCustomer(id);
            return StatusCode(StatusCodes.Status200OK);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
    
    [HttpGet]
    [Route("get-customer")]
    public IActionResult FindAllCustomer()
    {
        try
        {
            var customers = _customerService.FindAllCustomer();
            return StatusCode(StatusCodes.Status200OK, customers);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
    
    [HttpGet]
    [Route("get-users/{id:int}")]
    public IActionResult GetListsUser(int id)
    {
        try
        {
            var users = _customerService.getUsersByCustomer(id);
            return StatusCode(StatusCodes.Status200OK, users);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
}

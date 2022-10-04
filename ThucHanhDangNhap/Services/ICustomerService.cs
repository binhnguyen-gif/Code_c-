using ThucHanhDangNhap.Dtos.Customers;
using ThucHanhDangNhap.Dtos.Users;
using ThucHanhDangNhap.Models;

namespace ThucHanhDangNhap.Services;

public interface ICustomerService
{
    Customer CreateCustomer(CustomerDto customerDto);
    Customer UpdateCustomer(int id, CustomerDto customerDto);
    void DeleteCustomer(int id);
    List<Customer> FindAllCustomer();
    List<User> getUsersByCustomer(int id);
}
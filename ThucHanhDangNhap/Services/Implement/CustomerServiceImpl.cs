using ThucHanhDangNhap.DBContext;
using ThucHanhDangNhap.Dtos.Customers;
using ThucHanhDangNhap.ExceptionCustom;
using ThucHanhDangNhap.Models;

namespace ThucHanhDangNhap.Services.Implement;

public class CustomerServiceImpl : ICustomerService
{
    
    private readonly ApplicationDbContext _context;

    public CustomerServiceImpl(ApplicationDbContext context)
    {
        _context = context;
    }
    public Customer CreateCustomer(CustomerDto customerDto)
    {
        if (_context.Customers.Any(customer => customer.CCCD == customerDto.CCCD))
        {
            throw new FriendlyException($"Số CCCD: {customerDto.CCCD} đã tồn tại");
        }

        var customer = new Customer()
        {
            Fullname = customerDto.Fullname,
            Address = customerDto.Address,
            DateOfBirth = customerDto.DateOfBirth,
            CCCD = customerDto.CCCD
        };
        _context.Customers.Add(customer);
        _context.SaveChanges();
        return customer;
    }

    public Customer UpdateCustomer(int id, CustomerDto customerDto)
    {
        var customer = _context.Customers.FirstOrDefault(customer => customer.Id == id);
        if (customer == null)
        {
            throw new FriendlyException($"Customer id : {id} không tồn tại");
        }

        if (!customer.CCCD.Equals(customerDto.CCCD) && _context.Customers.All(c => c.CCCD == customerDto.CCCD))
        {
            throw new FriendlyException($"CCCd: {customerDto.CCCD} đã được sử dụng");
        }

        customer.Fullname = customerDto.Fullname;
        customer.Address = customerDto.Address;
        customer.DateOfBirth = customer.DateOfBirth;
        customer.CCCD = customer.CCCD;
        _context.SaveChanges();
        return customer;
    }

    public void DeleteCustomer(int id)
    {
        var customerFind = _context.Customers.FirstOrDefault(customer => customer.Id == id);
        if (customerFind == null)
        {
            throw new FriendlyException($"Customer Id: {id} không tồn tại");
        }

        _context.Users.RemoveRange(_context.Users.Where(u => u.CustomerId == id).ToList());
        _context.Customers.Remove(customerFind);
        _context.SaveChanges();
    }

    public List<Customer> FindAllCustomer()
    {
        return _context.Customers.ToList();
    }

    public List<User> getUsersByCustomer(int id)
    {
        if (_context.Users.All(customer => customer.Id == id))
        {
            throw new FriendlyException($"Customer id: {id} không tồn tại");
        }

        return _context.Users.Where(user => user.CustomerId == id).ToList();
    }
}
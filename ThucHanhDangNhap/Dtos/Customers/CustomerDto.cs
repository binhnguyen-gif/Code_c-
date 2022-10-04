using ThucHanhDangNhap.Models;

namespace ThucHanhDangNhap.Dtos.Customers;

public class CustomerDto
{
    public string Fullname { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string CCCD { get; set; }
    public string Address { get; set; }
}
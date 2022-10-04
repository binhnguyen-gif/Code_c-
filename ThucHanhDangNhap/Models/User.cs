namespace ThucHanhDangNhap.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public int UserType { get; set; }
    
    public int UserStatus { get; set; }

    public int? CustomerId { get; set; }
    public Customer? Customer { get; set; }
}
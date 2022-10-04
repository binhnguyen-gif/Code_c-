using System.ComponentModel.DataAnnotations;

namespace ThucHanhDangNhap.Dtos.Users;

public class UserDto
{
    [MinLength(5, ErrorMessage = "Username tối thiểu 5 kí tự")]
    [MaxLength(100, ErrorMessage = "")]
    public string Username { get; set; }

    [MinLength(5, ErrorMessage = "Password tối thiểu 5 kí tự")]
    public string Password { get; set; }

    [EmailAddress(ErrorMessage = "Sai định dạng email")]
    public string Email { get; set; }

    [MinLength(10, ErrorMessage = "Số điện thoại tối thiểu 10 số")]
    public string Phone { get; set; }

    [Range(1, 2, ErrorMessage = "UserType chỉ là 1 hoặc 2: 1 là Admin, 2 là Customer")]
    public int UserType { get; set; }
    
    public int? CustomerId { get; set; }
}
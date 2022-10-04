using System.ComponentModel.DataAnnotations;

namespace ThucHanhDangNhap.Dtos.Users;

public class ChangeStatusDto
{
    public string Username { get; set; }
    [Range(0,1, ErrorMessage = "UserStatus: 0-Blocked, 1-Unblocked")]
    public int UserStatus { get; set; }
}
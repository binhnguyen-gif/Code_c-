using System.Security.Cryptography;
using System.Text;

namespace ThucHanhDangNhap.Utils;

public class CommonUtils
{
    public static string CreateMD5(string input)
    {
        using var md5 = MD5.Create();
        var inputBytes = Encoding.Unicode.GetBytes(input);
        var hashBytes = md5.ComputeHash(inputBytes);
        return Convert.ToHexString(hashBytes);
    }
}
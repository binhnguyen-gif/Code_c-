namespace ThucHanhDangNhap.ExceptionCustom;

public class FriendlyException : Exception
{
    public FriendlyException(string? message) : base(message)
    {
    }
}
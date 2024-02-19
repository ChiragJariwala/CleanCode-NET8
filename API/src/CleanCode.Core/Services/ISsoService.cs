namespace CleanCode.Core.Services
{
    public interface ISsoService
    {
        Task<bool> ValidateToken(string token);
        Task<bool> Logout(string token);
    }
}

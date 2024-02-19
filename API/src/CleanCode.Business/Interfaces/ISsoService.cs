namespace CleanCode.Business.Interfaces
{
    public interface ISsoService
    {
        Task<bool> ValidateToken(string token);
        Task<bool> Logout(string token);
    }
}

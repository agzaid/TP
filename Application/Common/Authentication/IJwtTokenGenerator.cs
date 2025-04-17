namespace Application.Common.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(string role);
    }
}

using Data.Models;

namespace Api.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
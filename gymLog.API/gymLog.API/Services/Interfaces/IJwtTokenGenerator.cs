using gymLog.API.Model;

namespace gymLog.API.Services.Interfaces
{
    public interface IJwtTokenGenerator
    {
        /// <summary>
        /// Generates a JWT token for the specified user.
        /// </summary>
        /// <param name="user">The user for whom the token is generated.</param>
        /// <returns>A JWT token as a string.</returns>
        string GenerateToken(User user);
    }
}

using System.Security.Claims;
using System.Text;

namespace MoviesAdmin
{
    public static class ClaimsPrincipalExtensions
    {        
        public static string GetUserId(this ClaimsPrincipal user) =>
            user.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new InvalidOperationException("User is not authenticated");
        
    }
}

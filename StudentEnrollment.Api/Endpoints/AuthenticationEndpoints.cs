using Microsoft.AspNetCore.Identity;
using StudentEnrollment.Api.Dtos;
using StudentEnrollment.Data;

namespace StudentEnrollment.Api.Endpoints
{
    public static class AuthenticationEndpoints
    {
        public static void MapAuthenticationEndpoints(this IEndpointRouteBuilder routes)
        {
            routes.MapPost("/api/login/", async (LoginDto loginDto, UserManager<User> userManager) =>
            {
                var user = await userManager.FindByEmailAsync(loginDto.Username);
                if (user is null)
                {
                    return Results.Unauthorized();
                }
                bool isValidCredentials = await userManager.CheckPasswordAsync(user, loginDto.Password);

                if (!isValidCredentials)
                {
                    return Results.Unauthorized();
                }
                //generate token here

                return Results.Ok();
            })
                .WithTags("Authentication")
                .WithName("Login")
                .Produces(StatusCodes.Status200OK);
        }
    }
}

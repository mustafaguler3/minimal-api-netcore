using Microsoft.AspNetCore.Identity;
using StudentEnrollment.Api.Dtos;
using StudentEnrollment.Api.Services;
using StudentEnrollment.Data;

namespace StudentEnrollment.Api.Endpoints
{
    public static class AuthenticationEndpoints
    {
        public static void MapAuthenticationEndpoints(this IEndpointRouteBuilder routes)
        {
            routes.MapPost("/api/login/", async (LoginDto loginDto, IAuthManager authManager) =>
            {
                var response = authManager.Login(loginDto);
                //generate token here

                return Results.Ok();
            })
                .WithTags("Authentication")
                .WithName("Login")
                .Produces(StatusCodes.Status200OK);
        }
    }
}

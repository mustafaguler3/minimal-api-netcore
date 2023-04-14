using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using StudentEnrollment.Api.Dtos;
using StudentEnrollment.Api.Filters;
using StudentEnrollment.Api.Services;
using StudentEnrollment.Api.Validators;
using StudentEnrollment.Data;

namespace StudentEnrollment.Api.Endpoints
{
    public static class AuthenticationEndpoints
    {
        public static void MapAuthenticationEndpoints(this IEndpointRouteBuilder routes)
        {
            routes.MapPost("/api/login/", async (IValidator<LoginDto> validator,LoginDto loginDto, IAuthManager authManager) =>
            {
                /*var validationResult = await validator.ValidateAsync(loginDto);
                var errors = new List<ErrorResponseDto>();

                if (!validationResult.IsValid)
                {
                    return Results.BadRequest(validationResult.ToDictionary());
                }*/
                var response = await authManager.Login(loginDto);

                if (response is null)
                {
                    return Results.Unauthorized();
                }

                return Results.Ok(response);
            })
                .AddEndpointFilter<ValidationFilter<LoginDto>>()
                .AllowAnonymous()
                .WithTags("Authentication")
                .WithName("Login")
                .Produces(StatusCodes.Status200OK);


            routes.MapPost("/api/register/", async (RegisterDto registerDto, IAuthManager authManager) =>
            {
                var response = await authManager.Register(registerDto);

                if (!response.Any())
                {
                    return Results.Ok();
                }
                var errors = new List<ErrorResponseDto>();
                foreach (var error in errors)
                {
                    errors.Add(new ErrorResponseDto
                    {
                        Code = error.Code,
                        Description = error.Description,
                    });
                }
                return Results.BadRequest(errors);
            })
                .WithTags("Authentication")
                .WithName("Register")
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest);
        }
    }
}

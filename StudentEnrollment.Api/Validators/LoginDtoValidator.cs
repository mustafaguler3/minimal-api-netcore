using FluentValidation;
using StudentEnrollment.Api.Dtos;

namespace StudentEnrollment.Api.Validators
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x=>x.Username).NotEmpty().EmailAddress();

            RuleFor(x => x.Password).NotEmpty()
                .MinimumLength(6)
                .MaximumLength(20);
        }
    }
}

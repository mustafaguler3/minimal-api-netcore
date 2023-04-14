using FluentValidation;
using StudentEnrollment.Api.Dtos;

namespace StudentEnrollment.Api.Validators
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            Include(new LoginDtoValidator());

            RuleFor(x => x.FirstName)
                .NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();

            RuleFor(x => x.DateOfBirth).Must((dob) =>
            {
                if (dob.HasValue)
                {
                    return dob.Value < DateTime.Now;
                }
                return true;
            });
        }
    }
}

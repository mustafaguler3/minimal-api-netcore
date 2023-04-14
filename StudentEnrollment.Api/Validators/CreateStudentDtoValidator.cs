using FluentValidation;
using StudentEnrollment.Api.Dtos;

namespace StudentEnrollment.Api.Validators
{
    public class CreateStudentDtoValidator : AbstractValidator<CreateStudentDto>
    {
        public CreateStudentDtoValidator()
        {
            RuleFor(x=>x.FirstName).NotEmpty();
            RuleFor(x=>x.LastName).NotEmpty();
            RuleFor(x=>x.DateOfBirth)
                .LessThan(DateTime.Now).NotEmpty();
            RuleFor(x=>x.IdNumber).NotEmpty();
            RuleFor(x=>x.OriginalFileName).NotNull().When(x=>x.Picture != null);
        }
    }
}

using FluentValidation;
using StudentEnrollment.Api.Dtos;

namespace StudentEnrollment.Api.Validators
{
    public class StudentDtoValidator : AbstractValidator<StudentDto>
    {
        public StudentDtoValidator()
        {
            Include(new CreateStudentDtoValidator());
        }
    }
}

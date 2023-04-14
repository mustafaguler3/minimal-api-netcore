using FluentValidation;
using StudentEnrollment.Api.Dtos;
using StudentEnrollment.Data.Abstract;

namespace StudentEnrollment.Api.Validators
{
    public class EnrollmentDtoValidator : AbstractValidator<EnrollmentDto>
    {
        public EnrollmentDtoValidator(ICourseRepository courseRepository,IStudentRepository studentRepository)
        {
            Include(new CreateEnrollmentDtoValidator(courseRepository,studentRepository));
        }
    }
}

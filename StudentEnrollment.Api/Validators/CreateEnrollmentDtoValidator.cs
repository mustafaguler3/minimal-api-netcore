using FluentValidation;
using StudentEnrollment.Api.Dtos;
using StudentEnrollment.Data.Abstract;

namespace StudentEnrollment.Api.Validators
{
    public class CreateEnrollmentDtoValidator : AbstractValidator<CreateEnrollmentDto>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IStudentRepository _studentRepository;
        //CreateEnrollmentDto içindeki studentId ile courseId varmı yokmu sorgulamak için repository inject ediyoz
        public CreateEnrollmentDtoValidator(ICourseRepository courseRepository, IStudentRepository studentRepository)
        {
            _courseRepository = courseRepository;
            _studentRepository = studentRepository;

            RuleFor(x => x.CourseId).MustAsync(async (id, token) =>
            {
                var courseExists = await _courseRepository.Exists(id);
                return courseExists;
            }).WithMessage("{PropertyName} does not exist");

            RuleFor(x => x.StudentId).MustAsync(async (id, token) =>
            {
                var studentExists = await _studentRepository.Exists(id);
                return studentExists;
            }).WithMessage("{PropertyName} does not exist"); ;
        }
    }
}

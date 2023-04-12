using StudentEnrollment.Data;

namespace StudentEnrollment.Api.Dtos
{
    public class StudentDetailsDto : CreateStudentDto
    {
        public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public List<Course> Courses { get; set; } = new List<Course>();
    }
}

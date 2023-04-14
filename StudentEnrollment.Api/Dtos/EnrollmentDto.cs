namespace StudentEnrollment.Api.Dtos
{
    public class EnrollmentDto : CreateEnrollmentDto
    {
        public int CourseId { get; set; }
        public int StudentId { get; set; }

        public virtual CourseDto Course { get; set; }
        public virtual StudentDto Student { get; set; }
    }
}

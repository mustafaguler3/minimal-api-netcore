namespace StudentEnrollment.Api.Dtos
{
    public class CourseDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        public List<StudentDto> Students { get; set; } = new List<StudentDto>();
    }
}

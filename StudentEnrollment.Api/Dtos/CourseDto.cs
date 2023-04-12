namespace StudentEnrollment.Api.Dtos
{
    public class CourseDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Name { get; set; }

        public int Credits { get; set; }
    }
    public class CreateCourseDto
    {
        public string Title { get; set; }

        public int Credits { get; set; }
    }
}

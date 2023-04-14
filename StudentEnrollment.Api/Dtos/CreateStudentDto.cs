namespace StudentEnrollment.Api.Dtos
{
    public class CreateStudentDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string IdNumber { get; set; }
        public byte[] Picture { get; set; }
        public string OriginalFileName { get; set; }
    }
}

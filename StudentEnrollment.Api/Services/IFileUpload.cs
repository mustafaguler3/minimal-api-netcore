namespace StudentEnrollment.Api.Services
{
    public interface IFileUpload
    {
        string UploadFile(byte[] file,string imageName);
    }
}

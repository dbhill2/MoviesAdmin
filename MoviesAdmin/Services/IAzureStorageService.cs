namespace MoviesAdmin.Services
{
    public interface IAzureStorageService
    {
        //Service to upload to azure storage
        //return: the URL of the uploaded file as a string
        public Task<string> UploadFileAsync(IFormFile file, string fileName);
    }
}

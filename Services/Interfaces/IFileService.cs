namespace ApiWebKut.Services.Interfaces
{
    public interface IFileService
    {
        Task<string> SaveImageAsync(IFormFile imageFile);
    }
}

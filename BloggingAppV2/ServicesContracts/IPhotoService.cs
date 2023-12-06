using CloudinaryDotNet.Actions;

namespace BloggingApp.Web.ServicesContracts;

public interface IPhotoService
{
    Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
    Task<DeletionResult> DeletePhotoAsync(string publicId);
}
using testCleintPart.Models;

namespace testCleintPart.Contracts
{
    public interface IImageApiService
    {
        Task<List<ImageData>> GetAllImages();
        Task<bool> SendImage(ImageData imageData);
        Task<bool> EditImage(ImageData imageData);
        Task<bool> DeleteImage(string imageDataPath);
    }
}

using Microsoft.AspNetCore.Http;

namespace WebCozyShop.Services.Interface
{
    public interface ICloudinaryService
    {
        Task<string?> UploadImageAsync(IFormFile file, string folder = "product-variants");
        Task<bool> DeleteImageAsync(string publicId);
    }
}
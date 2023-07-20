using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using JosephKhaipi.Web.Data;

namespace JosephKhaipi.Web.Repositories
{
    public class CloudinaryImageRepository : IImageRepository
    {
        private readonly IConfiguration config;
        private readonly Account account;

        public CloudinaryImageRepository(IConfiguration config)
        {
            this.config = config;
            account = new Account(
                config.GetSection("Cloudinary")["CloudName"],
                config.GetSection("Cloudinary")["ApiKey"],
                config.GetSection("Cloudinary")["ApiSecret"]
                );
        }

        public async Task<string> UploadAsync(IFormFile file)
        {
            var client = new Cloudinary(account);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                DisplayName = file.FileName
            };
            var uploadResult = await client.UploadAsync(uploadParams);

            if(uploadResult != null && uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return uploadResult.SecureUri.ToString();
            }

            return null;
        }
    }
}

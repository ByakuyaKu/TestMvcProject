using TestMvcProject.Data;
using TestMvcProject.Models;
using TestMvcProject.Repository.Interfaces;

namespace TestMvcProject.Repository
{
    public class ImageRepository : Repository<Image>, IImageRepository
    {
        public ImageRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
        /// <summary>
        /// IFormFile to Image.
        /// </summary>
        public Image GetImg(IFormFile avatar, string fileName)
        {
            byte[] imageData;
            using (var binaryReader = new BinaryReader(avatar.OpenReadStream()))
            {
                imageData = binaryReader.ReadBytes((int)avatar.Length);
            }
            var img = new Image();
            img.Data = imageData;
            img.Name = fileName;
            return img;
        }
    }
}

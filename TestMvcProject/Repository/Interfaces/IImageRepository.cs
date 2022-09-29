using TestMvcProject.Models;

namespace TestMvcProject.Repository.Interfaces
{
    public interface IImageRepository : IRepository<Image>
    {
        /// <summary>
        /// IFormFile to Image.
        /// </summary>
        public Image GetImg(IFormFile avatar, string fileName);
    }
}

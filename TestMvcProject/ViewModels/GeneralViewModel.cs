using TestMvcProject.Models;

namespace TestMvcProject.ViewModels
{
    public class GeneralViewModel
    {
        public IEnumerable<Anime>? Animies { get; set; } = new List<Anime>();
        public IEnumerable<Manga>? Mangas { get; set; } = new List<Manga>();
        public IEnumerable<Author>? Authors { get; set; } = new List<Author>();
        public IEnumerable<Position>? Positions { get; set; } = new List<Position>();
        public IFormFile? Avatar { get; set; }
        public Image GetImg(IFormFile avatar, string fileName)
        {
            byte[] imageData;
            // считываем переданный файл в массив байтов
            using (var binaryReader = new BinaryReader(avatar.OpenReadStream()))
            {
                imageData = binaryReader.ReadBytes((int)avatar.Length);
            }
            // установка массива байтов
            var img = new Image();
            img.Data = imageData;
            //img.Name = "AvatarOf" + manga.Tittle;
            img.Name = fileName;
            return img;
        }
    }
}

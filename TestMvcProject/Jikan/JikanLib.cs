using JikanDotNet;
using Microsoft.EntityFrameworkCore;
using System.Net;
using TestMvcProject.Data;
using TestMvcProject.Models;
using Genre = TestMvcProject.Models.Genre;
using Image = TestMvcProject.Models.Image;

namespace TestMvcProject.Jikan
{
    public class JikanLib
    {
        protected readonly IJikan _jikan;
        protected readonly AppDbContext _appDbContext;

        public List<Genre> Genres { get; set; } = new List<Genre>();
        public List<Genre> NewGenres { get; set; } = new List<Genre>();

        public JikanLib(AppDbContext appDbContext, IJikan jikan)
        {
            _jikan = jikan;
            _appDbContext = appDbContext;
        }

        public Image GetImgData(JikanDotNet.ImagesSet imagesSet, string imgName)
        {
            var image = new Image();
            using (WebClient webClient = new WebClient())
            {
                image.Data = webClient.DownloadData(imagesSet.JPG.ImageUrl);
            }
            image.Name = imgName;

            return image;
        }

        public List<Genre> GetGenres(ICollection<MalUrl> genres)
        {
            var _genres = new List<Genre>();

            for (int i = 0; i < genres.Count; i++)
            {
                if (Genres.Exists(x => x.Name == genres.ElementAt(i).Name))
                {
                    _genres.Add(Genres.FirstOrDefault(x => x.Name == genres.ElementAt(i).Name));
                }
                else
                {
                    var curGenre = new Genre();
                    curGenre.Name = genres.ElementAt(i).Name;
                    Genres.Add(curGenre);
                    NewGenres.Add(curGenre);
                    _genres.Add(curGenre);
                }
            }

            return _genres;
        }
    }
}

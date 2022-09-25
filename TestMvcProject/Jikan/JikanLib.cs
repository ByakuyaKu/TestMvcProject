using JikanDotNet;
using System.Net;
using TestMvcProject.Data;
using TestMvcProject.Models;
using Anime = TestMvcProject.Models.Anime;
using Genre = TestMvcProject.Models.Genre;
using Image = TestMvcProject.Models.Image;

namespace TestMvcProject.Jikan
{
    /// <summary>
    /// JikanLib is class that contain logic for libs.
    /// </summary>
    public class JikanLib
    {
        protected readonly IJikan _jikan;
        protected readonly AppDbContext _appDbContext;

        public JikanLib(AppDbContext appDbContext, IJikan jikan)
        {
            _jikan = jikan;
            _appDbContext = appDbContext;
        }
        /// <summary>
        /// Get Image from JikanDotNet.ImagesSet.
        /// </summary>
        protected Image GetImgData(JikanDotNet.ImagesSet imagesSet, string imgName)
        {
            var image = new Image();
            using (WebClient webClient = new WebClient())
            {
                image.Data = webClient.DownloadData(imagesSet.JPG.ImageUrl);
            }
            image.Name = imgName;

            return image;
        }
        /// <summary>
        /// Get Genres from ICollection<MalUrl>.
        /// </summary>
        protected List<Genre> GetGenres(ICollection<MalUrl> genres, List<Genre> currentGenres, List<Genre> newGenres)
        {
            var _genres = new List<Genre>();

            for (int i = 0; i < genres.Count; i++)
            {
                var searchedGenre = currentGenres.FirstOrDefault(g => g.Name == genres.ElementAt(i).Name);
                if (searchedGenre != null)
                {
                    _genres.Add(currentGenres.FirstOrDefault(g => g.Name == genres.ElementAt(i).Name));
                }
                else
                {
                    var curGenre = new Genre();
                    curGenre.Name = genres.ElementAt(i).Name;
                    currentGenres.Add(curGenre);
                    newGenres.Add(curGenre);
                    _genres.Add(curGenre);
                }
            }

            return _genres;
        }
        /// <summary>
        /// Add position for author of anime.
        /// </summary>
        protected async Task<Author> AddPositionToAuthor(Author author, Anime anime, List<Position> positions, List<Position> newPositions,
            ICollection<string> _positions)
        {
            for (int i = 0; i < _positions.Count; i++)
            {
                var searchedPosition = positions.FirstOrDefault(p => p.Name == _positions.ElementAt(i));
                if (searchedPosition != null)
                {
                    //if (!searchedPosition.Anime.Exists(a => a.Id == anime.Id))
                    //{
                    //    searchedPosition.Anime.Add(anime);
                    //}

                    author.Positions?.Add(searchedPosition);
                }
                else
                {
                    var curPosition = new Position();
                    curPosition.Name = _positions.ElementAt(i);
                    //curPosition.Anime?.Add(anime);

                    positions.Add(curPosition);
                    newPositions.Add(curPosition);

                    author.Positions?.Add(curPosition);
                }
            }

            return author;
        }

    }
}

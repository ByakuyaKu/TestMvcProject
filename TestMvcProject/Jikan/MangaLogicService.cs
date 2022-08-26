using JikanDotNet;
using System.Net;
using TestMvcProject.Data;
using TestMvcProject.Models;
using Genre = TestMvcProject.Models.Genre;
using Image = TestMvcProject.Models.Image;
using Manga = TestMvcProject.Models.Manga;

namespace TestMvcProject.Jikan
{
    public class MangaLogicService : IMangaLogicService
    {
        private readonly IJikan _jikan;
        private readonly AppDbContext _appDbContext;

        public MangaLogicService(AppDbContext appDbContext,IJikan jikan)
        {
            _jikan = jikan;
            _appDbContext = appDbContext;
        }

        public List<Genre> Genres { get; set; } = new List<Genre>();
        //public List<Manga> mangasTop { get; set; } = new List<Manga>();


        public async Task<List<Manga>> GetTopManga()
        //public static async Task<Manga[]> GetTopManga()
        {
            var _mangasTop = await _jikan.GetTopMangaAsync();
            var mangasTop = new List<Manga>();
            //var mangasTop = new Manga[_mangasTop.Data.Count];
            for (int i = 0; i < _mangasTop.Data.Count; i++)
            {
                mangasTop.Add(GetMangaFromJikanManga(_mangasTop.Data.ElementAt(i)));
                //mangasTop[i] GetMangaFromJikanManga(_mangasTop.Data.ElementAt(i));
                //GetMangaFromJikanManga(_mangasTop.Data.ElementAt(i));
            }

            _appDbContext.AddRange(Genres);
            await _appDbContext.SaveChangesAsync();
            _appDbContext.AddRange(mangasTop);
            await _appDbContext.SaveChangesAsync();

            return mangasTop;
        }

        public async Task<Manga> GetMangaAsync(long id)
        {
            var _manga = await _jikan.GetMangaAsync(id);

            var manga = GetMangaFromJikanManga(_manga.Data);

            return manga;
        }



        private Manga GetMangaFromJikanManga(JikanDotNet.Manga manga)
        {
            var _manga = new Manga();
            _manga.Tittle = manga.Title;
            _manga.TitleJapanese = manga.TitleJapanese;
            _manga.Description = manga.Synopsis;
            //_manga.Duration = manga.;
            _manga.Favorites = manga.Favorites;
            _manga.Score = manga.Score;
            _manga.ScoredBy = manga.ScoredBy;
            _manga.Volumes = manga.Volumes;
            _manga.Popularity = manga.Popularity;
            _manga.Rank = manga.Rank;
            _manga.Status = manga.Status;
            _manga.Type = manga.Type;

            var img = GetImgData(manga.Images, "PoseterOf" + _manga.Tittle);
            if (img != null)
                _manga.Images?.Add(img);

            _manga.MangaStarts = manga.Published.From;
            _manga.MangaEnds = manga.Published.To;
            _manga.ChaptersCount = manga.Chapters;

            var gnrs = GetGenres(manga.Genres);
            if (gnrs.Count > 1)
                _manga.Genres?.AddRange(gnrs);
            //mangasTop.Add(_manga);
            return _manga;
        }

        private Image GetImgData(JikanDotNet.ImagesSet imagesSet, string imgName)
        {
            var image = new Image();
            using (WebClient webClient = new WebClient())
            {
                image.Data = webClient.DownloadData(imagesSet.JPG.ImageUrl);
            }
            image.Name = imgName;

            return image;
        }

        private List<Genre> GetGenres(ICollection<MalUrl> genres)
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
                    _genres.Add(curGenre);
                }
            }

            return _genres;
        }
    }
}

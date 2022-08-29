using JikanDotNet;
using Microsoft.EntityFrameworkCore;
using System.Net;
using TestMvcProject.Data;
using TestMvcProject.Models;
using Genre = TestMvcProject.Models.Genre;
using Image = TestMvcProject.Models.Image;
using Manga = TestMvcProject.Models.Manga;

namespace TestMvcProject.Jikan
{
    public class MangaLib : JikanLib, IMangaLib
    {
        public MangaLib(AppDbContext appDbContext, IJikan jikan) : base(appDbContext, jikan)
        {
        }

        public async Task<List<Manga>> GetTopMangaAsync()
        {
            Genres = await _appDbContext.Genres.ToListAsync();
            var _mangasTop = await _jikan.GetTopMangaAsync();
            var mangasTop = new List<Manga>();
            for (int i = 0; i < _mangasTop.Data.Count; i++)
            {
                mangasTop.Add(GetMangaFromJikanManga(_mangasTop.Data.ElementAt(i)));
            }

            if (NewGenres != null && NewGenres.Count > 0)
            {
                _appDbContext.Genres.AddRange(NewGenres);
                await _appDbContext.SaveChangesAsync();
            }

            _appDbContext.AddRange(mangasTop);
            await _appDbContext.SaveChangesAsync();

            NewGenres?.Clear();

            return mangasTop;
        }

        public async Task<Manga> GetMangaAsync(long id)
        {
            Genres = await _appDbContext.Genres.ToListAsync();
            var _manga = await _jikan.GetMangaAsync(id);

            var manga = GetMangaFromJikanManga(_manga.Data);

            if (NewGenres != null && NewGenres.Count > 0)
            {
                _appDbContext.Genres.AddRange(NewGenres);
                await _appDbContext.SaveChangesAsync();
            }

            _appDbContext.Add(manga);
            await _appDbContext.SaveChangesAsync();

            NewGenres?.Clear();

            return manga;
        }


        private Manga GetMangaFromJikanManga(JikanDotNet.Manga manga)
        {
            var _manga = new Manga();
            _manga.Tittle = manga.Title;
            _manga.TitleJapanese = manga.TitleJapanese;
            _manga.Description = manga.Synopsis;
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
            if (gnrs.Count > 0)
                _manga.Genres?.AddRange(gnrs);
            return _manga;
        }
    }
}

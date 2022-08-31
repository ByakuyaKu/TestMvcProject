using JikanDotNet;
using Microsoft.EntityFrameworkCore;
using System.Net;
using TestMvcProject.Data;
using TestMvcProject.Jikan.Interfaces;
using TestMvcProject.Models;
using Genre = TestMvcProject.Models.Genre;
using Image = TestMvcProject.Models.Image;
using Manga = TestMvcProject.Models.Manga;

namespace TestMvcProject.Jikan.Libs
{
    public class MangaLib : JikanLib, IMangaLib
    {
        public MangaLib(AppDbContext appDbContext, IJikan jikan) : base(appDbContext, jikan)
        {
        }

        public async Task<List<Manga>> GetTopMangaAsync()
        {
            //var a = await _jikan.GetPersonAsync(2836);
            var currentAuthors = await _appDbContext.Authors.Include(x => x.Manga).ToListAsync();
            var newAuthors = new List<Author>();

            //var currentPositions = await _appDbContext.Positions.Include(x => x.ma).ToListAsync();
            //var newPositions = new List<Position>();
            //var pos = new Position();
            //pos.Name = "Mangaka";
            //pos.Description = "Manga author";

            var currentGenres = await _appDbContext.Genres.ToListAsync();
            var newGenres = new List<Genre>();

            var _mangasTop = await _jikan.GetTopMangaAsync();
            var mangasTop = new List<Manga>();

            for (int i = 0; i < _mangasTop.Data.Count; i++)
            {
                var curManga = await GetMangaFromJikanMangaAsync(_mangasTop.Data.ElementAt(i), 
                    currentGenres, newGenres,
                    currentAuthors, newAuthors);
                mangasTop.Add(curManga);
            }
            
            if (newAuthors != null && newAuthors.Count > 0)
            {
                _appDbContext.Authors.AddRange(newAuthors);
                await _appDbContext.SaveChangesAsync();
            }

            if (newGenres != null && newGenres.Count > 0)
            {
                _appDbContext.Genres.AddRange(newGenres);
                await _appDbContext.SaveChangesAsync();
            }

            _appDbContext.AddRange(mangasTop);
            await _appDbContext.SaveChangesAsync();

            return mangasTop;
        }

        public async Task<Manga> GetMangaAsync(long id)
        {
            var currentAuthors = await _appDbContext.Authors.Include(x => x.Manga).ToListAsync();
            var newAuthors = new List<Author>();

            //var currentPositions = await _appDbContext.Positions.Include(x => x.Animies).ToListAsync();
            //var newPositions = new List<Position>();

            var currentGenres = await _appDbContext.Genres.ToListAsync();
            var newGenres = new List<Genre>();

            var _manga = await _jikan.GetMangaAsync(id);

            var manga = await GetMangaFromJikanMangaAsync(_manga.Data,
                currentGenres, newGenres,
                currentAuthors, newAuthors);

            if (newAuthors != null && newAuthors.Count > 0)
            {
                _appDbContext.Authors.AddRange(newAuthors);
                await _appDbContext.SaveChangesAsync();
            }

            if (newGenres != null && newGenres.Count > 0)
            {
                _appDbContext.Genres.AddRange(newGenres);
                await _appDbContext.SaveChangesAsync();
            }

            _appDbContext.Add(manga);
            await _appDbContext.SaveChangesAsync();

            return manga;
        }

        private async Task<Author> GetMangaAuthorAsync(long id, List<Author> currentAuthors, List<Author> newAuthors, Manga manga, MalUrl malUrl)
        {
            var searchAuthor = currentAuthors.FirstOrDefault(a => a.MalId == id);

            if (searchAuthor == null)
            {
                //var author = await _jikan.GetPersonAsync(id);
                var newAuthor = new Author();
                //newAuthor.About = author.Data.About;
                newAuthor.FirstName = malUrl.Name;
                //newAuthor.FirstName = author.Data.Name;
                //newAuthor.LastName = author.Data.FamilyName;
                //newAuthor.DateOfBirth = author.Data.Birthday;
                //newAuthor.MemberFavorites = author.Data.MemberFavorites;
                //newAuthor.WebsiteUrl = author.Data.WebsiteUrl;
                //newAuthor.Manga?.Add(manga);
                newAuthor.MalId = id;

                //var img = GetImgData(author.Data.Images, "PoseterOf" + author.Data.Name);
                //if (img != null)
                //    newAuthor.Images?.Add(img);

                currentAuthors.Add(newAuthor);
                newAuthors.Add(newAuthor);

                return newAuthor;
            }
            else
            {
                //searchAuthor.Manga?.Add(manga);

                return searchAuthor;
            }
        }

        private async Task<Manga> GetMangaFromJikanMangaAsync(JikanDotNet.Manga manga,
            List<Genre> currentGenres, List<Genre> newGenres,
            List<Author> currentAuthors, List<Author> newAuthors)
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

            var gnrs = GetGenres(manga.Genres, currentGenres, newGenres);
            if (gnrs.Count > 0)
                _manga.Genres?.AddRange(gnrs);

            for (int i = 0; i < manga.Authors.Count; i++)
            {
                var curAuthor = await GetMangaAuthorAsync(manga.Authors.ElementAt(i).MalId, currentAuthors, newAuthors, _manga, manga.Authors.ElementAt(i));
                _manga.Authors?.Add(curAuthor);
            }

            return _manga;
        }
    }
}

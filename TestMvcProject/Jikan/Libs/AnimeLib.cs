using JikanDotNet;
using Microsoft.EntityFrameworkCore;
using TestMvcProject.Data;
using TestMvcProject.Jikan.Interfaces;
using TestMvcProject.Models;
using Anime = TestMvcProject.Models.Anime;
using Genre = TestMvcProject.Models.Genre;
using Image = TestMvcProject.Models.Image;

namespace TestMvcProject.Jikan.Libs
{
    public class AnimeLib : JikanLib, IAnimeLib
    {
        public AnimeLib(AppDbContext appDbContext, IJikan jikan) : base(appDbContext, jikan)
        {
        }

        public async Task<List<Anime>> GetTopAnimeAsync()
        {
            var currentAuthors = await _appDbContext.Authors.Include(x=>x.Positions).ToListAsync();
            var newAuthors = new List<Author>();

            var currentPositions = await _appDbContext.Positions.Include(x=>x.Anime).ToListAsync();
            var newPositions = new List<Position>();

            var currentGenres = await _appDbContext.Genres.ToListAsync();
            var newGenres = new List<Genre>();

            var _animeTop = await _jikan.GetTopAnimeAsync();
            var animeTop = new List<Anime>();

            for (int i = 0; i < _animeTop.Data.Count; i++)
            {
                var curAnime = await GetAnimeFromJikanAnime(_animeTop.Data.ElementAt(i), 
                    currentAuthors, newAuthors, 
                    currentPositions, newPositions,
                    currentGenres, newGenres);

                animeTop.Add(curAnime);
            }

            if (newPositions != null && newPositions.Count > 0)
            {
                _appDbContext.Positions.AddRange(newPositions);
                await _appDbContext.SaveChangesAsync();
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

            _appDbContext.AddRange(animeTop);
            await _appDbContext.SaveChangesAsync();

            return animeTop;
        }

        public async Task<Anime> GetAnimeAsync(long id)
        {
            var currentAuthors = await _appDbContext.Authors.Include(x => x.Positions).ToListAsync();
            var newAuthors = new List<Author>();

            var currentPositions = await _appDbContext.Positions.Include(x => x.Anime).ToListAsync();
            var newPositions = new List<Position>();

            var currentGenres = await _appDbContext.Genres.ToListAsync();
            var newGenres = new List<Genre>();

            var _anime = await _jikan.GetAnimeAsync(id);

            var anime = await GetAnimeFromJikanAnime(_anime.Data,
                    currentAuthors, newAuthors,
                    currentPositions, newPositions,
                    currentGenres, newGenres);

            if (newPositions != null && newPositions.Count > 0)
            {
                _appDbContext.Positions.AddRange(newPositions);
                await _appDbContext.SaveChangesAsync();
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

            _appDbContext.Add(anime);
            await _appDbContext.SaveChangesAsync();

            return anime;
        }

        private async Task<List<Author>> GetAnimeStaffAsync(long id, 
            List<Author> currentAuthors, List<Author> newAuthors, 
            List<Position> currentPositions, List<Position> newPositions, 
            Anime anime)
        {
            var staff = await _jikan.GetAnimeStaffAsync(id);

            var _staff = new List<Author>();

            for (int i = 0; i < staff.Data.Count; i++)
            {
                var author = currentAuthors.FirstOrDefault(x => x.MalId == staff.Data.ElementAt(i).Person.MalId);
                if (author != null)
                {
                    author = await AddPositionToAuthor(author, anime, currentPositions, newPositions, staff.Data.ElementAt(i).Position);
                    _staff.Add(author);
                }
                else
                {
                    var newAuthor = new Author();
                    newAuthor.FirstName = staff.Data.ElementAt(i).Person.Name;
                    newAuthor.MalId = staff.Data.ElementAt(i).Person.MalId;
                    var img = GetImgData(staff.Data.ElementAt(i).Person.Images, "PoseterOf" + newAuthor.FirstName);
                    if (img != null)
                        newAuthor.Images?.Add(img); 

                    newAuthor = await AddPositionToAuthor(newAuthor, anime, currentPositions, newPositions, staff.Data.ElementAt(i).Position);

                    currentAuthors.Add(newAuthor);
                    newAuthors.Add(newAuthor);
                    _staff.Add(newAuthor);
                }
            }

            return _staff;
        }


        private async Task<Anime> GetAnimeFromJikanAnime(JikanDotNet.Anime anime,
            List<Author> currentAuthors, List<Author> newAuthors,
            List<Position> currentPositions, List<Position> newPositions,
            List<Genre> currentGenres, List<Genre> newGenres)
        {
            var _anime = new Anime();

            _anime.Tittle = anime.Title;
            _anime.TitleJapanese = anime.TitleJapanese;
            _anime.Description = anime.Synopsis;
            _anime.Duration = anime.Duration;
            _anime.Favorites = anime.Favorites;
            _anime.Score = anime.Score;
            _anime.ScoredBy = anime.ScoredBy;
            _anime.Popularity = anime.Popularity;
            _anime.Rank = anime.Rank;
            _anime.Status = anime.Status;
            _anime.Type = anime.Type;

            var img = GetImgData(anime.Images, "PoseterOf" + _anime.Tittle);
            if (img != null)
                _anime.Images?.Add(img);

            _anime.AnimeStarts = anime.Aired.From;
            _anime.AnimeEnds = anime.Aired.To;
            _anime.SeriesCount = anime.Episodes;

            var gnrs = GetGenres(anime.Genres, currentGenres, newGenres);
            if (gnrs.Count > 0)
                _anime.Genres?.AddRange(gnrs);

            _anime.Authors = await GetAnimeStaffAsync((long)anime.MalId, currentAuthors, newAuthors, currentPositions, newPositions, _anime);

            return _anime;
        }

    }
}

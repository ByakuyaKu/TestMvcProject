using JikanDotNet;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using TestMvcProject.Data;
using TestMvcProject.Models;
using Anime = TestMvcProject.Models.Anime;
using Image = TestMvcProject.Models.Image;

namespace TestMvcProject.Jikan
{
    public class AnimeLib : JikanLib, IAnimeLib
    {
        public AnimeLib(AppDbContext appDbContext, IJikan jikan) : base(appDbContext, jikan)
        {
        }

        public async Task<List<Anime>> GetTopAnimeAsync()
        {
            Genres = await _appDbContext.Genres.ToListAsync();
            var _animiesTop = await _jikan.GetTopAnimeAsync();
            var animiesTop = new List<Anime>();
            for (int i = 0; i < _animiesTop.Data.Count; i++)
            {
                animiesTop.Add(GetAnimeFromJikanAnime(_animiesTop.Data.ElementAt(i)));
            }

            if (NewGenres != null && NewGenres.Count > 0)
            {
                _appDbContext.Genres.AddRange(NewGenres);
                await _appDbContext.SaveChangesAsync();
            }

            _appDbContext.AddRange(animiesTop);
            await _appDbContext.SaveChangesAsync();

            NewGenres?.Clear();

            return animiesTop;
        }

        public async Task<Anime> GetAnimeAsync(long id)
        {
            Genres = await _appDbContext.Genres.ToListAsync();

            var _anime = await _jikan.GetAnimeAsync(id);

            var anime = GetAnimeFromJikanAnime(_anime.Data);

            if (NewGenres != null && NewGenres.Count > 0)
            {
                _appDbContext.Genres.AddRange(NewGenres);
                await _appDbContext.SaveChangesAsync();
            }

            _appDbContext.Add(anime);
            await _appDbContext.SaveChangesAsync();

            NewGenres?.Clear();

            return anime;
        }

        private Anime GetAnimeFromJikanAnime(JikanDotNet.Anime anime)
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

            var gnrs = GetGenres(anime.Genres);
            if (gnrs.Count > 0)
                _anime.Genres?.AddRange(gnrs);

            return _anime;
        }

    }
}

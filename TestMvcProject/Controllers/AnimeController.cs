using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TestMvcProject.Data;
using TestMvcProject.Jikan.Interfaces;
using TestMvcProject.Jikan.Libs;
using TestMvcProject.Repository.Interfaces;
using TestMvcProject.ViewHelperLib;
using Anime = TestMvcProject.Models.Anime;

namespace TestMvcProject.Controllers
{
    public class AnimeController : Controller
    {
        private readonly IAnimeRepository _animeRepository;
        private readonly IMangaRepository _mangaRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IImageRepository _imageRepository;

        public AnimeController(IAnimeRepository animeRepository,
            IMangaRepository mangaRepository,
            IGenreRepository genreRepository,
            IAuthorRepository authorRepository,
            IImageRepository imageRepository)
        {
            _animeRepository = animeRepository;
            _mangaRepository = mangaRepository;
            _genreRepository = genreRepository;
            _authorRepository = authorRepository;
            _imageRepository = imageRepository;
        }
        // GET: Anime
        public async Task<IActionResult> Index(string? sortOrder, string? searchString, string currentFilter, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentSortView"] = sortOrder?.Replace("_", " ").ToLower();

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            IEnumerable<Anime> AnimeList = await _animeRepository.FillAnimeListAsync(searchString);

            AnimeList = _animeRepository.SortAnime(sortOrder, AnimeList);

            int pageSize = 10;
            return View(await PaginatedList<Anime>.CreateAsync(AnimeList, pageNumber ?? 1, pageSize));
        }

        // GET: Anime
        public async Task<IActionResult> IndexUneditable(string? sortOrder, string? searchString, string currentFilter, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentSortView"] = sortOrder?.Replace("_", " ").ToLower();

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            IEnumerable<Anime> AnimeList = await _animeRepository.FillAnimeListAsync(searchString);

            AnimeList = _animeRepository.SortAnime(sortOrder, AnimeList);

            int pageSize = 10;
            return View(await PaginatedList<Anime>.CreateAsync(AnimeList, pageNumber ?? 1, pageSize));
        }

        // GET: Anime/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || id == Guid.Empty)
                return NotFound();

            var anime = await _animeRepository.GetAnimeByIdFullInfo((Guid)id, true);

            if (anime == null)
                return NotFound();

            await _authorRepository.SearchAuthorsImagesAsync(anime.Authors);
            await _mangaRepository.SearchMangasImagesAsync(anime.Manga);

            if (anime.Images != null && anime.Images.Count > 0)
                ViewBag.Poster = string.Format("data:image/png;base64,{0}", (Convert.ToBase64String(anime.Images.Last().Data)));

            return View(anime);
        }

        // GET: Anime/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.AuthorList = await _authorRepository.FillViewBagAuthorListAsync();
            ViewBag.MangaList = await _mangaRepository.FillViewBagMangaListAsync();
            ViewBag.GenreList = await _genreRepository.FillViewBagGenreListAsync();
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Anime anime)
        {
            if (!ModelState.IsValid)
                return View(anime);

            var mangas = _mangaRepository.GetAll();
            var authors = _authorRepository.GetAll();
            var genres = _genreRepository.GetAll();

            if (anime.Avatar != null)
            {
                var img = _imageRepository.GetImg(anime.Avatar, "AvatarOf" + anime.Tittle);
                anime.Images?.Add(img);
            }

            if (anime.MangaIdList != null && anime.MangaIdList.Count > 0)
                anime.Manga?.AddRange(mangas.Where(a => anime.MangaIdList.Any(m => m == a.Id)).ToList());

            if (anime.AuthorIdList != null && anime.AuthorIdList.Count > 0)
                anime.Authors?.AddRange(authors.Where(a => anime.AuthorIdList.Any(m => m == a.Id)).ToList());

            if (anime.GenreIdList != null && anime.GenreIdList.Count > 0)
                anime.Genres?.AddRange(genres.Where(g => anime.GenreIdList.Any(m => m == g.Id)).ToList());

            _animeRepository.Add(anime);
            await _animeRepository.SaveChangesAsync();

            TempData["success"] = "Anime created successfully!";

            //return RedirectToAction("AddPositionToAuthor", "Author", new { animeId = anime.Id});
            return RedirectToAction("Index");
        }

        // GET: Anime/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == Guid.Empty || id == null)
                return NotFound();

            var anime = await _animeRepository.GetAnimeByIdFullInfo((Guid)id, false);

            if (anime == null)
                return NotFound();

            ViewBag.MangaList = await _mangaRepository.FillViewBagMangaListAsync();
            ViewBag.AuthorList = await _authorRepository.FillViewBagAuthorListAsync();
            ViewBag.GenreList = await _genreRepository.FillViewBagGenreListAsync();

            if (anime.Images != null && anime.Images.Count > 0)
                ViewBag.Poster = string.Format("data:image/png;base64,{0}", (Convert.ToBase64String(anime.Images.Last().Data)));

            return View(anime);
        }

        // POST: Anime/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(Guid? id)
        {
            if (id == Guid.Empty || id == null)
                return NotFound();

            var anime = await _animeRepository.GetAnimeByIdWithImages((Guid)id, false);

            if (anime == null)
                return NotFound();

            if (anime.Images != null && anime.Images.Count > 0)
            {
                _imageRepository.RemoveRange(anime.Images);
                await _imageRepository.SaveChangesAsync();
            }

            _animeRepository.Remove(anime);
            await _animeRepository.SaveChangesAsync();

            TempData["success"] = "Anime deleted successfully!";

            return RedirectToAction("Index");
        }

        // GET: Anime/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || Guid.Empty == id)
                return NotFound();

            var anime = await _animeRepository.GetAnimeByIdFullInfo((Guid)id, false);

            if (anime == null)
                return NotFound();

            ViewBag.MangaList = await _mangaRepository.FillViewBagMangaListAsync();
            ViewBag.AuthorList = await _authorRepository.FillViewBagAuthorListAsync();
            ViewBag.GenreList = await _genreRepository.FillViewBagGenreListAsync();

            return View(anime);
        }

        // POST: Anime/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Anime anime)
        {
            if (anime == null)
                return NotFound();

            if (!ModelState.IsValid)
                return View(anime);

            _animeRepository.Attach(anime);
            await _animeRepository.LoadRealatedMangaAsync(anime);
            await _animeRepository.LoadRealatedAuthorsAsync(anime);
            await _animeRepository.LoadRealatedGenresAsync(anime);

            if (anime.Avatar != null)
            {
                var img = _imageRepository.GetImg(anime.Avatar, "PosterOf" + anime.Tittle);

                _imageRepository.Add(img);
                await _imageRepository.SaveChangesAsync();

                anime.Images?.Add(img);
            }

            if (anime.MangaIdList != null && anime.MangaIdList.Count > 0)
            {
                //var mangaList = await _appDbContext.Manga.Where(a => anime.MangaIdList.Any(m => m == a.Id)).ToListAsync();
                var mangaList = await _mangaRepository.GetMangaListByIdAsync(anime.MangaIdList);
                anime.Manga?.RemoveRange(0, anime.Manga.Count);
                anime.Manga?.AddRange(mangaList);
            }

            if (anime.AuthorIdList != null && anime.AuthorIdList.Count > 0)
            {
                //var authorList = await _appDbContext.Authors.Where(a => anime.AuthorIdList.Any(m => m == a.Id)).ToListAsync();
                var authorList = await _authorRepository.GetAuthorListByIdAsync(anime.AuthorIdList);
                anime.Authors?.RemoveRange(0, anime.Authors.Count);
                anime.Authors?.AddRange(authorList);
            }

            if (anime.GenreIdList != null && anime.GenreIdList.Count > 0)
            {
                //var genreList = await _appDbContext.Genres.Where(g => anime.GenreIdList.Any(m => m == g.Id)).ToListAsync();
                var genreList = await _genreRepository.GetGenreListById(anime.GenreIdList);
                anime.Genres?.RemoveRange(0, anime.Genres.Count);
                anime.Genres?.AddRange(genreList);
            }
            _animeRepository.Update(anime);
            await _animeRepository.SaveChangesAsync();

            TempData["success"] = "Anime updated successfully!";

            return RedirectToAction("Index");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TestMvcProject.Data;
using TestMvcProject.Repository;
using TestMvcProject.Repository.Interfaces;
using TestMvcProject.ViewHelperLib;
using Manga = TestMvcProject.Models.Manga;

namespace TestMvcProject.Controllers
{
    public class MangaController : Controller
    {
        private readonly IAnimeRepository _animeRepository;
        private readonly IMangaRepository _mangaRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IImageRepository _imageRepository;

        public MangaController(IAnimeRepository animeRepository,
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

        // GET: Manga
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

            IEnumerable<Manga> MangaList = await _mangaRepository.FillMangaListAsync(searchString);

            MangaList = _mangaRepository.SortManga(sortOrder, MangaList);

            int pageSize = 10;
            return View(await PaginatedList<Manga>.CreateAsync(MangaList, pageNumber ?? 1, pageSize));
        }

        // GET: Manga
        public async Task<IActionResult> IndexUneditable(string? sortOrder, string? searchString, string currentFilter, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentSortView"] = sortOrder?.Replace("_", " ");

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            IEnumerable<Manga> MangaList = await _mangaRepository.FillMangaListAsync(searchString);

            MangaList = _mangaRepository.SortManga(sortOrder, MangaList);

            int pageSize = 10;
            return View(await PaginatedList<Manga>.CreateAsync(MangaList, pageNumber ?? 1, pageSize));
        }

        // GET: Manga/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || id == Guid.Empty)
                return NotFound();

            var manga = await _mangaRepository.GetMangaByIdFullInfoAsync((Guid)id, true);

            if (manga == null)
                return NotFound();

            await _authorRepository.SearchAuthorsImagesAsync(manga.Authors);
            await _animeRepository.SearchAnimiesImagesAsync(manga.Anime);

            if (manga.Images != null && manga.Images.Count > 0)
                ViewBag.Poster = string.Format("data:image/png;base64,{0}", (Convert.ToBase64String(manga.Images.Last().Data)));

            return View(manga);
        }

        // GET: Manga/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.AnimeList = await _animeRepository.FillViewBagAnimeListAsync();
            ViewBag.AuthorList = await _authorRepository.FillViewBagAuthorListAsync();
            ViewBag.GenreList = await _genreRepository.FillViewBagGenreListAsync();
            return View();
        }

        // POST: Manga/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Manga manga)
        {
            if (!ModelState.IsValid)
                return View(manga);

            var animies = _animeRepository.GetAll();
            var authors = _authorRepository.GetAll();
            var genres = _genreRepository.GetAll();

            if (manga.Avatar != null)
            {
                var img = _imageRepository.GetImg(manga.Avatar, "AvatarOf" + manga.Tittle);
                manga.Images?.Add(img);
            }

            if (manga.AnimeIdList != null && manga.AnimeIdList.Count > 0)
                manga.Anime?.AddRange(animies.Where(a => manga.AnimeIdList.Any(m => m == a.Id)).ToList());

            if (manga.AuthorIdList != null && manga.AuthorIdList.Count > 0)
                manga.Authors?.AddRange(authors.Where(a => manga.AuthorIdList.Any(m => m == a.Id)).ToList());

            if (manga.GenreIdList != null && manga.GenreIdList.Count > 0)
                manga.Genres?.AddRange(genres.Where(g => manga.GenreIdList.Any(m => m == g.Id)).ToList());

            _mangaRepository.Add(manga);
            await _mangaRepository.SaveChangesAsync();

            TempData["success"] = "Manga created successfully!";

            return RedirectToAction("Index");
        }

        // GET: Manga/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || Guid.Empty == id)
                return NotFound();

            var manga = await _mangaRepository.GetMangaByIdFullInfoAsync((Guid)id, false);

            if (manga == null)
                return NotFound();

            ViewBag.AnimeList = await _animeRepository.FillViewBagAnimeListAsync();
            ViewBag.AuthorList = await _authorRepository.FillViewBagAuthorListAsync();
            ViewBag.GenreList = await _genreRepository.FillViewBagGenreListAsync();

            return View(manga);
        }

        // POST: Manga/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Manga manga)
        {
            if (manga == null)
                return NotFound();

            if (!ModelState.IsValid)
                return View(manga);

            //var _manga = await _mangaRepository.GetMangaByIdWithAnimeAuthorGenreAsync(manga.Id, false);
            _mangaRepository.Attach(manga);
            await _mangaRepository.LoadRealatedAnimiesAsync(manga);
            await _mangaRepository.LoadRealatedAuthorsAsync(manga);
            await _mangaRepository.LoadRealatedGenresAsync(manga);

            if (manga.Avatar != null)
            {
                var img = _imageRepository.GetImg(manga.Avatar, "AvatarOf" + manga.Tittle);

                _imageRepository.Add(img);
                await _imageRepository.SaveChangesAsync();

                manga.Images?.Add(img);
            }

            if (manga.AnimeIdList != null && manga.AnimeIdList.Count > 0)
            {
                //var animeList = await _appDbContext.Anime.Where(a => manga.AnimeIdList.Any(m => m == a.Id)).ToListAsync();
                var animeList = await _animeRepository.GetAnimeListByIdAsync(manga.AnimeIdList);
                manga.Anime?.RemoveRange(0, manga.Anime.Count);
                manga.Anime?.AddRange(animeList);
            }

            if (manga.AuthorIdList != null && manga.AuthorIdList.Count > 0)
            {
                //var authorList = await _appDbContext.Authors.Where(a => manga.AuthorIdList.Any(m => m == a.Id)).ToListAsync();
                var authorList = await _authorRepository.GetAuthorListByIdAsync(manga.AuthorIdList);
                manga.Authors?.RemoveRange(0, manga.Authors.Count);
                manga.Authors?.AddRange(authorList);
            }

            if (manga.GenreIdList != null && manga.GenreIdList.Count > 0)
            {
                //var genreList = await _appDbContext.Genres.Where(g => manga.GenreIdList.Any(m => m == g.Id)).ToListAsync();
                var genreList = await _genreRepository.GetGenreListById(manga.GenreIdList);
                manga.Genres?.RemoveRange(0, manga.Genres.Count);
                manga.Genres?.AddRange(genreList);
            }

            _mangaRepository.Update(manga);
            await _mangaRepository.SaveChangesAsync();

            TempData["success"] = "Manga updated successfully!";

            return RedirectToAction("Index");

        }

        // GET: Manga/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == Guid.Empty || id == null)
                return NotFound();

            var manga = await _mangaRepository.GetMangaByIdFullInfoAsync((Guid)id, false);

            if (manga == null)
                return NotFound();

            ViewBag.AnimeList = await _animeRepository.FillViewBagAnimeListAsync();
            ViewBag.AuthorList = await _authorRepository.FillViewBagAuthorListAsync();
            ViewBag.GenreList = await _genreRepository.FillViewBagGenreListAsync();

            if (manga.Images != null && manga.Images.Count > 0)
                ViewBag.Poster = string.Format("data:image/png;base64,{0}", (Convert.ToBase64String(manga.Images.Last().Data)));

            return View(manga);
        }

        // POST: Manga/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(Guid? id)
        {
            if (id == Guid.Empty || id == null)
                return NotFound();

            var manga = await _mangaRepository.GetMangaByIdWithImageAsync((Guid)id, false);

            if (manga == null)
                return NotFound();

            if (manga.Images != null && manga.Images.Count > 0)
            {
                _imageRepository.RemoveRange(manga.Images);
                await _imageRepository.SaveChangesAsync();
            }

            _mangaRepository.Remove(manga);
            await _mangaRepository.SaveChangesAsync();

            TempData["success"] = "Manga deleted successfully!";

            return RedirectToAction("Index");
        }
    }
}

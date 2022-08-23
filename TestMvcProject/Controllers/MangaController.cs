﻿using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Xml.Linq;
using ImageMagick;
using JikanDotNet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestMvcProject.Data;
using TestMvcProject.Models;
using TestMvcProject.ViewModels;
using Anime = TestMvcProject.Models.Anime;
using Manga = TestMvcProject.Models.Manga;

namespace TestMvcProject.Controllers
{
    public class MangaController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IJikan _jikan;

        public MangaController(AppDbContext appDbContext, IJikan jikan)
        {
            _appDbContext = appDbContext;
            _jikan = jikan;
        }

        // GET: Manga
        public async Task<IActionResult> Index()
        {
            var animies = await _jikan.GetTopMangaAsync();
            var _a = animies.Data.ToList();
            for (int i = 0; i < animies.Data.Count; i++)
            {
                var a = new Manga();
                a.Tittle = animies.Data.ElementAt(i).Title;
                //a.Images = animies.Data.ElementAt(i).Images.JPG.MaximumImageUrl;
                byte[] data;
                using (WebClient webClient = new WebClient())
                {
                    data = webClient.DownloadData(animies.Data.ElementAt(0).Images.JPG.ImageUrl);
                }
                    _appDbContext.Add(a);
            }
            await _appDbContext.SaveChangesAsync();
            IEnumerable<Manga> MangaList = await _appDbContext.Mangas.Include(a => a.Animies)
                .Include(a => a.Images)
                .Include(a => a.Authors)
                .ThenInclude(a => a.Positions)
                .ToListAsync();
            return View(MangaList);
        }

        // GET: Manga/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || id == Guid.Empty)
                return NotFound();

            var manga = await _appDbContext.Mangas.Include(m => m.Animies).ThenInclude(a => a.Images)
                .Include(m => m.Images)
                .Include(m => m.Authors)
                .ThenInclude(a => a.Images)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (manga == null)
                return NotFound();

            //manga.Animies = await _appDbContext.Animies.Where(a => a.MangaId == manga.Id).ToListAsync();
            //manga.Authors = await _appDbContext.Authors.Where(a => a.MangaId == manga.Id).ToListAsync(); 
            if (manga.Images != null && manga.Images.Count > 0)
                ViewBag.Poster = String.Format("data:image/png;base64,{0}", (Convert.ToBase64String(manga.Images.Last().Data)));
            //ViewBag.AnimeList = await FillViewBagAnimeList();
            //ViewBag.AuthorList = await FillViewBagAuthorList();

            return View(manga);
        }

        // GET: Manga/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.AnimeList = await ViewHelper.FillViewBagAnimeList(_appDbContext);
            ViewBag.AuthorList = await ViewHelper.FillViewBagAuthorList(_appDbContext);
            return View();
        }

        // POST: Manga/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Manga manga)
        {
            //if (manga.Avatar.Length > 1048576)
            //    ModelState.AddModelError("Avatar", "Poster file size must be less then 1mb");

            //if (manga.Avatar.ContentType != "image/jpeg" || manga.Avatar.ContentType != "image/png")
            //    ModelState.AddModelError("AvatarExt", "Poster file extention must be jpeg");


            if (!ModelState.IsValid)
                return View(manga);

            var animies = _appDbContext.Animies;
            var authors = _appDbContext.Authors;

            if (manga.Avatar != null)
            {
                var img = ViewHelper.GetImg(manga.Avatar, "AvatarOf" + manga.Tittle);

                using (var image = new MagickImage(img.Data))
                {
                    image.Resize(300, 0);
                    img.Data = image.ToByteArray();
                }

                manga.Images?.Add(img);

            }

            if (manga.AuthorId != null)
                manga.Authors?.AddRange(_appDbContext.Authors.Where(a => a.Id == manga.AuthorId));

            if (manga.AnimeIdList != null && manga.AnimeIdList.Count > 0)
                manga.Animies?.AddRange(animies.Where(a => manga.AnimeIdList.Any(m => m == a.Id)).ToList());

            if (manga.AuthorIdList != null && manga.AuthorIdList.Count > 0)
                manga.Authors?.AddRange(authors.Where(a => manga.AuthorIdList.Any(m => m == a.Id)).ToList());

            _appDbContext.Mangas.Add(manga);
            await _appDbContext.SaveChangesAsync();

            TempData["success"] = "Manga created successfully!";

            return RedirectToAction("Index");
        }

        // GET: Manga/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || Guid.Empty == id)
                return NotFound();

            var manga = await _appDbContext.Mangas.Include(m => m.Images)
                .Include(m => m.Authors)
                .Include(m => m.Animies)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (manga == null)
                return NotFound();

            ViewBag.AnimeList = await ViewHelper.FillViewBagAnimeList(_appDbContext);
            ViewBag.AuthorList = await ViewHelper.FillViewBagAuthorList(_appDbContext);

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

            //if (manga.Avatar.Length > 1048576)
            //    ModelState.AddModelError("Avatar", "Poster file size must be less then 1mb");

            if (!ModelState.IsValid)
                return View(manga);

            var _manga = await _appDbContext.Mangas.Include(m => m.Animies)
                .Include(m => m.Authors)
                .FirstOrDefaultAsync(m => m.Id == manga.Id);

            manga.Animies = _manga?.Animies;
            manga.Authors = _manga?.Authors;

            if (manga.Avatar != null)
            {
                var img = ViewHelper.GetImg(manga.Avatar, "AvatarOf" + manga.Tittle);

                _appDbContext.Images.Add(img);
                await _appDbContext.SaveChangesAsync();

                manga.Images?.Add(img);
            }

            if (manga.AnimeIdList != null && manga.AnimeIdList.Count > 0)
            {
                var animeList = await _appDbContext.Animies.Where(a => manga.AnimeIdList.Any(m => m == a.Id)).ToListAsync();
                manga.Animies?.RemoveRange(0, manga.Animies.Count);
                manga.Animies?.AddRange(animeList);
            }

            if (manga.AuthorIdList != null && manga.AuthorIdList.Count > 0)

            {
                var mangaList = await _appDbContext.Authors.Where(a => manga.AuthorIdList.Any(m => m == a.Id)).ToListAsync();
                manga.Authors?.RemoveRange(0, manga.Authors.Count);
                manga.Authors?.AddRange(mangaList);
            }

            //_appDbContext.Mangas.Update(manga);
            await _appDbContext.SaveChangesAsync();

            TempData["success"] = "Manga updated successfully!";

            return RedirectToAction("Index");

        }

        // GET: Manga/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == Guid.Empty || id == null)
                return NotFound();

            var manga = await _appDbContext.Mangas.FirstOrDefaultAsync(m => m.Id == id);

            if (manga == null)
                return NotFound();

            ViewBag.AnimeList = await ViewHelper.FillViewBagAnimeList(_appDbContext);
            ViewBag.AuthorList = await ViewHelper.FillViewBagAuthorList(_appDbContext);

            return View(manga);
        }

        // POST: Manga/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(Guid? id)
        {
            if (id == Guid.Empty || id == null)
                return NotFound();

            var manga = await _appDbContext.Mangas.Include(m => m.Images).FirstOrDefaultAsync(m => m.Id == id);

            if (manga == null)
                return NotFound();

            if (manga.Images != null && manga.Images.Count > 0)
            {
                _appDbContext.Images.RemoveRange(manga.Images);
                await _appDbContext.SaveChangesAsync();
            }

            _appDbContext.Mangas.Remove(manga);
            await _appDbContext.SaveChangesAsync();

            TempData["success"] = "Manga deleted successfully!";

            return RedirectToAction("Index");
        }

        //private bool MangaExists(Guid id)
        //{
        //    return (_appDbContext.Mangas?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestMvcProject.Data;
using TestMvcProject.Models;

namespace TestMvcProject.Controllers
{
    public class AnimeController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public AnimeController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            IEnumerable<Anime> AnimeList = _appDbContext.Animies.Include(a => a.Manga).Include(a => a.Images).Include(a=>a.Authors).ThenInclude(a=>a.Positions).ToList();
            return View(AnimeList);
        }


        //GET
        //public ActionResult Details(Guid id)
        //{
        //    IEnumerable<Anime> AnimeList = _appDbContext.Animies.Where(a=>a.Id == id).Include(a => a.Manga).Include(a => a.Authors).ToList();
        //    return View(AnimeList);
        //}

        //GET
        public IActionResult Create()
        {
            return View();
        }

        private Image GetImg(Anime anime)
        {
            byte[] imageData;
            // считываем переданный файл в массив байтов
            using (var binaryReader = new BinaryReader(anime.Avatar.OpenReadStream()))
            {
                imageData = binaryReader.ReadBytes((int)anime.Avatar.Length);
            }
            // установка массива байтов
            var img = new Image();
            img.Data = imageData;
            //img.Author = author;
            //img.AuthorId = author.Id;
            img.Name = "AvatarOf" + anime.Tittle;
            return img;
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Anime anime)
        {
            if (!ModelState.IsValid)
                return View(anime);

            if (anime.Avatar != null)
            {
                var img = GetImg(anime);
                anime.Images.Add(img);
            }

            _appDbContext.Animies.Add(anime);
            _appDbContext.SaveChanges();

            TempData["success"] = "Anime created successfully!";

            return RedirectToAction("Index");
        }

        // GET: PositionController/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == Guid.Empty || id == null)
                return NotFound();

            var anime = _appDbContext.Animies.Find(id);

            if (anime == null)
                return NotFound();

            return View(anime);
        }

        // POST: PositionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(Guid? id)
        {
            if (id == Guid.Empty || id == null)
                return NotFound();

            var anime = _appDbContext.Animies.Where(a => a.Id == id).Include(a => a.Images).Include(a=>a.Manga).Include(a => a.Authors).ThenInclude(a => a.Positions).FirstOrDefault();

            if (anime == null)
                return NotFound();

            if (anime.Images != null && anime.Images.Count > 0)
            {
                _appDbContext.Images.RemoveRange(anime.Images);
                _appDbContext.SaveChanges();
            }

            _appDbContext.Animies.Remove(anime);

            _appDbContext.SaveChanges();

            TempData["success"] = "Anime deleted successfully!";

            return RedirectToAction("Index");
        }

        // GET: PositionController/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == Guid.Empty || id == null)
                return NotFound();

            //var author = _appDbContext.Authors.Where(a=>a.Id==id).Include(a => a.Positions).Include(a => a.Images).FirstOrDefault();
            //var author = _appDbContext.Authors.Where(a => a.Id == id).FirstOrDefault();
            var anime = _appDbContext.Animies.Find(id);

            if (anime == null)
                return NotFound();

            return View(anime);
        }

        // POST: PositionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Anime anime)
        {
            if (!ModelState.IsValid)
                return View(anime);

            if (anime.Avatar != null)
            {
                var img = GetImg(anime);

                _appDbContext.Images.Add(img);
                _appDbContext.SaveChanges();

                anime.Images.Add(img);
            }
            _appDbContext.Animies.Update(anime);
            _appDbContext.SaveChanges();

            TempData["success"] = "Anime updated successfully!";

            return RedirectToAction("Index");
        }
    }
}

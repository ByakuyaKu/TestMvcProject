﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestMvcProject.Data;
using TestMvcProject.Models;

namespace TestMvcProject.Controllers
{
    public class AuthorController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public AuthorController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        // GET: PositionController
        public ActionResult Index()
        {
            IEnumerable<Author> authorList = _appDbContext.Authors.Include(p => p.Positions).Include(a => a.Images).ToList();
            return View(authorList);
        }

        // GET: PositionController/Details/5
        public ActionResult Details(Guid id)
        {
            var author = _appDbContext.Authors.Where(p => p.Id == id).Include(p => p.Positions).Include(a=>a.Images).ToList();
            if (author == null)
                return NotFound();

            return View(author);
        }

        // GET: PositionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PositionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Author author)
        {
            if (!ModelState.IsValid)
                return View(author);

            if (author.Avatar != null)
            {
                var img = GetImg(author);
                author.Images.Add(img);
            }

            _appDbContext.Authors.Add(author);
            _appDbContext.SaveChanges();

            TempData["success"] = "Author created successfully!";

            return RedirectToAction("Index");
        }

        // GET: PositionController/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == Guid.Empty || id == null)
                return NotFound();

            //var author = _appDbContext.Authors.Where(a=>a.Id==id).Include(a => a.Positions).Include(a => a.Images).FirstOrDefault();
            //var author = _appDbContext.Authors.Where(a => a.Id == id).FirstOrDefault();
            var author = _appDbContext.Authors.Find(id);

            if (author == null)
                return NotFound();

            return View(author);
        }
        private Image GetImg(Author author)
        {
            byte[] imageData;
            // считываем переданный файл в массив байтов
            using (var binaryReader = new BinaryReader(author.Avatar.OpenReadStream()))
            {
                imageData = binaryReader.ReadBytes((int)author.Avatar.Length);
            }
            // установка массива байтов
            var img = new Image();
            img.Data = imageData;
            //img.Author = author;
            //img.AuthorId = author.Id;
            img.Name = "AvatarOf" + author.FirstName + author.LastName;
            return img;
        }

        // POST: PositionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Author author)
        {
            //author.Images = _appDbContext.Authors.Where(a => a.Id == author.Id).Include(a => a.Positions).Include(a => a.Images).FirstOrDefault().Images;

            if (!ModelState.IsValid)
                return View(author);

            if (author.Avatar != null)
            {
                var img = GetImg(author);

                _appDbContext.Images.Add(img);
                _appDbContext.SaveChanges();

                author.Images.Add(img);
            }
            _appDbContext.Authors.Update(author);
            _appDbContext.SaveChanges();

            TempData["success"] = "Author updated successfully!";

            return RedirectToAction("Index");
        }

        // GET: PositionController/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == Guid.Empty || id == null)
                return NotFound();

            var author = _appDbContext.Authors.Find(id);

            if (author == null)
                return NotFound();

            return View(author);
        }

        // POST: PositionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(Guid? id)
        {
            if (id == Guid.Empty || id == null)
                return NotFound();

            var author = _appDbContext.Authors.Where(a => a.Id == id).Include(a => a.Images).FirstOrDefault();

            if (author == null)
                return NotFound();

            if (author.Images != null && author.Images.Count > 0)
            {
                _appDbContext.Images.RemoveRange(author.Images);
                _appDbContext.SaveChanges();
            }

            _appDbContext.Authors.Remove(author);

            _appDbContext.SaveChanges();

            TempData["success"] = "Author deleted successfully!";

            return RedirectToAction("Index");
        }
    }
}

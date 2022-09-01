﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestMvcProject.Data;
using TestMvcProject.Models;

namespace TestMvcProject.Controllers
{
    public class PositionController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public PositionController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        // GET: PositionController
        public ActionResult Index()
        {
            IEnumerable<Position> PositionList = _appDbContext.Positions.Include(p=>p.Authors).ToList();
            return View(PositionList);
        }

        // GET: PositionController/Details/5
        public ActionResult Details(Guid id)
        {
            var Position = _appDbContext.Positions.Where(p => p.Id == id).Include(p=>p.Authors).ToList();
            if (Position == null)
                return NotFound();

            return View(Position);
        }

        // GET: PositionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PositionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Position position)
        {
            if (!ModelState.IsValid)
                return View(position);

            _appDbContext.Positions.Add(position);
            _appDbContext.SaveChanges();

            TempData["success"] = "Position created successfully!";

            return RedirectToAction("Index");
        }

        // GET: PositionController/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == Guid.Empty || id == null)
                return NotFound();

            var position = _appDbContext.Positions.Find(id);

            if (position == null)
                return NotFound();

            return View(position);
        }

        // POST: PositionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Position position)
        {
            if (!ModelState.IsValid)
                return View(position);

            _appDbContext.Positions.Update(position);

            _appDbContext.SaveChanges();

            TempData["success"] = "Position updated successfully!";

            return RedirectToAction("Index");
        }

        // GET: PositionController/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == Guid.Empty || id == null)
                return NotFound();

            var position = _appDbContext.Positions.Find(id);

            if (position == null)
                return NotFound();

            return View(position);
        }

        // POST: PositionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(Guid? id)
        {
            if (id == Guid.Empty || id == null)
                return NotFound();

            var position = _appDbContext.Positions.Find(id);

            if (position == null)
                return NotFound();

            _appDbContext.Positions.Remove(position);

            _appDbContext.SaveChanges();

            TempData["success"] = "Position deleted successfully!";

            return RedirectToAction("Index");
        }
    }
}

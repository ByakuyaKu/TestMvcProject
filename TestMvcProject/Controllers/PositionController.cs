using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestMvcProject.Data;
using TestMvcProject.Models;
using TestMvcProject.Repository.Interfaces;
using TestMvcProject.ViewHelperLib;

namespace TestMvcProject.Controllers
{
    public class PositionController : Controller
    {
        private readonly IPositionRepository _positionRepository;

        public PositionController(IPositionRepository positionRepository)
        {
            _positionRepository = positionRepository;
        }
        // GET: PositionController
        public async Task<ActionResult> IndexAsync(string? sortOrder, string? searchString, string currentFilter, int? pageNumber)
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

            IEnumerable<Position> PositionList = await _positionRepository.FillPositionListAsync(searchString);

            PositionList = _positionRepository.SortPosition(sortOrder, PositionList);

            int pageSize = 10;
            return View(await PaginatedList<Position>.CreateAsync(PositionList, pageNumber ?? 1, pageSize));
        }

        // GET: PositionController/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null || id == Guid.Empty)
                return NotFound();

            var Position = await _positionRepository.GetPositionByIdWithAuthor((Guid)id, true);

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
        public async Task<ActionResult> Create(Position position)
        {
            if (!ModelState.IsValid)
                return View(position);

            _positionRepository.Add(position);
            await _positionRepository.SaveChangesAsync();

            TempData["success"] = "Position created successfully!";

            return RedirectToAction("Index");
        }

        // GET: PositionController/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == Guid.Empty || id == null)
                return NotFound();

            var position = await _positionRepository.GetPositionById((Guid)id, false);

            if (position == null)
                return NotFound();

            return View(position);
        }

        // POST: PositionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Position position)
        {
            if (!ModelState.IsValid)
                return View(position);

            _positionRepository.Update(position);

            await _positionRepository.SaveChangesAsync();

            TempData["success"] = "Position updated successfully!";

            return RedirectToAction("Index");
        }

        // GET: PositionController/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == Guid.Empty || id == null)
                return NotFound();

            var position = await _positionRepository.GetPositionById((Guid)id, false);

            if (position == null)
                return NotFound();

            return View(position);
        }

        // POST: PositionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeletePost(Guid? id)
        {
            if (id == Guid.Empty || id == null)
                return NotFound();

            var position = await _positionRepository.GetPositionById((Guid)id, false);

            if (position == null)
                return NotFound();

            _positionRepository.Remove(position);

            await _positionRepository.SaveChangesAsync();

            TempData["success"] = "Position deleted successfully!";

            return RedirectToAction("Index");
        }
    }
}

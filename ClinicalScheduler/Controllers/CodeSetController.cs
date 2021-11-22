using Microsoft.AspNetCore.Mvc;
using Scheduler.DataAccess;
using Scheduler.Models;

namespace ClinicalScheduler.Controllers
{
    public class CodeSetController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CodeSetController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<CodeSet> codeSetList = _db.CodeSets;
            return View(codeSetList);
        }

        //get
        public IActionResult Create()
        {
            
            return View();
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CodeSet codeSet)
        {
            if (ModelState.IsValid)
            {
                _db.CodeSets.Add(codeSet);
                _db.SaveChanges();
                TempData["Success"] = "Code Set created successfully";
                return RedirectToAction("Index");
            }
            return View(codeSet);
        }

        //get
        public IActionResult Edit(int? id)
        {
            if (id==null || id ==0) return NotFound();

            var codeSetInDb = _db.CodeSets.FirstOrDefault(c => c.Id == id);

            if (codeSetInDb == null) return NotFound();

            return View(codeSetInDb);
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CodeSet codeSet)
        {
            if (ModelState.IsValid)
            {
                _db.CodeSets.Update(codeSet);
                _db.SaveChanges();
                TempData["Success"] = "Code Set edited successfully";
                return RedirectToAction("Index");
            }
            return View(codeSet);
        }

        //get
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) return NotFound();

            var codeSetInDb = _db.CodeSets.FirstOrDefault(c => c.Id == id);

            if (codeSetInDb == null) return NotFound();

            return View(codeSetInDb);
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {

            var codeSetInDb = _db.CodeSets.FirstOrDefault(c => c.Id == id);

            if (codeSetInDb == null) return NotFound();

            _db.CodeSets.Remove(codeSetInDb);
            _db.SaveChanges();
            TempData["Success"] = "Code Set deleted successfully";

            return RedirectToAction("Index");
        }

    }
}

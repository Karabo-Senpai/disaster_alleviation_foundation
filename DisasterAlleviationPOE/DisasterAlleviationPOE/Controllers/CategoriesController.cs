using DisasterAlleviationPOE.AppData;
using DisasterAlleviationPOE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DisasterAlleviationPOE.Controllers
{
    public class CategoriesController : Controller
    {

        private readonly DisasterAlleviationContext alleviationContext;

        public CategoriesController(DisasterAlleviationContext context)
        {
            alleviationContext = context;
        }
        // GET: CategoriesController
        // GET: Categories
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await alleviationContext.Categories.ToListAsync());
        }

        // GET: CategoriesController/Details/5
       
          [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await alleviationContext.Categories
                .FirstOrDefaultAsync(c => c.CategoryID == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: CategoriesController/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: CategoriesController/Create
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryID,CategoryName")] Category category)
        {
            if (ModelState.IsValid)
            {
                alleviationContext.Add(category);
                await alleviationContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: CategoriesController/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await alleviationContext.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        // POST: CategoriesController/Edit/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryID,CategoryName")] Category category)
        {
            if (id != category.CategoryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    alleviationContext.Update(category);
                    await alleviationContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CategoryID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        // GET: CategoriesController/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await alleviationContext.Categories
                .FirstOrDefaultAsync(c => c.CategoryID == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: CategoriesController/Delete/5
        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await alleviationContext.Categories.FindAsync(id);
            alleviationContext.Categories.Remove(category);
            await alleviationContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return alleviationContext.Categories.Any(l => l.CategoryID == id);
        }
    }
}

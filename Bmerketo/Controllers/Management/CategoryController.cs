using Bmerketo.Models.Entities;
using Bmerketo.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Bmerketo.Controllers.Management
{
    public class CategoryController : Controller
    {
        private readonly ICategoryInterface _categoryService;

        public CategoryController(ICategoryInterface Service)
        {
            _categoryService = Service;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _categoryService.GetAllAsync();
            return View(data);
        }
        //Get
        public IActionResult Create()
        {
            return View();
        }
        //Set
        [HttpPost]
        public async Task<IActionResult> Create([Bind("CategoryName")] CategoryEntity category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            await _categoryService.AddAsync(category);
            return RedirectToAction(nameof(Index));
        }

        //------------------------

        //Get
        public async Task<IActionResult> Edit(int id)
        {
            var colorName = await _categoryService.GetByIdAsync(id);
            if (colorName == null) { return View("NotFound"); }


            return View(colorName);
        }
        //Set
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, CategoryName")] CategoryEntity category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            await _categoryService.UpdateAsync(id, category);
            return RedirectToAction(nameof(Index));
        }


        //-------------------------
        public async Task<IActionResult> Detials(int id)
        {
            var colorDetails = _categoryService.GetByIdAsync(id);
            if (colorDetails == null)
                return View("Empty");
            return View(colorDetails);
        }

        //------------***********-------------
        //Get
        public async Task<IActionResult> Delete(int id)
        {
            var colorName = await _categoryService.GetByIdAsync(id);
            if (colorName == null) { return View("NotFound"); }


            return View(colorName);
        }
        //Set
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var colorName = await _categoryService.GetByIdAsync(id);
            if (colorName == null) { return View("NotFound"); }
            await _categoryService.DeleteAsync(id);


            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> CategoryPartialView()
        {
            var data = await _categoryService.GetAllAsync();
            return PartialView("_CategoryPartial", data);
        }

        public async Task<IActionResult> MyPartialIndex()
        {
            var data = await _categoryService.GetAllAsync();
            return PartialView("_MyPartialView", data);
        }
    }
}

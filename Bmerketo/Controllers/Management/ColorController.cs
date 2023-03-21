using Bmerketo.Contexts;
using Bmerketo.Models.Entities;
using Bmerketo.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Bmerketo.Controllers.Management
{
    public class ColorController : Controller
    {
        private readonly IColorInterface _colorService;

        public ColorController(IColorInterface Service)
        {
            _colorService = Service;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _colorService.GetAllAsync();
            return View(data);
        }
        //Get
        public IActionResult Create()
        {
            return View();
        }
        //Set
        [HttpPost]
        public async Task<IActionResult> Create([Bind("ColorName")] ColorEntity color)
        {
            if (!ModelState.IsValid)
            {
                return View(color);
            }
            await _colorService.AddAsync(color);
            return RedirectToAction(nameof(Index));
        }

        //------------------------

        //Get
        public async Task<IActionResult> Edit(int id)
        {
            var colorName = await _colorService.GetByIdAsync(id);
            if (colorName == null) { return View("NotFound"); }


            return View(colorName);
        }
        //Set
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, ColorName")] ColorEntity color)
        {
            if (!ModelState.IsValid)
            {
                return View(color);
            }
            await _colorService.UpdateAsync(id, color);
            return RedirectToAction(nameof(Index));
        }


        //-------------------------
        public async Task<IActionResult> Detials(int id)
        {
            var colorDetails = _colorService.GetByIdAsync(id);
            if (colorDetails == null)
                return View("Empty");
            return View(colorDetails);
        }

        //------------***********-------------
        //Get
        public async Task<IActionResult> Delete(int id)
        {
            var colorName = await _colorService.GetByIdAsync(id);
            if (colorName == null) { return View("NotFound"); }


            return View(colorName);
        }
        //Set
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var colorName = await _colorService.GetByIdAsync(id);
            if (colorName == null) { return View("NotFound"); }
            await _colorService.DeleteAsync(id);


            return RedirectToAction(nameof(Index));
        }
    }

}

using Bmerketo.Models.Entities;
using Bmerketo.Services;
using Bmerketo.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Bmerketo.Controllers.Management
{
    public class BrandController : Controller
    {
        private readonly IBrandInterface _brandService;

        public BrandController(IBrandInterface brandService)
        {
            _brandService = brandService;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _brandService.GetAllAsync();
            return View(data);
        }

        //Get
        public IActionResult Create()
        {
            return View();
        }
        //Set
        [HttpPost]
        public async Task<IActionResult> Create([Bind("BrandName")] BrandEntity brand)
        {
            if (!ModelState.IsValid)
            {
                return View(brand);
            }
            await _brandService.AddAsync(brand);
            return RedirectToAction(nameof(Index));
        }

        //------------------------

        //Get
        public async Task<IActionResult> Edit(int id)
        {
            var colorName = await _brandService.GetByIdAsync(id);
            if (colorName == null) { return View("NotFound"); }


            return View(colorName);
        }
        //Set
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, BrandName")] BrandEntity brand)
        {
            if (!ModelState.IsValid)
            {
                return View(brand);
            }
            await _brandService.UpdateAsync(id, brand);
            return RedirectToAction(nameof(Index));
        }


        //-------------------------
        public async Task<IActionResult> Detials(int id)
        {
            var colorDetails = _brandService.GetByIdAsync(id);
            if (colorDetails == null)
                return View("Empty");
            return View(colorDetails);
        }

        //------------***********-------------
        //Get
        public async Task<IActionResult> Delete(int id)
        {
            var brandName = await _brandService.GetByIdAsync(id);
            if (brandName == null) { return View("NotFound"); }


            return View(brandName);
        }
        //Set
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var brandName = await _brandService.GetByIdAsync(id);
            if (brandName == null) { return View("NotFound"); }
            await _brandService.DeleteAsync(id);


            return RedirectToAction(nameof(Index));
        }
    }
}

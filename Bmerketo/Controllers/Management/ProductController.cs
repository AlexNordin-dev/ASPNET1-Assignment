using Bmerketo.Models.Entities;
using Bmerketo.Models.ViewModels;
using Bmerketo.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Bmerketo.Controllers.Management
{
    [Authorize(Roles = "Administrator , Product Manager")]
    public class ProductController : Controller
    {
        private readonly IProductInterface _productService;
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ProductController(IProductInterface Service, DataContext context, IWebHostEnvironment hostEnvironment)
        {
            _productService = Service;
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index(string searchString)
        {
            var allProduct = await _productService.GetAllAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                

                var filteredResult = allProduct.Where(n => string.Equals(n.ProductName.ToLower(), searchString.ToLower(), StringComparison.CurrentCultureIgnoreCase) || string.Equals(n.ShortDescription, searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();

                return View("Index", filteredResult);
            }

            return View("Index", allProduct);
        }
       
       



        //Get
        public IActionResult Create()
        {
            ViewData["BrandEntityId"] = new SelectList(_context.BrandEntity, "Id", "BrandName");
            ViewData["CategoryEntityId"] = new SelectList(_context.CategoryEntity, "Id", "CategoryName");
            ViewBag.ColorEntityIds = new SelectList(_context.ColorEntity, "Id", "ColorName");
            return View();
        }
        //Set
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,ProductName,ProductPrice,SKU,Quantity,ShortDescription,LongDescription,ImageAlt,ImageName,ImageFile,BrandEntityId,CategoryEntityId,Product_ColorEntity,ColorEntityIds")] NewProductVM product)
        {
            //if (!ModelState.IsValid)
            //{
            //    ViewData["BrandEntityId"] = new SelectList(_context.BrandEntity, "Id", "BrandName");
            //    ViewData["CategoryEntityId"] = new SelectList(_context.CategoryEntity, "Id", "CategoryName");
            //    ViewBag.ColorEntityIds = new SelectList(_context.ColorEntity, "Id", "ColorName");
            //    return View(product);
            //}

            string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(product.ImageFile.FileName);
            string exetention = Path.GetExtension(product.ImageFile.FileName);
            product.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssff") + exetention;
            string path = Path.Combine(wwwRootPath + "/Image/Product/", fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await product.ImageFile.CopyToAsync(fileStream);
            }

            await _productService.AddAsync(product);            
            return RedirectToAction(nameof(Index));
        }

        //------------------------

        //Get
        public async Task<IActionResult> Edit(int id)
        {
            var productEdit = await _productService.GetByIdAsync(id);
            if (productEdit == null) { return View("NotFound"); }

            var response = new NewProductVM()
            {
                Id = productEdit.Id,
                ProductName = productEdit.ProductName,
                ProductPrice = productEdit.ProductPrice,
                SKU = productEdit.SKU,
                Quantity = productEdit.Quantity,
                ShortDescription = productEdit.ShortDescription,
                LongDescription = productEdit.LongDescription,               
                BrandEntityId = productEdit.BrandEntityId,
                CategoryEntityId = productEdit.CategoryEntityId,
                ImageName = productEdit.ImageName,
                ImageAlt = productEdit.ImageAlt,
                //ColorEntityIds = productEdit.Product_ColorEntity.Select(n => n.ColorEntityId).ToList(),
            };

            ViewData["BrandEntityId"] = new SelectList(_context.BrandEntity, "Id", "BrandName");
            ViewData["CategoryEntityId"] = new SelectList(_context.CategoryEntity, "Id", "CategoryName");
            ViewBag.ColorEntityIds = new SelectList(_context.ColorEntity, "Id", "ColorName");
            return View(response);
        }
        //Set
        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewProductVM updateProduct)
        {
            if (id != updateProduct.Id) return View("NotFound");
            if (!ModelState.IsValid)
            {
                return View(updateProduct);
            }

            //Delete image from wwwroot
            var deleteimage = await _productService.GetByIdAsync(id);
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "image/product", deleteimage.ImageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
            //-----------------
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(updateProduct.ImageFile.FileName);
            string exetention = Path.GetExtension(updateProduct.ImageFile.FileName);
            updateProduct.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssff") + exetention;
            string path = Path.Combine(wwwRootPath + "/Image/Product/", fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await updateProduct.ImageFile.CopyToAsync(fileStream);
            }
            await _productService.UpdateAsync(updateProduct);
            return RedirectToAction(nameof(Index));
        }


        //Show Detials-------------------------
        [AllowAnonymous]
        public async Task<IActionResult> Detials(int id)
        {
            var productEdit = await _productService.GetByIdAsync(id);
            if (productEdit == null) { return View("NotFound"); }

            var response = new NewProductVM()
            {
                Id = productEdit.Id,
                ProductName = productEdit.ProductName,
                ProductPrice = productEdit.ProductPrice,
                SKU = productEdit.SKU,
                Quantity = productEdit.Quantity,
                ShortDescription = productEdit.ShortDescription,
                LongDescription = productEdit.LongDescription,
                BrandEntityId = productEdit.BrandEntityId,
                CategoryEntityId = productEdit.CategoryEntityId,
                ImageName = productEdit.ImageName,
                ImageAlt = productEdit.ImageAlt,
                //ColorEntityIds = productEdit.Product_ColorEntity.Select(n => n.ColorEntityId).ToList(),
            };

            ViewData["BrandEntityId"] = new SelectList(_context.BrandEntity, "Id", "BrandName");
            ViewData["CategoryEntityId"] = new SelectList(_context.CategoryEntity, "Id", "CategoryName");
            ViewBag.ColorEntityIds = new SelectList(_context.ColorEntity, "Id", "ColorName");
            return View(response);
        }

        //------------***********-------------
        //Get
        public async Task<IActionResult> Delete(int id)
        {
            var pName = await _productService.GetByIdAsync(id);
            if (pName == null) { return View("NotFound"); }


            return View(pName);
        }
        //Set
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deleteItem = await _productService.GetByIdAsync(id);
            //Delete image from wwwroot
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "image/product", deleteItem.ImageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
            _context.ProductEntity.Remove(deleteItem);


            if (deleteItem == null) { return View("NotFound"); }
            await _productService.DeleteAsync(id);


            return RedirectToAction(nameof(Index));
        }

    }
}

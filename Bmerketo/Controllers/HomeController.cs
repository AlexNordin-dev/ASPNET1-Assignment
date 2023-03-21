using Bmerketo.Models.Entities;
using Bmerketo.Models.ViewModels;
using Bmerketo.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bmerketo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductInterface _productService;
        private readonly ICategoryInterface _categoryService;
       
        public HomeController(IProductInterface Service, ICategoryInterface categoryService)
        {
            _productService = Service;         
            _categoryService = categoryService;
        }
        public List<CategoryEntity> categories { get; set; }
        public async Task<IActionResult> Index()
        {
            
            var allProduct = new ProductCollectionViewModel();
            allProduct.Products = await _productService.GetAllAsync();
            allProduct.Categories = await _categoryService.GetAllAsync();



            return View(allProduct);
        }

       

        public IActionResult Contact()
        {
            return View();
        }

     
    }
}

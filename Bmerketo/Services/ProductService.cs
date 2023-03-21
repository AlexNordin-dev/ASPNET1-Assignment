using Bmerketo.Contexts;
using Bmerketo.Models.Entities;
using Bmerketo.Models.ViewModels;
using Bmerketo.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq.Expressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Bmerketo.Services
{
    public class ProductService : IProductInterface
    {
        private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ProductService(DataContext dataContext, IWebHostEnvironment hostEnvironment)
        {
            _dataContext = dataContext;
            this._hostEnvironment = hostEnvironment;
        }

        public async Task AddAsync(NewProductVM product)
        {
           

            var NewProduct = new ProductEntity()
            {

                ProductName = product.ProductName,
                ProductPrice = product.ProductPrice,
                SKU = product.SKU,
                Quantity = product.Quantity,
                ShortDescription = product.ShortDescription,
                LongDescription = product.LongDescription,
                ImageName = product.ImageName,
                BrandEntityId = product.BrandEntityId,
                CategoryEntityId = product.CategoryEntityId,  
            };
            await _dataContext.ProductEntity.AddAsync(NewProduct);
            await _dataContext.SaveChangesAsync();

            //Add Product Color
            foreach (var colorId in product.ColorEntityIds)
            {
                var newProductColor = new Product_ColorEntity()
                {
                    ProductEntityId = NewProduct.Id,
                    ColorEntityId = colorId
                };
                await _dataContext.Product_ColorEntity.AddAsync(newProductColor);
            }
            await _dataContext.SaveChangesAsync();
        }


        public async Task DeleteAsync(int id)
        {
            var result = await _dataContext.ProductEntity.FirstOrDefaultAsync(x => x.Id == id);
            _dataContext.ProductEntity.Remove(result);
            await _dataContext.SaveChangesAsync();
        }

      

        public async Task<IEnumerable<ProductEntity>> GetAllAsync()
        {
           
            var MyColor = new List<ColorEntity>();

            //if (MyColor.Id == ProductEntity)
            //{

            //}
            var dataContext = _dataContext.ProductEntity.Include(p => p.BrandEntity).Include(p => p.CategoryEntity);
            return dataContext;
        }

     

        public async Task<ProductEntity> GetByIdAsync(int id)
        {
            var result = await _dataContext.ProductEntity.FirstOrDefaultAsync(x => x.Id == id);
            return result;

        }

        public async Task UpdateAsync(NewProductVM updateProduct)
        {
            var db = await _dataContext.ProductEntity.FirstOrDefaultAsync(n => n.Id == updateProduct.Id);
            if (db != null)
            {

                db.ProductName = updateProduct.ProductName;
                db.ProductPrice = updateProduct.ProductPrice;
                db.SKU = updateProduct.SKU;
                db.Quantity = updateProduct.Quantity;
                db.ShortDescription = updateProduct.ShortDescription;
                db.LongDescription = updateProduct.LongDescription;
                db.ImageName = updateProduct.ImageName;
                db.BrandEntityId = updateProduct.BrandEntityId;
                db.CategoryEntityId = updateProduct.CategoryEntityId; 
                await _dataContext.SaveChangesAsync();
            }


            //Remove existing color
            var Db = _dataContext.Product_ColorEntity.Where(n => n.ProductEntityId == updateProduct.Id).ToList();
            _dataContext.Product_ColorEntity.RemoveRange(Db);

            await _dataContext.SaveChangesAsync();


            //Add 
            foreach (var ColorId in updateProduct.ColorEntityIds)
            {
                var newActorMovie = new Product_ColorEntity()
                {
                    ProductEntityId = updateProduct.Id,
                    ColorEntityId = ColorId
                };
                await _dataContext.Product_ColorEntity.AddAsync(newActorMovie);
            }

            await _dataContext.SaveChangesAsync();


            //_dataContext.Update(newProduct);
            //await _dataContext.SaveChangesAsync();
            //return updateProduct;

        }
    }
}

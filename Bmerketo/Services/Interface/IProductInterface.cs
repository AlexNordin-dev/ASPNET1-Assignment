using Bmerketo.Models.Entities;
using Bmerketo.Models.ViewModels;
using System.Linq.Expressions;

namespace Bmerketo.Services.Interface
{
    public interface IProductInterface
    {
        Task<IEnumerable<ProductEntity>> GetAllAsync();
       
        Task<ProductEntity> GetByIdAsync(int id);
        //Task<ProductVM> GetNewProductVMValues();
        Task AddAsync(NewProductVM Newproduct);
        //Task<ProductEntity> UpdateAsync(int id, ProductEntity updateProduct);
        Task DeleteAsync(int id);
        Task UpdateAsync(NewProductVM updateProduct);
    }
}

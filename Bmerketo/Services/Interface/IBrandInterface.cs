using Bmerketo.Models.Entities;

namespace Bmerketo.Services.Interface
{
    public interface IBrandInterface
    {
        Task<IEnumerable<BrandEntity>> GetAllAsync();
        Task<BrandEntity> GetByIdAsync(int id);
        Task AddAsync(BrandEntity brand);
        Task<BrandEntity> UpdateAsync(int id, BrandEntity newBrand);
        Task DeleteAsync(int id);
    }
}

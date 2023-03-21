using Bmerketo.Models.Entities;

namespace Bmerketo.Services.Interface
{
    public interface IColorInterface
    {
        Task<IEnumerable<ColorEntity>> GetAllAsync();
        Task<ColorEntity> GetByIdAsync(int id);
        Task AddAsync(ColorEntity newcolor);
        Task<ColorEntity> UpdateAsync(int id, ColorEntity updateColor);
        Task DeleteAsync(int id);
    }
}

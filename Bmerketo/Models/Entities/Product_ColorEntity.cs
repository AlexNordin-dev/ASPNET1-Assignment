namespace Bmerketo.Models.Entities
{
    public class Product_ColorEntity
    {
        public int ProductEntityId { get; set; }
        public ProductEntity? ProductEntity { get; set; }
        public int ColorEntityId { get; set; }
        public ColorEntity? ColorEntity { get; set; }
    }
}

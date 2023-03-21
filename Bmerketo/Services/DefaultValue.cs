using Bmerketo.Models.Entities;

namespace Bmerketo.Services
{
    public class DefaultValue
    {
        public static void DefaultItem(IApplicationBuilder applicationBuilder)
        {
            using (var servicesScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = servicesScope.ServiceProvider.GetService<DataContext>();
                context.Database.EnsureCreated();

                //Brand
                if (!context.BrandEntity.Any())
                {
                    context.BrandEntity.AddRange(new List<BrandEntity>()
                    {
                        new BrandEntity()
                        {
                            BrandName = "Adidas"
                        },
                        new BrandEntity()
                        {
                            BrandName = "H&M"
                        },
                    });
                    context.SaveChanges();
                }
                //Catagory
                if (!context.CategoryEntity.Any())
                {
                    context.CategoryEntity.AddRange(new List<CategoryEntity>()
                    {
                        new CategoryEntity()
                        {
                            CategoryName = "Man"
                        },
                        new CategoryEntity()
                        {
                            CategoryName = "Women"
                        },
                    });
                    context.SaveChanges();
                }
                //Color
                if (!context.ColorEntity.Any())
                {
                    context.ColorEntity.AddRange(new List<ColorEntity>()
                    {
                        new ColorEntity()
                        {
                            ColorName = "Red"
                        },
                        new ColorEntity()
                        {
                            ColorName = "Green"
                        },
                    });
                    context.SaveChanges();
                }


                //Product
                if (!context.ProductEntity.Any())
                {
                    context.ProductEntity.AddRange(new List<ProductEntity>()
                    {
                        new ProductEntity()
                        {
                            ProductName = "T-Shert",
                            ProductPrice= 100,
                            SKU="4455UK",
                            Quantity=2,
                            ShortDescription="size M",
                            ImageName="blue-tshirt.png",
                            BrandEntityId=1,
                            CategoryEntityId=2,



                        },
                        new ProductEntity()
                        {
                          ProductName = "T-Shert Green",
                            ProductPrice= 200,
                            SKU="4455UK",
                            Quantity=5,
                            ShortDescription="Green T-Shert size L",
                            ImageName="black-dress.png",
                            BrandEntityId=1,
                            CategoryEntityId=1,
                        },
                    });
                    context.SaveChanges();
                }

                //Product_Color
                if (!context.Product_ColorEntity.Any())
                {
                    context.Product_ColorEntity.AddRange(new List<Product_ColorEntity>()
                    { new Product_ColorEntity()
                        {
                            ProductEntityId = 1,
                            ColorEntityId = 1
                        },
                        new Product_ColorEntity()
                        {
                            ProductEntityId = 2,
                            ColorEntityId = 2
                        },
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}

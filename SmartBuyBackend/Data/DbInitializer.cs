using Microsoft.EntityFrameworkCore;
using SmartBuyAPI.Models;

namespace SmartBuyAPI.Data
{
    public class DbInitializer
    {
        private readonly ModelBuilder modelBuilder;

        public DbInitializer(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }

        public void Seed()
        {
            var Categorys = new List<CategoryEntity>
            {
                   new CategoryEntity {
                       Id=1,
                       Name = "Комп'ютери та ноутбуки",
                       //Priority = 1,
                       IsDelete = false,
                       //DisplayOrder = 10,
                       DateCreated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                       DateLastEdit = default,
                       Image = "1.jpg",
                       Description = "description", },
                   new CategoryEntity {
                       Id=2,
                       Name = "Смартфони",
                       //Priority = 1,
                       IsDelete = false,
                      //DisplayOrder = 10,
                       DateCreated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                       DateLastEdit = default,
                       Image = "1.jpg",
                       Description = "description", },
                   new CategoryEntity {
                       Id=3,
                       Name = "Побутова техніка",
                       //Priority = 1,
                       IsDelete = false,
                       //DisplayOrder = 10,
                       DateCreated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                       DateLastEdit = default,
                       Image = "1.jpg",
                       Description = "description", },
                   new CategoryEntity {
                       Id=4,
                       Name = "Дача, сад, город",
                       //Priority = 1,
                       IsDelete = false,
                       //DisplayOrder = 10,
                       DateCreated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                       DateLastEdit = default,
                       Image = "1.jpg",
                       Description = "description", },
                   new CategoryEntity {
                       Id=5,
                       Name = "Спорт і захоплення",
                       //Priority = 1,
                       IsDelete = false,
                       //DisplayOrder = 10,
                       DateCreated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                       DateLastEdit = default,
                       Image = "1.jpg",
                       Description = "description", },
                   new CategoryEntity {
                       Id=6,
                       Name = "Офіс, школа, книги",
                       //Priority = 1,
                       IsDelete = false,
                       //DisplayOrder = 10,
                       DateCreated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                       DateLastEdit = default,
                       Image = "1.jpg",
                       Description = "description", },
                   new CategoryEntity {
                       Id=7,
                       Name = "test",
                       //Priority = 1,
                       IsDelete = false,
                       //DisplayOrder = 10,
                       DateCreated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                       DateLastEdit = default,
                       Image = "1.jpg",
                       Description = "description test", }                      
            };

            modelBuilder.Entity<CategoryEntity>().HasData(Categorys);

            modelBuilder.Entity<ProductEntity>().HasData(
                new ProductEntity
                {
                    Id = 1,
                    Name = "ПК Х123434",
                    CategoryId = 1,
                    IsDelete = false,
                    DateCreated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                    DateLastEdit = default,
                    Description = "test",
                    ShortDescription = "figna",
                    Price = 436765,
                    Image = "1.jpg",
                },
                new ProductEntity
                {
                    Id = 2,
                    Name = "iHunt Titan P13000 PRO 2021",
                    CategoryId = 2,
                    IsDelete = false,
                    DateCreated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                    DateLastEdit = default,
                    Description = "Ми представляємо вам найпотужнішу, саму оснащену, ударотривкий та найефективнішу версію смартфона 2021 року від румунської компанії iHunt .",
                    ShortDescription = "figna",
                    Price = 13940,
                    Image = "1.jpg"
                },
                new ProductEntity
                {
                    Id = 3,
                    Name = "BEKO CNA295K20XP",
                    CategoryId = 3,
                    IsDelete = false,
                    DateCreated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                    DateLastEdit = default,
                    Description = "Холодильники з системою NeoFrost ",
                    ShortDescription = "figna",
                    Price = 10999,
                    Image = "1.jpg"
                },
                new ProductEntity
                {
                    Id = 4,
                    Name = "Bosch UniversalChain 40",
                    CategoryId = 4,
                    IsDelete = false,
                    DateCreated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                    DateLastEdit = default,
                    Description = "Ланцюгова пила Bosch UniversalChain ",
                    ShortDescription = "figna",
                    Price = 3958,
                    Image = "1.jpg"
                },
                new ProductEntity
                {
                    Id = 5,
                    Name = "Champion Spark 29 19.5 Black-neon yellow-white",
                    CategoryId = 5,
                    IsDelete = false,
                    DateCreated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                    DateLastEdit = default,
                    Description = "Велосипед Champion Spark 29 ",
                    ShortDescription = "figna",
                    Price = 5460,
                    Image = "1.jpg"
                },
                new ProductEntity
                {
                    Id = 6,
                    Name = "Zoom Stora Enso А4 80 г/м2 клас С + 5 пачок по 500 аркушів Біла",
                    CategoryId = 6,
                    IsDelete = false,
                    DateCreated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                    DateLastEdit = default,
                    Description = "ВНабір паперу офісного Zoom Stora Enso А4 80 г/м2 клас С + 5 пачок по 500 аркушів Біла ",
                    ShortDescription = "figna",
                    Price = 1199,
                    Image = "1.jpg"
                }

            );
        }
    }
}

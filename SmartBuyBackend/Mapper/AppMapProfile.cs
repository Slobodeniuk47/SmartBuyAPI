using AutoMapper;
using SmartBuyAPI.Models;
using SmartBuyAPI.Models.DTO.Category;
using SmartBuyAPI.Models.DTO.Product;

namespace SmartBuyAPI.Mapper
{
    public class AppMapProfile : Profile
    {
        public AppMapProfile()
        {
            CreateMap<CategoryEntity, CategoryItemDTO>();

            CreateMap<CategoryCreateDTO, CategoryEntity>()
                .ForMember(x => x.Image, opt => opt.Ignore());



            CreateMap<ProductEntity, ProductItemDTO>()
                .ForMember(x => x.CategoryName, opt => opt.MapFrom(x => x.Category.Name));
            CreateMap<ProductCreateDTO, ProductEntity>()
                .ForMember(x => x.CategoryId, opt => opt.MapFrom(x => x.CategoryId == 0 ? null : x.CategoryId))
                .ForMember(x => x.Image, opt => opt.Ignore());
        }
    }
}
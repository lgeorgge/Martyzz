using AutoMapper;
using Martyzz.Domain.Models;
using Martyzz.Dtos;
using Martyzz.Mappings.Resolvers;

namespace Martyzz.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(D => D.BrandName, Opt => Opt.MapFrom(P => P.Brand.Name))
                .ForMember(D => D.CategoryName, Opt => Opt.MapFrom(P => P.ProductCategory.Name))
                .ForMember(D => D.PictureUrl, Opt => Opt.MapFrom<ProductImageUrlResolver>());
            CreateMap<Brand, BrandDto>();
            CreateMap<Category, CategoryDto>();
        }
    }
}

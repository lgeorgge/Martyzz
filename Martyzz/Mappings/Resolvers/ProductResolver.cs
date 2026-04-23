using AutoMapper;
using Martyzz.Domain.Models;
using Martyzz.Dtos;

namespace Martyzz.Mappings.Resolvers
{
    public class ProductImageUrlResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly IConfiguration _config;

        public ProductImageUrlResolver(IConfiguration configuration)
        {
            _config = configuration;
        }

        public string Resolve(
            Product source,
            ProductDto destination,
            string destMember,
            ResolutionContext context
        )
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return _config["ApiSettings:BaseUrl"] + source.PictureUrl;
            }
            return string.Empty;
        }
    }
}

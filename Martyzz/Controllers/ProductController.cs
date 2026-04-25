using AutoMapper;
using Martyzz.Domain.Common;
using Martyzz.Domain.Common.OrderBy;
using Martyzz.Domain.Models;
using Martyzz.Domain.Repo.Interfaces;
using Martyzz.Domain.Specifications.ProductSpecs;
using Martyzz.Dtos;
using Martyzz.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Martyzz.Controllers
{
    public class ProductController(IGenericRepository<Product> repo, IMapper mapper)
        : BaseApiController
    {
        private readonly IGenericRepository<Product> _repo = repo;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<ActionResult<GetAllResult<Product>>> GetProducts(
            int? page,
            int? pageSize,
            ProductSortBy? sortBy
        )
        {
            var specs = new ProductSpecs(null, sortBy, page, pageSize);

            var products = await _repo.GetAll(specs);

            var productDtos = _mapper.Map<List<ProductDto>>(products.Items);
            return Ok(
                new GetAllResult<ProductDto>(
                    Items: productDtos,
                    Total: products.Total,
                    Page: products.Page,
                    PageSize: products.PageSize,
                    HasMore: products.HasMore
                )
            );
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(Guid id)
        {
            var specs = new ProductSpecs(id);
            var product = await _repo.Get(specs);
            if (product == null)
                return NotFound(new ApiException(404));

            return Ok(_mapper.Map<ProductDto>(product));
        }
    }
}

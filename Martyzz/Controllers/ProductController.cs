using AutoMapper;
using Martyzz.Domain.Common;
using Martyzz.Domain.Models;
using Martyzz.Domain.Repo.Interfaces;
using Martyzz.Domain.Specifications.ProductSpecs;
using Martyzz.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Martyzz.Controllers
{
    public class ProductController(IGenericRepository<Product> repo, IMapper mapper)
        : BaseApiController
    {
        private readonly IGenericRepository<Product> _repo = repo;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<ActionResult<GetAllResult<Product>>> GetProducts(int? page, int? pageSize)
        {
            var pagination = new Pagination
            {
                Page = (page < 1 || page == null) ? 1 : page.Value,
                PageSize =
                    (pageSize < 1 || pageSize == null || pageSize > 100) ? 10 : pageSize.Value,
            };

            var specs = new ProductSpecs(null);

            var products = await _repo.GetAll(pagination: pagination, specs);

            var productDtos = _mapper.Map<List<ProductDto>>(products.Items);
            return Ok(
                new GetAllResult<ProductDto>(
                    Items: productDtos,
                    Total: products.Total,
                    Page: pagination.Page,
                    PageSize: pagination.PageSize,
                    HasMore: pagination.Page * pagination.PageSize < products.Total
                )
            );
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(Guid id)
        {
            var specs = new ProductSpecs(id);
            var product = await _repo.Get(specs);
            if (product == null)
                return NotFound();

            return Ok(_mapper.Map<ProductDto>(product));
        }
    }
}

using AutoMapper;
using Martyzz.Domain.Common;
using Martyzz.Domain.Models;
using Martyzz.Domain.Repo.Interfaces;
using Martyzz.Domain.Specifications;
using Martyzz.Domain.Specifications.BrandSpecs;
using Martyzz.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Martyzz.Controllers
{
    public class BrandController(IGenericRepository<Brand> repo, IMapper mapper) : BaseApiController
    {
        private readonly IGenericRepository<Brand> _repo = repo;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<ActionResult<List<Brand>>> GetBrands(int? page, int? pageSize)
        {
            page = (page < 1 || page == null) ? 1 : page;
            pageSize = (pageSize < 1 || pageSize == null || pageSize > 100) ? 10 : pageSize;

            Pagination pagination = new(page.Value, pageSize.Value);

            ISpecifications<Brand> specs = new BrandSpecs(null);
            var brandsResult = await _repo.GetAll(pagination, specs);

            var brandsDto = _mapper.Map<List<BrandDto>>(brandsResult.Items);

            GetAllResult<BrandDto> result = new(
                brandsDto,
                brandsResult.Total,
                pagination.Page,
                pagination.PageSize,
                pagination.Page * pagination.PageSize < brandsResult.Total
            );

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Brand>> GetBrandById(int id)
        {
            ISpecifications<Brand> specs = new BrandSpecs(id);
            var brand = await _repo.Get(specs);
            if (brand == null)
                return NotFound();

            return Ok(_mapper.Map<BrandDto>(brand));
        }
    }
}

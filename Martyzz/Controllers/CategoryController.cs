using AutoMapper;
using Martyzz.Domain.Common;
using Martyzz.Domain.Models;
using Martyzz.Domain.Repo.Interfaces;
using Martyzz.Domain.Specifications;
using Martyzz.Domain.Specifications.CategorySpecs;
using Martyzz.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Martyzz.Controllers
{
    public class CategoryController(IGenericRepository<Category> repo, IMapper mapper)
        : BaseApiController
    {
        private readonly IGenericRepository<Category> _repo = repo;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetCategories(int? page, int? pageSize)
        {
            page = (page < 1 || page == null) ? 1 : page;
            pageSize = (pageSize < 1 || pageSize == null || pageSize > 100) ? 10 : pageSize;

            Pagination pagination = new(page.Value, pageSize.Value);

            ISpecifications<Category> specs = new CategorySpecs(null);
            var categoriesResult = await _repo.GetAll(pagination, specs);

            var categoriesDto = _mapper.Map<List<CategoryDto>>(categoriesResult.Items);

            GetAllResult<CategoryDto> result = new(
                categoriesDto,
                categoriesResult.Total,
                pagination.Page,
                pagination.PageSize,
                pagination.Page * pagination.PageSize < categoriesResult.Total
            );

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            ISpecifications<Category> specs = new CategorySpecs(id);
            var category = await _repo.Get(specs);
            if (category == null)
                return NotFound();

            return Ok(_mapper.Map<CategoryDto>(category));
        }
    }
}

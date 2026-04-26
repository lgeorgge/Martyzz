using Martyzz.Domain.Models;
using Martyzz.Domain.Repo.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Martyzz.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _repo;
        private readonly ILogger<BasketController> _logger;

        public BasketController(IBasketRepository repo, ILogger<BasketController> logger)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Basket?>> GetBasketAsync(
            Guid id,
            CancellationToken cancellationToken
        )
        {
            if (id == Guid.Empty)
                return BadRequest("Invalid basket id.");

            var basket = await _repo.GetBasketAsync(id, cancellationToken);
            if (basket == null)
                return NotFound();

            return Ok(basket);
        }

        [HttpPut]
        public async Task<ActionResult<Basket?>> UpdateBasketAsync(
            [FromBody] Basket basket,
            CancellationToken cancellationToken
        )
        {
            if (basket == null)
                return BadRequest("Basket payload is required.");

            var updated = await _repo.UpdateBasketAsync(basket, cancellationToken);
            return Ok(updated);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteBasketAsync(
            Guid id,
            CancellationToken cancellationToken
        )
        {
            if (id == Guid.Empty)
                return BadRequest("Invalid basket id.");

            var deleted = await _repo.DeleteBasketAsync(id, cancellationToken);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}

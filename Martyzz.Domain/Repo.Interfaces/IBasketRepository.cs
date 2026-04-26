using Martyzz.Domain.Models;

namespace Martyzz.Domain.Repo.Interfaces
{
    public interface IBasketRepository
    {
        Task<Basket?> GetBasketAsync(Guid basketId, CancellationToken cancellationToken = default);
        Task<Basket?> UpdateBasketAsync(
            Basket basket,
            CancellationToken cancellationToken = default
        );
        Task<bool> DeleteBasketAsync(Guid basketId, CancellationToken cancellationToken = default);
    }
}

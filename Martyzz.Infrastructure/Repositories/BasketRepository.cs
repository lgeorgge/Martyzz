using System.Text.Json;
using Martyzz.Domain.Models;
using Martyzz.Domain.Repo.Interfaces;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace Martyzz.Infrastructure.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _redis;
        private readonly ILogger<BasketRepository> _logger;

        public BasketRepository(IConnectionMultiplexer redis, ILogger<BasketRepository> logger)
        {
            _redis = redis.GetDatabase();
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> DeleteBasketAsync(
            Guid basketId,
            CancellationToken cancellationToken = default
        )
        {
            if (basketId == Guid.Empty)
                throw new ArgumentException("basketId must be provided", nameof(basketId));

            try
            {
                return await _redis.KeyDeleteAsync(basketId.ToString());
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                _logger.LogInformation(
                    "Delete operation was canceled for basket {BasketId}",
                    basketId
                );
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete basket {BasketId}", basketId);
                throw;
            }
        }

        public async Task<Basket?> GetBasketAsync(
            Guid basketId,
            CancellationToken cancellationToken = default
        )
        {
            if (basketId == Guid.Empty)
                return null;

            try
            {
                var value = await _redis.StringGetAsync(basketId.ToString());
                return value.IsNullOrEmpty
                    ? null
                    : JsonSerializer.Deserialize<Basket>(value.ToString());
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get basket {BasketId}", basketId);
                throw;
            }
        }

        public async Task<Basket?> UpdateBasketAsync(
            Basket basket,
            CancellationToken cancellationToken = default
        )
        {
            ArgumentNullException.ThrowIfNull(basket);

            try
            {
                var payload = JsonSerializer.Serialize(basket);
                var result = await _redis.StringSetAsync(
                    basket.Id.ToString(),
                    payload,
                    TimeSpan.FromDays(2)
                );

                if (!result)
                {
                    _logger.LogWarning("Failed to set basket {BasketId} in redis", basket.Id);
                }

                return await GetBasketAsync(basket.Id, cancellationToken);
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating or updating basket {BasketId}", basket.Id);
                throw;
            }
        }
    }
}

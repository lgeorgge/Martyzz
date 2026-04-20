namespace Martyzz.Domain.Common
{
    public record GetAllResult<T>(
        IEnumerable<T> Items,
        int Total,
        int Page,
        int PageSize,
        bool HasMore
    );
}

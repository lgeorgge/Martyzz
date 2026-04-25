using System.Numerics;

namespace Martyzz.Domain.Common;

public record PaginatedResult<T>(
    IEnumerable<T> Items,
    int Total,
    int Page,
    int PageSize,
    bool HasMore
);

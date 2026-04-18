using System.Numerics;

namespace Martyzz.Domain.Common;

public record PagedResult<T>(List<T> Items, int Total);

using System.Linq.Expressions;
using RiverSong.Shared.Application.Models;
using RiverSong.Shared.Domain.Common;

namespace RiverSong.Shared.Application.Contracts;

public interface IAsyncRepository<T> where T : EntityBase
{
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? whereExpression = null);
    Task<Page<T>> GetPageAsync(int page, int pageSize, Expression<Func<T, bool>>? whereExpression = null);

    Task<T?> GetByIdAsync(Guid id);

    Task<T> AddAsync(T entity);
    Task<T> UpdatedAsync(T entity);
    Task RemoveAsync(T entity);
    Task RemoveByIdAsync(Guid id);
}
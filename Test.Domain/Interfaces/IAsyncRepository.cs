using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Test.Domain.Dto;
using Test.Domain.Entities;

namespace Test.Domain.Interfaces
{
   public interface IAsyncRepository<IEntity,T> where IEntity : BaseEntity<T>
    {
        Task<IEntity> GetByIdAsync(T id, CancellationToken cancellationToken = default);
        Task<IEntity> GetExpressionAsync(Expression<Func<IEntity, bool>> predicate);
        Task<bool> AnyExpressionAsync(Expression<Func<IEntity, bool>> predicate);
        Task<IReadOnlyList<IEntity>> ListAllAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<IEntity>> ListAllAsync(Expression<Func<IEntity, bool>> predicate, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<IEntity>> ListAllAsync(Expression<Func<IEntity, bool>> predicate, PaginationDto pagination, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<IEntity>> ListAllAsync(Expression<Func<IEntity, bool>> predicate, PaginationDto pagination, CancellationToken cancellationToken = default, params Expression<Func<IEntity, object>>[] children);
        Task<IEntity> AddAsync(IEntity entity, CancellationToken cancellationToken = default);
        Task AddRangeAsync(IList<IEntity> entities, CancellationToken cancellationToken = default);
        Task UpdateAsync(IEntity entity, CancellationToken cancellationToken = default);
        Task UpdateRangeAsync(IList<IEntity> entities, CancellationToken cancellationToken = default);
        Task DeleteAsync(IEntity entity, CancellationToken cancellationToken = default);
        Task DeleteRangeAsync(IList<IEntity> entities, CancellationToken cancellationToken = default);
        IQueryable<IEntity> Include(params Expression<Func<IEntity, object>>[] children);
         Task<IDbContextTransaction> BeginTransactionAsync();
    }
}

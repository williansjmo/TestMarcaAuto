using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Test.Domain.Dto;
using Test.Domain.Entities;
using Test.Domain.Interfaces;
using Test.Infrastructure.Extensions;
using Test.Infrastructure.Persistence;

namespace Test.Infrastructure.Repository
{
    public class AsyncRepository<IEntity, T> : IAsyncRepository<IEntity, T> where IEntity : BaseEntity<T>
    {
        private readonly TestDbContext _dbContext;
        protected DbSet<IEntity> DbSet { get; set; }

        public AsyncRepository(TestDbContext dbContext)
        {
            _dbContext = dbContext;
            DbSet = dbContext.Set<IEntity>();
        }

        public async Task<IEntity> AddAsync(IEntity entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.Set<IEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task AddRangeAsync(IList<IEntity> entities, CancellationToken cancellationToken = default)
        {
            using (var tr = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    await _dbContext.Set<IEntity>().AddRangeAsync(entities);
                    await _dbContext.SaveChangesAsync();
                    tr.Commit();
                }
                catch (Exception ex)
                {
                    tr.Rollback();
                }
            }
        }

        public async Task<bool> AnyExpressionAsync(Expression<Func<IEntity, bool>> predicate) => await _dbContext.Set<IEntity>().AnyAsync(predicate);

        public async Task DeleteAsync(IEntity entity, CancellationToken cancellationToken = default)
        {
            _dbContext.Set<IEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public Task DeleteRangeAsync(IList<IEntity> entities, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEntity> GetByIdAsync(T id, CancellationToken cancellationToken = default)
        {
            var keyValues = new object[] { id };
            return await _dbContext.Set<IEntity>().FindAsync(keyValues, cancellationToken);
        }

        public async Task<IEntity> GetExpressionAsync(Expression<Func<IEntity, bool>> predicate) => await _dbContext.Set<IEntity>().Where(predicate).FirstOrDefaultAsync();

        public IQueryable<IEntity> Include(params Expression<Func<IEntity, object>>[] children)
        {
            var table = _dbContext.Set<IEntity>();
            return table.IncludeMultiple(children);
        }

        public async Task<IReadOnlyList<IEntity>> ListAllAsync(CancellationToken cancellationToken = default) => await _dbContext.Set<IEntity>().ToListAsync(cancellationToken);

        public async Task<IReadOnlyList<IEntity>> ListAllAsync(Expression<Func<IEntity, bool>> predicate, CancellationToken cancellationToken = default) => await _dbContext.Set<IEntity>().Where(predicate).ToListAsync(cancellationToken);
        public async Task<IReadOnlyList<IEntity>> ListAllAsync(Expression<Func<IEntity, bool>> predicate, PaginationDto pagination, CancellationToken cancellationToken = default)
        {
            var table = _dbContext.Set<IEntity>().Where(predicate);
            pagination.Count = await table.CountAsync();
            return await table.Paginate(pagination).ToListAsync(cancellationToken);
        }
        public async Task<IReadOnlyList<IEntity>> ListAllAsync(Expression<Func<IEntity, bool>> predicate, PaginationDto pagination, CancellationToken cancellationToken = default, params Expression<Func<IEntity, object>>[] children)
        {
            var table = _dbContext.Set<IEntity>().Where(predicate);
            pagination.Count = await table.CountAsync();
            return await table.Paginate(pagination).IncludeMultiple(children).ToListAsync(cancellationToken);
        }

        public async Task UpdateAsync(IEntity entity, CancellationToken cancellationToken = default)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public Task UpdateRangeAsync(IList<IEntity> entities, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _dbContext.Database.BeginTransactionAsync();
        }
    }
}

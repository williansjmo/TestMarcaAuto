using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Test.Domain.Entities;

namespace Test.Domain.Interfaces
{
    public interface IGenericService<TEntity, T>
    {
        Task<CarBrands> AddAsync(TEntity entity);
        Task<CarBrands> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(T Id);
        Task<List<CarBrands>> GetAllAsync();
        Task<CarBrands> GetAsync(T Id);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}

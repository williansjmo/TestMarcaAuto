using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Test.Domain.Entities;
using Test.Domain.Interfaces;

namespace Test.Domain.Services
{
    public class CarBrandsService : IGenericService<CarBrands,Guid>
    {
        private readonly IAsyncRepository<CarBrands, Guid> _repository;
        public CarBrandsService(IAsyncRepository<CarBrands,Guid> repository)
        {
            _repository = repository;
        }

        public Task<CarBrands> AddAsync(CarBrands entity)
        {
            return _repository.AddAsync(entity);
        }

        public async Task<bool> AnyAsync(Expression<Func<CarBrands, bool>> predicate) => await _repository.AnyExpressionAsync(predicate);

        public async Task<IDbContextTransaction> BeginTransactionAsync()
      => await _repository.BeginTransactionAsync();

        public async Task<bool> DeleteAsync(Guid Id)
        {
            try
            {
                var carBrand = await GetAsync(Id);
                await _repository.DeleteAsync(carBrand);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<CarBrands>> GetAllAsync()
        {
            return (List<CarBrands>)await _repository.ListAllAsync();
        }

        public async Task<CarBrands> GetAsync(Guid Id)
       => await _repository.GetByIdAsync(Id);

        public async Task<CarBrands> UpdateAsync(CarBrands entity)
        {
            await _repository.UpdateAsync(entity);
            return entity;
        }
    }
 
}

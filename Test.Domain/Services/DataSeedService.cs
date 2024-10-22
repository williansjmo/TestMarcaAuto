using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Domain.Entities;
using Test.Domain.Interfaces;

namespace Test.Domain.Services
{
    public class DataSeedService
    {
        private readonly IGenericService<CarBrands, Guid> _carBrandsService;

        public DataSeedService(IGenericService<CarBrands, Guid> carBrandsService)
        {
            _carBrandsService = carBrandsService;
        }
        public async Task AddCarBrand()
        {
            List<CarBrands> carBrands = new List<CarBrands>()
            {
                new CarBrands() { Id = Guid.NewGuid(), Brand = "BMW" },
                 new CarBrands() { Id = Guid.NewGuid(), Brand = "Ford" },
                  new CarBrands() { Id = Guid.NewGuid(), Brand = "Geely" },
                   new CarBrands() { Id = Guid.NewGuid(), Brand = "Honda" },
                    new CarBrands() { Id = Guid.NewGuid(), Brand = "Mazda" },
            };
            using (var tr = await _carBrandsService.BeginTransactionAsync())
            {
                try
                {
                    foreach (var carBrand in carBrands)
                    {
                        if(! await _carBrandsService.AnyAsync(a=> a.Brand == carBrand.Brand))
                            await _carBrandsService.AddAsync(carBrand);
                    }
                    tr.Commit();
                }
                catch (Exception ex)
                {
                    tr.Rollback();
                }
            }
        }
    }
}

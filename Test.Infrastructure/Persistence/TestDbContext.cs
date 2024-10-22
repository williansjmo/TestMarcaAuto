using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Test.Domain.Entities;

namespace Test.Infrastructure.Persistence
{
    public class TestDbContext : DbContext
    {
        public DbSet<CarBrands> CarBrands { get; set; }
        public TestDbContext(DbContextOptions<TestDbContext> dbContext) : base(dbContext)
        {
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using Newtonsoft.Json;
using System;
using Test.Api.Controllers;
using Test.Domain.Entities;
using Test.Domain.Interfaces;
using Test.Domain.Services;
using Test.Infrastructure.Persistence;
using Test.Infrastructure.Repository;
using TestProject1.Api;

namespace TestProject1
{
    public class UnitTest1 : IClassFixture<WebApplicationFactory<Program>>
    {
        CarBrandApi api;
        private readonly WebApplicationFactory<Program> _factory;
        public UnitTest1(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async void Test1()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("https://localhost:7136/api/CarBrands");
            response.EnsureSuccessStatusCode();
            var context = await response.Content.ReadAsStringAsync();
            var responseModel = JsonConvert.DeserializeObject<List<CarBrands>>(context);
            Assert.NotNull(responseModel);
            Assert.Equal(5, responseModel.Count);
        }
    }
}
using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using WarehouseBackend.Core.AutoMapper;
using WarehouseBackend.Core.Controllers;
using WarehouseBackend.Core.Services;
using WarehouseBackend.DataAccess;
using WarehouseBackend.Models.ApiRequests;
using Xunit;

namespace WarehouseBackend.UnitTests
{
    public class WarehouseControllerTests
    {
        public WarehouseControllerTests()
        {
            _warehouseController = GetWarehouseController();
        }

        [Fact]
        public async void AddContainerTestSuccess()
        {
            var date = DateTime.Now;

            var request = new AddContainerRequest
            {
                ClientId = "TestClient",
                Type = "Type",
                Notes = "Notes",
                DateExpectedToShip = date
            };

            var result = await _warehouseController.AddContainerAsync(request).ConfigureAwait(false);

            Assert.Equal(result.Value.ClientId, request.ClientId);
            Assert.Equal(result.Value.Type, request.Type);
            Assert.Equal(result.Value.Notes, request.Notes);
            Assert.True(date.Equals(result.Value.DateExpectedToShip));
        }

        private ILogger<T> GetLogger<T>()
        {
            var mockLogger = new Mock<ILogger<T>>();

            return mockLogger.Object;
        }

        private IMapper GetMapper()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });

            return mapperConfiguration.CreateMapper();
        }

        private WarehouseDbContext GetWarehouseInMemoryDbContext()
        {
            var dbContextOptions = new DbContextOptionsBuilder<WarehouseDbContext>()
                .UseInMemoryDatabase(databaseName: "expenses")
                .Options;

            return new WarehouseDbContext(dbContextOptions);
        }

        private IWarehouseDataAccess GetDataAccess()
        {
            return new WarehouseDataAccess(GetWarehouseInMemoryDbContext(), GetLogger<WarehouseDataAccess>());
        }

        private WarehouseController GetWarehouseController()
        {
            var mapper = GetMapper();
            var dataAccess = GetDataAccess();
            var warehouseService = new WarehouseService(dataAccess, mapper, GetLogger<WarehouseService>());

            var controller = new WarehouseController(GetLogger<WarehouseController>(), warehouseService);

            return controller;
        }

        private readonly WarehouseController _warehouseController;
    }
}

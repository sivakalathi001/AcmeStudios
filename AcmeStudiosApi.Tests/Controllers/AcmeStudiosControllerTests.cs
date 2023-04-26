using AcmeStudiosApi.Controllers;
using AcmeStudiosApi.Entities;
using AcmeStudiosApi.Models;
using AcmeStudiosApi.Profiles;
using AcmeStudiosApi.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace AcmeStudiosApi.Tests.Controllers
{
    public class AcmeStudiosControllerTests
    {

        private readonly AcmeStudiosController _acmeStudiosController;

        public AcmeStudiosControllerTests()
        {

            var studioItem1 = new StudioItem()
            {
                StudioItemId = 5,
                Acquired = new DateTime(2017, 1, 19, 4, 0, 15),
                Sold = new DateTime(2017, 2, 19, 4, 0, 15),
                Name = "Super Man",
                Description = "Childrens movie",
                SerialNumber = "SM1000",
                Price = 10,
                SoldFor = 15,
                Eurorack = true,
                StudioItemTypeId = 2
            };
            var studioItem2 = new StudioItem()
            {
                StudioItemId = 6,
                Acquired = new DateTime(2017, 1, 19, 4, 0, 15),
                Sold = new DateTime(2017, 2, 19, 4, 0, 15),
                Name = "Spider Man",
                Description = "Childrens movie",
                SerialNumber = "SM1001",
                Price = 10,
                SoldFor = 15,
                Eurorack = true,
                StudioItemTypeId = 3
            };

            var studioItems = new List<StudioItem> { studioItem1, studioItem2 };

            var mapperConfiguration = new MapperConfiguration(
               cfg => cfg.AddProfile<AcmeStudiosProfile>());

            var mapper = new Mapper(mapperConfiguration);

            ServiceResponse<IEnumerable<GetStudioItemDto>> serviceResponse = new()
            {
                Data = mapper.Map<IEnumerable<GetStudioItemDto>>(studioItems),
                Message = "Here's all the item in Studio Types",
                Success = true
            };

            var interfaceWithDatabaseMock = new Mock<IInterfaceWithDatabase>();
            interfaceWithDatabaseMock
                .Setup(s => s.GetAllStudioItemsAsync()).Returns(Task.FromResult(serviceResponse));


            var logger = Mock.Of<ILogger<AcmeStudiosController>>();

            _acmeStudiosController = new AcmeStudiosController(logger, interfaceWithDatabaseMock.Object);

        }


        [Fact]
        public async Task GetStudioItems_GetAction_MustReturnOkObjectResult()
        {
            /// Arrange

            /// Act
            var results = await _acmeStudiosController.GetStudioItems();

            /// Assert
            Assert.IsType<OkObjectResult>(results);

        }

        [Fact]
        public async Task GetStudioItems_GetAction_MustReturnNumberOfAssignedStudioItems()
        {
            ///Arrange


            ///Act  
            var results = await _acmeStudiosController.GetStudioItems();

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(results);

            Assert.Equal(2,
                   (((ServiceResponse<IEnumerable<GetStudioItemDto>>)((OkObjectResult)actionResult).Value).Data).Count());
        }
    }
}
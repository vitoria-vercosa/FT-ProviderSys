using FT_ProviderSys.Controllers;
using FT_ProviderSys.Services.Interfaces;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;

namespace FT_ProviderSys.UnitTest
{
    public class ProviderUnitTest
    {
        private readonly ProviderController _providerController;
        private readonly Mock<IProviderService> _mockProviderService;

        public ProviderUnitTest()
        {
            _mockProviderService = new Mock<IProviderService>();
            _providerController = new ProviderController(_mockProviderService.Object);
        }

        [Fact(DisplayName = "GetAll Should Return 200Ok")]
        [Trait("Provider","GetAll")]
        public async Task GetAll_ShouldReturn200Ok()
        {
            // Act
            var response = await _providerController.GetAll();
            var statusCode = (response.Result as ObjectResult).StatusCode;

            // Assert
            Assert.Equal(200, statusCode);
        }
    }
}

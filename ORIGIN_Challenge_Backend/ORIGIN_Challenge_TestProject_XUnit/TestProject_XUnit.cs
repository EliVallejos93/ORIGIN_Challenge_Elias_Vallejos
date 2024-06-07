using Microsoft.AspNetCore.Mvc;
using Moq;
using ORIGIN_Challenge_API.Controllers;
using ORIGIN_Challenge_API.Services;

namespace TestProject_XUnit
{
    public class TarjetasControllerTests
    {
        private readonly Mock<ITarjetasService> _mockTarjetasService;
        private readonly TarjetasController _controller;

        public TarjetasControllerTests()
        {
            _mockTarjetasService = new Mock<ITarjetasService>();
            _controller = new TarjetasController(_mockTarjetasService.Object);
        }

        [Fact]
        public async Task InsertarDatosAleatorios_ReturnsOk()
        {
            // Arrange
            _mockTarjetasService.Setup(service => service.AgregarTarjetaAleatoria());

            // Act
            var result = await _controller.InsertarDatosAleatorios();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task VerificarTarjeta_ReturnsOk()
        {
            // Arrange
            string numeroTarjeta = "1234567890";
            _mockTarjetasService.Setup(service => service.VerificarTarjeta(numeroTarjeta));
            _mockTarjetasService.Setup(service => service.ResetearConteoPin());

            // Act
            var result = await _controller.VerificarTarjeta(numeroTarjeta);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task VerificarTarjeta_ReturnsNotFound()
        {
            // Arrange
            string numeroTarjeta = "1234567890";
            _mockTarjetasService.Setup(service => service.VerificarTarjeta(numeroTarjeta))
                .Throws(new KeyNotFoundException());

            // Act
            var result = await _controller.VerificarTarjeta(numeroTarjeta);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task VerificarTarjeta_ReturnsLocked()
        {
            // Arrange
            string numeroTarjeta = "1234567890";
            _mockTarjetasService.Setup(service => service.VerificarTarjeta(numeroTarjeta))
                .Throws(new InvalidOperationException("Account locked"));

            // Act
            var result = await _controller.VerificarTarjeta(numeroTarjeta);

            // Assert
            var lockedResult = (IActionResult)result as ObjectResult;
            Assert.NotNull(lockedResult);
            Assert.Equal(423, lockedResult.StatusCode);
            Assert.Equal("Account locked", lockedResult.Value);
        }

        [Fact]
        public async Task VerificarPin_ReturnsOk()
        {
            // Arrange
            string numeroTarjeta = "1234567890";
            string numeroPin = "1234";
            _mockTarjetasService.Setup(service => service.VerificarPin(numeroTarjeta, numeroPin));

            // Act
            var result = await _controller.VerificarPin(numeroTarjeta, numeroPin);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task VerificarPin_ReturnsUnauthorized()
        {
            // Arrange
            string numeroTarjeta = "1234567890";
            string numeroPin = "1234";
            _mockTarjetasService.Setup(service => service.VerificarPin(numeroTarjeta, numeroPin))
                .Throws(MiExcepcion.UnauthorizedException("Unauthorized"));

            // Act
            var result = await _controller.VerificarPin(numeroTarjeta, numeroPin);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal(401, unauthorizedResult.StatusCode);
            Assert.Equal("Unauthorized", unauthorizedResult.Value);
        }
    }
}
using System.Net;
using APBD_Project.Controllers;
using APBD_Project.Models;
using APBD_Project.Repositories;
using APBD_Project.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace APBD_Tests;

public class RevenueControllerTests
    {
        private readonly RevenueController _controller;
        private readonly Mock<IPaymentRepository> _mockPaymentRepository;
        private readonly Mock<IContractRepository> _mockContractRepository;
        private readonly CurrencyService _mockCurrencyService;
        private readonly RevenueService _revenueService;

        public RevenueControllerTests()
        {
            _mockPaymentRepository = new Mock<IPaymentRepository>();
            _mockContractRepository = new Mock<IContractRepository>();

            var mockHttpMessageHandler = new MockHttpMessageHandler(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{\"rates\": {\"USD\": 1}}")
            });
            var httpClient = new HttpClient(mockHttpMessageHandler);

            _mockCurrencyService = new CurrencyService(httpClient);

            _revenueService = new RevenueService(
                _mockPaymentRepository.Object,
                _mockContractRepository.Object,
                _mockCurrencyService
            );

            _controller = new RevenueController(_revenueService);
        }

        [Fact]
        public async Task GetCurrentRevenue_ReturnsOkResult_WithRevenue()
        {
            var currency = "USD";
            var expectedRevenue = 1000m;
            _mockPaymentRepository.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(new List<Payment> { new Payment { Amount = expectedRevenue } });

            var result = await _controller.GetCurrentRevenue(currency);

            var actionResult = Assert.IsType<ActionResult<decimal>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<decimal>(okResult.Value);
            Assert.Equal(expectedRevenue, returnValue);
        }

        [Fact]
        public async Task GetPredictedRevenue_ReturnsOkResult_WithRevenue()
        {
            var currency = "USD";
            var expectedRevenue = 1500m;
            _mockContractRepository.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(new List<Contract> { new Contract { Price = expectedRevenue } });
            _mockPaymentRepository.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(new List<Payment>());

            var result = await _controller.GetPredictedRevenue(currency);

            var actionResult = Assert.IsType<ActionResult<decimal>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<decimal>(okResult.Value);
            Assert.Equal(expectedRevenue, returnValue);
        }

        [Fact]
        public async Task GetCurrentRevenueForProduct_ReturnsOkResult_WithRevenue()
        {
            var softwareId = 1;
            var currency = "USD";
            var expectedRevenue = 500m;
            _mockPaymentRepository.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(new List<Payment>
                {
                    new Payment { Amount = expectedRevenue, Contract = new Contract { SoftwareId = softwareId } }
                });

            var result = await _controller.GetCurrentRevenueForProduct(softwareId, currency);

            var actionResult = Assert.IsType<ActionResult<decimal>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<decimal>(okResult.Value);
            Assert.Equal(expectedRevenue, returnValue);
        }

        [Fact]
        public async Task GetPredictedRevenueForProduct_ReturnsOkResult_WithRevenue()
        {
            var softwareId = 1;
            var currency = "USD";
            var expectedRevenue = 750m;
            _mockContractRepository.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(new List<Contract>
                {
                    new Contract { Price = expectedRevenue, SoftwareId = softwareId }
                });
            _mockPaymentRepository.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(new List<Payment>
                {
                    new Payment { Amount = expectedRevenue, Contract = new Contract { SoftwareId = softwareId } }
                });

            var result = await _controller.GetPredictedRevenueForProduct(softwareId, currency);

            var actionResult = Assert.IsType<ActionResult<decimal>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<decimal>(okResult.Value);
            Assert.Equal(expectedRevenue * 2, returnValue);
        }
    }
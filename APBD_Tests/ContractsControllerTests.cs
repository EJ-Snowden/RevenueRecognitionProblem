using APBD_Project.Controllers;
using APBD_Project.Models;
using APBD_Project.Repositories;
using APBD_Project.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace APBD_Tests;

public class ContractsControllerTests
{
    private readonly ContractsController _controller;
    private readonly Mock<IContractRepository> _mockContractRepository;
    private readonly Mock<IClientRepository> _mockClientRepository;
    private readonly Mock<ISoftwareRepository> _mockSoftwareRepository;
    private readonly Mock<IPaymentRepository> _mockPaymentRepository;
    private readonly ContractService _contractService;

    public ContractsControllerTests()
    {
        _mockContractRepository = new Mock<IContractRepository>();
        _mockClientRepository = new Mock<IClientRepository>();
        _mockSoftwareRepository = new Mock<ISoftwareRepository>();
        _mockPaymentRepository = new Mock<IPaymentRepository>();

        _contractService = new ContractService(
            _mockContractRepository.Object,
            _mockClientRepository.Object,
            _mockSoftwareRepository.Object,
            _mockPaymentRepository.Object
        );

        _controller = new ContractsController(_contractService);
    }

    [Fact]
    public async Task GetContractById_ReturnsOkResult_WhenContractExists()
    {
        var contractId = 1;
        _mockContractRepository.Setup(repo => repo.GetByIdAsync(contractId))
            .ReturnsAsync(new Contract { ContractId = contractId });

        var result = await _controller.GetContractById(contractId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<Contract>(okResult.Value);
        Assert.Equal(contractId, returnValue.ContractId);
    }

    [Fact]
    public async Task GetContractById_ReturnsNotFound_WhenContractDoesNotExist()
    {
        var contractId = 1;
        _mockContractRepository.Setup(repo => repo.GetByIdAsync(contractId))
            .ThrowsAsync(new KeyNotFoundException("No such contract was found"));

        var result = await _controller.GetContractById(contractId);

        Assert.IsType<NotFoundResult>(result);
    }
}
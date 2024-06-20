using APBD_Project.Controllers;
using APBD_Project.Models;
using APBD_Project.Repositories;
using APBD_Project.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace APBD_Tests;

public class ClientsControllerTests
{
    private readonly ClientsController _controller;
    private readonly Mock<IClientRepository> _mockClientRepository;
    private readonly ClientService _clientService;

    public ClientsControllerTests()
    {
        _mockClientRepository = new Mock<IClientRepository>();
        _clientService = new ClientService(_mockClientRepository.Object);
        _controller = new ClientsController(_clientService);
    }

    [Fact]
    public async Task GetClientById_ReturnsOkResult_WhenClientExists()
    {
        var clientId = 1;
        _mockClientRepository.Setup(repo => repo.GetByIdAsync(clientId))
            .ReturnsAsync(new Client { ClientId = clientId, FirstName = "Test Client" });

        var result = await _controller.GetClient(clientId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<Client>(okResult.Value);
        Assert.Equal(clientId, returnValue.ClientId);
    }

    [Fact]
    public async Task GetClientById_ReturnsNotFound_WhenClientDoesNotExist()
    {
        var clientId = 1;
        _mockClientRepository.Setup(repo => repo.GetByIdAsync(clientId))
            .ReturnsAsync((Client)null);

        var result = await _controller.GetClient(clientId);

        Assert.IsType<NotFoundResult>(result);
    }
}
using APBD_Project.Controllers;
using APBD_Project.Models;
using APBD_Project.Repositories;
using APBD_Project.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace APBD_Tests;

public class SoftwareControllerTests
{
    private readonly SoftwareController _controller;
    private readonly Mock<ISoftwareRepository> _mockSoftwareRepository;
    private readonly SoftwareService _softwareService;

    public SoftwareControllerTests()
    {
        _mockSoftwareRepository = new Mock<ISoftwareRepository>();
        _softwareService = new SoftwareService(_mockSoftwareRepository.Object);
        _controller = new SoftwareController(_softwareService);
    }

    [Fact]
    public async Task GetSoftwareById_ReturnsOkResult_WhenSoftwareExists()
    {
        var softwareId = 1;
        _mockSoftwareRepository.Setup(repo => repo.GetByIdAsync(softwareId))
            .ReturnsAsync(new Software { SoftwareId = softwareId, Name = "Test Software" });

        var result = await _controller.GetSoftwareById(softwareId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<Software>(okResult.Value);
        Assert.Equal(softwareId, returnValue.SoftwareId);
    }

    [Fact]
    public async Task GetSoftwareById_ReturnsNotFound_WhenSoftwareDoesNotExist()
    {
        var softwareId = 1;
        _mockSoftwareRepository.Setup(repo => repo.GetByIdAsync(softwareId))
            .ThrowsAsync(new KeyNotFoundException("No such software was found"));

        var result = await _controller.GetSoftwareById(softwareId);

        Assert.IsType<NotFoundResult>(result);
    }
}
using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Empresa.Application.UseCases.Interfaces;
using Empresa.BFF.Controllers;
using Empresa.Domain.Entidades;
using Empresa.BFF.Dtos.Leads;

namespace Empresa.BFF.ControllersTest.Leads
{
    public class LeadsControllerTests
    {
        private readonly Mock<IGetLeadsByStatus> _mockGetLeadsByStatus;
        private readonly Mock<IUpdateLeadStatus> _mockUpdateLeadStatus;
        private readonly LeadsController _controller;

        public LeadsControllerTests()
        {
            _mockGetLeadsByStatus = new Mock<IGetLeadsByStatus>();
            _mockUpdateLeadStatus = new Mock<IUpdateLeadStatus>();
            _controller = new LeadsController(_mockGetLeadsByStatus.Object, _mockUpdateLeadStatus.Object);
        }

        [Fact]
        public async Task Get_WithInvalidIdStatus_ReturnsBadRequest()
        {
            int invalidIdStatus = 0;

            var result = await _controller.Get(invalidIdStatus);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Get_WithValidIdStatus_ReturnsOkResultWithLeads()
        {
            int validIdStatus = 1;
            var expectedLeads = new List<Lead> { /* ... populate with test leads ... */ };
            _mockGetLeadsByStatus.Setup(service => service.ExecuteAsync(validIdStatus)).ReturnsAsync(expectedLeads);

            var result = await _controller.Get(validIdStatus);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedLeads = Assert.IsType<List<Lead>>(okResult.Value);
            Assert.Equal(expectedLeads, returnedLeads);
            _mockGetLeadsByStatus.Verify(service => service.ExecuteAsync(validIdStatus), Times.Once);
        }

        [Fact]
        public async Task UpdateStatus_WithValidRequest_ReturnsOkResult()
        {
            int validId = 1;
            var request = new UpdateStatusRequest(true);
            _mockUpdateLeadStatus.Setup(service => service.ExecuteAsync(validId, request.Accepted)).Returns(Task.CompletedTask);

            var result = await _controller.UpdateStatus(validId, request);

            Assert.IsType<OkResult>(result);
            _mockUpdateLeadStatus.Verify(service => service.ExecuteAsync(validId, request.Accepted), Times.Once);
        }

        [Fact]
        public async Task UpdateStatus_WithException_ReturnsBadRequest()
        {
            int validId = 1;
            var request = new UpdateStatusRequest(true);
            var exceptionMessage = "An error occurred";
            _mockUpdateLeadStatus.Setup(service => service.ExecuteAsync(validId, request.Accepted)).ThrowsAsync(new Exception(exceptionMessage));

            var result = await _controller.UpdateStatus(validId, request);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(exceptionMessage, badRequestResult.Value);
            _mockUpdateLeadStatus.Verify(service => service.ExecuteAsync(validId, request.Accepted), Times.Once);
        }
    }
}
using Empresa.Application.UseCases;
using Empresa.Domain.Adapters;
using Moq;
using Xunit;

namespace Empresa.Application.UseCasesTest.Leads
{
    public class UpdateLeadStatusTests
    {
        private readonly Mock<ILeadsRepository> _mockLeadsRepository;
        private readonly UpdateLeadStatus _updateLeadStatus;

        public UpdateLeadStatusTests()
        {
            _mockLeadsRepository = new Mock<ILeadsRepository>();
            _updateLeadStatus = new UpdateLeadStatus(_mockLeadsRepository.Object);
        }

        [Fact]
        public async Task ExecuteAsync_CallsUpdateLeadStatusOnRepository_WithCorrectParameters()
        {
            int testId = 1;
            bool testAccepted = true;

            _mockLeadsRepository.Setup(repo => repo.UpdateLeadStatus(testId, testAccepted))
                .Returns(Task.CompletedTask)
                .Verifiable();

            await _updateLeadStatus.ExecuteAsync(testId, testAccepted);

            _mockLeadsRepository.Verify(repo => repo.UpdateLeadStatus(testId, testAccepted), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ThrowsException_WhenRepositoryThrowsException()
        {
            int testId = 1;
            bool testAccepted = true;
            var exception = new Exception("Test exception");

            _mockLeadsRepository.Setup(repo => repo.UpdateLeadStatus(testId, testAccepted))
                .ThrowsAsync(exception);

            var ex = await Assert.ThrowsAsync<Exception>(() => _updateLeadStatus.ExecuteAsync(testId, testAccepted));
            Assert.Equal("Test exception", ex.Message);
        }

    }
}
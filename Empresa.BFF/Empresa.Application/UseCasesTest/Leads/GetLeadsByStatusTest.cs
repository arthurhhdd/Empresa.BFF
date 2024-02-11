using Empresa.Application.UseCases;
using Empresa.Domain.Adapters;
using Empresa.Domain.Entidades;
using Moq;
using Xunit;

public class GetLeadsByStatusTests
{
    private readonly Mock<ILeadsRepository> _mockLeadsRepository;
    private readonly GetLeadsByStatus _getLeadsByStatus;

    public GetLeadsByStatusTests()
    {
        _mockLeadsRepository = new Mock<ILeadsRepository>();
        _getLeadsByStatus = new GetLeadsByStatus(_mockLeadsRepository.Object);
    }

    [Fact]
    public async Task ExecuteAsync_WithValidStatusId_ReturnsLeads()
    {
        // Arrange
        int statusId = 1;
        var leads = new List<Lead>
        {
            new Lead { Price = 400 }, 
            new Lead { Price = 600 }
        };
        _mockLeadsRepository.Setup(repo => repo.GetLeadsByStatus(statusId)).ReturnsAsync(leads);

        var result = await _getLeadsByStatus.ExecuteAsync(statusId);

        _mockLeadsRepository.Verify(repo => repo.GetLeadsByStatus(statusId), Times.Once);
        Assert.Equal(2, result.Count);
        Assert.Equal(400, result[0].Price);
        Assert.Equal(540, result[1].Price);
    }

    [Fact]
    public async Task ExecuteAsync_WithInvalidStatusId_ReturnsEmptyList()
    {
        int statusId = -1;
        var leads = new List<Lead>();
        _mockLeadsRepository.Setup(repo => repo.GetLeadsByStatus(statusId)).ReturnsAsync(leads);

        var result = await _getLeadsByStatus.ExecuteAsync(statusId);

        _mockLeadsRepository.Verify(repo => repo.GetLeadsByStatus(statusId), Times.Once);
        Assert.Empty(result);
    }

    [Fact]
    public async Task ExecuteAsync_WhenRepositoryThrowsException_PropagatesException()
    {
        int statusId = 1;
        _mockLeadsRepository.Setup(repo => repo.GetLeadsByStatus(statusId)).ThrowsAsync(new System.Exception("Repository failure"));

        await Assert.ThrowsAsync<System.Exception>(() => _getLeadsByStatus.ExecuteAsync(statusId));
        _mockLeadsRepository.Verify(repo => repo.GetLeadsByStatus(statusId), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_WithNoPriceDiscount_ReturnsLeadsWithOriginalPrice()
    {
        int statusId = 1;
        var leads = new List<Lead>
        {
            new Lead { Price = 100 }, 
            new Lead { Price = 300 }  
        };
        _mockLeadsRepository.Setup(repo => repo.GetLeadsByStatus(statusId)).ReturnsAsync(leads);

        var result = await _getLeadsByStatus.ExecuteAsync(statusId);

        _mockLeadsRepository.Verify(repo => repo.GetLeadsByStatus(statusId), Times.Once);
        Assert.All(result, lead => Assert.True(lead.Price <= 500));
    }
}
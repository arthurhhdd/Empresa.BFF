using Empresa.Domain.Entidades;

namespace Empresa.Application.UseCases.Interfaces
{
    public interface IGetLeadsByStatus
    {
        Task<List<Lead>> ExecuteAsync(int statusId);
    }
}

using Empresa.Domain.Entidades;

namespace Empresa.Domain.Adapters
{
    public interface ILeadsRepository
    {
        Task<List<Lead>> GetLeadsByStatus(int statusId);
        Task UpdateLeadStatus(int id, bool accepted);
    }
}

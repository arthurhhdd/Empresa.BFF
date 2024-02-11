
using Empresa.Domain.Entidades;

namespace Empresa.Application.UseCases.Interfaces
{
    public interface IUpdateLeadStatus
    {
        Task ExecuteAsync(int id, bool accepted);
    }
}

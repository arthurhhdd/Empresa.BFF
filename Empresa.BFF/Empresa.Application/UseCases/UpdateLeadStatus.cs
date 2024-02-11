using Empresa.Application.UseCases.Interfaces;
using Empresa.Domain.Adapters;
using Empresa.Domain.Entidades;
using Empresa.Infra;

namespace Empresa.Application.UseCases
{
    public class UpdateLeadStatus : IUpdateLeadStatus
    {
        private ILeadsRepository _leadsRepository;

        public UpdateLeadStatus(ILeadsRepository leadsRepository)
        {
            _leadsRepository = leadsRepository;
        }

        public async Task ExecuteAsync(int id, bool accepted)
        {
            await _leadsRepository.UpdateLeadStatus(id, accepted);
        }
    }
}

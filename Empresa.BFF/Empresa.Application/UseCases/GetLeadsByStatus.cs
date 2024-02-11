
    using Empresa.Application.UseCases.Interfaces;
    using Empresa.Domain.Adapters;
    using Empresa.Domain.Entidades;

    namespace Empresa.Application.UseCases
    {
        public class GetLeadsByStatus : IGetLeadsByStatus
        {
            private ILeadsRepository _leadsRepository;

            public GetLeadsByStatus(ILeadsRepository leadsRepository)
            {
                _leadsRepository = leadsRepository;
            }

        public async Task<List<Lead>> ExecuteAsync(int statusId)
        {
            var leads = await _leadsRepository.GetLeadsByStatus(statusId);

            foreach (var lead in leads)
            {
                if (lead.Price > 500)
                {
                    lead.Price *= 0.9m;
                }
            }

            return leads;
        }
    }
}

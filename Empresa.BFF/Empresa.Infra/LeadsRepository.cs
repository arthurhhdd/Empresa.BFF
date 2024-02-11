
using Empresa.Domain.Adapters;
using Empresa.Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Empresa.Infra
{
    public class LeadsRepository : ILeadsRepository
    {
        private EmpresaDbContext _db;
        private IEmailService _email;

        public LeadsRepository(EmpresaDbContext db, IEmailService email)
        {
            _db = db;
            _email = email;
        }

        public async Task<List<Lead>> GetLeadsByStatus(int statusId)
        {
            return await _db.Leads
                .Include(l => l.Client)
                .Where(l => l.StatusId == statusId)
                .ToListAsync();
        }

        public async Task UpdateLeadStatus(int id, bool accepted)
        {
            var lead = await _db.Leads.Include(l => l.Client).FirstOrDefaultAsync(l => l.Id == id);
            if (lead == null)
                throw new Exception("Lead não encontrado");

            lead.StatusId = accepted ? 3 : 2;

            if (accepted)
            {
                string discountMessage = "";
                if (lead.Price > 500)
                {
                    lead.Price *= 0.9m; // Apply 10% discount
                    discountMessage = " A 10% discount has been applied.";
                }

                await _email.SendEmailAsync(
                    "vendas@test.com",
                    "Lead Accepted",
                   $"The lead for client: {lead.Client.NameClient} with service description: '{lead.Description}' and quoted price: ${lead.Price} has been accepted.{discountMessage}"
                );
            }

            await _db.SaveChangesAsync();
        }
    }
}

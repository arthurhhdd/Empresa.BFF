using Microsoft.AspNetCore.Mvc;
using Empresa.Infra;
using Microsoft.EntityFrameworkCore;
using Empresa.Application.UseCases;
using Empresa.BFF.Dtos.Leads;
using Empresa.Application.UseCases.Interfaces;

namespace Empresa.BFF.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LeadsController : ControllerBase
    {
        private readonly IGetLeadsByStatus _getLeadsByStatus;
        private readonly IUpdateLeadStatus _updateLeadStatus;

        public LeadsController(
            IGetLeadsByStatus getLeadsByStatus,
            IUpdateLeadStatus updateLeadStatus)
        {
            _getLeadsByStatus = getLeadsByStatus ?? throw new ArgumentNullException(nameof(getLeadsByStatus));
            _updateLeadStatus = updateLeadStatus ?? throw new ArgumentNullException(nameof(updateLeadStatus));
        }

        [HttpGet("{idStatus}")]
        public async Task<IActionResult> Get([FromRoute] int idStatus)
        {
            if (idStatus == 0)
            {
                return BadRequest("O idStatus não pode ser vazio.");
            }
            // alterar nome para ExecuteAsync
            var leads = await _getLeadsByStatus.ExecuteAsync(idStatus);
            return Ok(leads);
        }
            
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateStatusRequest request)
        {
            try
            {
                await _updateLeadStatus.ExecuteAsync(id, request.Accepted);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

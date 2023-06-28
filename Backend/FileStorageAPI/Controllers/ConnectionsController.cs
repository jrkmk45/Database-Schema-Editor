using DBPrototyperAPI.ActionFilters;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Dtos.Connection;
using Services.Services.Interfaces;
using System.Security.Claims;

namespace DBPrototyperAPI.Controllers
{
    [Route("api/schemes/{schemeId:int}/tables/{tableId:int}/attributes/{attributeId:int}/connections")]
    [AccessFilter]
    [ApiController, Authorize]
    public class ConnectionsController : ControllerBase
    {
        private readonly IConnectionService _connectionService;
        private readonly IAttributeService _attributeService;
        public ConnectionsController(IConnectionService connectionService,
            IAttributeService attributeService) 
        {
            _connectionService = connectionService;
            _attributeService = attributeService;
        }

        [HttpPost]
        public async Task<ActionResult<ConnectionDTO>> CreateConnection(int attributeId, CreateConnectionDTO createConnectionDTO)
        {
            try
            {
                var requesterId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var result = await _connectionService.CreateConnectionAsync(attributeId, requesterId, createConnectionDTO);
                return CreatedAtAction(null, result);
            } 
            catch (ResourceNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpDelete]
        public async Task<ActionResult> DeleteConnections(int attributeId)
        {
            try
            {
                await _attributeService.DeleteAttributeConnectionsAsync(attributeId);
                return NoContent();
            }
            catch (ResourceNotFoundException ex) 
            {
                return NotFound(ex.Message);
            }
        }
        
    }
}

using DBPrototyperAPI.ActionFilters;
using Domain.Exceptions;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Services.Dtos.Table;
using Services.Services.Interfaces;
using System.Security.Claims;

namespace DBPrototyperAPI.Controllers
{
    [AccessFilter]
    [Route("api/schemes/{schemeId:int}/tables"), ApiController]
    [EnableCors, Authorize]
    public class TablesController : ControllerBase
    {
        private readonly ITableService _tableService;
        public TablesController(ITableService tableService)
        {
            _tableService = tableService;
        }

        
        [HttpPost]
        public async Task<ActionResult<Table>> CreateTable(int schemeId, CreateTableDTO createTableDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid properties");

            try
            {
                var requesterId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var result = await _tableService.CreateTableAsync(schemeId, requesterId, createTableDTO);
                return CreatedAtAction(null, null, result);
            }
            catch (ResourceNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{tableId:int}")]
        public async Task<ActionResult> DeleteTable(int tableId)
        {
            try
            {
                await _tableService.DeleteTableAsync(tableId);
                return NoContent();
            }
            catch (ResourceNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPatch("{tableId:int}")]
        public async Task<ActionResult> PatchTable(int tableId, [FromBody] PatchTableDTO patchTableDTO)
        {
            try
            {
                await _tableService.PatchTableAsync(tableId, patchTableDTO);
                return NoContent();
            } catch (ResourceNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}

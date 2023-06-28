using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Services.Dtos.Scheme;
using Services.Services.Interfaces;
using System.Security.Claims;
using DBPrototyperAPI.ActionFilters;

namespace DBPrototyperAPI.Controllers
{
    [Route("api/schemes"), ApiController, EnableCors]
    public class SchemesController : ControllerBase
    {
        private readonly ISchemeService _schemeService;
        public SchemesController(ISchemeService schemeService)
        {
            _schemeService = schemeService;
        }
        
        [Authorize, HttpPost]
        public async Task<ActionResult<SchemeListItemDTO>> CreateScheme([FromBody] CreateSchemeDTO createSchemeDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid properties");

            var requesterId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _schemeService.CreateSchemeAsync(createSchemeDTO, requesterId);
            return CreatedAtAction(nameof(GetScheme), new { schemeId = result.Id }, result);
        }

        [Authorize]
        [AccessFilter]
        [HttpGet("{schemeId:int}")]
        public async Task<ActionResult<SchemeDTO>> GetScheme(int schemeId)
        {
            try
            {
                return Ok(await _schemeService.GetSchemeAsync(schemeId));
            } 
            catch (ResourceNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [AccessFilter]
        [HttpDelete("{schemeId:int}")]
        public async Task<ActionResult> DeleteScheme(int schemeId)
        {
            try
            {
                await _schemeService.DeleteSchemeAsync(schemeId);
                return NoContent();
            }
             catch (ResourceNotFoundException ex) 
            {
                return NotFound(ex.Message);
            }
        }
    }
}

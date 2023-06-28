using DBPrototyperAPI.ActionFilters;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Services.Dtos.Attribute;
using Services.Services.Interfaces;
using System.Security.Claims;

namespace DBPrototyperAPI.Controllers
{
    [Route("api/schemes/{schemeId:int}/tables/{tableId:int}/attributes")]
    [ApiController, EnableCors, Authorize]
    public class AttributesController : ControllerBase
    {
        private readonly IAttributeService _attributeService;
        public AttributesController(IAttributeService attributeService)
        { 
            _attributeService = attributeService;
        }

        [AccessFilter]
        [HttpPost]
        public async Task<ActionResult<AttributeDTO>> CreateAttribute(int tableId, CreateAttributeDTO createAttributeDTO)
        {
            try
            {
                var requesterId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var result = await _attributeService.CreateAttributeAsync(tableId, requesterId, createAttributeDTO);
                return CreatedAtAction(null, result);
            }
            catch (ResourceNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [AccessFilter]
        [HttpDelete("{attributeId:int}")]
        public async Task<ActionResult> DeleteAttribute(int attributeId)
        {
            try
            {
                await _attributeService.DeleteAttributeAsync(attributeId);
                return NoContent();
            }
            catch (ResourceNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}

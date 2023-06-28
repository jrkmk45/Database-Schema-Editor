using Domain.Exceptions;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Repository.Repositories.Interfaces;
using System.Security.Claims;

namespace DBPrototyperAPI.ActionFilters
{
    public class AccessFilterAttribute : System.Attribute, IAsyncActionFilter
    {
        private ISchemeRepository schemeRepository;
        private ITableRepository tableRepository;
        private IAttributeRepository attributeRepository;
        private IConnectionRepository connectionRepository;

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            InitializeDependencies(context);

            var controllerName = context.RouteData.Values["controller"].ToString();
            var actionName = context.RouteData.Values["action"].ToString();

            var userId = Convert.ToInt32(context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var arguments = context.ActionArguments;
            var checkList = new List<(string, Func<int, Task<Scheme>>)>
            {
                ("connectionId", GetConnectionsSchemeAsync),
                ("attributeId", GetAttributesSchemeAsync),
                ("tableId", GetTablesSchemeAsync),
                ("schemeId", schemeRepository.GetSchemeAsync)
            };

            foreach (var pair  in checkList)
            {
                if (arguments.TryGetValue(pair.Item1, out var itemId))
                {
                    try
                    {
                        var scheme = await pair.Item2((int)itemId!);
                        if (scheme.UserId != userId)
                        {
                            context.Result = new ForbidResult();
                            return;
                        }
                    }
                    catch (ResourceNotFoundException ex)
                    {
                        context.Result = new NotFoundObjectResult(ex.Message);
                        return;
                    }
                }
            }
            var result = await next();
            // execute any code after the action executes
        }

        private async Task<Scheme> GetConnectionsSchemeAsync(int connectionId)
        {
            var connection = await connectionRepository.GetConnectionAsync(connectionId);
            if (connection == null)
                throw new ResourceNotFoundException($"No connection with id {connectionId}");

            return await GetAttributesSchemeAsync(connection.AttributeFromId);
        }

        private async Task<Scheme> GetAttributesSchemeAsync(int attributeId)
        {
            var attribute = await attributeRepository.GetAttributeAsync(attributeId);
            if (attribute == null)
                throw new ResourceNotFoundException($"No attribute with id {attributeId}");

            return await GetTablesSchemeAsync(attribute.TableId);
        }

        private async Task<Scheme> GetTablesSchemeAsync(int tableId)
        {
            var table = await tableRepository.GetTableAsync(tableId);
            if (table == null)
                throw new ResourceNotFoundException($"No table with id {tableId}");

            return table.Scheme;
        }
        private void InitializeDependencies(ActionExecutingContext context)
        {
            schemeRepository = context.HttpContext.RequestServices.GetService<ISchemeRepository>();
            tableRepository = context.HttpContext.RequestServices.GetService<ITableRepository>();
            attributeRepository = context.HttpContext.RequestServices.GetService<IAttributeRepository>();
            connectionRepository = context.HttpContext.RequestServices.GetService<IConnectionRepository>();
        }
    }
}

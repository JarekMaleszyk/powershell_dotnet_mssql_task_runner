using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using offler_db_context.Context;
using offler_db_context.Entities;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace offler_backend_api.Services
{
    public class ConfigurationFilterService : IParameterFilter
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public ConfigurationFilterService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
        {
            if (parameter.Name.Equals("ScriptCode", StringComparison.InvariantCultureIgnoreCase))
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<OfflerDbContext>();
                IEnumerable<TalendConfig> tasks = dbContext.TalendConfig.OrderBy(x => x.ScriptCode).ToArray();

                parameter.Schema.Enum = tasks.Select(p => new OpenApiString(p.ScriptCode)).ToList<IOpenApiAny>();
            }
        }
    }
}

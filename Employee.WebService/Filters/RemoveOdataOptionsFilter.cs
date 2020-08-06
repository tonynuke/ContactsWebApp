using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Employee.WebService.Filters
{
    public class RemoveOdataOptionsFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            bool isOdataController = context.MethodInfo
                .GetParameters().Any(parameter =>
                    parameter.ParameterType.BaseType == typeof(Microsoft.AspNet.OData.Query.ODataQueryOptions));

            if (isOdataController)
            {
                operation.Parameters.Clear();
            }
        }
    }
}
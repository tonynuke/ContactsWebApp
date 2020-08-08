using System;
using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Employee.WebService.Filters
{
    public class OdataOptionsIgnoreFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            Type odataType = typeof(Microsoft.AspNet.OData.Query.ODataQueryOptions);
            bool isOdataOperation = context.MethodInfo.GetParameters()
                .Any(parameter => parameter.ParameterType.BaseType == odataType);

            if (isOdataOperation)
            {
                operation.Parameters.Clear();
            }
        }
    }
}
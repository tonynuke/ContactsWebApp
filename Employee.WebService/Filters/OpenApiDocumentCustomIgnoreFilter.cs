using System.Linq;
using Employee.Domain.Contacts;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Employee.WebService.Filters
{
    /// <summary>
    /// Фильтр ненужных схем.
    /// </summary>
    /// <remarks>
    /// Из-за OData в спецификации появляются ненужные схемы.
    /// Оставляет в спецификации только схемах собственных типов.
    /// </remarks>
    public sealed class OpenApiDocumentCustomIgnoreFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument document, DocumentFilterContext context)
        {
            var dtoTypes = typeof(Startup).Assembly.GetTypes()
                .Select(type => type.Name).ToList();
            dtoTypes.Add(nameof(ContactType));

            var schemasKeys = context.SchemaRepository.Schemas.Select(schema => schema.Key);
            var ignoreKeys = schemasKeys.Where(key => !dtoTypes.Any(key.EndsWith));

            foreach (var key in ignoreKeys)
            {
                context.SchemaRepository.Schemas.Remove(key);
            }
        }
    }
}
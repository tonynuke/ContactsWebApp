using System;
using System.Collections.Generic;
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
            ICollection<Type> dtoTypes = typeof(DTO.ContactDTO).Assembly.GetTypes().ToList();
            dtoTypes.Add(typeof(ContactType));

            var schemeKeys = context.SchemaRepository.Schemas
                .Select(schema => schema.Key)
                .Where(key => !dtoTypes.Any(type => type.FullName.Contains(key)));

            foreach (var key in schemeKeys)
            {
                context.SchemaRepository.Schemas.Remove(key);
            }
        }
    }
}
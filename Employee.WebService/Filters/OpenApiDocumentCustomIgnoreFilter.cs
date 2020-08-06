using System;
using System.Collections.Generic;
using System.Linq;
using Employee.Domain.Contacts;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Employee.WebService.Filters
{
    public sealed class OpenApiDocumentCustomIgnoreFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument document, DocumentFilterContext context)
        {
            List<Type> dtoTypes = typeof(DTO.ContactDTO).Assembly.GetTypes().ToList();
            dtoTypes.Add(typeof(ContactType));

            var keysToDelete = context.SchemaRepository.Schemas
                .Select(schema => schema.Key)
                .Where(key => !dtoTypes.Any(type => type.FullName.Contains(key)));

            foreach (var key in keysToDelete)
            {
                context.SchemaRepository.Schemas.Remove(key);
            }
        }
    }
}
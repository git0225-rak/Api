using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simpolo_Endpoint.DAO.Services
{
    public class SecurityRequirementsOperationFilter: IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            //var requiredScopes = context.MethodInfo
            //    .GetCustomAttributes(true)
            //    .OfType<AuthorizeAttribute>()
            //    .Select(attr => attr.Policy)
            //    .Distinct();

            //if (requiredScopes.Any())
            //{
            //    operation.Security ??= new List<OpenApiSecurityRequirement>();

            //    var jwtSecurityScheme = new OpenApiSecurityScheme
            //    {
            //        Reference = new OpenApiReference
            //        {
            //            Type = ReferenceType.SecurityScheme,
            //            Id = "Bearer"
            //        }
            //    };

            //    var securityRequirements = new OpenApiSecurityRequirement
            //{
            //    { jwtSecurityScheme, requiredScopes.ToList() }
            //};

            //    operation.Security.Add(securityRequirements);
            //}


            var hasAuthorizeAttribute = context.MethodInfo.DeclaringType.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any()
            || context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any();

            if (hasAuthorizeAttribute)
            {
                operation.Security ??= new List<OpenApiSecurityRequirement>();

                var scheme = new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } };
                operation.Security.Add(new OpenApiSecurityRequirement { [scheme] = new List<string>() });
            }
        }
    }
}

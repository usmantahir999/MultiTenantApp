﻿using NJsonSchema;
using NSwag;
using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;
using System.Reflection;

namespace Infrastructure.OpenApi
{
    public class SwaggerHeaderAttributeProcessor  : IOperationProcessor
    {
        public bool Process(OperationProcessorContext context)
        {
            if (context.MethodInfo.GetCustomAttribute(typeof(SwaggerHeaderAttribute)) is SwaggerHeaderAttribute swaggerHeader)
            {
                var parameters = context.OperationDescription.Operation.Parameters;

                var existstingParam = parameters
                    .FirstOrDefault(p => p.Kind == OpenApiParameterKind.Header && p.Name == swaggerHeader.HeaderName);

                if (existstingParam is not null)
                {
                    parameters.Remove(existstingParam);
                }

                parameters.Add(new OpenApiParameter
                {
                    Name = swaggerHeader.HeaderName,
                    Kind = OpenApiParameterKind.Header,
                    Description = swaggerHeader.Description,
                    IsRequired = true,
                    Schema = new JsonSchema
                    {
                        Type = JsonObjectType.String,
                        Default = swaggerHeader.DefaultValue,
                    }
                });
            }
            return true;
        }
    }
}

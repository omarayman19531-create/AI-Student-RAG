using Application.Features.Authantication.Command.Register;
using Application.Mapping;
using Application.Validation;
using Application.Validation.auth;
using Application.Validation.implmention;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Depenceinject
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureapp(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => { }, typeof(MappingProfile).Assembly);
            services.AddMediatR(cfg =>
             cfg.RegisterServicesFromAssembly(typeof(RegisterCommand).Assembly));
            services.AddValidatorsFromAssemblyContaining<RegisterValid>();
            services.AddScoped<IvalidationService, validationservies>();
            services.AddRateLimiter(opation =>
            {
                opation.AddSlidingWindowLimiter("AuthPolicy", opation =>
                {
                    opation.PermitLimit = 5;
                    opation.Window = TimeSpan.FromMinutes(1);
                    opation.SegmentsPerWindow = 6;
                    opation.QueueLimit = 0;
                });
                opation.OnRejected = async (context, cancellationToken) =>
                {
                    context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;

                    await context.HttpContext.Response.WriteAsJsonAsync(new
                    {
                        message = "Too many Requests Please try try again later"
                    });


                };
            });



            services.AddScoped<IvalidationService, validationservies>();
            
            services.AddMemoryCache();

            return services;
        }
    }
    }

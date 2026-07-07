using EnglishLearning.Application.Common;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EnglishLearning.Application.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        services.AddAutoMapper(typeof(MappingsProfile));

        return services;
    }
}

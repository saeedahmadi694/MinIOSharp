using MinIOSharpSharp.AspNetCore.Config;
using MinIOSharpSharp.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace MinIOSharpSharp.AspNetCore.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMinIOSharpService(this IServiceCollection services, Action<MinIOSharpSetting> configure)
    {
        services.Configure(configure);

        services.AddScoped((Func<IServiceProvider, IMinIOSharpService>)(sp =>
        {
            var setting = sp.GetRequiredService<IOptionsMonitor<MinIOSharpSetting>>().CurrentValue;
            return new MinIOSharpService(
                setting.SecretKey,
                setting.SiteKey,
                setting.VerificationUrl
            );
        }));

        return services;
    }
}


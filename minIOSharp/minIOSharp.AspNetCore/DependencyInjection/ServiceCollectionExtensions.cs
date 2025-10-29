using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Minio;
using MinIOSharp.Core.Config;
using MinIOSharp.Core.Services;

namespace MinIOSharp.AspNetCore.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBhMinio(this IServiceCollection services, Uri url, Action<MinIOSetting>? configure = null)
    {
        return services.AddBhMinio(Options.DefaultName, url, configure);
    }

    public static IServiceCollection AddBhMinio(this IServiceCollection services, Action<MinIOSetting> configure)
    {
        return services.AddBhMinio(Options.DefaultName, configure);
    }

    public static IServiceCollection AddBhMinio(this IServiceCollection services, string name, Uri url, Action<MinIOSetting>? configure = null)
    {
        return services.AddBhMinio(name, delegate (MinIOSetting options)
        {
            string[] array = url.UserInfo.Split(':');
            if (array.Length != 2)
            {
                throw new InvalidOperationException("Invalid credentials format: " + url.UserInfo + ". s3://accessKey:secretKey@endpoint expected");
            }

            options.Endpoint = url.Authority;
            options.AccessKey = array[0];
            options.SecretKey = array[1];
            options.Region = url.AbsolutePath.TrimStart('/');
            configure?.Invoke(options);
        });
    }

    public static IServiceCollection AddBhMinio(this IServiceCollection services, string name, Action<MinIOSetting> configure)
    {
        services.Configure(name, configure);
        services.TryAddSingleton<IMinioClientFactory, MinioClientFactory>();
        services.TryAddSingleton<IBlobNamingNormalizer, MinioBlobNamingNormalizer>();
        services.TryAddSingleton((IServiceProvider sp) => sp.GetRequiredService<IMinioClientFactory>().CreateClient(name));
        services.TryAddSingleton<IMinIOService, MinIOService>();
        return services;
    }
}


using System.Globalization;
using FluentValidation;
using Hospital.Application.Common.Helpers.Queue;
using Hospital.Application.Common.Managers;
using Hospital.Application.Common.Models;

namespace Hospital.BackgroundApp.Configs;

public static class SettingsConfig
{
    public static IServiceCollection AddSettingsConfig(this IServiceCollection services, IConfiguration configuration)
    {
        var cultureInfo = new CultureInfo("tr-TR");
        System.Threading.Thread.CurrentThread.CurrentUICulture = cultureInfo;
        ValidatorOptions.Global.LanguageManager.Culture = cultureInfo;
        CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
        CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

        services.Configure<TokenSetting>(configuration.GetSection("TokenSetting"));
        services.Configure<RabbitMQSetting>(configuration.GetSection("RabbitMQSetting"));
        services.AddTransient<QueueHelper>();
        services.AddTransient<TokenManager>();
        services.AddTransient<FileManager>();
        return services;
    }
}
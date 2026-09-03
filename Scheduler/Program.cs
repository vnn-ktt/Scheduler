using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Scheduler.Application.Features.Providers.CreateProvider;
using Scheduler.Infrastructure.Integrations.Telegram;
using Scheduler.Infrastructure.Integrations.Telegram.Configuration;
using Scheduler.Infrastructure.Integrations.Telegram.Routing;
using Scheduler.Infrastructure.Integrations.Telegram.Screens;
using Scheduler.Infrastructure.Persistence;
using System.Text;
using Telegram.Bot;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

var builder = Host.CreateApplicationBuilder(args);

var connectionString =
    builder.Configuration.GetConnectionString("Scheduler")
    ?? throw new InvalidOperationException(
        "ConnectionStrings:Scheduler is not configured."
    );

builder.Services
    .AddOptions<BotSettings>()
    .Bind(builder.Configuration.GetSection(BotSettings.SectionName))
    .Validate(
        settings => !string.IsNullOrWhiteSpace(settings.BotToken),
        "Telegram:BotToken is not configured."
    )
    .ValidateOnStart();

builder.Services.AddSingleton<ITelegramBotClient>(
    serviceProvider =>
    {
        var settings = serviceProvider
            .GetRequiredService<IOptions<BotSettings>>()
            .Value;

        return new TelegramBotClient(
            settings.BotToken
        );
    }
);

builder.Services.AddDbContext<SchedulerDbContext>(
    options =>
    {
        options.UseNpgsql(connectionString);
    }
);

builder.Services.AddHostedService<TelegramBotWorker>();

builder.Services.AddScoped<TelegramUpdateHandler>();

builder.Services.AddScoped<CommandHandler>();
builder.Services.AddScoped<CallbackHandler>();

builder.Services.AddScoped<MainMenuHandler>();
builder.Services.AddScoped<ClientHandler>();
builder.Services.AddScoped<ProviderHandler>();

builder.Services.AddScoped<CreateProviderHandler>();


var app = builder.Build();

await app.RunAsync();
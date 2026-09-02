using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace Scheduler.Infrastructure.Integrations.Telegram;

public sealed class TelegramBotWorker : BackgroundService
{
    private readonly ITelegramBotClient _bot;
    private readonly IServiceScopeFactory _scopeFactory;
    
    public TelegramBotWorker(
        ITelegramBotClient bot,
        IServiceScopeFactory scopeFactory)
    {
        _bot = bot;
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(
        CancellationToken cancellationToken)
    {
        var me = await _bot.GetMe(cancellationToken);

        Console.WriteLine($"Bot {me.Username} is starting...");

        _bot.StartReceiving(
            updateHandler: HandleUpdateAsync,
            errorHandler: HandleErrorAsync,
            receiverOptions: new ReceiverOptions
            {
                DropPendingUpdates = true
            },
            cancellationToken
        );

        await Task.Delay(
            Timeout.Infinite,
            cancellationToken
        );
    }

    private async Task HandleUpdateAsync(
    ITelegramBotClient bot,
    Update update,
    CancellationToken cancellationToken)
    {
        await using var scope =
            _scopeFactory.CreateAsyncScope();

        var updateHandler =
            scope.ServiceProvider
                .GetRequiredService<TelegramUpdateHandler>();

        await updateHandler.HandleAsync(
            bot,
            update,
            cancellationToken
        );
    }

    private Task HandleErrorAsync(
        ITelegramBotClient bot,
        Exception exception,
        HandleErrorSource source,
        CancellationToken cancellationToken)
    {
        Console.WriteLine(
            $"Telegram error ({source}): {exception}"
        );

        return Task.CompletedTask;
    }
}
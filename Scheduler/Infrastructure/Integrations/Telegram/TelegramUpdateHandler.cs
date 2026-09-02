using Scheduler.Infrastructure.Integrations.Telegram.Routing;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Scheduler.Infrastructure.Integrations.Telegram;

public sealed class TelegramUpdateHandler
{
    private readonly CommandHandler _commandHandler;
    private readonly CallbackHandler _callbackHandler;

    public TelegramUpdateHandler(
        CommandHandler commandHandler,
        CallbackHandler callbackHandler)
    {
        _commandHandler = commandHandler;
        _callbackHandler = callbackHandler;
    }

    public async Task HandleAsync(
        ITelegramBotClient bot,
        Update update,
        CancellationToken cancellationToken)
    {
        try
        {
            if (update.Message is not null)
            {
                await _commandHandler.HandleAsync(
                    bot,
                    update.Message,
                    cancellationToken
                );

                return;
            }

            if (update.CallbackQuery is not null)
            {
                await _callbackHandler.HandleAsync(
                    bot,
                    update.CallbackQuery,
                    cancellationToken
                );
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine(
                $"Update handling error: {exception}"
            );
        }
    }
}
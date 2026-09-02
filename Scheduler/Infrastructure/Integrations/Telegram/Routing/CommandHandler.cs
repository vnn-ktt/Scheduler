using Scheduler.Infrastructure.Integrations.Telegram.Screens;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Scheduler.Infrastructure.Integrations.Telegram.Routing;

public sealed class CommandHandler
{
    private readonly MainMenuHandler _mainMenuHandler;

    public CommandHandler(
        MainMenuHandler mainMenuHandler)
    {
        _mainMenuHandler = mainMenuHandler;
    }

    public async Task HandleAsync(
        ITelegramBotClient bot,
        Message message,
        CancellationToken cancellationToken)
    {
        if (message.Text is null)
        {
            return;
        }

        switch (message.Text)
        {
            case "/start":
                await _mainMenuHandler.SendAsync(
                    bot,
                    message.Chat.Id,
                    cancellationToken
                );
                break;

            case "/help":
                await bot.SendMessage(
                    chatId: message.Chat.Id,
                    text: "Используйте /start для открытия меню.",
                    cancellationToken: cancellationToken
                );
                break;

            default:
                await bot.SendMessage(
                    chatId: message.Chat.Id,
                    text: "Используйте меню бота.",
                    cancellationToken: cancellationToken
                );
                break;
        }
    }
}
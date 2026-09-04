using Scheduler.Infrastructure.Integrations.Telegram.Screens;
using Scheduler.Infrastructure.Integrations.Telegram.State;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Scheduler.Infrastructure.Integrations.Telegram.Routing;

public sealed class CommandHandler
{
    private readonly TelegramSessionStore _sessionStore;
    private readonly MainMenuHandler _mainMenuHandler;

    public CommandHandler(MainMenuHandler mainMenuHandler, TelegramSessionStore sessionStore)
    {
        _mainMenuHandler = mainMenuHandler;
        _sessionStore = sessionStore;
    }

    public async Task HandleAsync(
        ITelegramBotClient bot,
        Message message,
        CancellationToken cancellationToken)
    {
        if (message.Text is null ||
            message.From is null)
        {
            return;
        }

        var state = _sessionStore.Get(
            message.From.Id
        );

        if (state != TelegramConversationState.None)
        {
            await HandleConversationStateAsync(
                bot,
                message,
                state,
                cancellationToken
            );

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

            default:
                await bot.SendMessage(
                    chatId: message.Chat.Id,
                    text: "Используйте /start",
                    cancellationToken: cancellationToken
                );
                break;
        }
    }

    private async Task HandleConversationStateAsync(
        ITelegramBotClient bot,
        Message message,
        TelegramConversationState state,
        CancellationToken cancellationToken)
    {
        switch (state)
        {
            case TelegramConversationState.WaitingForProviderName:
                await HandleProviderNameAsync(
                    bot,
                    message,
                    cancellationToken
                );

                break;
        }
    }
    private async Task HandleProviderNameAsync(
    ITelegramBotClient bot,
    Message message,
    CancellationToken cancellationToken)
    {
        var name = message.Text?.Trim();

        if (string.IsNullOrWhiteSpace(name))
        {
            await bot.SendMessage(
                chatId: message.Chat.Id,
                text: "Название не может быть пустым.",
                cancellationToken: cancellationToken
            );

            return;
        }

        _sessionStore.Clear(
            message.From!.Id
        );

        await bot.SendMessage(
            chatId: message.Chat.Id,
            text:
            $"""
            Профиль создан.

            Имя: {name}
            """,
                cancellationToken: cancellationToken
            );
    }

}
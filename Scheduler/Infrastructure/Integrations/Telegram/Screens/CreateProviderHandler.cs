using Scheduler.Infrastructure.Integrations.Telegram.State;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Scheduler.Infrastructure.Integrations.Telegram.Screens;

public sealed class CreateProviderHandler
{
    private readonly TelegramSessionStore _sessionStore;

    public CreateProviderHandler(
        TelegramSessionStore sessionStore)
    {
        _sessionStore = sessionStore;
    }

    public async Task StartAsync(
        ITelegramBotClient bot,
        CallbackQuery callback,
        CancellationToken cancellationToken)
    {
        if (callback.Message is null)
        {
            return;
        }

        _sessionStore.Set(
            callback.From.Id,
            TelegramConversationState.WaitingForProviderName
        );

        await bot.EditMessageText(
            chatId: callback.Message.Chat.Id,
            messageId: callback.Message.MessageId,
            text:
            """
            Создание профиля исполнителя

            Как вас будут видеть клиенты?

            Отправьте имя или название.
            """,
            cancellationToken: cancellationToken
        );
    }
}
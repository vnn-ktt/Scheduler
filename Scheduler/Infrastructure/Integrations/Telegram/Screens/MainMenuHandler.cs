using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace Scheduler.Infrastructure.Integrations.Telegram.Screens;

public sealed class MainMenuHandler
{
    public async Task SendAsync(
        ITelegramBotClient bot,
        long chatId,
        CancellationToken cancellationToken)
    {
        await bot.SendMessage(
            chatId: chatId,
            text:
            """
            Добро пожаловать в Scheduler.

            Кто вы?
            """,
            replyMarkup: CreateKeyboard(),
            cancellationToken: cancellationToken
        );
    }

    public async Task EditAsync(
        ITelegramBotClient bot,
        long chatId,
        int messageId,
        CancellationToken cancellationToken)
    {
        await bot.EditMessageText(
            chatId: chatId,
            messageId: messageId,
            text:
            """
            Добро пожаловать в Scheduler.

            Кто вы?
            """,
            replyMarkup: CreateKeyboard(),
            cancellationToken: cancellationToken
        );
    }

    private static InlineKeyboardMarkup CreateKeyboard()
    {
        return new InlineKeyboardMarkup(
        [
            [
                InlineKeyboardButton.WithCallbackData(
                    "Я клиент",
                    CallbackData.Client
                )
            ],
            [
                InlineKeyboardButton.WithCallbackData(
                    "Я исполнитель",
                    CallbackData.Provider
                )
            ]
        ]);
    }
}
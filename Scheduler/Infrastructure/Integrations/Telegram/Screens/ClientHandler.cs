using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace Scheduler.Infrastructure.Integrations.Telegram.Screens;

public sealed class ClientHandler
{
    public async Task ShowAsync(
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
            Режим клиента.

            Здесь позже появится выбор специалиста и запись.
            """,
            replyMarkup: new InlineKeyboardMarkup(
            [
                [
                    InlineKeyboardButton.WithCallbackData(
                        "Назад",
                        CallbackData.MainMenu
                    )
                ]
            ]),
            cancellationToken: cancellationToken
        );
    }
}
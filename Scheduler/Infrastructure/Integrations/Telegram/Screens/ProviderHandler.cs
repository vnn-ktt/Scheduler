using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace Scheduler.Infrastructure.Integrations.Telegram.Screens;

public sealed class ProviderHandler
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
            Кабинет исполнителя.

            Здесь будут услуги, расписание и записи клиентов.
            """,
            replyMarkup: new InlineKeyboardMarkup(
            [
                [
                    InlineKeyboardButton.WithCallbackData(
                        "Создать профиль",
                        CallbackData.ProviderCreate
                    )
                ],
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
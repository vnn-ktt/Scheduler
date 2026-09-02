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
            text: GetText(),
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
            text: GetText(),
            replyMarkup: CreateKeyboard(),
            cancellationToken: cancellationToken
        );
    }

    private static string GetText()
    {
        return
            """
            Scheduler

            Записывайтесь к специалистам
            или принимайте записи от своих клиентов.

            Что вы хотите сделать?
            """;
    }

    private static InlineKeyboardMarkup CreateKeyboard()
    {
        return new InlineKeyboardMarkup(
        [
            [
                InlineKeyboardButton.WithCallbackData(
                    "📅 Записаться",
                    CallbackData.Client
                )
            ],
            [
                InlineKeyboardButton.WithCallbackData(
                    "💼 Принимать записи",
                    CallbackData.Provider
                )
            ]
        ]);
    }
}
using Scheduler.Infrastructure.Integrations.Telegram.Screens;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Scheduler.Infrastructure.Integrations.Telegram.Routing;

public sealed class CallbackHandler
{
    private readonly MainMenuHandler _mainMenuHandler;
    private readonly ClientHandler _clientHandler;
    private readonly ProviderHandler _providerHandler;

    public CallbackHandler(
        MainMenuHandler mainMenuHandler,
        ClientHandler clientHandler,
        ProviderHandler providerHandler)
    {
        _mainMenuHandler = mainMenuHandler;
        _clientHandler = clientHandler;
        _providerHandler = providerHandler;
    }

    public async Task HandleAsync(
        ITelegramBotClient bot,
        CallbackQuery callback,
        CancellationToken cancellationToken)
    {
        if (callback.Message is null ||
            callback.Data is null)
        {
            return;
        }

        await bot.AnswerCallbackQuery(
            callbackQueryId: callback.Id,
            cancellationToken: cancellationToken
        );

        var chatId = callback.Message.Chat.Id;
        var messageId = callback.Message.MessageId;

        switch (callback.Data)
        {
            case CallbackData.MainMenu:
                await _mainMenuHandler.EditAsync(
                    bot,
                    chatId,
                    messageId,
                    cancellationToken
                );
                break;

            case CallbackData.Client:
                await _clientHandler.ShowAsync(
                    bot,
                    chatId,
                    messageId,
                    cancellationToken
                );
                break;

            case CallbackData.Provider:
                await _providerHandler.ShowAsync(
                    bot,
                    chatId,
                    messageId,
                    cancellationToken
                );
                break;
        }
    }
}
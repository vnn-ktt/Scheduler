using System.Collections.Concurrent;

namespace Scheduler.Infrastructure.Integrations.Telegram.State;

public sealed class  TelegramSessionStore 
{
    private readonly ConcurrentDictionary<long, TelegramConversationState> _states = new();
    public TelegramConversationState Get(long userId)
    {
        return _states.TryGetValue(
            userId, 
            out var state
        ) 
            ? state 
            : TelegramConversationState.None;
    }

    public void Set(long telegramUserId, TelegramConversationState state)
    {
        _states[telegramUserId] = state;
    }

    public void Clear(long telegramUserId)
    {
        _states.TryRemove(telegramUserId, out _);
    }
}
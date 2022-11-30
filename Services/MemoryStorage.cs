using System.Collections.Concurrent;
using MyTexterBot.Models;

namespace MyTexterBot.Services;

public class MemoryStorage: IStorage
{
    private readonly ConcurrentDictionary<long, Session> _sessions;

    public MemoryStorage()
    {
        _sessions = new ConcurrentDictionary<long, Session>();
    }
    
    public Session GetSession(long chatId)
    {
        if (_sessions.ContainsKey(chatId))
            return _sessions[chatId];

        var newSesion = new Session() { LanguageCode = "ru" };
        _sessions.TryAdd(chatId, newSesion);
        return newSesion;
    }
}
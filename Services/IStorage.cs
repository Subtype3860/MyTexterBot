using MyTexterBot.Models;

namespace MyTexterBot.Services;

public interface IStorage
{
    Session GetSession(long chatId);
}
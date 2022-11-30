using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyTexterBot.Configuration;
using MyTexterBot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MyTexterBot.Controller
{
    internal class VoiceMessageController
    {
        private readonly IStorage _memoryMemoryStorage;
        private readonly ITelegramBotClient _telegramClient;
        private readonly IFileHandler _audioFileHandler;

        public VoiceMessageController(IStorage memoryStorage,ITelegramBotClient telegramBotClient, IFileHandler audioFileHandler)
        {
            _memoryMemoryStorage = memoryStorage;
            _telegramClient = telegramBotClient;
            _audioFileHandler = audioFileHandler;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            var fileId = message.Voice?.FileId;
            if (fileId == null)
                return;

            await _audioFileHandler.Download(fileId, ct);

            string userLanguageCode = _memoryMemoryStorage.GetSession(message.Chat.Id).LanguageCode!; // Здесь получим язык из сессии пользователя
            var result = _audioFileHandler.Process(userLanguageCode); // Запустим обработку
            await _telegramClient.SendTextMessageAsync(message.Chat.Id, result, cancellationToken: ct);
        }
    }
}

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
        private readonly IStorage _storage;
        private readonly ITelegramBotClient _telegramClient;
        private readonly IFileHandler _audioFileHandler;

        public VoiceMessageController(IStorage storage,ITelegramBotClient telegramBotClient, IFileHandler audioFileHandler)
        {
            _storage = storage;
            _telegramClient = telegramBotClient;
            _audioFileHandler = audioFileHandler;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            var fileId = message.Voice?.FileId;
            if (fileId == null)
                return;
            await _audioFileHandler.Download(fileId, ct);
            await _telegramClient.SendTextMessageAsync(message.Chat.Id, "Голосовое сообзщение загружено", cancellationToken: ct);
            var userLanguageCode = _storage.GetSession(message.Chat.Id).LanguageCode;
            _audioFileHandler.Process(userLanguageCode!);
            await _telegramClient.SendTextMessageAsync(message.Chat.Id, "Голосовое сообщение конвертировано в формат .WAV",
                cancellationToken: ct);
        }
    }
}

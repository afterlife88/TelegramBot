using Telegram.Bot;

namespace TelegramBot.Core
{
    public static class Bot
    {
        private static Api _bot;

        /// <summary>
        /// Initialize bot
        /// </summary>
        public static Api Get()
        {
            if (_bot != null) return _bot;
            _bot = new Api(Config.BotApiKey);
            _bot.SetWebhook(Config.WebHookUrl);
            return _bot;
        }
    }
}
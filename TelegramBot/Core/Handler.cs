using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.DataProcessing;

namespace TelegramBot.Core
{
    public class Handler
    {
        #region Api Helpers
        private readonly Api _bot;
        private readonly CommandsDictionary _cmdDictionary;
        #endregion
        public Handler()
        {
            _bot = Bot.Get();
            _cmdDictionary = new CommandsDictionary();

        }
        /// <summary>
        /// Handle all msg from client 
        /// </summary>
        public async void Handle(Message message)
        {
            var textResponse = await _cmdDictionary.ReturnAction(message.Text);
            await _bot.SendTextMessage(message.Chat.Id, textResponse);
        }

    }
}

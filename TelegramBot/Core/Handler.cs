using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.ApiHelpers;
using Weather = TelegramBot.ApiHelpers.Weather;

namespace TelegramBot.Core
{
    public class Handler
    {
        #region Api Helpers
        private readonly Api _bot;
        private ExchangeRates _rates;
        private Weather _weather;
        private BashQuote _bashQuote;
        #endregion
        public Handler()
        {
            _bot = Bot.Get();

        }
        /// <summary>
        /// Handle all msg from client 
        /// </summary>
        public async void Handle(Message message)
        {
            var userInput = message.Text.Split(' ');
            switch (userInput[0])
            {
                case "/start":
                    await
                        _bot.SendTextMessage(message.Chat.Id,
                            "Привет! Я скромный бот, просто напиши /help и увидишь все мои возможности ;)");
                    break;
                case "/help":
                    await _bot.SendTextMessage(message.Chat.Id, "Пример использования: \n/weather название города, \n/ExchangeRates -> курс валют, " +
                                                   "\n/bash -> цитатник");
                    break;
                case "/weather":
                    _weather = new Weather();
                    if (userInput.Length == 1)
                    {
                        await _bot.SendTextMessage(message.Chat.Id, "Укажите название города!");
                        break;
                    }
                    var list = await _weather.GetWeather(userInput[1]);
                    foreach (var item in list)
                    {
                        await
                            _bot.SendTextMessage(message.Chat.Id,
                                $"Температура в городе {userInput[1]}, Максимальная температура {item.MaxTemperature} минимальная температура {item.MinTemperature}");
                    }
                    break;
                case "/bash":
                    _bashQuote = new BashQuote();
                    var msg = _bashQuote.GetQuote();
                    await _bot.SendTextMessage(message.Chat.Id, msg);
                    break;
                case "/ExchangeRates":
                    _rates = new ExchangeRates();
                    var listResponse = _rates.GetRates();
                    foreach (var quote in listResponse.Quotes)
                    {
                        await _bot.SendTextMessage(message.Chat.Id,
                            $"Курс доллара к евро состоянием на  {quote.Value.Date} Покупка: {quote.Value.Ask,10} Продажа :{quote.Value.Bid,10}");
                    }
                    break;
                default:
                    await _bot.SendTextMessage(message.Chat.Id, "Введена неверная команда, воспользуйтесь /help для информации");
                    break;

            }

        }

    }
}

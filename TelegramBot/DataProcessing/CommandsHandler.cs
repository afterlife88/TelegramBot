using System.Threading.Tasks;
using TelegramBot.ApiHelpers;

namespace TelegramBot.DataProcessing
{
    public class CommandsHandler : ICommandsHandler
    {
        private ExchangeRates _rates;
        private Weather _weather;
        private BashQuote _bashQuote;

        public  async Task<string> Start(string command)
        {
            return "Привет! Я скромный бот, просто напиши /help и увидишь все мои возможности";
        }

        public async Task<string> Weather(string command)
        {
            _weather = new Weather();
            var arrayText = SplitUserTxt(command);
            if (arrayText.Length == 1)
            {
                return "Укажите название города!";

            }
            var list = await _weather.GetWeather(arrayText[1]);
            string response = null;
            foreach (var item in list)
                response = $"Температура в городе {arrayText[1]}, Максимальная температура {item.MaxTemperature} минимальная температура {item.MinTemperature}";
            return response;
        }

        public async Task<string> Rates(string command)
        {
            _rates = new ExchangeRates();
            var listResponse = _rates.GetRates();
            string response = null;
            foreach (var quote in listResponse.Quotes)
            {
                response = $"Курс доллара к евро состоянием на  {quote.Value.Date} Покупка: {quote.Value.Ask,10} Продажа :{quote.Value.Bid,10}";
            }
            return response;
        }
        public async Task<string> Bash(string command)
        {
            _bashQuote = new BashQuote();
            var msg = _bashQuote.GetQuote();
            return msg;
        }

        public async Task<string> Help(string command)
        {
            return "Пример использования: \n/weather название города, \n/ExchangeRates -> курс валют, " +
                   "\n/bash -> цитатник";
        }

        private string[] SplitUserTxt(string text)
        {
            return text.Split(' ');
        }
    }
}
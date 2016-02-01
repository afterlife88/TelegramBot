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
            return "Hello! I am a humble bot, just write /help and see all my options";
        }

        public async Task<string> Weather(string command)
        {
            _weather = new Weather();
            var arrayText = SplitUserTxt(command);
            if (arrayText.Length == 1)
            {
                return "Specify the name of the city!";

            }
            var list = await _weather.GetWeather(arrayText[1]);
            string response = null;
            foreach (var item in list)
                response = $"The temperature in the city  {arrayText[1]}, Maximum temperature {item.MaxTemperature}, minimum temperature {item.MinTemperature}";
            return response;
        }

        public async Task<string> Rates(string command)
        {
            _rates = new ExchangeRates();
            var listResponse = _rates.GetRates();
            string response = null;
            foreach (var quote in listResponse.Quotes)
            {
                response = $"The dollar against the euro as of  {quote.Value.Date} Ask: {quote.Value.Ask,10} Bid :{quote.Value.Bid,10}";
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
            return "Example of use: \n/weather city name, \n/ExchangeRates ->  exchange rate " +
                   "\n/bash -> russian quote pad";
        }

        private string[] SplitUserTxt(string text)
        {
            return text.Split(' ');
        }
    }
}
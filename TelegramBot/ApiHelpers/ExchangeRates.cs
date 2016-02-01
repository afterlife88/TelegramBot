using System;
using Oanda;

namespace TelegramBot.ApiHelpers
{
    public class ExchangeRates
    {
        DateTime _dateTime = DateTime.UtcNow.Date;
        public GetRatesResponse GetRates()
        {
            var api = new Oanda.ExchangeRates("0dcc874a159313499d9ce8b57251a296-231c572079b52de13e9d19033b937ad8");
            var responseGetRatesForSeveralQuotes = api.GetRates("USD",
                quote: "EUR",
                date: _dateTime.ToString("yyyy-dd-MM"),
                fields: Oanda.ExchangeRates.RatesFields.Averages);
           
            return responseGetRatesForSeveralQuotes;
        }
    }
}
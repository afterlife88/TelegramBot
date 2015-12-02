using System;
using Oanda;

namespace TelegramBot.ApiHelpers
{
    public class ExchangeRates
    {
        DateTime _dateTime = DateTime.UtcNow.Date;
        public GetRatesResponse GetRates()
        {
            var api = new Oanda.ExchangeRates("iPWLHRIMsm0FntO9rtbor0E6");
            var responseGetRatesForSeveralQuotes = api.GetRates("USD",
                quote: "EUR",
                date: _dateTime.ToString("yyyy-dd-MM"),
                fields: Oanda.ExchangeRates.RatesFields.Averages);

            return responseGetRatesForSeveralQuotes;
        }
    }
}
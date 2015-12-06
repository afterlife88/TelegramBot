using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using TelegramBot.Models;

namespace TelegramBot.ApiHelpers
{
    public class Weather
    {
        private string AppID = "KEY";

        public async Task<List<WeatherModel>> GetWeather(string location)
        {
       
            string url =
                $"http://api.openweathermap.org/data/2.5/forecast/daily?q={location}&type=accurate&mode=xml&units=metric&cnt=1&appid={AppID}";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string response = await client.GetStringAsync(url);
                    if (!(response.Contains("message") && response.Contains("cod")))
                    {
                        XElement xEl = XElement.Load(new System.IO.StringReader(response));
                        return GetWeatherInfo(xEl);
                    }
                    else
                    {
                        return new List<WeatherModel>();
                    }
                }
                catch (HttpRequestException)
                {
                    return new List<WeatherModel>();
                }
            }
        }

        private List<WeatherModel> GetWeatherInfo(XElement xEl)
        {
            IEnumerable<WeatherModel> w = xEl.Descendants("time").Select((el) =>
                new WeatherModel
                {
                    Humidity = el.Element("humidity").Attribute("value").Value + "%",
                    MaxTemperature = el.Element("temperature").Attribute("max").Value + "°",
                    MinTemperature = el.Element("temperature").Attribute("min").Value + "°",
                    Temperature = el.Element("temperature").Attribute("day").Value + "°",
                    Weather = el.Element("symbol").Attribute("name").Value,
                    WeatherDay = DayOfTheWeek(el),
                    WindDirection = el.Element("windDirection").Attribute("name").Value,
                    WindSpeed = el.Element("windSpeed").Attribute("mps").Value + "mps"
                });

            return w.ToList();
        }
        private  string DayOfTheWeek(XElement el)
        {
            DayOfWeek dW = Convert.ToDateTime(el.Attribute("day").Value).DayOfWeek;
            return dW.ToString();
        }
    }
}
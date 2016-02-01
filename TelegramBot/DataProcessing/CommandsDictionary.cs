using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TelegramBot.DataProcessing
{
    public class CommandsDictionary
    {
        private static ICommandsHandler _valueProcessor;
        private static Dictionary<string, Func<string, Task<string>>> _dictionary;
        public CommandsDictionary() : this(new CommandsHandler()) { }

        public CommandsDictionary(ICommandsHandler valueProcessor)
        {
            _valueProcessor = valueProcessor;
            _dictionary = CreateDictionary();
        }

        public Dictionary<string, Func<string, Task<string>>> CreateDictionary()
        {
            var dictionary = new Dictionary<string, Func<string, Task<string>>>
                                 {
                                     {"/bash", _valueProcessor.Bash},
                                     {"/weather", _valueProcessor.Weather},
                                     {"/ExchangeRates", _valueProcessor.Rates},
                                     {"/start", _valueProcessor.Start },
                                     {"/help",  _valueProcessor.Help},
                                 };
            return dictionary;
        }

        public async Task<string> ReturnAction(string name)
        {
            var arr = name.Split(' ');
            if (_dictionary.ContainsKey(arr[0]))
            { 
                return await _dictionary[arr[0]](name);
            }
            return "An invalid command, use the / help for information";

        }
    }
}
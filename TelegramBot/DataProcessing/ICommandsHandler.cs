using System.Threading.Tasks;

namespace TelegramBot.DataProcessing
{
    public interface ICommandsHandler
    {
        Task<string> Start(string command);
        Task<string> Weather(string command);
        Task<string> Rates(string command);
        Task<string> Bash(string command);
        Task<string> Help(string command);
    }
}

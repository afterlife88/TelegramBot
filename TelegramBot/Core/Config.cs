namespace TelegramBot.Core
{
    public static class Config
    {
       /// <summary>
        /// Bot token 
        /// </summary>
        public static string BotApiKey => "KEY";

        /// <summary>
        /// WebHook URL with secure https port
        /// </summary>
        public static string WebHookUrl => "https://webhooktelegram.azurewebsites.net:443/api/webhook";
    }
}
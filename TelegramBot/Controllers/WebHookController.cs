using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Telegram.Bot.Types;
using TelegramBot.Core;

namespace TelegramBot.Controllers
{
    public class WebHookController : ApiController
    {
        [HttpPost]
        public OkResult Post(Update update)
        {
            Task.Run(() => new Handler().Handle(update.Message));
            return Ok();
        }
    }

}


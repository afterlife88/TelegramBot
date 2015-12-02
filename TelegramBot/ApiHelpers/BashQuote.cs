using System;
using System.Text;
using HtmlAgilityPack;

namespace TelegramBot.ApiHelpers
{
    public class BashQuote
    {
        public string GetQuote()
        {
            HtmlWeb htmlWeb = new HtmlWeb();
            htmlWeb.OverrideEncoding = htmlWeb.OverrideEncoding = Encoding.GetEncoding("windows-1251");
            HtmlDocument document = htmlWeb.Load("http://bash.im/quote/" + Guid.NewGuid());
            HtmlNode quoteBash = document.GetElementbyId("body");
            var html = quoteBash.InnerHtml;
            string msg = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(html));
            string result = this.ParseHtml(msg);
            return result;
        }
        private string ParseHtml(string html)
        {
            var templateStart = "<div class=\"text\">";
            var templateEnd = "</div>";

            html = html.Remove(0, html.LastIndexOf(templateStart, StringComparison.Ordinal) + templateStart.Length);
            html = html.Remove(html.IndexOf(templateEnd, StringComparison.Ordinal));
            html = html.Replace("<br>", "\n");
            html = html.Replace("&lt;", "").Replace("&gt;", "");
            return html;
        }
    }
}
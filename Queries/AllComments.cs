using IcingaBot.Abstract_Classes;
using IcingaBot.ApiComunication;
using IcingaBot.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace IcingaBot.Queries
{
    internal class AllComments : Query
    {
        public override string Message { get; set; }

        public override async Task<dynamic> Execute() {
            Message = $"These are all the comments ever made:";
            var body = new BodyRequestModels(new string[] { "author", "entry_time", "text", "service_name" }, "true");
            var response = await HttpComunicationsHandler.HttpResonseMessage(Consts.CommentsUrl, HttpMethod.Post, body);
            if (response == Consts.EmptyResultQuery) {
                return "";
            } else {
                return Formatter.FormatComments(response);
            }
        }
    }
}
using IcingaBot.Abstract_Classes;
using IcingaBot.ApiComunication;
using IcingaBot.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace IcingaBot.Queries
{
    public class AllCommentsOneHost : Query
    {
        public List<string> ListParams { get; set; }
        public override string Message { get; set; }

        public AllCommentsOneHost(List<string> list) {
            ListParams = list;
        }

        public override async Task<dynamic> Execute() {
            Message = $"These are all the comments of {ListParams[0]}:";
            var body = new BodyRequestModels(new string[] { "author", "entry_time", "text", "service_name" }, "comment.host_name==\"" + ListParams[0] + "\"");
            var response = await HttpComunicationsHandler.HttpResonseMessage(Consts.CommentsUrl, HttpMethod.Post, body);
            if (response == Consts.EmptyResultQuery) {
                return "";
            } else {
                return Formatter.FormatComments(response);
            }
        }
    }
}

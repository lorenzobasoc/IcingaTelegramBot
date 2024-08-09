using IcingaBot.Abstract_Classes;
using IcingaBot.ApiComunication;
using IcingaBot.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace IcingaBot.Queries
{
    internal class AServiceFromASingleHost : Query
    {
        private readonly List<string> ListParams;
        public override string Message { get; set; }

        public AServiceFromASingleHost(List<string> listParams) {
            ListParams = listParams;
        }

        public override async Task<dynamic> Execute() {
            Message = $"These are the attributes of {ListParams[1]}'s {ListParams[0]}.";
            var body = new BodyRequestModels(new string[] { "acknowledgement", "last_check", "state" }, "match(\"" + ListParams[0] + "\",service.host_name) && service.name==\"" + ListParams[1] + "\"");
            var response = await HttpComunicationsHandler.HttpResonseMessage(Consts.ServicesUrl, HttpMethod.Post, body);
            if (response == Consts.EmptyResultQuery) {
                return "";
            } else {
                return Formatter.FormatAttributesService(response);
            }
        }
    }
}
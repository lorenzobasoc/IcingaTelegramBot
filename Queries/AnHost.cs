using IcingaBot.Abstract_Classes;
using IcingaBot.ApiComunication;
using IcingaBot.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace IcingaBot.Queries
{
    internal class AnHost : Query
    {
        private readonly List<string> ListParams;
        public override string Message { get; set; }

        public AnHost(List<string> listParams) {
            this.ListParams = listParams;
        }

        public override async Task<dynamic> Execute() {
            Message = $"These are the {ListParams[0]}' attributes:";
            var body = new BodyRequestModels(new string[] { "state", "acknowledgement", "display_name", "groups", "last_check", "last_state_change", "next_check", "vars" }, "host.name==\"" + ListParams[0] + "\"");
            var response = await HttpComunicationsHandler.HttpResonseMessage(Consts.HostsUrl, HttpMethod.Post, body);
            if (response == Consts.EmptyResultQuery) {
                return "";
            } else {
                return Formatter.FormatHost(response);
            }
        }
    }
}
using IcingaBot.Abstract_Classes;
using IcingaBot.ApiComunication;
using IcingaBot.Bot;
using IcingaBot.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace IcingaBot.Queries
{
    internal class AllServicesOneHost : Query
    {
        private readonly List<string> ListParams;
        public override string Message { get; set; }

        public AllServicesOneHost(List<string> listParams) {
            ListParams = listParams;
        }

        public override async Task<dynamic> Execute() {
            Message = $"These are all the services of {ListParams[0]}.";
            var body = new BodyRequestModels(new string[] { "name", "state", "acknowledgement" }, "match(\"" + ListParams[0] + "\",service.host_name)");
            var response = await HttpComunicationsHandler.HttpResonseMessage(Consts.ServicesUrl, HttpMethod.Post, body);
            if (response == Consts.EmptyResultQuery) {
                return "";
            } else {
                await InputHandler.WriteMessage($"These are the {ListParams[0]}'s services states:");
                await InputHandler.SendPhoto(await QueriesSupportClass.AllServicesFromAHostChartAsync(ListParams[0]));
                return Formatter.FormatServicesState(response);
            }
        }
    }
}
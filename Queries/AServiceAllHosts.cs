using IcingaBot.Abstract_Classes;
using IcingaBot.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace IcingaBot.Queries
{
    internal class AServiceAllHosts : Query
    {
        private readonly List<string> ListParams;
        public override string Message { get; set; }

        public AServiceAllHosts(List<string> listParams) {
            ListParams = listParams;
        }

        public override async Task<dynamic> Execute() {
            Message =  $"The {ListParams[0]}'s state in all hosts: ";
            var body = new BodyRequestModels(new string[] { "name" }, "service.name ==\"" + ListParams[0] + "\"");
            var response = await HttpComunicationsHandler.HttpResonseMessage(Consts.ServicesUrl, HttpMethod.Post, body);
            if (response == Consts.EmptyResultQuery) {
                return "";
            } else {
                return await QueriesSupportClass.ServiceFromAllHostsChartAsync(ListParams[0]);
            }
            
        }
    }
}
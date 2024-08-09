using IcingaBot.Abstract_Classes;
using IcingaBot.ApiComunication;
using IcingaBot.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace IcingaBot.Queries
{
    internal class AllHosts : Query
    {
        
        public override string Message { get; set; }

        public async override Task<dynamic> Execute() {
            Message = "These are status of all hosts:";
            var listNumbers = new List<int>();
            var body = new BodyRequestModels(new string[] { "name" }, "host.state==0 && host.vars.stock_biv ==\"true\"");
            var responseStock = await HttpComunicationsHandler.HttpResonseMessage(Consts.HostsUrl, HttpMethod.Post, body);
            listNumbers.Add(QueriesSupportClass.GetHostsAndServicesNumber(responseStock));

            body = new BodyRequestModels(new string[] { "name" }, "host.state==0 && host.vars.stock_biv==\"false\"");
            var responseUp = await HttpComunicationsHandler.HttpResonseMessage(Consts.HostsUrl, HttpMethod.Post, body);
            listNumbers.Add(QueriesSupportClass.GetHostsAndServicesNumber(responseUp));

            body = new BodyRequestModels(new string[] { "name" }, "host.state==1");
            var responseCritical = await HttpComunicationsHandler.HttpResonseMessage(Consts.HostsUrl, HttpMethod.Post, body);
            listNumbers.Add(QueriesSupportClass.GetHostsAndServicesNumber(responseCritical));

            body = new BodyRequestModels(new string[] { "name" }, "host.state==1 && host.acknowledgement==1");
            var responseAck = await HttpComunicationsHandler.HttpResonseMessage(Consts.HostsUrl, HttpMethod.Post, body);
            listNumbers.Add(QueriesSupportClass.GetHostsAndServicesNumber(responseAck));

            body = new BodyRequestModels(new string[] { "name" }, "host.state==3");
            var responseUnknown = await HttpComunicationsHandler.HttpResonseMessage(Consts.HostsUrl, HttpMethod.Post, body);
            listNumbers.Add(QueriesSupportClass.GetHostsAndServicesNumber(responseUnknown));

            body = new BodyRequestModels(new string[] { "name" }, "true");
            var totalHosts = await HttpComunicationsHandler.HttpResonseMessage(Consts.HostsUrl, HttpMethod.Post, body);
            return Formatter.CreateChart(listNumbers, Consts.HostStates, QueriesSupportClass.GetHostsAndServicesNumber(totalHosts));
        }
    }
}
using IcingaBot.Abstract_Classes;
using IcingaBot.ApiComunication;
using IcingaBot.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace IcingaBot.Queries
{
    internal class AllServices : Query
    {
        public override string Message { get; set; }

        public async override Task<dynamic> Execute() {
            Message = $"These are the status of all the services.";
            var listNumbers = new List<int>();
            var body = new BodyRequestModels(new string[] { "name" }, "service.state==1");
            var responseWarning = await HttpComunicationsHandler.HttpResonseMessage(Consts.ServicesUrl, HttpMethod.Post, body);
            listNumbers.Add(QueriesSupportClass.GetHostsAndServicesNumber(responseWarning));

            body = new BodyRequestModels(new string[] { "name" }, "service.state==0");
            var responseUp = await HttpComunicationsHandler.HttpResonseMessage(Consts.ServicesUrl, HttpMethod.Post, body);
            listNumbers.Add(QueriesSupportClass.GetHostsAndServicesNumber(responseUp));

            body = new BodyRequestModels(new string[] { "name" }, "service.state==2");
            var responseCritical = await HttpComunicationsHandler.HttpResonseMessage(Consts.ServicesUrl, HttpMethod.Post, body);
            listNumbers.Add(QueriesSupportClass.GetHostsAndServicesNumber(responseCritical));

            body = new BodyRequestModels(new string[] { "name" }, "service.state==2 && service.acknowledgement==1");
            var responseAck = await HttpComunicationsHandler.HttpResonseMessage(Consts.ServicesUrl, HttpMethod.Post, body);
            listNumbers.Add(QueriesSupportClass.GetHostsAndServicesNumber(responseAck));

            body = new BodyRequestModels(new string[] { "name" }, "service.state==3");
            var responseUnknown = await HttpComunicationsHandler.HttpResonseMessage(Consts.ServicesUrl, HttpMethod.Post, body);
            listNumbers.Add(QueriesSupportClass.GetHostsAndServicesNumber(responseUnknown));

            body = new BodyRequestModels(new string[] { "name" }, "true");
            var totalServices = await HttpComunicationsHandler.HttpResonseMessage(Consts.ServicesUrl, HttpMethod.Post, body);
            return Formatter.CreateChart(listNumbers, Consts.ServiceStates, QueriesSupportClass.GetHostsAndServicesNumber(totalServices));
        }
    }
}
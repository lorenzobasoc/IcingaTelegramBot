using IcingaBot.ApiComunication;
using IcingaBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Telegram.Bot.Types.InputFiles;

namespace IcingaBot.Queries
{
    public static class QueriesSupportClass
    {
        public static async Task<InputOnlineFile> ServiceFromAllHostsChartAsync(string service) {
            var listNumbers = new List<int>();
            var body = new BodyRequestModels(new string[] { "name" }, "service.state==1 && service.name==\"" + service + "\"");//.vars.snmp_label
            var responseWarning = await HttpComunicationsHandler.HttpResonseMessage(Consts.ServicesUrl, HttpMethod.Post, body);
            listNumbers.Add(GetHostsAndServicesNumber(responseWarning));

            body = new BodyRequestModels(new string[] { "name" }, "service.state==0 && service.name ==\"" + service + "\"");
            var responseUp = await HttpComunicationsHandler.HttpResonseMessage(Consts.ServicesUrl, HttpMethod.Post, body);
            listNumbers.Add(GetHostsAndServicesNumber(responseUp));

            body = new BodyRequestModels(new string[] { "name" }, "service.state==2 && service.name==\"" + service + "\"");
            var responseCritical = await HttpComunicationsHandler.HttpResonseMessage(Consts.ServicesUrl, HttpMethod.Post, body);
            listNumbers.Add(GetHostsAndServicesNumber(responseCritical));

            body = new BodyRequestModels(new string[] { "name" }, "service.state==2 && service.acknowledgement==1 && service.name==\"" + service + "\"");
            var responseAck = await HttpComunicationsHandler.HttpResonseMessage(Consts.ServicesUrl, HttpMethod.Post, body);
            listNumbers.Add(GetHostsAndServicesNumber(responseAck));

            body = new BodyRequestModels(new string[] { "name" }, "service.state==3 && service.name==\"" + service + "\"");
            var responseUnknown = await HttpComunicationsHandler.HttpResonseMessage(Consts.ServicesUrl, HttpMethod.Post, body);
            listNumbers.Add(GetHostsAndServicesNumber(responseUnknown));

            body = new BodyRequestModels(new string[] { "name" }, "service.name ==\"" + service + "\"");
            var totalServices = await HttpComunicationsHandler.HttpResonseMessage(Consts.ServicesUrl, HttpMethod.Post, body);
            return Formatter.CreateChart(listNumbers, Consts.ServiceStates, GetHostsAndServicesNumber(totalServices));
        }

        public static bool ValidateCommand(string command) {
            return Consts.Commands.Any(c => c == command);
        }

        public static string DataConverter(string d) {
            var substringTime = d.Substring(0, StopSubstring(d, '.', 0));
            var isEpoch = long.TryParse(substringTime, out var time);
            if (isEpoch) {
                return DateTimeOffset.FromUnixTimeSeconds(time).Date.ToShortDateString();
            } else {
                return "";
            }
        }

        public static async Task<InputOnlineFile> AllServicesFromAHostChartAsync(string host) {
            var listNumbers = new List<int>();
            var body = new BodyRequestModels(new string[] { "name" }, "service.state==1 && match(\"" + host + "\",service.host_name)");
            var responseWarning = await HttpComunicationsHandler.HttpResonseMessage(Consts.ServicesUrl, HttpMethod.Post, body);
            listNumbers.Add(GetHostsAndServicesNumber(responseWarning));

            body = new BodyRequestModels(new string[] { "name" }, "service.state==0 && match(\"" + host + "\",service.host_name)");
            var responseUp = await HttpComunicationsHandler.HttpResonseMessage(Consts.ServicesUrl, HttpMethod.Post, body);
            listNumbers.Add(GetHostsAndServicesNumber(responseUp));

            body = new BodyRequestModels(new string[] { "name" }, "service.state==2 && match(\"" + host + "\",service.host_name)");
            var responseCritical = await HttpComunicationsHandler.HttpResonseMessage(Consts.ServicesUrl, HttpMethod.Post, body);
            listNumbers.Add(GetHostsAndServicesNumber(responseCritical));

            body = new BodyRequestModels(new string[] { "name" }, "service.state==2 && service.acknowledgement==1 && match(\"" + host + "\",service.host_name)");
            var responseAck = await HttpComunicationsHandler.HttpResonseMessage(Consts.ServicesUrl, HttpMethod.Post, body);
            listNumbers.Add(GetHostsAndServicesNumber(responseAck));

            body = new BodyRequestModels(new string[] { "name" }, "service.state==3 && match(\"" + host + "\",service.host_name)");
            var responseUnknown = await HttpComunicationsHandler.HttpResonseMessage(Consts.ServicesUrl, HttpMethod.Post, body);
            listNumbers.Add(GetHostsAndServicesNumber(responseUnknown));

            body = new BodyRequestModels(new string[] { "name" }, "match(\"" + host + "\",service.host_name)");
            var totalServices = await HttpComunicationsHandler.HttpResonseMessage(Consts.ServicesUrl, HttpMethod.Post, body);
            return Formatter.CreateChart(listNumbers, Consts.ServiceStates, GetHostsAndServicesNumber(totalServices));
        }

        public static async Task<int> NumberOfElements(string url, string filter) {
            var body = new BodyRequestModels(new string[] { "name" }, filter);
            var message = await HttpComunicationsHandler.HttpResonseMessage(url, HttpMethod.Post, body);
            var array = message.Split("name");
            return (array.Length - 1) / 2;
        }

        public static async Task<string> GetUpHosts() {
            var names = "";
            var body = new BodyRequestModels(new string[] { "name" }, "host.vars.stock_biv ==\"false\" && host.state==0");
            var message = await HttpComunicationsHandler.HttpResonseMessage(Consts.HostsUrl, HttpMethod.Get, body);
            var array = message.Split("name");
            for (int i = 1; i < array.Length; i += 2) {
                var stopSubstring = StopSubstring(array[i], '"', 3);
                names += (array[i].Substring(3, stopSubstring)) + "\n";
            }
            return names;
        }

        public static int StopSubstring(string s, char stop, int startIndex) {
            var count = 0;
            for (int i = startIndex; i < s.Length; i++) {
                if (s[i] == stop) {
                    break;
                }
                count++;
            }
            return count;
        }

        public static int GetHostsAndServicesNumber(string response) {
            var array = response.Split("name");
            if (array[0] == Consts.EmptyResultQuery) {
                return 0;
            } else {
                return (array.Length - 1) / 2;
            }
        }
    }
}

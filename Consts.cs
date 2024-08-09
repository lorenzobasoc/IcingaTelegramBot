using IcingaBot.Bot;
using System.Collections.Generic;

namespace IcingaBot
{
    public static class Consts
    {
        #region Labels
        public const string GET_ALL_HOSTS = "Get all Hosts";
        public const string GET_ALL_COMMENTS = "Get All Comments";
        public const string GET_ALL_COMMENTS_ONE_HOST = "Get All Comments of an Host";
        public const string GET_A_SERVICE_FROM_ALL_HOSTS = "Get a Service From All Hosts";
        public const string GET_A_SERVICE_FROM_A_SINGLE_HOST = "Get a Service from a Sigle Host";
        public const string GET_ALL_SERVICES_FROM_A_HOST = "Get All Services of An Host";
        public const string GET_ALL_SERVICES = "Get All Servicies";
        public const string GET_AN_HOST = "Get An Host";
        public static readonly string[] Options = { GET_ALL_HOSTS, GET_ALL_COMMENTS, GET_ALL_SERVICES, GET_ALL_COMMENTS_ONE_HOST,  GET_ALL_SERVICES_FROM_A_HOST, GET_A_SERVICE_FROM_ALL_HOSTS, GET_A_SERVICE_FROM_A_SINGLE_HOST, GET_AN_HOST };
        #endregion

        #region Urls
        public const string HostsUrl = "https://icingang.edp-solari.lan/icinga/v1/objects/hosts";
        public const string CommentsUrl = "https://icingang.edp-solari.lan/icinga/v1/objects/comments";
        public const string ServicesUrl = "https://icingang.edp-solari.lan/icinga/v1/objects/services";
        #endregion

        #region Params Type
        public static InputHandler.Type[] AServiceFromAHostParamsType = new InputHandler.Type[] { InputHandler.Type.host, InputHandler.Type.host, InputHandler.Type.service};

        public static Dictionary<string, InputHandler.Type[]> LabelAndTypeParamsDictionary = new() {
            { GET_A_SERVICE_FROM_A_SINGLE_HOST, AServiceFromAHostParamsType },
        };
        #endregion

        public static readonly Dictionary<string, int> NumberOfParams = new() {
            { GET_ALL_HOSTS, 0 },
            { GET_ALL_COMMENTS, 0 },
            { GET_ALL_COMMENTS_ONE_HOST, 1 },
            { GET_A_SERVICE_FROM_ALL_HOSTS, 1 },
            { GET_A_SERVICE_FROM_A_SINGLE_HOST, 2 },
            { GET_ALL_SERVICES_FROM_A_HOST, 1 },
            { GET_ALL_SERVICES, 0 },
            { GET_AN_HOST, 1 },
        };


        public static readonly Dictionary<string, string> HostStates = new() {
            { "STOCK", "#055722" },
            { "UP", "#5af28f" },
            { "CRITICAL", "#de0909" },
            { "ACK", "#ed9fe9" },
            { "UNKNOWN", "#969696" },
        };
        public static readonly Dictionary<string, string> ServiceStates = new() {
            { "WARN", "#e9ed00" },
            { "UP", "#5af28f" },
            { "CRITICAL", "#de0909" },
            { "ACK", "#ed9fe9" },
            { "UNKNOWN", "#969696" },
        };

        public static List<string> ColorsList = new() { "#5af28f", "#055722", "#de0909", "#ed9fe9", "#969696" };

        public static List<string> Commands = new() { "/start", "/restart", "/back" };

        public const string EmptyResultQuery = "{\"results\":[]}";
        
        public const string Error404String = "{\"error\":404.0,\"status\":\"No objects found.\"}";
        
        public const string EmptyResultAnswer = "The search has not produced any results.";

        public const string Error = "Please push a button or type a correct category.";

        public const string ServerDownError = "<!DOCTYPE HTML PUBLIC \"-//IETF//DTD HTML 2.0//EN\"><html><head><title>503 Service Unavailable</title></head><body><h1>Service Unavailable</h1><p>The server is temporarily unable to service your request due to maintenance downtime or capacity problems. Please try again later.</p><hr><address>Apache/2.4.38 (Debian) Server at icingang.edp-solari.lan Port 443</address></body></html>";
    }
}

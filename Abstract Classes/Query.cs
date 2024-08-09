using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IcingaBot.Abstract_Classes
{
    public abstract class Query
    {
        public static Query GetQuery(string button, List<string> listParams) {
            return button switch
            {
                Consts.GET_ALL_COMMENTS_ONE_HOST => new Queries.AllCommentsOneHost(listParams),
                Consts.GET_ALL_SERVICES_FROM_A_HOST => new Queries.AllServicesOneHost(listParams),
                Consts.GET_A_SERVICE_FROM_ALL_HOSTS => new Queries.AServiceAllHosts(listParams),
                Consts.GET_AN_HOST => new Queries.AnHost(listParams),
                Consts.GET_A_SERVICE_FROM_A_SINGLE_HOST => new Queries.AServiceFromASingleHost(listParams),
                Consts.GET_ALL_HOSTS => new Queries.AllHosts(),
                Consts.GET_ALL_COMMENTS => new Queries.AllComments(),
                Consts.GET_ALL_SERVICES => new Queries.AllServices(),
                _ => throw new Exception("Invalid input."),
            };
        }

        public abstract string Message { get; set; }
        public abstract Task<dynamic> Execute();

    }
}

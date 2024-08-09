using System.Collections.Generic;

namespace IcingaBot.Bot
{
    public static class BotSupportClass
    {
        public static string LastUpdateMessage { get; set; }
        public static List<string> ParamsList { get; set; } = new List<string>();
        public static int RemaininParams { get; set; }
        public static int Counter { get; set; } = 1;
        public static InputHandler.State State = InputHandler.State.GetQuery;
    }
}

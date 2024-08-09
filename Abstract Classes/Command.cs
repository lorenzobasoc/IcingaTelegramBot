using IcingaBot.Commands;
using Telegram.Bot.Types;

namespace IcingaBot.AbstractModels
{
    public abstract class Command
    {
        public static Command GetCommand(string command) {
            return command switch
            {
                "/start" => new RestartCommand(),
                "/restart" => new RestartCommand(),
                "/back" => new BackCommand(),
                _ => null,
            };
        }

        public abstract void ExecuteAsync(Update update);
    }
}

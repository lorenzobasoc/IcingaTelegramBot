using System;
using System.Threading;
using System.Threading.Tasks;
using IcingaBot.Bot;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;

namespace IcingaBot
{

    class Program
    {
        static async Task Main() {
            try {
                BotComunication.TelegramClient = new TelegramBotClient("token");
                var me = await BotComunication.TelegramClient.GetMeAsync();
                Console.WriteLine($"user: {me.Id}\nname: {me.FirstName}.");
                using var cts = new CancellationTokenSource();
                BotComunication.TelegramClient.StartReceiving(new DefaultUpdateHandler(BotComunication.HandleUpdateAsync, BotComunication.HandleErrorAsync), cts.Token);
                Console.WriteLine($"Start the bot for {me.Username}");
                Console.ReadLine();
                cts.Cancel();
            } catch (Exception ex) {
                Console.Error.WriteLine($"Generated an {ex} exception.");
                Console.Error.WriteLine(ex.Message);
            }
        }
    }
}

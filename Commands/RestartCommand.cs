using IcingaBot.AbstractModels;
using IcingaBot.Bot;
using IcingaBot.Keyboards;
using IcingaBot.Queries;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace IcingaBot.Commands
{
    public class RestartCommand : Command
    {
        public async override void ExecuteAsync(Update update) { 
            var rkm = new InlineKeyboardMarkup(Keyboard.GetInlineKeyboard(Consts.Options));
            var idMessage = update.Message.Chat.Id;
            await BotComunication.TelegramClient.SendTextMessageAsync(idMessage, $"Welcome to the IcingaTelegramBot! Here you can view the status of the hosts and the services on the Icinga server.");
            await BotComunication.TelegramClient.SendTextMessageAsync(idMessage, $"Host's State:");
            await BotComunication.TelegramClient.SendPhotoAsync(idMessage, await new AllHosts().Execute());
            await BotComunication.TelegramClient.SendTextMessageAsync(idMessage, $"Services's State:");
            await BotComunication.TelegramClient.SendTextMessageAsync(idMessage, $"Processing informations...");
            await BotComunication.TelegramClient.SendPhotoAsync(idMessage, await new AllServices().Execute(), replyMarkup: rkm);
        }
    }
}

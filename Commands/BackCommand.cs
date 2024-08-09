using IcingaBot.AbstractModels;
using IcingaBot.Bot;
using IcingaBot.Keyboards;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace IcingaBot.Commands
{
    public class BackCommand : Command
    {
        public async override void ExecuteAsync(Update update) {
            var buttonItem = Consts.Options;
            var rkm = new InlineKeyboardMarkup(Keyboard.GetInlineKeyboard(buttonItem));
            var idMessage = update.Message.Chat.Id;
            BotSupportClass.State = InputHandler.State.GetQuery;
            await BotComunication.TelegramClient.SendTextMessageAsync(idMessage, $"Choose an option, please:" , replyMarkup: rkm);
        }
    }
}

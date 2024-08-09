using System;
using System.Threading;
using System.Threading.Tasks;
using IcingaBot.AbstractModels;
using IcingaBot.Keyboards;
using IcingaBot.Queries;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace IcingaBot.Bot
{
    public class BotComunication
    {
        public static TelegramBotClient TelegramClient;
        
        public static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken) {
            var errorMessage = exception switch {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };
            Console.Error.WriteLine(exception);
            return Task.CompletedTask;
        }

        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken ctx) {
            var buttonItem = Consts.Options;
            var rkm = new InlineKeyboardMarkup(Keyboard.GetInlineKeyboard(buttonItem));
            try {
                switch (update.Type) {
                    case UpdateType.CallbackQuery:
                        await botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, $"You chose \"{update.CallbackQuery.Data}\".");
                        var callBackHandler = new InputHandler(update, UpdateType.CallbackQuery);
                        BotSupportClass.State = InputHandler.State.GetQuery;
                        await callBackHandler.HandleInput();
                        break;
                    case UpdateType.Message:
                        if (update.Message.Type != MessageType.Text) {
                            await botClient.SendTextMessageAsync(update.Message.Chat.Id, $"Please push a button or type a correct category.", replyMarkup: rkm);
                            break;
                        }
                        var text = update.Message.Text;
                        if (text[0] == '/' && QueriesSupportClass.ValidateCommand(text)){
                            var command = Command.GetCommand(text);
                            command.ExecuteAsync(update);
                        } else {
                            var messageHandler = new InputHandler(update, UpdateType.Message);
                            await messageHandler.HandleInput();
                        }
                        break;
                    default:
                        Console.Error.WriteLine("Invalid update format. Please press a button on the screen or type a valid category.");
                        break;
                }
            } catch (Exception ex) {
                Console.Error.WriteLine($"Generated an {ex} exception.");
                Console.Error.WriteLine(ex.Message);
            }
        }
    }
}


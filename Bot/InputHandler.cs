using IcingaBot.Abstract_Classes;
using IcingaBot.Keyboards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace IcingaBot.Bot
{
    public class InputHandler {
        public static long ChatIdStatic;
        private string UpdateText { get; }
        private Update Update { get; }
        public static string QueryParam { get; set; }
        private readonly InlineKeyboardMarkup InlineKeyboard = new(Keyboard.GetInlineKeyboard(Consts.Options));
       
        public InputHandler(Update update, UpdateType type) {
            Update = update;
            if (type == UpdateType.Message) {
                UpdateText = update.Message.Text;
                ChatIdStatic = update.Message.Chat.Id;
            } else {
                UpdateText = update.CallbackQuery.Data;
                ChatIdStatic = update.CallbackQuery.Message.Chat.Id;
            }
        }

        public async Task HandleInput() {
            if (BotSupportClass.State is State.GetQuery) {
                await HandleQueriesAsync();
            } else {
                await GetParamsAsync();
            }
        }

        private async Task GetParamsAsync() {
            var numParamsOfQuery = Consts.NumberOfParams[BotSupportClass.LastUpdateMessage];
            BotSupportClass.RemaininParams = numParamsOfQuery - BotSupportClass.Counter;
            QueryParam = UpdateText;
            BotSupportClass.ParamsList.Add(QueryParam);
            if (BotSupportClass.RemaininParams == 0) {
                BotSupportClass.State = State.GetQuery;
                BotSupportClass.Counter = 1;
                await ExecuteQuery(BotSupportClass.LastUpdateMessage, BotSupportClass.ParamsList);
                BotSupportClass.ParamsList = new List<string>();
            } else {
                BotSupportClass.Counter++;
                var paramType = Consts.LabelAndTypeParamsDictionary[BotSupportClass.LastUpdateMessage][BotSupportClass.Counter];
                await WriteMessage($"Please type the {paramType}'s name: ");
            }
        }

        public async Task HandleQueriesAsync() {
            if (Consts.Options.Contains(UpdateText)){
                if (Consts.NumberOfParams[UpdateText] != 0) {
                    await RequestForAParamAsync();
                } else {
                    await ExecuteQuery(UpdateText, null);
                }
            } else {
                await WriteMessage(Consts.Error, keyboard: InlineKeyboard);
            }
        }

        private async Task RequestForAParamAsync() {
            Type paramType;
            if (UpdateText == Consts.GET_A_SERVICE_FROM_ALL_HOSTS) {
                paramType = Type.service;
            } else {
                paramType = Type.host;
            }
            var insertParam = $"Please enter the {paramType} name you want to view: ";
            await WriteMessage(insertParam);
            BotSupportClass.LastUpdateMessage = UpdateText;
            BotSupportClass.State = State.GetParams;
        }

        private async Task ExecuteQuery(string queryName, List<string> paramsList) {
            var query = Query.GetQuery(queryName, paramsList);
            await WriteMessage("Processing informations...");
            var result = await query.Execute();
            var resultType = result.GetType();
            if (!resultType.Equals(typeof(string))) {
                await WriteMessage(query.Message);
                await SendPhoto(result);
                await WriteMessage("Done.", keyboard: InlineKeyboard);
            } else if (result == "" || result == Consts.ServerDownError) {
                    await WriteMessage(Consts.EmptyResultAnswer, keyboard: InlineKeyboard);
            } else {
                await WriteMessage(query.Message);
                await WriteMessage(result);
                await WriteMessage("Done.", keyboard: InlineKeyboard);
            }
        }
    

        public static async Task WriteMessage(string output, IReplyMarkup keyboard = null) {
            await BotComunication.TelegramClient.SendTextMessageAsync(ChatIdStatic, output, replyMarkup: keyboard);
        }


        public static async Task SendPhoto(InputOnlineFile fts, IReplyMarkup keyboard = null) {
            await BotComunication.TelegramClient.SendPhotoAsync(ChatIdStatic, fts, replyMarkup: keyboard);
        }

        public enum Type
        {
            host,
            service,
        }

        public enum State
        {
            GetParams,
            GetQuery,
        }
    }
}

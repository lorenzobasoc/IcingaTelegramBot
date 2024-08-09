using System.Linq;
using Telegram.Bot.Types.ReplyMarkups;

namespace IcingaBot.Keyboards
{
    public static class Keyboard
    {
        public static InlineKeyboardButton[][] GetInlineKeyboard(string[] stringArray) {
            var keyboardInline = new InlineKeyboardButton[stringArray.Length][];
            var keyboardButtons = new InlineKeyboardButton[stringArray.Length];
            for (var i = 0; i < stringArray.Length; i++) {

                keyboardButtons[i] = new InlineKeyboardButton {
                    Text = stringArray[i],
                    CallbackData = stringArray[i],
                };
            }
            for (var j = 1; j <= stringArray.Length; j++) {
                keyboardInline[j - 1] = keyboardButtons.Take(1).ToArray();
                keyboardButtons = keyboardButtons.Skip(1).ToArray();
            }
            return keyboardInline;
        }
    }
}

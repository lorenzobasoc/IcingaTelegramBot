using IcingaBot.Queries;
using System;

namespace IcingaBot.Models
{
    public class CommentModel
    {
        public string author { get; set; }
        public string entry_time { get; set; }
        public string text { get; set; }
        public string service_name { get; set; }
        public string name { get; set; }

        public override string ToString() {
            var substringTime = entry_time.Substring(0, QueriesSupportClass.StopSubstring(entry_time, '.', 0));
            var isEpoch = long.TryParse(substringTime, out var time);
            var dateTimeOffset = "";
            if (isEpoch) {
                dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(time).Date.ToShortDateString();
            }
            if (service_name == "") {
                return $"Author: {author}\n" +
                       $"Entry Time: {dateTimeOffset}\n" +
                       $"Text: {text}\n" +
                       $"Name: {name}\n";
            } else {
                return $"Author: {author}\n" +
                       $"Entry Time: {dateTimeOffset}\n" +
                       $"Text: {text}\n" +
                       $"Service name: {service_name}\n" +
                       $"Name: {name}\n";
            }
        }
    }
}

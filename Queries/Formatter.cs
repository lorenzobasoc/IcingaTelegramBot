using IcingaBot.Models;
using IcingaBot.Queries;
using Microcharts;
using Newtonsoft.Json;
using SkiaSharp;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot.Types.InputFiles;

namespace IcingaBot.ApiComunication
{
    public class Formatter
    {
        public static InputOnlineFile CreateChart(List<int> values, Dictionary<string, string> labels, int maxValue) {
            var keys = labels.Keys.ToArray();
            var entries = new ChartEntry[] {
                new ChartEntry(values[0])
                {
                    Label = keys[0],
                    TextColor = SKColor.Parse(labels[keys[0]]),
                    ValueLabel = values[0].ToString(),
                    ValueLabelColor = SKColor.Parse(labels[keys[0]]),
                    Color = SKColor.Parse(labels[keys[0]])
                },
                new ChartEntry(values[1])
                {
                    Label = keys[1],
                    TextColor = SKColor.Parse(labels[keys[1]]),
                    ValueLabel = values[1].ToString(),
                    ValueLabelColor = SKColor.Parse(labels[keys[1]]),
                    Color = SKColor.Parse(labels[keys[1]])
                },
                new ChartEntry(values[2])
                {
                    Label = keys[2],
                    TextColor = SKColor.Parse(labels[keys[2]]),
                    ValueLabel = values[2].ToString(),
                    ValueLabelColor = SKColor.Parse(labels[keys[2]]),
                    Color = SKColor.Parse(labels[keys[2]])
                },
                new ChartEntry(values[3])
                {
                    Label = keys[3],
                    TextColor = SKColor.Parse(labels[keys[3]]),
                    ValueLabel = values[3].ToString(),
                    ValueLabelColor = SKColor.Parse(labels[keys[3]]),
                    Color = SKColor.Parse(labels[keys[3]])
                },
                new ChartEntry(values[4])
                {
                    Label = keys[4],
                    TextColor = SKColor.Parse(labels[keys[4]]),
                    ValueLabel = values[4].ToString(),
                    ValueLabelColor = SKColor.Parse(labels[keys[4]]),
                    Color = SKColor.Parse(labels[keys[4]]),
                },
            };
            var chart = new BarChart() { Entries = entries, IsAnimated = false, LabelOrientation = Orientation.Horizontal, ValueLabelOrientation = Orientation.Horizontal, MaxValue = maxValue };
            var bmp = new SKBitmap(450, 450);
            var canvas = new SKCanvas(bmp);
            chart.DrawContent(canvas, 450, 450);
            canvas.Flush();
            canvas.Save();
            var image = SKImage.FromBitmap(bmp);
            var data = image.Encode(SKEncodedImageFormat.Png, 1000);
            var stream = data.AsStream();
            return new InputOnlineFile(stream);
        }

        public static string FormatHost(string response) {
            var formattedResponse = "";
            if (response != Consts.EmptyResultQuery) {
                formattedResponse = "        Host Info   |    Value\n|------------------------------|\n";
                var array = response.Split("{");
                var hostInfo = ("{" + array[3] + "{" + array[4])[0..^9];
                var hostModel = JsonConvert.DeserializeObject<HostModel>(hostInfo);
                formattedResponse += hostModel.ToString();
            }
            return formattedResponse;
        }

        public static string FormatComments(string response) {
            var formattedResponse = "";
            if (response != Consts.EmptyResultQuery) {
                var counter = 1;
                var singleCommentsList = GetSingleComments(response);
                foreach (var c in singleCommentsList) {
                    var commentModel = JsonConvert.DeserializeObject<CommentModel>(c);
                    formattedResponse += $"COMMENT N.{counter}\n-------------------------\n" + commentModel.ToString() + "\n-------------------------\n";
                    counter++;
                }
            }
            return formattedResponse;
        }

        private static List<string> GetSingleComments(string response) {
            var list = new List<string>();
            var array = response.Split("{");
            for (int i = 3; i < array.Length; i += 4) {
                var comment = array[i];
                comment = "{" + comment;
                var stopSubstring = QueriesSupportClass.StopSubstring(comment, '}', 0);
                var jsonComment = comment.Substring(0, stopSubstring) + ",";
                var name = array[i + 2];
                var nameStopSubstring = QueriesSupportClass.StopSubstring(name, '!', 2);
                var jsonName = name.Substring(2, nameStopSubstring);
                jsonName = jsonName + "\"}";
                var json = jsonComment + jsonName;
                list.Add(json);
            }
            return list;
        }

        public static string FormatAttributesService(string response) {
            var formattedResponse = "";
            if (response != Consts.EmptyResultQuery) {
                formattedResponse = "   Service attrbute's Name |  Value\n------------------------------\n";
                var array = response.Split("{");
                var attributesInfo = ("{" + array[3])[0..^9];
                var serviceAttributeModel = JsonConvert.DeserializeObject<ServiceAttributesModel>(attributesInfo);
                formattedResponse += serviceAttributeModel.ToString();
            }
            return formattedResponse;
        }

        public static string FormatServicesState(string response) {
            var formattedResponse = "";
            if (response != Consts.EmptyResultQuery) {
                formattedResponse = "  Service Name          |State\n----------------------------------\n";
                var singleServicesList = GetSingleService(response);
                foreach (var s in singleServicesList) {
                    var serviceModel = JsonConvert.DeserializeObject<ServiceStateModel>(s);
                    formattedResponse += serviceModel.ToString();
                }

            }
            return  formattedResponse;
        }

        private static List<string> GetSingleService(string response) {
            var list = new List<string>();
            var array = response.Split("{");
            for (int i = 3; i < array.Length; i += 4) {
                var service = array[i];
                service = "{" + service;
                var stopSubstring = QueriesSupportClass.StopSubstring(service, '}', 0);
                var jsonComment = service.Substring(0, stopSubstring) + "}";
                list.Add(jsonComment);
            }
            return list;
        }
    }
}

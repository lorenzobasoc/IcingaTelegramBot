namespace IcingaBot.Models
{
    public class BodyRequestModels
    {
        public string[] attrs { get; set; }
        public string filter { get; set; }

        public BodyRequestModels(string[] attr, string filters) {
            attrs = attr;
            filter = filters;
        }
    }
}

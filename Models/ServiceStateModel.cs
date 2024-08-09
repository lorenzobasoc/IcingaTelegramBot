namespace IcingaBot.Models
{
    public class ServiceStateModel
    {
        public string name { get; set; }
        public string state { get; set; }
        public string acknowledgement { get; set; }

        public override string ToString() {
            if (state == "2.0" && acknowledgement == "1.0") {
                state = "4.0";
            }
            if (name.Length + state.Length < 34) {
                for (int i = name.Length + state.Length; i < 34; i++) {
                    name += " ";
                }
            }
            name += "|";
            return $"| {name} {state} |\n";
        }
    }
}

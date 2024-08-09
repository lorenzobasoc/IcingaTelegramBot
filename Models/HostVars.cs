namespace IcingaBot.Models
{
    public class HostVars
    {
        public string geolocation { get; set; }
        public string ip1 { get; set; }
        public string ip2 { get; set; }
        public string solari_ip1 { get; set; }
        public string solari_ip2 { get; set; }
        public string solari_type { get; set; }
        public string stock_biv { get; set; }

        public override string ToString() {
            var labels = new string[] { "Geolocation:", "Solari ip1:", "Solari ip2:", "Stock biv:"};
            var values = new string[] { geolocation, solari_ip1, solari_ip2, stock_biv};
            for (int i = 0; i < labels.Length; i++) {
                for (int j = labels[i].Length + values[i].Length; j < 32; j++) {
                    labels[i] += " ";
                }
            }
            return $"{labels[0]} {geolocation}|\n|{labels[1]} {solari_ip1}|\n|{labels[2]} {solari_ip2}|\n|{labels[3]} {stock_biv}";
        }
    }
}

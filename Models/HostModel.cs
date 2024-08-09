using IcingaBot.Queries;

namespace IcingaBot.Models
{
    public class HostModel
    {
        public string Acknowledgement { get; set; }
        public string Display_name { get; set; }
        public string[] Groups { get; set; }
        public string Last_check { get; set; }
        public string Last_state_change { get; set; }
        public string Next_check { get; set; }
        public string State { get; set; }
        public HostVars Vars { get; set; }

        public override string ToString() {
            var labels = new string[] { "Acknowledgement:", "Display name:", "Group:", "Last check:", "Last state change:", "Next check:", "State:" };
            var values = new string[] { Acknowledgement, Display_name, Groups[0], QueriesSupportClass.DataConverter(Last_check), QueriesSupportClass.DataConverter(Last_state_change), QueriesSupportClass.DataConverter(Next_check), State};
            for (int i = 0; i < labels.Length; i++) { 
                for (int j = labels[i].Length+values[i].Length; j < 32; j++) {
                    labels[i] += " ";
                }
            }
            if (State == "1") { 
                return $"|{labels[0]} {Acknowledgement}|\n|{labels[1]} {Display_name}|\n|{labels[2]} {Groups[0]}|\n|{labels[3]} {QueriesSupportClass.DataConverter(Last_check)}|\n|{labels[4]} {QueriesSupportClass.DataConverter(Last_state_change)}|\n|{labels[5]} {QueriesSupportClass.DataConverter(Next_check)}|\n|{labels[6]} {State}|\n|{Vars.ToString()}";
            } else {
                return $"|{labels[1]} {Display_name}|\n|{labels[2]} {Groups[0]}|\n|{labels[3]} {QueriesSupportClass.DataConverter(Last_check)}|\n|{labels[4]} {QueriesSupportClass.DataConverter(Last_state_change)}|\n|{labels[5]} {QueriesSupportClass.DataConverter(Next_check)}|\n|{labels[6]} {State}|\n|{Vars.ToString()}";
            }
        }

      
    }
}

using IcingaBot.Queries;

namespace IcingaBot.Models
{
    public class ServiceAttributesModel
    {
        public string acknowledgement { get; set; }
        public string last_check { get; set; }
        public string state { get; set; }

        public override string ToString() {
            if (state == "1") {
                return $"|Acknowledgement: {acknowledgement}|\n|Last check: {QueriesSupportClass.DataConverter(last_check)}|\n|State: {state}|\n";
            } else {
                return $"|Last check: {QueriesSupportClass.DataConverter(last_check)}|\n|State: {state}|\n";
            }
        }
    }
}

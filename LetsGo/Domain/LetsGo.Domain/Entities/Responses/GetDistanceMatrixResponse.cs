using System.Collections.Generic;

namespace LetsGo.Domain.Entities.Responses
{
    public class GetDistanceMatrixResponse
    {
        public List<string> Destination_Addresses { get; set; }
        public List<string> Origin_Addresses { get; set; }
        public List<Destinations> Rows { get; set; }
        public string Status { get; set; }
    }

    public class Destinations
    {
        public List<Destination> Elements { get; set; }
    }

    public class Destination
    {
        public TextValue Distance { get; set; }
        public TextValue Duration { get; set; }
        public string Status { get; set; }
    }

    public class TextValue
    {
        public string Text { get; set; }
        public int Value { get; set; }
    }
}
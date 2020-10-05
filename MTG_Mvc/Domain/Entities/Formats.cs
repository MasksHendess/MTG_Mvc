using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTG_Mvc.Domain.Entities
{
    public class Formats
    {
        public List<string> formats { get; set; } = new List<string>();
        public Formats()
        {
            formats.Add("Standard");
            formats.Add("Modern");
            formats.Add("Legacy");

            formats.Add("EDH - Commander");
            formats.Add("Canadian Highlander");

            formats.Add("Historic");
            formats.Add("Brawl");

            formats.Add("Pioneer");
            formats.Add("Pauper");
            formats.Add("Jumpstart");
            formats.Add("KitchenTable");
            formats.Add("Block Constructed");
            formats.Add("Draft");
            formats.Add("Limited");

        }
    }
}

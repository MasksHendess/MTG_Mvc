using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MTG_Mvc.Models
{
    public class decklist
    {
        public string id { get; set; }
        public List<card> cards { get; set; } = new List<card>();
        
    }
}

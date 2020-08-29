using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MTG_Mvc.Models
{
    public class card
    {
        public int id { get; set; }
        public int quantity{ get; set; }
        public string name{ get; set; }
        public string set { get; set; }

    }
}

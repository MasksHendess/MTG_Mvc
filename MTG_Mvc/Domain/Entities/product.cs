using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MTG_Mvc.Domain.Entities
{
    public class product
    {
        public string id { get; set; }
        public string cmc { get; set; }
        [JsonPropertyName("img")]
        public string img { get; set; }
        public string cardtitle { get; set; }
        public string description { get; set; }

        public int[] ratings { get; set; }

        public override string ToString () => JsonSerializer.Serialize<product>(this);
        
    }
}

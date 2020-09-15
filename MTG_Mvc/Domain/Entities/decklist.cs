using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MTG_Mvc.Domain.Entities
{
    public class decklist
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string deckName { get; set; }
        public int cardsAmount { get; set; }
        public int creaturesAmount { get; set; }
        public int sorceriesAmount { get; set; }
        public int instantsAmount { get; set; }
        public int landsAmount { get; set; }

        public int avarageCMC { get; set; }
        public List<card> cards { get; set; } = new List<card>();
    }
}

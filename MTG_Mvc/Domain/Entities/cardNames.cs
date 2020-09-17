using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MTG_Mvc.Domain.Entities
{
    public class cardNames
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string firstName { get; set; }
        public string secondName { get; set; }
        [ForeignKey("cards")]
        public int cardid { get; set; }
    }
}

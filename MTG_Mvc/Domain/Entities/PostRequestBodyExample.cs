using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTG_Mvc.Domain.Entities
{
    public class PostRequestBodyExample
    {
        public List<string> exampleCards { get; set; } = new List<string>();
        public PostRequestBodyExample()
        {
            exampleCards.Add("4 Cauldron Familiar (ELD) 81");
            exampleCards.Add("4 Archfiends Vessel (M21) 88");
            exampleCards.Add("3 Mountain (ELD)");
            exampleCards.Add("7 Swanp (ELD)");
            exampleCards.Add("4 Witch's Over (ELD) 237");
        }
    }
}
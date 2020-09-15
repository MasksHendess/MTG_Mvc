using MTG_Mvc.DBContext;
using MTG_Mvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTG_Mvc.Repositories
{
    public class cardRepository : IcardRepositoryInterface
    {
        #region properties
        private readonly SqlDbContext dbContext;
        #endregion
        #region constructor
        public cardRepository(SqlDbContext DBContext)
        {
            dbContext = DBContext;
        }
        #endregion
        public bool checkIfCardExsists(card card)
        {
            var cards = dbContext.cards.ToList();
            foreach (var dbCard in cards)
            {
                if (card.name == dbCard.name)
                    return true;
            }

            return false;
        }
        public card GetCard(card Card)
        {
            var cards = dbContext.cards.Where(x => x.name == Card.name).FirstOrDefault();
            Card.imageUrl = cards.imageUrl;   //deckList.FirstOrDefault().ImageUrl;
            Card.artist = cards.artist; //deckList.FirstOrDefault().Artist;

            Card.set = cards.set;
            Card.cmc = Convert.ToDecimal(cards.cmc); // Mana Cost number
            Card.manaCost = cards.manaCost; // Mana Cost string {1}{R}
            Card.flavourText = cards.flavourText;
            Card.rarity = cards.rarity;
            Card.type = cards.type;
            Card.text = cards.text;
            Card.power = cards.power;
            Card.toughness = cards.toughness;
            return Card;
        }

    }
}

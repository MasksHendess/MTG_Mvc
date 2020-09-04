using Microsoft.EntityFrameworkCore;
using MTG_Mvc.DBContext;
using MTG_Mvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTG_Mvc.Repositories
{
    public class decklistRepository : IdecklistRepositoryInterface
    {
        private readonly SqlDbContext dbContext;
        public decklistRepository(SqlDbContext DBContext)
        {
            dbContext = DBContext;
        }

        public async Task<IEnumerable<decklist>> GetAllDeckListsAsync()
        {
           // var cards = dbContext.cards.ToList();
            var decks = await dbContext.decklists.ToListAsync();
            foreach (var item in decks)
            {
                var cards = dbContext.cards.Where(x => x.decklistid == item.id).ToList();
                item.cards = cards;
            }
            return decks;
        }
        public async Task<decklist> GetDeckListByIdAsync(int id)
        {
            var cards = dbContext.cards.Where(x => x.decklistid == id).ToList();
            var deck = await dbContext.decklists.FirstOrDefaultAsync(x => x.id == id);
            if(deck!= null)
            { 
            deck.cards = cards;
            }
            return deck;
        }
        public void Delete(decklist decklist)
        {
            dbContext.decklists.Remove(decklist);
            dbContext.SaveChanges();
        }

        public void Post(decklist decklist)
        {
            dbContext.decklists.Add(decklist);
            dbContext.SaveChanges();
        }

        public void Update(decklist decklist)
        {
            dbContext.decklists.Update(decklist);
            dbContext.SaveChanges();
        }
    }
}

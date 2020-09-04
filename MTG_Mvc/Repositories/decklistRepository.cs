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
            return await dbContext.decklists.ToListAsync();
        }
        public async Task<decklist> GetDeckListByIdAsync(int id)
        {
            // var result = dbContext.cards.Where(x => x.decklistid == id).ToList();
            return await dbContext.decklists.FirstOrDefaultAsync(x => x.id == id); ;
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
    }
}

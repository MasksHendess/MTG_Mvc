using MTG_Mvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTG_Mvc.Repositories
{
    public interface IdecklistRepositoryInterface
    {
        Task<IEnumerable<decklist>> GetAllDeckListsAsync();

        Task<decklist> GetDeckListByIdAsync(int id);

        void Delete(decklist decklist);
        
    }
}

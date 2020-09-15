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
        IEnumerable<decklist> GetAllDeckLists();
        Task<decklist> GetDeckListByIdAsync(int id);
        void Delete(decklist decklist);
        void Post(decklist decklist);
        void Update(decklist decklist);
    }
}

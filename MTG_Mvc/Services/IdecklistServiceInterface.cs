using MTG_Mvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTG_Mvc.Services
{
    public interface IdecklistServiceInterface
    {
        Task<IEnumerable<decklist>> GetAllDeckListsAsync();
        Task<decklist> GetDeckListByIdAsync(int id);
        Task<decklist> DeleteDeckList(int id);
        Task<decklist> CreateNewDeckListFromTXTAsync(List<card> cardsInDeck);
        Task<List<card>> fetchCardInformationFromAPI(List<card> cardList);
        List<card> convertRequestBodyToCardList(string decklist);
        Task<decklist> UpdateDeckListAsync(decklist decklist);
    }
}

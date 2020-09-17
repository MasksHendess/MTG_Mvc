using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MTG_Mvc.DBContext;
using MTG_Mvc.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MTG_Mvc.Repositories;
using MtgApiManager.Lib.Model;
using System.Security.Policy;
using MTG_Mvc.APIControllers;

namespace MTG_Mvc.Services
{
    public class decklistService : IdecklistServiceInterface
    {
        #region properties
        private readonly IdecklistRepositoryInterface idecklistRepository;
        private readonly IcardRepositoryInterface icardRepository;
        private readonly mtgioAPIController mtgioAPIController;
        #endregion
        #region constructor
        public IWebHostEnvironment WebHostEnvironment { get; }
        public decklistService(IWebHostEnvironment webHostEnvironment, IdecklistRepositoryInterface DecklistRepository,
        IcardRepositoryInterface _icardRepositoryInterface, mtgioAPIController _mtgioAPIController)
        {
            WebHostEnvironment = webHostEnvironment;
            idecklistRepository = DecklistRepository;
            icardRepository = _icardRepositoryInterface;
            mtgioAPIController = _mtgioAPIController;
        }
        #endregion
        #region publicAsyncFunctions
        public async Task<IEnumerable<decklist>> GetAllDeckListsAsync()
        {
            return await idecklistRepository.GetAllDeckListsAsync();
        }

        public async Task<decklist> GetDeckListByIdAsync(int id)
        {
            return await idecklistRepository.GetDeckListByIdAsync(id);
        }

        public async Task<decklist> DeleteDeckList(int id)
        {
            var decklist = await idecklistRepository.GetDeckListByIdAsync(id);
            idecklistRepository.Delete(decklist);
            return decklist;
        }

        public async Task<decklist> UpdateDeckListAsync(decklist decklist)
        {
            idecklistRepository.Update(decklist);
            return await idecklistRepository.GetDeckListByIdAsync(decklist.id);
        }
        public async Task<List<string>> CreateNewDecklist(string requestBody)
        {
            List<string> result = new List<string>();
            try
            {
                decklist deckList = new decklist();

                deckList.deckName = "Default Deck Name";
                var requestList = convertRequestToList(requestBody); // name set and quantity isMainboard
                foreach (var card in requestList)
                {
                    card newCard = new card();
                    var cardExsists = icardRepository.checkIfCardExsists(card);
                    if (!cardExsists)
                    {
                        var cardInfo = await getCardInfoFromAPI(card.name, card.set);

                        
                        newCard = mapCardInfo(card, cardInfo, false);
                        deckList.cards.Add(newCard);

                        if (cardInfo.FirstOrDefault().Names != null)
                        {
                            card otherCard = new card();
                            otherCard = mapCardInfo(otherCard, cardInfo, true);
                            deckList.cards.Add(otherCard);
                        }
                    }
                    else
                    {
                        newCard = icardRepository.GetCard(card);
                        deckList.cards.Add(newCard);
                    }
                }
                deckList = addCardTypeQuantityToDecklist(deckList);
                saveDecklistToDB(deckList);

               
                result = deckList.cards.Select(x => x.name).OrderBy(name => name).ToList();
            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong.", e);
            }

            return result;
        }
        #endregion
        #region SyncronuspublicFunctions
        public IEnumerable<decklist> GetAllDeckLists()
        {
            return idecklistRepository.GetAllDeckLists();
        }

        #endregion
        #region privateFunctions
        private async Task<List<Card>> getCardInfoFromAPI(string cardName, string cardSet)
        {
            var result = await mtgioAPIController.getCardbyCardName(cardName, cardSet);
            return result;
        }

        private card mapCardInfo(card newCard, List<Card> cards, bool splitCard)
        {
            if (splitCard)
            {

                var obj = cards.Where(x => x.ImageUrl != null).LastOrDefault();
                newCard.imageUrl = obj.ImageUrl;   //deckList.FirstOrDefault().ImageUrl;
                newCard.artist = obj.Artist; //deckList.FirstOrDefault().Artist;

                newCard.set = cards.FirstOrDefault().Set;

                newCard.cmc = Convert.ToDecimal(cards.LastOrDefault().Cmc); // Mana Cost number
                newCard.manaCost = cards.LastOrDefault().ManaCost; // Mana Cost string {1}{R}
                newCard.flavourText = cards.LastOrDefault().Flavor;
                newCard.rarity = cards.LastOrDefault().Rarity;
                newCard.type = cards.LastOrDefault().Type;
                newCard.text = cards.LastOrDefault().Text;
                newCard.power = cards.LastOrDefault().Power;
                newCard.toughness = cards.LastOrDefault().Toughness;

                newCard.name = cards.LastOrDefault().Name;

               // icardRepository.PostcardNames(names);
            }
            else
            { 
            var OBJ = cards.Where(x => x.ImageUrl != null).FirstOrDefault();
            newCard.imageUrl = OBJ.ImageUrl;   //deckList.FirstOrDefault().ImageUrl;
            newCard.artist = OBJ.Artist; //deckList.FirstOrDefault().Artist;

            newCard.set = cards.FirstOrDefault().Set;
            newCard.cmc = Convert.ToDecimal(cards.FirstOrDefault().Cmc); // Mana Cost number
            newCard.manaCost = cards.FirstOrDefault().ManaCost; // Mana Cost string {1}{R}
            newCard.flavourText = cards.FirstOrDefault().Flavor;
            newCard.rarity = cards.FirstOrDefault().Rarity;
            newCard.type = cards.FirstOrDefault().Type;
            newCard.text = cards.FirstOrDefault().Text;
            newCard.power = cards.FirstOrDefault().Power;
            newCard.toughness = cards.FirstOrDefault().Toughness;
                if (cards.FirstOrDefault().Names != null)
                {
                    cardNames names = new cardNames();
                    names.firstName = cards.FirstOrDefault().Names[0];
                    names.secondName = cards.FirstOrDefault().Names[1];
                    newCard.cardNames = names;
                }
            }

            return newCard;
        }
        private List<card> convertRequestToList(string Decklist)
        {
            List<card> result = new List<card>();
            string[] splitRequestBody = Decklist.Split("\n");

            bool isSideBoardCard = false;
            foreach (var line in splitRequestBody)
            {
                if (!isSideBoardCard)
                    isSideBoardCard = checkIfCardIsSideboardCard(line);

                if (line != "Deck\r" && line != "Sideboard\r" && line != "Commander\r" && line != "\r")
                {
                    string[] splitLine = line.Split(" ");               // [0]20    [1]Mountain    [2](IKO)
                                                                        // This works great except that cardnames can contain whitespaces 

                    string name = "";
                    int i = 0;
                    foreach (var item in splitLine)
                    {

                        if (item.Contains("("))
                        {
                            name = name.Substring(0, name.Length - 1);
                            card NewCard = new card();
                            NewCard.name = name;
                            NewCard.set = item;
                            NewCard.quantity = Convert.ToInt32(splitLine[0]);
                            NewCard.isMainBoard = !isSideBoardCard;
                            result.Add(NewCard);

                        }
                        if (i > 0) // item 0 is not part of name
                        {
                            name += item + " ";
                        }
                        i++;
                    }
                }
            }
            return result;
        }

        private void saveDecklistToDB(decklist decklist)
        {
            try
            {
                idecklistRepository.Post(decklist);

            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong.", e);
            }
        }

        private decklist addCardTypeQuantityToDecklist(decklist decklist)
        {
            decimal sum = 0;
            foreach (var card in decklist.cards)
            {
                if (card.type.Contains("Land"))
                    decklist.landsAmount += card.quantity;

                if (card.type.Contains("Instant"))
                    decklist.instantsAmount += card.quantity;

                if (card.type.Contains("Sorcery"))
                    decklist.sorceriesAmount += card.quantity;

                if (card.type.Contains("Creature"))
                    decklist.creaturesAmount += card.quantity;

                if (card.isMainBoard)
                    decklist.cardsAmount += card.quantity;

                sum += card.cmc;
            }
            decklist.avarageCMC = Convert.ToInt32(sum / decklist.cards.Count);

            return decklist;
        }
        private bool checkIfCardIsSideboardCard(string line)
        {
            if (line == "Sideboard\r")
            {
                return true;
            }
            else
                return false;
        }
        #endregion
    }
}
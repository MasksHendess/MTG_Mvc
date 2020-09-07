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
        private readonly IdecklistRepositoryInterface decklistRepository;
        private readonly mtgioAPIController mtgioAPIController;

        public IWebHostEnvironment WebHostEnvironment { get; }
        public decklistService(IWebHostEnvironment webHostEnvironment, IdecklistRepositoryInterface DecklistRepository, mtgioAPIController _mtgioAPIController)
        {
            WebHostEnvironment = webHostEnvironment;
            decklistRepository = DecklistRepository;
            mtgioAPIController = _mtgioAPIController;
        }

        public async Task<IEnumerable<decklist>> GetAllDeckListsAsync()
        {
            return await decklistRepository.GetAllDeckListsAsync();
        }

        public async Task<decklist> GetDeckListByIdAsync(int id)
        {
            return await decklistRepository.GetDeckListByIdAsync(id);
        }

        public async Task<decklist> DeleteDeckList(int id)
        {
            var decklist = await decklistRepository.GetDeckListByIdAsync(id);
            decklistRepository.Delete(decklist);
            return decklist;
        }

        public async Task<decklist> UpdateDeckListAsync(decklist decklist)
        {
            decklistRepository.Update(decklist);
            return await decklistRepository.GetDeckListByIdAsync(decklist.id);
        }
        public async Task<decklist> CreateNewDeckListFromTXTAsync(List<card> cardsInDeck)
        {
            decklist NewDeck = new decklist();

            if (cardsInDeck != null)
            {
                NewDeck.deckName = "Default Deck Name";
                NewDeck.cards = cardsInDeck;
                decklistRepository.Post(NewDeck);
            }
            return NewDeck;
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
        public async Task<List<card>> fetchCardInformationFromAPI(List<card> cardList)
        {
            var deckList = new List<MtgApiManager.Lib.Model.Card>();
            foreach (var card in cardList)
            {
                deckList = await mtgioAPIController.getCardbyCardName(card.name);
                if(deckList != null && deckList.Count > 0)
                { 
                card.imageUrl = deckList.FirstOrDefault().ImageUrl;
                card.set = deckList.FirstOrDefault().Set;
                card.artist = deckList.FirstOrDefault().Artist;
                card.cmc = Convert.ToDecimal(deckList.FirstOrDefault().Cmc);
                card.flavourText = deckList.FirstOrDefault().Flavor;
                card.rarity = deckList.FirstOrDefault().Rarity;
                card.type = deckList.FirstOrDefault().Type;
                card.text = deckList.FirstOrDefault().Text;
                }
            }

            return cardList;
        }
        public List<card> convertRequestBodyToCardList(string Decklist)
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
                            //NewCard.set = item;
                            name = name.Substring(0, name.Length - 1);
                            card NewCard = new card();
                            NewCard.name = name;
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
    }
}
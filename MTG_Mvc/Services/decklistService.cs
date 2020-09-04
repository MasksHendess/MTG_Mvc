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

namespace MTG_Mvc.Services
{
    public class decklistService : IdecklistServiceInterface
    {
       // private readonly SqlDbContext dbContext;
        private readonly IdecklistRepositoryInterface decklistRepository;
        public IWebHostEnvironment WebHostEnvironment { get; }
        public decklistService(IWebHostEnvironment webHostEnvironment, IdecklistRepositoryInterface DecklistRepository)
        {
            WebHostEnvironment = webHostEnvironment;
            decklistRepository = DecklistRepository;
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

        private bool checkIfCardIsSideboardCard(string line)
        {
            if (line == "Sideboard\r")
            {
                return true;
            }
            else
                return false;
        }
        public decklist PostDeckList(string Decklist)
        {
            string[] splitRequestBody = Decklist.Split("\n");
            decklist NewDeck = new decklist();

            bool isSideBoardCard = false;
            foreach (var line in splitRequestBody)
            {
                if(!isSideBoardCard)
                    isSideBoardCard = checkIfCardIsSideboardCard(line);
               
                if (line != "Deck\r" && line != "Sideboard\r" && line !="Commander\r" && line !="\r")
                {
                    card NewCard = new card();

                    string[] splitLine = line.Split(" ");               // [0]20    [1]Mountain    [2](IKO)
                    // This works great except that cardnames can contain whitespaces 
                    NewCard.quantity = Convert.ToInt32(splitLine[0]);

                    string name = "";
                    int i = 0;
                    foreach (var item in splitLine)
                    {
                        if (item.Contains("("))
                        {
                            NewCard.set = item;
                            NewCard.name = name.Substring(0, name.Length - 1);
                        }
                        if (i > 0) // item 0 is not part of name
                        {
                            name += item + " ";
                        }
                        i++;
                    }
                   NewCard.isMainBoard = !isSideBoardCard;
                   NewDeck.cards.Add(NewCard);
                }
            }

            if (NewDeck != null)
            {
                NewDeck.deckName = "Default Deck Name";
               decklistRepository.Post(NewDeck);
            }
            return NewDeck;
        }

    }
}
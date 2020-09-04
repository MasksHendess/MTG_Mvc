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
        private readonly decklistRepository decklistRepository;
        public IWebHostEnvironment WebHostEnvironment { get; }
        public decklistService(IWebHostEnvironment webHostEnvironment, decklistRepository DecklistRepository)
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
        public decklist PostDeckList(string Decklist)
        {
            string[] splitRequestBody = Decklist.Split("\n");
            decklist NewDeck = new decklist();

            foreach (var line in splitRequestBody)
            {
                if (line != "Deck\r")
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
                        NewDeck.cards.Add(NewCard);
                }
            }

            if (NewDeck != null)
            {
                decklistRepository.Post(NewDeck);
                //dbContext.decklists.Add(NewDeck);
                //dbContext.SaveChanges();
            }
            return NewDeck;
        }

    }
}
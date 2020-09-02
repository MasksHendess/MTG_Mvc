using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MTG_Mvc.DBContext;
using MTG_Mvc.Models;
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
using MTG_Mvc.Interface;

namespace MTG_Mvc.Services
{
    public class decklistService : IdecklistInterface
    {
        private readonly SqlDbContext dbContext;
        public IWebHostEnvironment WebHostEnvironment { get; }
        public decklistService(SqlDbContext DbContext, IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
            dbContext = DbContext;
        }

        public List<decklist> GetAllDeckLists()
        {
            var result = dbContext.decklists.ToList();
            var cards = dbContext.cards.ToList();
            foreach (var item in result)
            {
                var dbCards = cards.Where(x => x.decklistid == item.id).ToList();
                item.cards = dbCards;
            }
            return result;
        }

        public List<card> GetDeckList(int id)
        {
            var result = dbContext.cards.Where(x => x.decklistid == id).ToList();
            return result;
        }

        public string DeleteDeckList(int id)
        {
            decklist decklist = dbContext.decklists.Where(x => x.id == id).FirstOrDefault();
            dbContext.decklists.Remove(decklist);
            dbContext.SaveChanges();
            return "Item Deleted";
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
                dbContext.decklists.Add(NewDeck);
                dbContext.SaveChanges();
            }
            return NewDeck;
        }

    }
}
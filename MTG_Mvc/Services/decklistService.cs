using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MTG_Mvc.DBContext;
using MTG_Mvc.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Threading.Tasks;

namespace MTG_Mvc.Services
{
    public class decklistService
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
            return result;
        }

        public decklist PostDeckList(string Decklist)
        {
            string[] splitRequestBody = Decklist.Split("\n");

            decklist NewDeck = new decklist();
            int id = dbContext.decklists.Count() + 1;
            NewDeck.id = id.ToString();


            foreach (var line in splitRequestBody)
            {
                card NewCard = new card();
                //NewCard.id = id;
                //id++;

                int quantity = line[0] - '0';
                NewCard.quantity = quantity;

                int subStringIndex = 0;
                foreach (var setSign in line)
                {
                    if (setSign == '(')
                    {
                        subStringIndex = line.IndexOf(setSign);
                    }

                    NewCard.set = line.Substring(subStringIndex + 1, 3);
                }

                int index = line.IndexOf('(');
                if (index > 0)
                {
                    NewCard.name = line.Substring(1, index - 1);
                }

                NewDeck.cards.Add(NewCard);
            }

            dbContext.decklists.Add(NewDeck);
            dbContext.SaveChangesAsync();

            return NewDeck;
        }

    }
}

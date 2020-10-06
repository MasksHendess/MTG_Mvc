using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using MTG_Mvc.DBContext;
using MTG_Mvc.Services;
using MTG_Mvc.Domain.Entities;
using Newtonsoft.Json;
using MtgApiManager.Lib.Service;
using MtgApiManager.Lib.Core;
using MtgApiManager.Lib.Model;
using System.Text.Json;
using MTG_Mvc.Repositories;
using Microsoft.Extensions.FileProviders;

namespace MTG_Mvc.APIControllers
{
    [Route("api/[controller]")] // api/mtgioAPI
    [ApiController]
    public class mtgioAPIController : ControllerBase
    {
        private readonly SqlDbContext dbContext;
        public mtgioAPIController(SqlDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        [HttpGet]
        [Route("test/{cardName}")]
        public async Task<List<Card>> getCardbyCardName(string cardName, string cardSet)
        {
            CardService service = new CardService();
            //Exceptional<List<Card>> result = new Exceptional<List<Card>>();
            List<Card> value = new List<Card>();
            Exceptional<List<Card>> result = new Exceptional<List<Card>>();

            if (cardName.Contains("/")) // Cards that contain / are split cards or aftermath cards
            {
                var splitcard = cardName.Split(" ");
                // cardName = cardName.Substring(0, cardName.IndexOf("/"));
                if (cardSet == "(AKR)" || cardSet == "(AKH)" || cardSet == "(HOU)")
                {
                    // Amonkhet Remastered, Amonkhet, Hour of Devestation 
                    // The only sets that contains all unique aftermath cards (some reprits exist in some commander product)

                    result = await service.Where(x => x.Name, splitcard[0]).Where(x => x.Layout, "aftermath").AllAsync(); 
                    //   var resultB = await service.Where(x => x.Name, splitcard[2]).Where(x => x.Layout, "aftermath").AllAsync();
                }
                else
                {
                    result = await service.Where(x => x.Name, splitcard[0]).Where(x => x.Layout, "split").AllAsync();
                }
            }
            else
            {
                result = await service.Where(x => x.Name, cardName).AllAsync();
            }

           // result = await service.Where(x => x.Name, cardName).AllAsync();

            if (result.IsSuccess)
            {
                bool cardHasTwoNames = false;
                string secondName = "";
                foreach (var card in result.Value)
                {
                    value.Add(card); //= result.Value;
                    if (card.Names != null)
                    {
                        cardHasTwoNames = true;
                        secondName = card.Names[1];
                    }
                }

                if(cardHasTwoNames)
                {
                     result = await service.Where(x => x.Name, secondName).AllAsync();
                    if (result.IsSuccess)
                    {
                        foreach (var card in result.Value)
                        {
                            value.Add(card);
                        }
                    }
                }
            }
            else
            {
                var exception = result.Exception;
            }
            return value;
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<List<Card>>> GetCardsFromAPI(int id)
        {
            var cards = dbContext.cards.Where(x => x.decklistid == id).ToList();
            CardService service = new CardService();
            Exceptional<List<Card>> result = new Exceptional<List<Card>>();
            List<Card> value = new List<Card>();
            foreach (var item in cards)
            {
                result = await (service.Where(x => x.Name, item.name).AllAsync()); // Spam that API!
                if (result.IsSuccess)
                {
                    foreach (var card in result.Value)
                    {
                        value.Add(card); //= result.Value;
                    }
                }
                else
                {
                    var exception = result.Exception;
                }
            }

            foreach (var card in cards)
            {
                var matchingCards = value.Where(x => x.Name == card.name);
                card.imageUrl = matchingCards.FirstOrDefault().ImageUrl;
                dbContext.cards.Update(card);
                dbContext.SaveChanges();
            }
            return Ok(value);
        }

        //[HttpGet]
        //[Route("{id}")]

        //[HttpPost]

        //[HttpDelete]
        //[Route("{id}")]

    }
}

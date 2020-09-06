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
        public async Task<ActionResult<List<Card>>> GetDecklists()
        {
            var cards = dbContext.cards.Where(x => x.decklistid == 4).ToList();
           // var decklist = dbContext.decklists.FirstOrDefault();
            
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

            foreach (var wigga in cards)
            {
                var dude = value.Where(x => x.Name == wigga.name);
                wigga.imageUrl = dude.FirstOrDefault().ImageUrl;
                dbContext.cards.Update(wigga);
                dbContext.SaveChanges();
            }
            // Exceptional<List<Card>> result = service.All(); 
            // var result = service.Where(x => x.Name, "Viashino Pyromancer").All();
          
            
            
            return Ok(value);
        }

        //[HttpGet]
        //[Route("{id}")]

        //[HttpPost]

        //[HttpDelete]
        //[Route("{id}")]

    }
}

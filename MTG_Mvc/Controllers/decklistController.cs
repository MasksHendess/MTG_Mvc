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
using MTG_Mvc.Models;
using MTG_Mvc.Services;
using Newtonsoft.Json;

namespace MTG_Mvc.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class decklistController : ControllerBase
    {
        private readonly SqlDbContext dbContext; 
        public decklistService deckListService { get; set; }
        public decklistController( SqlDbContext DbContext, decklistService DeckListService)
        {
            dbContext = DbContext;
            deckListService = DeckListService;
        }

        [HttpDelete]
        [Route("/{id}")] //products/decklist
        public ActionResult DeleteDeck(string id)
        {
            //decklist Killme = dbContext.decklists.Where(x => x.id == id).FirstOrDefault();
            //if (Killme != null)
            //{
            //    dbContext.decklists.Remove(Killme);
            //    dbContext.SaveChanges();
            //}
            return Ok();
        }

        [HttpGet]
       // [Route("decklist")] //products/decklist
        public ActionResult<List<decklist>> GetDecklists()
        {
            var result = deckListService.GetAllDeckLists();
            return result;
        }

        [HttpPost]
       // [Route("decklist")] //products/decklist
        public async Task<IActionResult> Index()
        {
            var reader = new StreamReader(HttpContext.Request.Body);
            string requestBody = await reader.ReadToEndAsync();
            var NewDeck = deckListService.PostDeckList(requestBody);


            return Ok(NewDeck); 
        }
    }
}

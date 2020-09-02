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
using MTG_Mvc.Interface;
using MTG_Mvc.Models;
using MTG_Mvc.Services;
using Newtonsoft.Json;

namespace MTG_Mvc.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class decklistController : ControllerBase
    {
        public IdecklistInterface Idecklist { get; set; }
        public decklistController( IdecklistInterface DecklistInterface)
        {
            Idecklist = DecklistInterface;
        }

        [HttpDelete]
        public ActionResult DeleteDeck(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid deck id");
           var result = Idecklist.DeleteDeckList(id);
            return Ok(result);
        }

        [HttpGet]
        public ActionResult<List<decklist>> GetDecklists()
        {
            var result = Idecklist.GetAllDeckLists();
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Index()
        {
            var reader = new StreamReader(HttpContext.Request.Body);
            string requestBody = await reader.ReadToEndAsync();
            var NewDeck = Idecklist.PostDeckList(requestBody);
            return Ok(NewDeck); 
        }
    }
}

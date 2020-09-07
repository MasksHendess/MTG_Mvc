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

namespace MTG_Mvc.APIControllers
{
    [Route("api/[controller]")] // api/decklist
    [ApiController]
    public class decklistController : ControllerBase
    {
        private readonly IdecklistServiceInterface decklistService;
        public decklistController( IdecklistServiceInterface DecklistService)
        {
            decklistService = DecklistService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<decklist>>> GetDecklists()
        {
            return Ok(await decklistService.GetAllDeckListsAsync());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<decklist>> GetDeckById(int id)
        {
           return await decklistService.GetDeckListByIdAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> Index()
        {
            var reader = new StreamReader(HttpContext.Request.Body);
            string requestBody = await reader.ReadToEndAsync();

            var cardsInDeck = decklistService.convertRequestBodyToCardList(requestBody);
            cardsInDeck = await decklistService.fetchCardInformationFromAPI(cardsInDeck);
            var NewDeck = decklistService.CreateNewDeckListFromTXTAsync(cardsInDeck);
            //got some JSON serilizationError when line 46-47 was called from CreateNewDeckListTXTAsync. 
            //put those lines here for now. The call works, but line 46 & 47 should be called from CreateNewDeckListTXTASync.
            // Investigate how to fix that issue
            return Ok(NewDeck); 
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<decklist>> DeleteDeck(int id)
        {
            var result = await decklistService.DeleteDeckList(id);
            return Ok(result);
        }
    }
}

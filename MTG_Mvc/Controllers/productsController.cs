using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using MTG_Mvc.Models;
using MTG_Mvc.Services;
using Newtonsoft.Json;

namespace MTG_Mvc.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class productsController : ControllerBase
    {
        public JsonFileProductService ProductService { get; }
        public productsController(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        [HttpGet]
        public IEnumerable<product> Get()
        {
            return ProductService.GetProducts();
        }

        [Route("Rate")]
        [HttpGet]
        public ActionResult Get([FromQuery] string productId,
                                [FromQuery] int rating)
        {
            ProductService.AddRating(productId, rating);

            return Ok();
        }

        [HttpPost]
        [Route("decklist")] //products/decklist
        public async Task<IActionResult> Index()
        {
            var reader = new StreamReader(HttpContext.Request.Body);
            string requestBody = await reader.ReadToEndAsync();

            string[] splitRequestBody = requestBody.Split("\n");

            decklist NewDeck = new decklist(); 
            int id = 0;
            foreach (var line in splitRequestBody)
            {
                card NewCard = new card();
                NewCard.id = id;
                id++;

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

            return Ok(NewDeck);
        }
    }
}

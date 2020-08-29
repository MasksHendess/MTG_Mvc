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
            //string T = "ABCD";
            //string ST = T.Substring(3, 1);
            var reader = new StreamReader(HttpContext.Request.Body);
            string body = await reader.ReadToEndAsync();

            string[] Lines = body.Split("\n");
            var result = JsonConvert.SerializeObject(Lines);

            decklist NewDeck = new decklist(); 
            int id = 0;
            foreach (var item in Lines)
            {
                card NewCard = new card();
                NewCard.id = id;
                id++;

                int quantity = item[0] - '0';
                NewCard.quantity = quantity;
                
                int start = 0;
                foreach (var sign in item)
                {
                    if (sign == '(')
                    {
                        start = item.IndexOf(sign);
                    }
                    
                    NewCard.set = item.Substring(start + 1, 3); 
                }

                int index = item.IndexOf('(');
                if (index > 0)
                {
                    NewCard.name = item.Substring(1, index - 1);
                }

                NewDeck.cards.Add(NewCard);
            }
            //  
            return Ok(NewDeck);
        }
    }
}

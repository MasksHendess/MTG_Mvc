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
using MTG_Mvc.Domain.Entities;
using MTG_Mvc.Services;
using Newtonsoft.Json;

namespace MTG_Mvc.APIControllers
{
    [Route("[controller]")]
    [ApiController]
    public class productsController : ControllerBase
    {
        public JsonFileProductService ProductService { get; }
        private readonly SqlDbContext dbContext;
        public productsController(JsonFileProductService productService, SqlDbContext DbContext)
        {
            ProductService = productService;
            dbContext = DbContext;
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
    }
}

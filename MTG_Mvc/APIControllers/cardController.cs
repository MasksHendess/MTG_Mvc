using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MTG_Mvc.DBContext;
using MTG_Mvc.Domain.Entities;
using MTG_Mvc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTG_Mvc.APIControllers
{
    [Route("api/[controller]")] // api/card
    [ApiController]
    public class cardController : ControllerBase
    {
        private readonly SqlDbContext sqlDbContext;
        public cardController(SqlDbContext _sqlDbContext)
        {
            sqlDbContext = _sqlDbContext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<decklist>>> GetCards()
        {
            return Ok(await sqlDbContext.cards.ToListAsync());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<card>> GetDeckById(int id)
        {
            return await sqlDbContext.cards.FindAsync(id);
        }
    }
}

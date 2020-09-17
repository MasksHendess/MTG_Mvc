using Microsoft.EntityFrameworkCore;
using MTG_Mvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTG_Mvc.DBContext
{
    public class SqlDbContext : DbContext
    {
        public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
        {

        }
        public DbSet<decklist> decklists { get; set; }
        public DbSet<card> cards { get; set; }
        public DbSet<cardNames> doubleName_cards { get; set; }
    }
}

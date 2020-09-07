using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MTG_Mvc.DBContext;
using MTG_Mvc.Domain.Entities;
using MTG_Mvc.Services;

namespace MTG_Mvc.Controllers
{
    public class cardViewController : Controller
    {
        private readonly SqlDbContext context;
        public cardViewController(SqlDbContext Context)
        {
            context = Context;
        }
        // GET: cardView
        public ActionResult Index()
        {
            var cards = context.cards.ToList();
            return View(cards);
        }

        // GET: cardView/Details/5
        public ActionResult Details(int id)
        {
            var card = context.cards.Find(id);
            return View(card);
        }
    }
}

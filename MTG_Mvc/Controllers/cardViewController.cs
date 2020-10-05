using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MTG_Mvc.DBContext;
using MTG_Mvc.Domain.Entities;
using MTG_Mvc.Repositories;
using MTG_Mvc.Services;

namespace MTG_Mvc.Controllers
{
    public class cardViewController : Controller
    {
        private readonly SqlDbContext context;
        IcardRepositoryInterface icardRepositoryInterface;
        public cardViewController(SqlDbContext Context, IcardRepositoryInterface iCardRepositoryInterface)
        {
            context = Context;
            icardRepositoryInterface = iCardRepositoryInterface;
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
            List<card> results = new List<card>();

            var firstName = context.doubleName_cards.Where(x => x.cardid == id).Select(x => x.firstName).FirstOrDefault();
            var secondName = context.doubleName_cards.Where(x => x.cardid == id).Select(x => x.secondName).FirstOrDefault();
            if(firstName!=null && secondName!=null)
            {
                var firstCard = context.cards.Where(x => x.name == firstName).FirstOrDefault();
                var secondCard = context.cards.Where(x => x.name == secondName).FirstOrDefault();
                if(firstCard== null)
                {
                    firstCard = context.cards.Where(x => x.name.Contains(secondName) /*== firstName +" /// " + secondName*/).FirstOrDefault();
                }
                results.Add(firstCard);
                results.Add(secondCard);
            }
            else
            {
                var card = context.cards.Find(id);
                results.Add(card);
            }

            return View(results);
        }
    }
}

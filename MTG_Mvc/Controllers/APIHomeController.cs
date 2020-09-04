using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MTG_Mvc.DBContext;
using MTG_Mvc.Domain.Entities;

namespace MTG_Mvc.Controllers
{
    public class APIHomeController : Controller
    {
        private readonly SqlDbContext _context;

        public APIHomeController(SqlDbContext context)
        {
            _context = context;
        }

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}

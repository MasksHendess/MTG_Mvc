using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MTG_Mvc.DBContext;
using MTG_Mvc.Domain.Entities;
using MTG_Mvc.Services;

namespace MTG_Mvc.Controllers
{
    public class decklistViewController : Controller
    {
        //private readonly SqlDbContext _context;
        private readonly IdecklistServiceInterface IdecklistService;

        public decklistViewController(SqlDbContext context, IdecklistServiceInterface idecklistServiceInterface)
        {
            //_context = context;
            IdecklistService = idecklistServiceInterface;
        }

        // GET: decklistView
        public async Task<IActionResult> Index()
        {
            return View(await IdecklistService.GetAllDeckListsAsync());
        }

        // GET:decklistView/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var decklist = await IdecklistService.GetDeckListByIdAsync(Convert.ToInt32(id));

            if (decklist == null)
            {
                return NotFound();
            }
            return View(decklist);
        }

        // GET:decklistView/Details/5
        public async Task<IActionResult> DetailsSpoiler(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var decklist = await IdecklistService.GetDeckListByIdAsync(Convert.ToInt32(id));

            if (decklist == null)
            {
                return NotFound();
            }
            return View(decklist);
        }

        // GET: decklistView/Create
        public IActionResult Create()
        {
            return View();
        }

        // GET:decklistView/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var decklist = await IdecklistService.GetDeckListByIdAsync(Convert.ToInt32(id));
             //   _context.decklists.FindAsync(id);
            if (decklist == null)
            {
                return NotFound();
            }
            return View(decklist);
        }

        // GET: decklistView/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var decklist = await IdecklistService.GetDeckListByIdAsync(Convert.ToInt32(id));
            //_context.decklists
            //.FirstOrDefaultAsync(m => m.id == id);
            if (decklist == null)
            {
                return NotFound();
            }

            return View(decklist);
        }

        // POST: decklistView/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,deckName")] decklist decklist)
        {
            if (id != decklist.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                  await IdecklistService.UpdateDeckListAsync(decklist);
                    //_context.Update(decklist);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!decklistExists(decklist.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(decklist);
        }

        // POST: decklistView/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,deckName,cards")] decklist decklist)
        {
            if (ModelState.IsValid)
            {

                await IdecklistService.GetAllDeckListsAsync(); // Change this

                //_context.Add(decklist);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(decklist);
        }

        // POST: decklistView/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
             await IdecklistService.DeleteDeckList(id);
            //var decklist = await _context.decklists.FindAsync(id);
            //_context.decklists.Remove(decklist);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool decklistExists(int id)
        {
            var decklist = IdecklistService.GetDeckListByIdAsync(id).Result;
            if (decklist!=null)
                return true;
            else
                return false;
        }
    }
}

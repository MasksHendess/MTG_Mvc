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
    public class HelloWorldController : Controller
    {
        private readonly SqlDbContext _context;

        public HelloWorldController(SqlDbContext context)
        {
            _context = context;
        }

        // GET: HelloWorld
        public async Task<IActionResult> Index()
        {
            return View(await _context.decklists.ToListAsync());
        }

        // GET: HelloWorld/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var decklist = await _context.decklists
                .FirstOrDefaultAsync(m => m.id == id);
            if (decklist == null)
            {
                return NotFound();
            }

            return View(decklist);
        }

        // GET: HelloWorld/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HelloWorld/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,deckName")] decklist decklist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(decklist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(decklist);
        }

        // GET: HelloWorld/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var decklist = await _context.decklists.FindAsync(id);
            if (decklist == null)
            {
                return NotFound();
            }
            return View(decklist);
        }

        // POST: HelloWorld/Edit/5
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
                    _context.Update(decklist);
                    await _context.SaveChangesAsync();
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

        // GET: HelloWorld/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var decklist = await _context.decklists
                .FirstOrDefaultAsync(m => m.id == id);
            if (decklist == null)
            {
                return NotFound();
            }

            return View(decklist);
        }

        // POST: HelloWorld/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var decklist = await _context.decklists.FindAsync(id);
            _context.decklists.Remove(decklist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool decklistExists(int id)
        {
            return _context.decklists.Any(e => e.id == id);
        }
    }
}

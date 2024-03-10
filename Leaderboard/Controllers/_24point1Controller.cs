using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Leaderboard.Data;
using Leaderboard.Models;
using Leaderboard.Models.Enums;

namespace Leaderboard.Controllers
{
    public class _24point1Controller : Controller
    {
        private readonly LeaderboardContext _context;

        public _24point1Controller(LeaderboardContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Atleta == null)
            {
                return NotFound();
            }

            var atleta = await _context.Atleta.FindAsync(id);
            if (atleta == null)
            {
                return NotFound();
            }
            return View();
        }
        private bool AtletaExists(int id)
        {
            return (_context.Atleta?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // POST: Atletas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Categoria,Workout1,Workout2,Workout3")] Atleta atleta)
        {
            if (id != atleta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(atleta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AtletaExists(atleta.Id))
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
            return View();
        }
    }
}

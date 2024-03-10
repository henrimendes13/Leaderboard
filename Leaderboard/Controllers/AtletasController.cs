using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Leaderboard.Data;
using Leaderboard.Models;
using Leaderboard.Models.Enums;

namespace Leaderboard.Controllers
{
    public class AtletasController : Controller
    {
        private readonly LeaderboardContext _context;

        public AtletasController(LeaderboardContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var atletas = await _context.Atleta.ToListAsync();

            return View(atletas);
        }

        // GET: Atletas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Atleta == null)
            {
                return NotFound();
            }

            var atleta = await _context.Atleta
                .FirstOrDefaultAsync(m => m.Id == id);
            if (atleta == null)
            {
                return NotFound();
            }

            return View(atleta);
        }

        // GET: Atletas/Create
        public IActionResult Create()
        {
            ViewBag.CategoriaList = GetCategoriaSelectList();
            return View();
        }

        // POST: Atletas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Categoria")] Atleta atleta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(atleta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.CategoriaList = GetCategoriaSelectList();
            return View(atleta);
        }

        // Método auxiliar para obter o SelectList da Categoria
        private SelectList GetCategoriaSelectList()
        {
            var valoresEnum = Enum.GetValues(typeof(Categoria));

            var listaItens = new List<SelectListItem>();
            foreach (var valorEnum in valoresEnum)
            {
                listaItens.Add(new SelectListItem
                {
                    Text = Enum.GetName(typeof(Categoria), valorEnum),
                    Value = valorEnum.ToString()
                });
            }

            return new SelectList(listaItens, "Value", "Text");
        }
             		
		// GET: Atletas/Edit/5
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
            return View(atleta);
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
            return View(atleta);
        }

        // GET: Atletas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Atleta == null)
            {
                return NotFound();
            }

            var atleta = await _context.Atleta
                .FirstOrDefaultAsync(m => m.Id == id);
            if (atleta == null)
            {
                return NotFound();
            }

            return View(atleta);
        }

        // POST: Atletas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Atleta == null)
            {
                return Problem("Entity set 'LeaderboardContext.Atleta'  is null.");
            }
            var atleta = await _context.Atleta.FindAsync(id);
            if (atleta != null)
            {
                _context.Atleta.Remove(atleta);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AtletaExists(int id)
        {
            return (_context.Atleta?.Any(e => e.Id == id)).GetValueOrDefault();
        }

		public IActionResult SelectCategoria()
		{
			// Obtendo os valores do enum Categoria
			var valoresEnum = Enum.GetValues(typeof(Categoria));

			// Criando uma lista de SelectListItem a partir do enum
			var listaItens = new List<SelectListItem>();
			foreach (var valorEnum in valoresEnum)
			{
				listaItens.Add(new SelectListItem
				{
					Text = Enum.GetName(typeof(Categoria), valorEnum),
					Value = valorEnum.ToString()
				});
			}

			// Criando um SelectList com a lista de itens
			var selectList = new SelectList(listaItens, "Value", "Text");

			// Passando o SelectList para a visão
			ViewBag.CategoriaList = selectList;

			return View();
		}
	}
}

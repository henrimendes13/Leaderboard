using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Leaderboard.Data;
using Leaderboard.Models;
using Leaderboard.Models.DTO;

namespace Leaderboard.Controllers
{
    public class WorkoutAtletaResultsController : Controller
    {
        private readonly LeaderboardContext _context;

        public WorkoutAtletaResultsController(LeaderboardContext context)
        {
            _context = context;
        }

        // GET: WorkoutAtletaResults
        public async Task<IActionResult> Index()
        {
            var atletas = await _context.Atleta.ToListAsync();
            var workouts = await _context.Workout.ToListAsync();
            var workoutAtletaResult = await _context.WorkoutAtletaResult.ToListAsync();

            List<AtletaWorkoutDTO> lista = new List<AtletaWorkoutDTO>();

            
            foreach (var atleta in atletas)
            {
                AtletaWorkoutDTO atletaWorkoutDTO = new AtletaWorkoutDTO();
                atletaWorkoutDTO.AtletaName = atleta.Nome;

                foreach (var workout in workouts)
                {
                    var workoutAtleta = workoutAtletaResult.FirstOrDefault(a => a.AtletaId == atleta.Id && a.WorkoutId == workout.Id);
                    if(workoutAtleta == null)
                    {
                        atletaWorkoutDTO.AtletaResults.Add("");
                    }
                    else
                    {
                        atletaWorkoutDTO.AtletaResults.Add(workoutAtleta.Resultado);
                    }
                }
                lista.Add(atletaWorkoutDTO);
            }
            return View(lista);
        }

        // GET: WorkoutAtletaResults/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.WorkoutAtletaResult == null)
            {
                return NotFound();
            }

            var workoutAtletaResult = await _context.WorkoutAtletaResult
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workoutAtletaResult == null)
            {
                return NotFound();
            }

            return View(workoutAtletaResult);
        }

        // GET: WorkoutAtletaResults/Create
        public IActionResult Create()
        {
            CreateAtletasViewBag();
            CreateWorkoutViewBag();

            return View();
        }

        // POST: WorkoutAtletaResults/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AtletaId,WorkoutId,Resultado")] WorkoutAtletaResult workoutAtletaResult)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workoutAtletaResult);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(workoutAtletaResult);
        }

        // GET: WorkoutAtletaResults/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.WorkoutAtletaResult == null)
            {
                return NotFound();
            }

            var workoutAtletaResult = await _context.WorkoutAtletaResult.FindAsync(id);
            if (workoutAtletaResult == null)
            {
                return NotFound();
            }
            return View(workoutAtletaResult);
        }

        // POST: WorkoutAtletaResults/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AtletaId,WorkoutId,Resultado")] WorkoutAtletaResult workoutAtletaResult)
        {
            if (id != workoutAtletaResult.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workoutAtletaResult);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkoutAtletaResultExists(workoutAtletaResult.Id))
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
            return View(workoutAtletaResult);
        }

        // GET: WorkoutAtletaResults/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.WorkoutAtletaResult == null)
            {
                return NotFound();
            }

            var workoutAtletaResult = await _context.WorkoutAtletaResult
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workoutAtletaResult == null)
            {
                return NotFound();
            }

            return View(workoutAtletaResult);
        }

        // POST: WorkoutAtletaResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.WorkoutAtletaResult == null)
            {
                return Problem("Entity set 'LeaderboardContext.WorkoutAtletaResult'  is null.");
            }
            var workoutAtletaResult = await _context.WorkoutAtletaResult.FindAsync(id);
            if (workoutAtletaResult != null)
            {
                _context.WorkoutAtletaResult.Remove(workoutAtletaResult);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkoutAtletaResultExists(int id)
        {
          return (_context.WorkoutAtletaResult?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public void CreateAtletasViewBag()
        {
            var ateltas = _context.Atleta.ToList();

            var listaItens = new List<SelectListItem>();
            foreach (var atelta in ateltas)
            {
                listaItens.Add(new SelectListItem
                {
                    Text = atelta.Nome,
                    Value = atelta.Id.ToString()
                });
            }

            var selectList = new SelectList(listaItens, "Value", "Text");

            ViewBag.Atletas = selectList;
        }

        public void CreateWorkoutViewBag()
        {
            var workouts = _context.Workout.ToList();

            var listaItens = new List<SelectListItem>();
            foreach (var workout in workouts)
            {
                listaItens.Add(new SelectListItem
                {
                    Text = workout.Name,
                    Value = workout.Id.ToString()
                });;
            }

            var selectList = new SelectList(listaItens, "Value", "Text");

            ViewBag.Workouts = selectList;
        }
    }
}

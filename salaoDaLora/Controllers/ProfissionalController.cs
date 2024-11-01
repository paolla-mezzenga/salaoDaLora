using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using salaoDaLora.Data;
using salaoDaLora.Models;
using System.Threading.Tasks;

namespace salaoDaLora.Controllers
{
    public class ProfissionalController : Controller
    {
        private readonly SalaoDaLoraContext _context;

        public ProfissionalController(SalaoDaLoraContext context)
        {
            _context = context;
        }

        //Index - listagem
        public async Task<IActionResult> Index()
        {
            return View(await _context.Profissionais.ToListAsync());
        }

        //Details - detalhes do profissioal
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profissional = await _context.Profissionais
                .FirstOrDefaultAsync(m => m.ProfissionalId == id);
            if (profissional == null)
            {
                return NotFound();
            }

            return View(profissional);
        }

        //Create - criar novo profissional
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Especialidade")] Profissional profissional)
        {
            if (ModelState.IsValid)
            {
                _context.Add(profissional);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(profissional);
        }

        //Edir - editar profissional
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profissional = await _context.Profissionais.FindAsync(id);
            if (profissional == null)
            {
                return NotFound();
            }
            return View(profissional);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProfissionalId,Nome,Especialidade")] Profissional profissional)
        {
            if (id != profissional.ProfissionalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profissional);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfissionalExists(profissional.ProfissionalId))
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
            return View(profissional);
        }

        //Delte - deletar profisisonal
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profissional = await _context.Profissionais
                .FirstOrDefaultAsync(m => m.ProfissionalId == id);
            if (profissional == null)
            {
                return NotFound();
            }

            return View(profissional);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var profissional = await _context.Profissionais.FindAsync(id);
            _context.Profissionais.Remove(profissional);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfissionalExists(int id)
        {
            return _context.Profissionais.Any(e => e.ProfissionalId == id);
        }
    }
}

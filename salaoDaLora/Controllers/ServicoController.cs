using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using salaoDaLora.Data;
using salaoDaLora.Models;
using System.Threading.Tasks;

namespace salaoDaLora.Controllers
{
    public class ServicoController : Controller
    {
        private readonly SalaoDaLoraContext _context;

        public ServicoController(SalaoDaLoraContext context)
        {
            _context = context;
        }

        //Index - listagem 
        public async Task<IActionResult> Index()
        {
            return View(await _context.Servicos.ToListAsync());
        }

        //Details - ver detalhes
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servico = await _context.Servicos
                .FirstOrDefaultAsync(m => m.ServicoId == id);
            if (servico == null)
            {
                return NotFound();
            }

            return View(servico);
        }

        //Create - criar novo serviço
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Descricao,Preco")] Servico servico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(servico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(servico);
        }

        //Edit - editar serviço
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servico = await _context.Servicos.FindAsync(id);
            if (servico == null)
            {
                return NotFound();
            }
            return View(servico);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServicoId,Descricao,Preco")] Servico servico)
        {
            if (id != servico.ServicoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(servico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServicoExists(servico.ServicoId))
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
            return View(servico);
        }

        //Delete - deletar serviço
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servico = await _context.Servicos
                .FirstOrDefaultAsync(m => m.ServicoId == id);
            if (servico == null)
            {
                return NotFound();
            }

            return View(servico);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var servico = await _context.Servicos.FindAsync(id);
            _context.Servicos.Remove(servico);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServicoExists(int id)
        {
            return _context.Servicos.Any(e => e.ServicoId == id);
        }
    }
}

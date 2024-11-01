using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using salaoDaLora.Data;
using salaoDaLora.Models;
using System.Linq;
using System.Threading.Tasks;

namespace salaoDaLora.Controllers
{
    public class AgendamentoController : Controller
    {
        private readonly SalaoDaLoraContext _context;
        //construtor
        public AgendamentoController(SalaoDaLoraContext context)
        {
            _context = context;
        }

        //Index - listagem
        public async Task<IActionResult> Index()
        {
            var agendamentos = _context.Agendamentos
                .Include(a => a.Cliente)
                .Include(a => a.Profissional)
                .Include(a => a.Servico);
            return View(await agendamentos.ToListAsync());
        }

        //Details - ver detalhes
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agendamento = await _context.Agendamentos
                .Include(a => a.Cliente)
                .Include(a => a.Profissional)
                .Include(a => a.Servico)
                .FirstOrDefaultAsync(m => m.AgendamentoId == id);
            if (agendamento == null)
            {
                return NotFound();
            }

            return View(agendamento);
        }

        //Create - adciionar novo agendamento
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Nome");
            ViewData["ProfissionalId"] = new SelectList(_context.Profissionais, "ProfissionalId", "Nome");
            ViewData["ServicoId"] = new SelectList(_context.Servicos, "ServicoId", "Descricao");

            return View();
        }

        [HttpPost]
        //Bind -> indica as propiedades que serão preenchidas
        public async Task<IActionResult> Create([Bind("DataAgendamento,ClienteId,ProfissionalId,ServicoId")] Agendamento agendamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(agendamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(agendamento);
        }



        //Edit - editar o agendamento
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agendamento = await _context.Agendamentos.FindAsync(id);
            if (agendamento == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Nome", agendamento.ClienteId);
            ViewData["ProfissionalId"] = new SelectList(_context.Profissionais, "ProfissionalId", "Nome", agendamento.ProfissionalId);
            ViewData["ServicoId"] = new SelectList(_context.Servicos, "ServicoId", "Descricao", agendamento.ServicoId);
            return View(agendamento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AgendamentoId,DataAgendamento,ClienteId,ProfissionalId,ServicoId")] Agendamento agendamento)
        {
            if (id != agendamento.AgendamentoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agendamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgendamentoExists(agendamento.AgendamentoId))
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
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Nome", agendamento.ClienteId);
            ViewData["ProfissionalId"] = new SelectList(_context.Profissionais, "ProfissionalId", "Nome", agendamento.ProfissionalId);
            ViewData["ServicoId"] = new SelectList(_context.Servicos, "ServicoId", "Descricao", agendamento.ServicoId);
            return View(agendamento);
        }

        //Delete - deletar agendamento
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agendamento = await _context.Agendamentos
                .Include(a => a.Cliente)
                .Include(a => a.Profissional)
                .Include(a => a.Servico)
                .FirstOrDefaultAsync(m => m.AgendamentoId == id);
            if (agendamento == null)
            {
                return NotFound();
            }

            return View(agendamento);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var agendamento = await _context.Agendamentos.FindAsync(id);
            _context.Agendamentos.Remove(agendamento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgendamentoExists(int id)
        {
            return _context.Agendamentos.Any(e => e.AgendamentoId == id);
        }
    }
}

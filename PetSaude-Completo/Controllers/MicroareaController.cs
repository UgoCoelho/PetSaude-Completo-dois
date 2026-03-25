using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetSaude_Completo.Data;
using PetSaude_Completo.Models;

namespace PetSaude_Completo.Controllers
{
    public class MicroareaController : Controller
    {
        private readonly PetSaude_CompletoContext _context;

        public MicroareaController(PetSaude_CompletoContext context)
        {
            _context = context;
        }

        // GET: Microarea
        public async Task<IActionResult> Index()
        {
            var petSaude_CompletoContext = _context.Microarea.Include(m => m.AgenteSaude).Include(m => m.UnidadeAtendimento);
            return View(await petSaude_CompletoContext.ToListAsync());
        }

        // GET: Microarea/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var microarea = await _context.Microarea
                .Include(m => m.AgenteSaude)
                .Include(m => m.UnidadeAtendimento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (microarea == null)
            {
                return NotFound();
            }

            return View(microarea);
        }

        // GET: Microarea/Create
        public IActionResult Create()
        {
            ViewData["AgenteSaudeId"] = new SelectList(_context.AgenteSaude, "Id", "Id");
            ViewData["UnidadeAtendimentoId"] = new SelectList(_context.Set<UnidadeAtendimento>(), "Id", "Id");
            return View();
        }

        // POST: Microarea/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Codigo,Nome,Descricao,UnidadeAtendimentoId,AgenteSaudeId")] Microarea microarea)
        {
            if (ModelState.IsValid)
            {
                _context.Add(microarea);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AgenteSaudeId"] = new SelectList(_context.AgenteSaude, "Id", "Id", microarea.AgenteSaudeId);
            ViewData["UnidadeAtendimentoId"] = new SelectList(_context.Set<UnidadeAtendimento>(), "Id", "Id", microarea.UnidadeAtendimentoId);
            return View(microarea);
        }

        // GET: Microarea/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var microarea = await _context.Microarea.FindAsync(id);
            if (microarea == null)
            {
                return NotFound();
            }
            ViewData["AgenteSaudeId"] = new SelectList(_context.AgenteSaude, "Id", "Id", microarea.AgenteSaudeId);
            ViewData["UnidadeAtendimentoId"] = new SelectList(_context.Set<UnidadeAtendimento>(), "Id", "Id", microarea.UnidadeAtendimentoId);
            return View(microarea);
        }

        // POST: Microarea/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Codigo,Nome,Descricao,UnidadeAtendimentoId,AgenteSaudeId")] Microarea microarea)
        {
            if (id != microarea.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(microarea);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MicroareaExists(microarea.Id))
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
            ViewData["AgenteSaudeId"] = new SelectList(_context.AgenteSaude, "Id", "Id", microarea.AgenteSaudeId);
            ViewData["UnidadeAtendimentoId"] = new SelectList(_context.Set<UnidadeAtendimento>(), "Id", "Id", microarea.UnidadeAtendimentoId);
            return View(microarea);
        }

        // GET: Microarea/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var microarea = await _context.Microarea
                .Include(m => m.AgenteSaude)
                .Include(m => m.UnidadeAtendimento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (microarea == null)
            {
                return NotFound();
            }

            return View(microarea);
        }

        // POST: Microarea/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var microarea = await _context.Microarea.FindAsync(id);
            if (microarea != null)
            {
                _context.Microarea.Remove(microarea);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MicroareaExists(int id)
        {
            return _context.Microarea.Any(e => e.Id == id);
        }
    }
}

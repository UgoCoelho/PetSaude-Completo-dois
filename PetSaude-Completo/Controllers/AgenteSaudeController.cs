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
    public class AgenteSaudeController : Controller
    {
        private readonly PetSaude_CompletoContext _context;

        public AgenteSaudeController(PetSaude_CompletoContext context)
        {
            _context = context;
        }

        // GET: AgenteSaude
        public async Task<IActionResult> Index()
        {
            var petSaude_CompletoContext = _context.AgenteSaude.Include(a => a.UnidadeAtendimento);
            return View(await petSaude_CompletoContext.ToListAsync());
        }

        // GET: AgenteSaude/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agenteSaude = await _context.AgenteSaude
                .Include(a => a.UnidadeAtendimento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agenteSaude == null)
            {
                return NotFound();
            }

            return View(agenteSaude);
        }

        // GET: AgenteSaude/Create
        public IActionResult Create()
        {
            ViewData["UnidadeAtendimentoId"] = new SelectList(_context.Set<UnidadeAtendimento>(), "Id", "Id");
            return View();
        }

        // POST: AgenteSaude/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,CPF,Telefone,Matricula,UnidadeAtendimentoId")] AgenteSaude agenteSaude)
        {
            if (ModelState.IsValid)
            {
                _context.Add(agenteSaude);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UnidadeAtendimentoId"] = new SelectList(_context.Set<UnidadeAtendimento>(), "Id", "Id", agenteSaude.UnidadeAtendimentoId);
            return View(agenteSaude);
        }

        // GET: AgenteSaude/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agenteSaude = await _context.AgenteSaude.FindAsync(id);
            if (agenteSaude == null)
            {
                return NotFound();
            }
            ViewData["UnidadeAtendimentoId"] = new SelectList(_context.Set<UnidadeAtendimento>(), "Id", "Id", agenteSaude.UnidadeAtendimentoId);
            return View(agenteSaude);
        }

        // POST: AgenteSaude/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,CPF,Telefone,Matricula,UnidadeAtendimentoId")] AgenteSaude agenteSaude)
        {
            if (id != agenteSaude.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agenteSaude);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgenteSaudeExists(agenteSaude.Id))
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
            ViewData["UnidadeAtendimentoId"] = new SelectList(_context.Set<UnidadeAtendimento>(), "Id", "Id", agenteSaude.UnidadeAtendimentoId);
            return View(agenteSaude);
        }

        // GET: AgenteSaude/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agenteSaude = await _context.AgenteSaude
                .Include(a => a.UnidadeAtendimento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agenteSaude == null)
            {
                return NotFound();
            }

            return View(agenteSaude);
        }

        // POST: AgenteSaude/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var agenteSaude = await _context.AgenteSaude.FindAsync(id);
            if (agenteSaude != null)
            {
                _context.AgenteSaude.Remove(agenteSaude);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgenteSaudeExists(int id)
        {
            return _context.AgenteSaude.Any(e => e.Id == id);
        }
    }
}

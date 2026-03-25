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
    public class UnidadeAtendimentoController : Controller
    {
        private readonly PetSaude_CompletoContext _context;

        public UnidadeAtendimentoController(PetSaude_CompletoContext context)
        {
            _context = context;
        }

        // GET: UnidadeAtendimento
        public async Task<IActionResult> Index()
        {
            return View(await _context.UnidadeAtendimento.ToListAsync());
        }

        // GET: UnidadeAtendimento/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unidadeAtendimento = await _context.UnidadeAtendimento
                .FirstOrDefaultAsync(m => m.Id == id);
            if (unidadeAtendimento == null)
            {
                return NotFound();
            }

            return View(unidadeAtendimento);
        }

        // GET: UnidadeAtendimento/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UnidadeAtendimento/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UnidadeAtendimento unidade)
        {
            if (ModelState.IsValid)
            {
                _context.Add(unidade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(unidade);
        }

        // GET: UnidadeAtendimento/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unidadeAtendimento = await _context.UnidadeAtendimento.FindAsync(id);
            if (unidadeAtendimento == null)
            {
                return NotFound();
            }
            return View(unidadeAtendimento);
        }

        // POST: UnidadeAtendimento/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] UnidadeAtendimento unidadeAtendimento)
        {
            if (id != unidadeAtendimento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(unidadeAtendimento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UnidadeAtendimentoExists(unidadeAtendimento.Id))
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
            return View(unidadeAtendimento);
        }

        // GET: UnidadeAtendimento/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unidadeAtendimento = await _context.UnidadeAtendimento
                .FirstOrDefaultAsync(m => m.Id == id);
            if (unidadeAtendimento == null)
            {
                return NotFound();
            }

            return View(unidadeAtendimento);
        }

        // POST: UnidadeAtendimento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var unidadeAtendimento = await _context.UnidadeAtendimento.FindAsync(id);
            if (unidadeAtendimento != null)
            {
                _context.UnidadeAtendimento.Remove(unidadeAtendimento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UnidadeAtendimentoExists(int id)
        {
            return _context.UnidadeAtendimento.Any(e => e.Id == id);
        }
    }
}

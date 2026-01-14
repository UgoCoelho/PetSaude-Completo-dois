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
    public class FrequenciaController : Controller
    {
        private readonly PetSaude_CompletoContext _context;

        public FrequenciaController(PetSaude_CompletoContext context)
        {
            _context = context;
        }

        // GET: Frequencia
        public async Task<IActionResult> Index()
        {
            return View(await _context.Frequencia.ToListAsync());
        }

        // GET: Frequencia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var frequencia = await _context.Frequencia
                .FirstOrDefaultAsync(m => m.FrequenciaId == id);
            if (frequencia == null)
            {
                return NotFound();
            }

            return View(frequencia);
        }

        // GET: Frequencia/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Frequencia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FrequenciaId,Nome,Fator")] Frequencia frequencia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(frequencia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(frequencia);
        }

        // GET: Frequencia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var frequencia = await _context.Frequencia.FindAsync(id);
            if (frequencia == null)
            {
                return NotFound();
            }
            return View(frequencia);
        }

        // POST: Frequencia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("FrequenciaId,Nome,Fator")] Frequencia frequencia)
        {
            if (id != frequencia.FrequenciaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(frequencia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FrequenciaExists(frequencia.FrequenciaId))
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
            return View(frequencia);
        }

        // GET: Frequencia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var frequencia = await _context.Frequencia
                .FirstOrDefaultAsync(m => m.FrequenciaId == id);
            if (frequencia == null)
            {
                return NotFound();
            }

            return View(frequencia);
        }

        // POST: Frequencia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var frequencia = await _context.Frequencia.FindAsync(id);
            if (frequencia != null)
            {
                _context.Frequencia.Remove(frequencia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FrequenciaExists(int? id)
        {
            return _context.Frequencia.Any(e => e.FrequenciaId == id);
        }
    }
}

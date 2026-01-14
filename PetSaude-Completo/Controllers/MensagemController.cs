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
    public class MensagemController : Controller
    {
        private readonly PetSaude_CompletoContext _context;

        public MensagemController(PetSaude_CompletoContext context)
        {
            _context = context;
        }

        // GET: Mensagem
        public async Task<IActionResult> Index()
        {
            var petSaude_CompletoContext = _context.Mensagem.Include(m => m.Area).Include(m => m.Categoria).Include(m => m.FrequenciaMensagem);
            return View(await petSaude_CompletoContext.ToListAsync());
        }

        // GET: Mensagem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mensagem = await _context.Mensagem
                .Include(m => m.Area)
                .Include(m => m.Categoria)
                .Include(m => m.FrequenciaMensagem)
                .FirstOrDefaultAsync(m => m.MensagemId == id);
            if (mensagem == null)
            {
                return NotFound();
            }

            return View(mensagem);
        }

        // GET: Mensagem/Create
        public IActionResult Create()
        {
            ViewData["AreaId"] = new SelectList(_context.Area, "AreaId", "Nome");
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "CategoriaId", "Nome");
            ViewData["FrequenciaMensagemId"] = new SelectList(_context.Frequencia, "FrequenciaId", "Nome");

            // 🔥 COMORBIDADES
            ViewData["Comorbidades"] = _context.Comorbidade.ToList();

            return View();
        }

        // POST: Mensagem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
    [Bind("MensagemId,Titulo,AreaId,CategoriaId,HorarioEnvio,FrequenciaMensagemId,Conteudo,DataInicio,DataFim")]
    Mensagem mensagem,
    List<int> comorbidadesSelecionadas)
        {
            if (ModelState.IsValid)
            {
                // 🔥 SALVAR RELAÇÃO N:N
                foreach (var comorbidadeId in comorbidadesSelecionadas)
                {
                    mensagem.MensagemComorbidades.Add(new MensagemComorbidade
                    {
                        ComorbidadeId = comorbidadeId
                    });
                }

                _context.Add(mensagem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Recarregar dados se der erro
            ViewData["AreaId"] = new SelectList(_context.Area, "AreaId", "AreaId", mensagem.AreaId);
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "CategoriaId", "CategoriaId", mensagem.CategoriaId);
            ViewData["FrequenciaMensagemId"] = new SelectList(_context.Frequencia, "FrequenciaId", "FrequenciaId", mensagem.FrequenciaMensagemId);
            ViewData["Comorbidades"] = _context.Comorbidade.ToList();

            return View(mensagem);
        }

        // GET: Mensagem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var mensagem = await _context.Mensagem
                .Include(m => m.MensagemComorbidades)
                .FirstOrDefaultAsync(m => m.MensagemId == id);

            if (mensagem == null)
                return NotFound();

            ViewData["AreaId"] = new SelectList(_context.Area, "AreaId", "AreaId", mensagem.AreaId);
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "CategoriaId", "CategoriaId", mensagem.CategoriaId);
            ViewData["FrequenciaMensagemId"] = new SelectList(_context.Frequencia, "FrequenciaId", "FrequenciaId", mensagem.FrequenciaMensagemId);

            ViewData["Comorbidades"] = _context.Comorbidade.ToList();
            ViewData["ComorbidadesSelecionadas"] = mensagem.MensagemComorbidades
                .Select(mc => mc.ComorbidadeId)
                .ToList();

            return View(mensagem);
        }

        // POST: Mensagem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
    int id,
    [Bind("MensagemId,Titulo,AreaId,CategoriaId,HorarioEnvio,FrequenciaMensagemId,Conteudo,DataInicio,DataFim")]
    Mensagem mensagem,
    List<int> comorbidadesSelecionadas)
        {
            if (id != mensagem.MensagemId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    // 🔥 REMOVE RELAÇÕES ANTIGAS
                    var existentes = _context.MensagemComorbidade
                        .Where(mc => mc.MensagemId == mensagem.MensagemId);

                    _context.MensagemComorbidade.RemoveRange(existentes);

                    // 🔥 ADICIONA NOVAS
                    foreach (var comorbidadeId in comorbidadesSelecionadas)
                    {
                        _context.MensagemComorbidade.Add(new MensagemComorbidade
                        {
                            MensagemId = mensagem.MensagemId,
                            ComorbidadeId = comorbidadeId
                        });
                    }

                    _context.Update(mensagem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MensagemExists(mensagem.MensagemId))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["AreaId"] = new SelectList(_context.Area, "AreaId", "AreaId", mensagem.AreaId);
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "CategoriaId", "CategoriaId", mensagem.CategoriaId);
            ViewData["FrequenciaMensagemId"] = new SelectList(_context.Frequencia, "FrequenciaId", "FrequenciaId", mensagem.FrequenciaMensagemId);
            ViewData["Comorbidades"] = _context.Comorbidade.ToList();

            return View(mensagem);
        }

        // GET: Mensagem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mensagem = await _context.Mensagem
                .Include(m => m.Area)
                .Include(m => m.Categoria)
                .Include(m => m.FrequenciaMensagem)
                .FirstOrDefaultAsync(m => m.MensagemId == id);
            if (mensagem == null)
            {
                return NotFound();
            }

            return View(mensagem);
        }

        // POST: Mensagem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mensagem = await _context.Mensagem.FindAsync(id);
            if (mensagem != null)
            {
                _context.Mensagem.Remove(mensagem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MensagemExists(int id)
        {
            return _context.Mensagem.Any(e => e.MensagemId == id);
        }
    }
}

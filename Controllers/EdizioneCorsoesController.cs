using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Formazione.Data;
using Formazione.Models;

namespace Formazione.Controllers
{
    [Authentication]
    public class EdizioneCorsoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EdizioneCorsoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EdizioneCorsoes
        public async Task<IActionResult> Index()
        {
              return _context.Edizioni != null ?
                
                          View(await _context.Edizioni.Where(m => m.eliminato == 0 || m.eliminato == null ).ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Edizioni'  is null.");
        }

        

        // GET: EdizioneCorsoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Edizioni == null)
            {
                return NotFound();
            }

            var edizioneCorso = await _context.Edizioni
                .FirstOrDefaultAsync(m => m.EdizioneCorsoID == id);
            if (edizioneCorso == null)
            {
                return NotFound();
            }

            return View(edizioneCorso);
        }

        // GET: EdizioneCorsoes/Create
        public IActionResult Create(int? id)
        {
            if (id == null)
            {
                ViewData["ProgettoFormativoID"] = new SelectList(_context.Progetti.Where(m => m.eliminato == 0 ), "ProgettoFormativoID", "ProgettoFormativoID");
            }

            else
            {
                ViewData["ProgettoFormativoID"] = new SelectList(_context.Progetti.Where(m => m.eliminato == 0 && m.ProgettoFormativoID == id), "ProgettoFormativoID", "ProgettoFormativoID");

            }


            return View();
        }

        // POST: EdizioneCorsoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EdizioneCorsoID,ProgettoFormativoID,Anno,NumeroEdizione,QuantitàModuli,Descrizione,dataorains,IDutenteins,dataoraultmod,IDutenteultmod,eliminato,IDutenteultcanc,dataoraultcanc")] EdizioneCorso edizioneCorso)
        {
            if (ModelState.IsValid)
            {
                
                edizioneCorso.eliminato = 0;
                edizioneCorso.dataorains = System.DateTime.Now;
                edizioneCorso.IDutenteins =  HttpContext.Session.GetInt32("utente_id");               
                _context.Add(edizioneCorso);
                await _context.SaveChangesAsync();             
                Utility.SalvaLogDef(tipoOperazione.inserimento, "Edizione " + edizioneCorso.NumeroEdizione + " Progetto " + edizioneCorso.ProgettoFormativoID, _context, HttpContext.Session);

                return RedirectToAction(nameof(Index));
            }
            return View(edizioneCorso);
        }

        // GET: EdizioneCorsoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["ProgettoFormativoID"] = new SelectList(_context.Progetti.Where(m => m.eliminato == 0 || m.eliminato == null), "ProgettoFormativoID", "ProgettoFormativoID");
            
            if (id == null || _context.Edizioni == null)
            {
                return NotFound();
            }

            var edizioneCorso = await _context.Edizioni.FindAsync(id);
            if (edizioneCorso == null)
            {
                return NotFound();
            }
            return View(edizioneCorso);
        }

        // POST: EdizioneCorsoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EdizioneCorsoID,ProgettoFormativoID,Anno,NumeroEdizione,QuantitàModuli,Descrizione,dataorains,IDutenteins,dataoraultmod,IDutenteultmod,eliminato,IDutenteultcanc,dataoraultcanc")] EdizioneCorso edizioneCorso)
        {
            
            if (id != edizioneCorso.EdizioneCorsoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                    if (int.TryParse(Request.Form["ProgettoFormativoID"], out int ProgettoFormativoID))
            {
                 edizioneCorso.ProgettoFormativoID = ProgettoFormativoID;
            }
                try
                {
                    edizioneCorso.dataoraultmod = System.DateTime.Now;
                    edizioneCorso.IDutenteultmod = HttpContext.Session.GetInt32("utente_id");

                    

                    ///inserire id utente modifica
                    _context.Update(edizioneCorso);
                    await _context.SaveChangesAsync();
                    Utility.SalvaLogDef(tipoOperazione.modifica, "Edizione " + edizioneCorso.NumeroEdizione + " Progetto " + edizioneCorso.ProgettoFormativoID, _context, HttpContext.Session);


                }


                catch (DbUpdateConcurrencyException)
                {
                    if (!EdizioneCorsoExists(edizioneCorso.EdizioneCorsoID))
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

            return View(edizioneCorso);
        }

        // GET: EdizioneCorsoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Edizioni == null)
            {
                return NotFound();
            }

            var edizioneCorso = await _context.Edizioni
                .FirstOrDefaultAsync(m => m.EdizioneCorsoID == id);
            if (edizioneCorso == null)
            {
                return NotFound();
            }

            return View(edizioneCorso);
        }

        // POST: EdizioneCorsoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Edizioni == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Edizioni'  is null.");
            }
            var edizioneCorso = await _context.Edizioni.FindAsync(id);
            if (edizioneCorso != null)
            {
                edizioneCorso.eliminato = 1;
                edizioneCorso.dataoraultcanc = System.DateTime.Now;
                edizioneCorso.IDutenteultcanc =  HttpContext.Session.GetInt32("utente_id");


                //_context.Edizioni.Remove(edizioneCorso);

                _context.Update(edizioneCorso);
                await _context.SaveChangesAsync();

                Utility.SalvaLogDef(tipoOperazione.eliminazione, "Edizione " + edizioneCorso.NumeroEdizione + " Progetto " + edizioneCorso.ProgettoFormativoID, _context, HttpContext.Session);


            }
            return RedirectToAction(nameof(Index));
        }

        private bool EdizioneCorsoExists(int id)
        {
          return (_context.Edizioni?.Any(e => e.EdizioneCorsoID == id)).GetValueOrDefault();
        }
    }
}


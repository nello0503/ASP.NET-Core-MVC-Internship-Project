using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Formazione.Data;
using Formazione.Models;

namespace Formazione.Controllers
{
    [Authentication]
    public class TipoCorsoController : Controller
    {
        private readonly ApplicationDbContext _context;

 

        public TipoCorsoController(ApplicationDbContext context)
        {
            _context = context;
        }



        // GET: TipoCorso

        [Authentication]
        public async Task<IActionResult> Index()
        {
            return _context.Corsi != null ?
                        View(await _context.Corsi.Where(m => m.eliminato == 0).ToListAsync()) :
                        Problem("Entity set 'ApplicationContext.Progetti' is null. "); 
        }

 

        // GET: TipoCorso/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Corsi == null)
            {
                return NotFound();
            }

 

            var tipoCorso = await _context.Corsi
                .FirstOrDefaultAsync(m => m.TipoCorsoID == id);
            if (tipoCorso == null)
            {
                return NotFound();
            }

 

            return View(tipoCorso);
        }

    


        // GET: TipoCorso/Create
        public IActionResult Create()
        {



            return View();
        }

 

        // POST: TipoCorso/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoCorsoID,Codice,DescrizioneCodice,Durata,Sedute,Moduli,AnniValidità,MaxDiscenti,dataorains,IDutenteins,dataoraultmod,IDutenteultmod,eliminato,IDutenteultcanc,dataoraultcanc")] TipoCorso tipoCorso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoCorso);
                await _context.SaveChangesAsync();


                tipoCorso.dataorains = System.DateTime.Now;
                tipoCorso.IDutenteins = HttpContext.Session.GetInt32("utente_id");
                tipoCorso.eliminato = 0;
              
                Utility.SalvaLogDef(tipoOperazione.inserimento, "Corso " + tipoCorso.Codice + " Descrizione " + tipoCorso.DescrizioneCodice, _context, HttpContext.Session);

                return RedirectToAction(nameof(Index));
            }
            return View(tipoCorso);
        }

 

        // GET: TipoCorso/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Corsi == null)
            {
                return NotFound();
            }

 

            var tipoCorso = await _context.Corsi.FindAsync(id);
            if (tipoCorso == null)
            {
                return NotFound();
            }
            return View(tipoCorso);
        }

 

        // POST: TipoCorso/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TipoCorsoID,Codice,DescrizioneCodice,Durata,Sedute,Moduli,AnniValidità,MaxDiscenti,dataorains,IDutenteins,dataoraultmod,IDutenteultmod,eliminato,IDutenteultcanc,dataoraultcanc")] TipoCorso tipoCorso)
        {
            if (id != tipoCorso.TipoCorsoID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await _context.SaveChangesAsync();
                Utility.SalvaLogDef(tipoOperazione.modifica, "Corso " + tipoCorso.Codice + " Descrizione " + tipoCorso.DescrizioneCodice, _context, HttpContext.Session);

                try
                {
                    tipoCorso.dataoraultmod = System.DateTime.Now;
                    tipoCorso.IDutenteultmod = HttpContext.Session.GetInt32("utente_id");
                    _context.Update(tipoCorso);


                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoCorsoExists(tipoCorso.TipoCorsoID))
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
            return View(tipoCorso);
        }

 

        // GET: TipoCorso/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Corsi == null)
            {
                return NotFound();
            }

 

            var tipoCorso = await _context.Corsi
                .FirstOrDefaultAsync(m => m.TipoCorsoID == id);
            if (tipoCorso == null)
            {
                return NotFound();
            }

 

            return View(tipoCorso);
        }

 

        // POST: TipoCorso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Corsi == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Corsi'  is null.");
            }
            var tipoCorso = await _context.Corsi.FindAsync(id);
            if (tipoCorso != null)
            {

 

                tipoCorso.eliminato = 1;
                tipoCorso.dataoraultcanc = System.DateTime.Now;
                tipoCorso.IDutenteultcanc =  HttpContext.Session.GetInt32("utente_id");
                _context.Update(tipoCorso);
                

            }
            
            await _context.SaveChangesAsync();
            Utility.SalvaLogDef(tipoOperazione.eliminazione, "Corso " + tipoCorso.Codice + " Descrizione " + tipoCorso.DescrizioneCodice, _context, HttpContext.Session);

            return RedirectToAction(nameof(Index));
        }


       
 

        private bool TipoCorsoExists(int id)
        {
          return (_context.Corsi?.Any(e => e.TipoCorsoID == id)).GetValueOrDefault();
        }
    }
}
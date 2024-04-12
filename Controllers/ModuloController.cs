using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Formazione.Data;
using Formazione.Models;
using Microsoft.VisualBasic;
using System.Reflection;

namespace Formazione.Controllers
{
    [Authentication]
    public class ModuloController : Controller
    {
 
        private readonly ApplicationDbContext _context;

        

        public ModuloController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Modulo
        public async Task<IActionResult> Index()
        {
            return _context.Moduli != null ?
                          View(await _context.Moduli.Where(m => m.eliminato == 0).ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Moduli'  is null.");



        }

       

        // GET: Modulo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Moduli == null)
            {
                return NotFound();
            }

            var modulo = await _context.Moduli
                .Include(m => m.Edizione)
                .FirstOrDefaultAsync(m => m.ModuloID == id);
            if (modulo == null)
            {
                return NotFound();
            }

            return View(modulo);
        }

        // GET: Modulo/Create
        public IActionResult Create(int? id)
        {
            if (id==null) { 

              ViewData["EdizioneCorsoID"] = new SelectList(_context.Edizioni.Where(m => m.eliminato == 0), "EdizioneCorsoID", "EdizioneCorsoID");
               
            }

            else
            {
               
                ViewData["EdizioneCorsoID"] = new SelectList(_context.Edizioni.Where(m =>(m.EdizioneCorsoID == id && m.eliminato == 0)), "EdizioneCorsoID", "EdizioneCorsoID");

            }  
            

            return View();
        }

        // POST: Modulo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ModuloID,EdizioneCorsoID,data_inizio,numero,data_fine,totale_ore,dataoraultmod,IDutenteultmod,IDutenteultcanc,dataoraultcanc")] Modulo modulo)
        {
            var progetto = _context.Edizioni.Where(m => m.EdizioneCorsoID == modulo.EdizioneCorsoID).Select(m => m.ProgettoFormativoID).FirstOrDefault();

            if (ModelState.IsValid)
            {
                _context.Add(modulo);
                await _context.SaveChangesAsync();

                modulo.eliminato = 0;
                modulo.dataorains = System.DateTime.Now;
                modulo.data_fine = modulo.data_inizio.Date.AddHours(modulo.data_fine.Hour).AddMinutes(modulo.data_fine.Minute);
                modulo.IDutenteins = HttpContext.Session.GetInt32("utente_id");
                modulo.totale_ore = (modulo.data_fine - modulo.data_inizio).Hours;



                Utility.SalvaLogDef(tipoOperazione.inserimento, "Modulo " + modulo.numero + " Edizione " + modulo.EdizioneCorsoID, _context, HttpContext.Session);

                return RedirectToAction("VediEdizioni", "ProgettoFormativo", new { id = progetto });

            }
            ViewData["EdizioneCorsoID"] = new SelectList(_context.Edizioni, "EdizioneCorsoID", "EdizioneCorsoID", modulo.EdizioneCorsoID);
            return RedirectToAction("VediEdizioni", "ProgettoFormativo", new {idd = progetto});
        }

        // GET: Modulo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Moduli == null)
            {
                return NotFound();
            }

            var modulo = await _context.Moduli.FindAsync(id);
            if (modulo == null)
            {
                return NotFound();
            }

            

            ViewData["EdizioneCorsoID"] = new SelectList(_context.Edizioni, "EdizioneCorsoID", "EdizioneCorsoID", modulo.EdizioneCorsoID);


            return View(modulo);
        }

        // POST: Modulo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ModuloID,EdizioneCorsoID,numero,data_inizio,data_fine,totale_ore,dataorains,IDutenteins,dataoraultmod,IDutenteultmod,eliminato,IDutenteultcanc,dataoraultcanc")] Modulo modulo)
        {
            if (id != modulo.ModuloID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    modulo.dataoraultmod = System.DateTime.Now;
                    modulo.IDutenteultmod = HttpContext.Session.GetInt32("utente_id");
                    _context.Update(modulo);

                    await _context.SaveChangesAsync();

                    Utility.SalvaLogDef(tipoOperazione.modifica, "Modulo " + modulo.numero + " Edizione " + modulo.EdizioneCorsoID, _context, HttpContext.Session);
                 
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModuloExists(modulo.ModuloID))
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
            ViewData["EdizioneCorsoID"] = new SelectList(_context.Edizioni, "EdizioneCorsoID", "EdizioneCorsoID", modulo.EdizioneCorsoID);
            return View(modulo);
        }

        // GET: Modulo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Moduli == null)
            {
                return NotFound();
            }

            var modulo = await _context.Moduli
                .Include(m => m.Edizione)
                .FirstOrDefaultAsync(m => m.ModuloID == id);
            if (modulo == null)
            {
                return NotFound();
            }

            return View(modulo);
        }

        // POST: Modulo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Moduli == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Moduli'  is null.");
            }
            var modulo = await _context.Moduli.FindAsync(id);
            if (modulo != null)
            {
                modulo.eliminato = 1;
                modulo.dataoraultcanc = System.DateTime.Now;
                modulo.IDutenteultcanc   = HttpContext.Session.GetInt32("utente_id");
                _context.Moduli.Update(modulo);


                Utility.SalvaLogDef(tipoOperazione.eliminazione, "Modulo " + modulo.numero + " Edizione " + modulo.EdizioneCorsoID, _context, HttpContext.Session);

                await _context.SaveChangesAsync();
            }
            

            
            return RedirectToAction(nameof(Index));
        }

        private bool ModuloExists(int id)
        {
          return (_context.Moduli?.Any(e => e.ModuloID == id)).GetValueOrDefault();
        }
    }
}

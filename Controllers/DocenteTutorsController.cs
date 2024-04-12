using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Formazione.Data;
using Formazione.Models;
using Microsoft.CodeAnalysis.Options;
using Newtonsoft.Json.Linq;
using System.Numerics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Formazione.Controllers
{
    [Authentication]
    public class DocenteTutorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DocenteTutorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DocenteTutors
        public async Task<IActionResult> Index()
        {
            return _context.DocenteTutor != null ? 
                          View(await _context.DocenteTutor.Where(m => m.eliminato == 0 || m.eliminato == null).ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.DocentiTutor'  is null.");
        }

        // GET: DocenteTutors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DocenteTutor == null)
            {
                return NotFound();
            }

            var docenteTutor = await _context.DocenteTutor
                .FirstOrDefaultAsync(m => m.DocenteTutorID == id);
            if (docenteTutor == null)
            {
                return NotFound();
            }
            return View(docenteTutor);
        }

        // GET: DocenteTutors/Create
        public IActionResult Create()
        {

           

            ViewData["TipologieDocentiTutor"]    = Enum.GetNames(typeof(TipologiaDocenteTutor)).ToList();
            return View();
        }

        // POST: DocenteTutors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DocenteTutorID,Note,Cognome,Nome,Tipologia,Qualifica,CF,Email,Telefono,Cellulare,dataorains,IDutenteins,dataoraultmod,IDutenteultmod,eliminato,IDutenteultcanc,dataoraultcanc")] DocenteTutor docenteTutor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(docenteTutor);
                await _context.SaveChangesAsync();

                docenteTutor.eliminato = 0;
                docenteTutor.dataorains = System.DateTime.Now;
                docenteTutor.IDutenteins = HttpContext.Session.GetInt32("utente_id");

                Utility.SalvaLogDef(tipoOperazione.inserimento, "DocenteTutor " + docenteTutor.DocenteTutorID, _context, HttpContext.Session);

                return RedirectToAction(nameof(Index));
            }

            return View(docenteTutor);
        }

        // GET: DocenteTutors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DocenteTutor == null)
            {
                return NotFound();
            }

            var docenteTutor = await _context.DocenteTutor.FindAsync(id);
            if (docenteTutor == null)
            {
                return NotFound();
            }
            Utility.SalvaLogDef(tipoOperazione.modifica, "DocenteTutor " + docenteTutor.DocenteTutorID, _context, HttpContext.Session);

            return View(docenteTutor);
        }

        // POST: DocenteTutors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DocenteTutorID,Note,Cognome,Nome,Tipologia,Qualifica,CF,Email,Telefono,Cellulare,dataorains,IDutenteins,dataoraultmod,IDutenteultmod,eliminato,IDutenteultcanc,dataoraultcanc")] DocenteTutor docenteTutor)
        {
            if (id != docenteTutor.DocenteTutorID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(docenteTutor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocenteTutorExists(docenteTutor.DocenteTutorID))
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
            return View(docenteTutor);
        }

        // GET: DocenteTutors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DocenteTutor == null)
            {
                return NotFound();
            }

            var docenteTutor = await _context.DocenteTutor
                .FirstOrDefaultAsync(m => m.DocenteTutorID == id);
            if (docenteTutor == null)
            {
                return NotFound();
            }

            return View(docenteTutor);
        }

        // POST: DocenteTutors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DocenteTutor == null)
            {
                return Problem("Entity set 'ApplicationDbContext.DocentiTutor'  is null.");
            }
            var docenteTutor = await _context.DocenteTutor.FindAsync(id);
            if (docenteTutor != null)

            {
                docenteTutor.eliminato = 1;
                docenteTutor.dataoraultcanc = System.DateTime.Now;
                docenteTutor.IDutenteultcanc = HttpContext.Session.GetInt32("utente_id");
                _context.Update(docenteTutor);

            }
            
            await _context.SaveChangesAsync();

            Utility.SalvaLogDef(tipoOperazione.eliminazione, "DocenteTutor " + docenteTutor.DocenteTutorID, _context, HttpContext.Session);

            return RedirectToAction(nameof(Index));
        }

        private bool DocenteTutorExists(int id)
        {
          return (_context.DocenteTutor?.Any(e => e.DocenteTutorID == id)).GetValueOrDefault();
        }
    }
}

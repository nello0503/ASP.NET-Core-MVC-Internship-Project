using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Formazione.Data;
using Formazione.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;


namespace Formazione.Controllers
{
   
    public class UtenteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UtenteController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authentication]
        // GET: Utente
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetRuoloUtenteLoggato() == TipologiaUtente.Admin)
            {
                return _context.Utente != null ?
                        View(await _context.Utente.Where(m => m.eliminato == 0).ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Utente'  is null.");
            }
            else
                return RedirectToAction("Privacy", "Home");
        }




        [Authentication]
        // GET: Utente/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetRuoloUtenteLoggato() == TipologiaUtente.Admin)
            {
                if (id == null || _context.Utente == null)
                {
                    return NotFound();
                }

                var utente = await _context.Utente
                    .FirstOrDefaultAsync(m => m.utente_id == id && m.eliminato == 0);
                if (utente == null)
                {
                    return NotFound();
                }

                return View(utente);
            }
            else
                return RedirectToAction("Privacy", "Home");

        }

        [Authentication]
        // GET: Utente/Create
        public IActionResult Create()
        {


            if (HttpContext.Session.GetRuoloUtenteLoggato() == TipologiaUtente.Admin)
            {
                ViewData["ruolo"] = new SelectList(_context.Utente.Where(m => m.eliminato == 0 || m.eliminato == null), "ruolo", "ruolo");
                return View();
            } else
                return RedirectToAction("Privacy", "Home");


        }


        [Authentication]
        // POST: Utente/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("utente_id,cognome,nome,email,username,password,ruolo,CF,unitaprodAbilitate,eliminato,dataorains,IDutenteins,dataoraultmod,IDutenteultmod,IDutenteultcanc,dataoraultcanc")] Utente utente)
        {

           

                if (ModelState.IsValid)
                {

                    utente.eliminato = 0;
                    utente.IDutenteins = HttpContext.Session.GetIdUtenteLoggato();
                    utente.dataorains = System.DateTime.Now;
                    _context.Add(utente);
                    await _context.SaveChangesAsync();
                    Data.Utility.SalvaLogDef(tipoOperazione.inserimento, "UtenteID = " + utente.utente_id, _context, HttpContext.Session);


                return View(utente);
            }

            return View(utente);

        }







        [Authentication]
        // GET: Utente/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Utente == null)
            {
                return NotFound();
            }

            var utente = await _context.Utente.FindAsync(id);
            if (utente == null)
            {
                return NotFound();
            }
            return View(utente);
        }

        [Authentication]
        // POST: Utente/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("utente_id,cognome,nome,email,username,password,ruolo,CF,unitaprodAbilitate,eliminato,dataorains,IDutenteins,dataoraultmod,IDutenteultmod,IDutenteultcanc,dataoraultcanc")] Utente utente)
        {

           
            if (id != utente.utente_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
               
               
                try
                {       
                    utente.IDutenteultmod = HttpContext.Session.GetIdUtenteLoggato();
                    utente.dataoraultmod = System.DateTime.Now;
                    _context.Update(utente);
                    await _context.SaveChangesAsync();
                    

                }
                
                catch (DbUpdateConcurrencyException)
                {
                    if (!UtenteExists(utente.utente_id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                Data.Utility.SalvaLogDef(tipoOperazione.modifica, "UtenteID = " + id, _context, HttpContext.Session);
                return RedirectToAction(nameof(Index));
            }
            return View(utente);
        }

        [Authentication]
        // GET: Utente/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Utente == null)
            {
                return NotFound();
            }

            var utente = await _context.Utente
                .FirstOrDefaultAsync(m => m.utente_id == id);
            if (utente == null)
            {
                return NotFound();
            }

            return View(utente);
        }

        [Authentication]
        // POST: Utente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Utente == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Utente'  is null.");
            }
            var utente = await _context.Utente.FindAsync(id);
            if (utente != null)
            {
                utente.IDutenteultcanc = HttpContext.Session.GetIdUtenteLoggato();
                utente.eliminato = 1;
                utente.dataoraultcanc = System.DateTime.Now;
                _context.Utente.Update(utente);
                

            }

            await _context.SaveChangesAsync();
            Data.Utility.SalvaLogDef(tipoOperazione.eliminazione, "UtenteID = " + utente.utente_id, _context, HttpContext.Session);
            return RedirectToAction(nameof(Index));
        }

        private bool UtenteExists(int id)
        {
            return (_context.Utente?.Any(e => e.utente_id == id)).GetValueOrDefault();
        }
        // Get Action
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        //Post Action

        [HttpPost]
        public ActionResult Login(Utente u)
        {
            if (HttpContext.Session.GetString("username") == null)
            {
                if (true)
                {

                    var obj = _context.Utente.Where(a => a.username.Equals(u.username) && a.password.Equals(u.password)).FirstOrDefault();
                    if (obj != null)
                    {
                        HttpContext.Session.SetString("UserName", obj.username.ToString());
                        HttpContext.Session.SetInt32("Ruolo", (int)obj.ruolo);
                        HttpContext.Session.SetInt32("utente_id", obj.utente_id);
                        HttpContext.Session.SetString("UnitaProdAbilitate", obj.unitaprodAbilitate == null ? "" : obj.unitaprodAbilitate.ToString());

                        Data.Utility.SalvaLogDef(tipoOperazione.login, "UtenteID = " + obj.utente_id, _context, HttpContext.Session);

                        return RedirectToAction("Index", "Home");
                    }

                    else
                    {
                        /*creo una variabile temporanea che mi serve per passarla a javascript e vedere se nella sessione la passoword è giusta o meno*/
                        TempData["ErrorMessage"] = "Utente o Password Errata! Riprovare";
                        return RedirectToAction("Login");
                    }
                }
            }
            else
            {
                return RedirectToAction("Index");
            }

        }



        public  async Task<ActionResult> Logout()
        {


            LogTable log = new LogTable
            {
                utenteID = HttpContext.Session.GetIdUtenteLoggato(),
                dataOra = System.DateTime.Now,
                operazione = tipoOperazione.logout, // Ensure that this cast is valid
                messaggio = "Logout UtenteID = " + HttpContext.Session.GetIdUtenteLoggato(),
            };

            _context.LogTables.Add(log);
            await _context.SaveChangesAsync();



            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName");
            HttpContext.Session.Remove("Ruolo");
            HttpContext.Session.Remove("UnitaProdAbilitate");
            HttpContext.Session.Remove("utente_id");




            return RedirectToAction("Login");
        }
    }

}

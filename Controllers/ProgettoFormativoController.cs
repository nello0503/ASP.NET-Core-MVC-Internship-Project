using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Formazione.Data;
using Formazione.Models;
using System.Data;
using JetBrains.Annotations;
using SQLitePCL;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NuGet.Frameworks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Formazione.Controllers
{
    
    public class ProgettoFormativoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProgettoFormativoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authentication]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SalvaPianificazione(CustomViewPianificazioneProg customView)
        {
            if (!ModelState.IsValid)
            {
                InsertUpdatePianificazioni(customView.PianificazioneProgetto);



                Utility.SalvaLogDef(tipoOperazione.modifica, "Pianificazioni " + customView.PianificazioneProgetto[0].ProgettoFormativoID, _context, HttpContext.Session);



                return RedirectToAction("VediPianificazione", new { id = customView.PianificazioneProgetto[0].ProgettoFormativoID });
            }
            //return View(customView);
            return RedirectToAction(nameof(Index));
        }




        [Authentication]
        public void InsertUpdatePianificazioni(List<Pianificazione> PianificazioneProgetto)
        {



            foreach (Pianificazione item in PianificazioneProgetto)
            {
                if (item.PianificazioneID == 0)
                    _context.Pianificazioni.Add(item);
                else
                    _context.Pianificazioni.Update(item);
            }

            _context.SaveChanges();

        }
        
        [Authentication]
        [HttpGet]
        // GET: ProgettoFormativo
        public async Task<IActionResult> Index(DateTime? fromDate = null, DateTime? toDate = null, ProgettoFormativo.ProgettoFilter filter = ProgettoFormativo.ProgettoFilter.Tutti, string OrderBy = "")

        {
            DateTime currentDate = DateTime.Now;
            
            

            var query = _context.Progetti.Where(m => m.eliminato == 0 || m.eliminato == null).Include(c => c.TipoCorso).AsNoTracking();

            switch (filter)
            {
                case ProgettoFormativo.ProgettoFilter.InCorso:
                    query = (from t1 in query
                             from p in t1.edizioneCorso
                             from u in p.Moduli
                             where u.data_inizio.Date <= DateTime.Now.Date && u.data_fine.Date >= DateTime.Now.Date
                             select t1);
                    break;
                case ProgettoFormativo.ProgettoFilter.NonInCorso:
                    query = (from t1 in query
                             from p in t1.edizioneCorso
                             from u in p.Moduli
                             where !(u.data_inizio.Date <= DateTime.Now.Date && u.data_fine.Date >= DateTime.Now.Date)
                             select t1);
                    break;
                case ProgettoFormativo.ProgettoFilter.SenzaEdizioni:
                    query = query.Where(m => m.edizioneCorso.Count == 0);
                    break;
                case ProgettoFormativo.ProgettoFilter.Tutti:
                default:
                    break;
            }



            //Filtro Datapicker
            if(fromDate.HasValue && toDate.HasValue)
            {
                toDate = toDate.Value.Date.AddDays(1); //Un giorno per includere le date fino a mezzanotte
                query = query.Where(m => m.edizioneCorso.Any(ed => ed.Moduli.All(mod => mod.data_inizio.Date >= fromDate) && ed.Moduli.All(mod => mod.data_fine.Date <= toDate)));
            }

            switch (OrderBy)
            {
                case "ID_desc":
                    query = query.OrderByDescending(m => m.ProgettoFormativoID);
                    break;
                case "direttore_scientifico":
                    query = query.OrderBy(m => m.direttore_scientifico);
                    break;
                case "direttore_scientifico_desc":
                    query = query.OrderByDescending(m => m.direttore_scientifico);
                    break;
                // Aggiungi altri casi per ulteriori opzioni di ordinamento, se necessario
                default:
                    // Imposta un ordinamento predefinito (es. per ID_desc)
                    query = query.OrderByDescending(m => m.ProgettoFormativoID);
                    break;
            }

            return View(query);
        }
        
        [Authentication]
        public IActionResult VediEdizioni(int? id)
        {

            var tables = new CustomViewProgEdiz
            {
                listaProgetti =_context.Progetti.Where(m => m.ProgettoFormativoID == id && (m.eliminato == 0 || m.eliminato == null)).ToList(),
                listaEdizioni =_context.Edizioni.Where(m => m.ProgettoFormativoID == id && (m.eliminato == 0 || m.eliminato == null)).ToList().OrderBy(m=>m.NumeroEdizione),
                listaModuli = _context.Moduli.Where(m => m.eliminato == 0).ToList()

            };
            return View(tables);

        }

        [Authentication]
        public IActionResult VediPianificazione(int? id)
        {
            ProgettoFormativo project = _context.Progetti.FirstOrDefault(m => m.ProgettoFormativoID == id && m.eliminato == 0);
            var edizioni = _context.Edizioni.Where(m => m.ProgettoFormativoID == id && m.eliminato == 0).ToList();
            var unitaProd = _context.UnitaProduttive.Where(m => m.eliminato == 0).ToList();
            var listaPianificazioni = _context.Pianificazioni.Where(m => m.ProgettoFormativoID == id).ToList();



            foreach (var itemUP in unitaProd)
            {
                foreach (var itemEdizione in edizioni)
                {
                    var valuePian = listaPianificazioni.Where(m => m.EdizioneCorsoID == itemEdizione.EdizioneCorsoID && m.UnitaProduttivaID == itemUP.UnitaProduttivaID).Select(n => n.NumeroPrevisto);
                    string valueLoad = "";
                    if (valuePian.Count() == 0)
                    {
                        Pianificazione nuova = new Pianificazione();
                        nuova.UnitaProduttivaID = itemUP.UnitaProduttivaID;
                        nuova.EdizioneCorsoID = itemEdizione.EdizioneCorsoID;
                        nuova.ProgettoFormativoID = itemEdizione.ProgettoFormativoID;
                        nuova.NumeroPrevisto = 0;
                        nuova.NumeroAssegnato = 0;
                        listaPianificazioni.Add(nuova);
                    }
                }



            }


            CustomViewPianificazioneProg viewmodel = new CustomViewPianificazioneProg(
             project.ProgettoFormativoID,
             project.TipoCorsoID,
             project.titolo,
             project.direttore_scientifico,
             project.tipologia_evento,
             project.tutor,
             project.Codice,
             project.tutor_aula,
             project.ore_edizione,
            project.numero_partecipanti,
             project.sede_svolgimento,
             edizioni,
              listaPianificazioni,
                 unitaProd);




            return View(viewmodel);
        }
        
        [Authentication]
        // GET: ProgettoFormativo/Create
        public IActionResult Create()
        {

            var tipoCorsi = _context.Corsi.Where(m=>m.eliminato==0).Select(tc => new SelectListItem { Value = tc.TipoCorsoID.ToString(), Text = tc.DescrizioneCodice+"-"+tc.Codice }).ToList();

            ViewData["TipoCorsoID"] = new SelectList(tipoCorsi, "Value", "Text");

            return View();
        }


        [Authentication]
        // POST: ProgettoFormativo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProgettoFormativoID,Codice,TipoCorsoID,titolo,direttore_scientifico,tipologia_evento,tutor,tutor_aula,ore_edizione,numero_partecipanti,sede_svolgimento,dataorains,IDutenteins,dataoraultmod,IDutenteultmod,eliminato,IDutenteultcanc,dataoraultcanc")] ProgettoFormativo progettoFormativo)
        {
            if (ModelState.IsValid)
            {
                if (int.TryParse(Request.Form["TipoCorsoID"], out int tipoCorsoID))
                {
                    progettoFormativo.TipoCorsoID = tipoCorsoID;
                }

                progettoFormativo.eliminato = 0;
                progettoFormativo.dataorains = System.DateTime.Now;
                progettoFormativo.IDutenteins =  HttpContext.Session.GetInt32("utente_id");
              
                _context.Add(progettoFormativo);
                await _context.SaveChangesAsync();
                Utility.SalvaLogDef(tipoOperazione.inserimento, "Progetto " + progettoFormativo.Codice + " Titolo " + progettoFormativo.titolo, _context, HttpContext.Session);

                return RedirectToAction(nameof(Index));
            }
            return View(progettoFormativo);
        }

        
        [Authentication]
        // GET: ProgettoFormativo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var tipoCorsi = _context.Corsi.Where(m=>m.eliminato == 0).Select(tc => new SelectListItem { Value = tc.TipoCorsoID.ToString(), Text = tc.DescrizioneCodice+"-"+tc.Codice }).ToList();
            ViewData["TipoCorsoID"] = new SelectList(tipoCorsi, "Value", "Text");


            if (id == null || _context.Progetti == null)
            {
                return NotFound();
            }

            var progettoFormativo = await _context.Progetti.FindAsync(id);
            if (progettoFormativo == null)
            {
                return NotFound();
            }
            
            return View(progettoFormativo);
        }
        [Authentication]
        // POST: ProgettoFormativo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProgettoFormativoID,Codice,TipoCorsoID,titolo,direttore_scientifico,tipologia_evento,tutor,tutor_aula,ore_edizione,numero_partecipanti,sede_svolgimento,dataorains,IDutenteins,dataoraultmod,IDutenteultmod,eliminato,IDutenteultcanc,dataoraultcanc")] ProgettoFormativo progettoFormativo)
        {
            if (id != progettoFormativo.ProgettoFormativoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (int.TryParse(Request.Form["TipoCorsoID"], out int TipoCorsoID))
                {
                    progettoFormativo.TipoCorsoID = TipoCorsoID;
                }
                try
                {
                    progettoFormativo.dataoraultmod = System.DateTime.Now;
                    progettoFormativo.IDutenteultmod =  HttpContext.Session.GetInt32("utente_id");
                    _context.Update(progettoFormativo);

                    await _context.SaveChangesAsync();
                    Utility.SalvaLogDef(tipoOperazione.modifica, "Progetto " + progettoFormativo.Codice + " Titolo " + progettoFormativo.titolo, _context, HttpContext.Session);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProgettoFormativoExists(progettoFormativo.ProgettoFormativoID))
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
            return View(progettoFormativo);
        }

        [Authentication]
        
        // GET: ProgettoFormativo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Progetti == null)
            {
                return NotFound();
            }

            var progettoFormativo = await _context.Progetti
                .FirstOrDefaultAsync(m => m.ProgettoFormativoID == id && m.eliminato == 0);
            if (progettoFormativo == null)
            {
                return NotFound();
            }

            return View(progettoFormativo);
        }


        [Authentication]
        // GET: ProgettoFormativo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Progetti == null)
            {
                return NotFound();
            }

            var progettoFormativo = await _context.Progetti
                .FirstOrDefaultAsync(m => m.ProgettoFormativoID == id);
            if (progettoFormativo == null)
            {
                return NotFound();
            }

            return View(progettoFormativo);
        }
        [Authentication]
        // POST: ProgettoFormativo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Progetti == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Progetti'  is null.");
            }
            var progettoFormativo = await _context.Progetti.FindAsync(id);
            if (progettoFormativo != null)
            {
                progettoFormativo.eliminato = 1;
                progettoFormativo.dataoraultcanc = System.DateTime.Now;
                progettoFormativo.IDutenteultcanc =  HttpContext.Session.GetInt32("utente_id");
                _context.Update(progettoFormativo);
                await _context.SaveChangesAsync();
                //_context.Progetti.Remove(progettoFormativo);
                Utility.SalvaLogDef(tipoOperazione.inserimento, "Progetto " + progettoFormativo.Codice + " Titolo " + progettoFormativo.titolo, _context, HttpContext.Session);

            }



            return RedirectToAction(nameof(Index));
        }
        [Authentication]
        private bool ProgettoFormativoExists(int id)
        {
            return (_context.Progetti?.Any(e => e.ProgettoFormativoID == id)).GetValueOrDefault();
        }

        
        public ActionResult IndexDatore()
        {

            var pian = _context.Pianificazioni
                    .Where(p => p.ProgettoFormativoID != null)
                    .Select(p => p.ProgettoFormativoID)
                    .Distinct()
                    .ToList();

            var progettoFormativos = _context.Progetti
                .Where(m => (m.eliminato == 0 || m.eliminato == null) && pian.Contains(m.ProgettoFormativoID))
                .ToList();


            return View(progettoFormativos);
        }


        public IActionResult VediAssegnazioni(int id)
        {



            var listaunita =  HttpContext.Session
                        .GetListaUnitaAbilitate();



            var tables = new CustomViewAssegnazione
            {

                progettoID = id,

                listaDiscenti = _context.Discente
                          .Where(m => m.eliminato == 0 && m.ProgettoFormativoID == id)
                          .ToList(),
                listaPianificazioni = _context.Pianificazioni
                         .Where(m => m.ProgettoFormativoID == id)
                         .ToList(),
                unita = new SelectList(_context.UnitaProduttive.Where(m => listaunita.Contains(m.Codice)), "UnitaProduttivaID", "Codice"),
               
                
                

                numeroEdiz = new SelectList(_context.Edizioni.Where(m => m.ProgettoFormativoID == id && m.eliminato == 0), "EdizioneCorsoID", "NumeroEdizione"),



            };



            return View(tables);



        }





        public async Task<IActionResult> SalvaDiscente(CustomViewAssegnazione newDisc)
            {


            Discente disc = new Discente
            {


                ProgettoFormativoID = newDisc.discente.ProgettoFormativoID,
                eliminato = 0,
                Matricola = newDisc.discente.Matricola,
                Assegnato = false,
                nome = newDisc.discente.nome,
                cognome = newDisc.discente.cognome,
                cf = newDisc.discente.cf,
                data_di_nascita = newDisc.discente.data_di_nascita,
                dataoraultmod = System.DateTime.Now,
                EdizioneCorsoID = newDisc.discente.EdizioneCorsoID,
                UnitaProduttiva = newDisc.discente.UnitaProduttiva,

                IDutenteins =  HttpContext.Session.GetIdUtenteLoggato(),
                dataorains = System.DateTime.Now,

            };

            _context.Discente.Add(disc);

            await _context.SaveChangesAsync();
            Utility.SalvaLogDef(tipoOperazione.inserimento, "Progetto " + newDisc.progettoID+ " CF " + disc.cf, _context, HttpContext.Session);



            return RedirectToAction("VediAssegnazioni", new { id = newDisc.discente.ProgettoFormativoID });

        }

           public async Task<IActionResult> DeleteDip(int id){
            
            var discente = await _context.Discente.FindAsync(id);
            var progetto = discente.ProgettoFormativoID;

            if(discente == null)
            {
                RedirectToAction("IndexDatore");
            }


            if (discente != null) {
                discente.eliminato = 1;
                discente.dataoraultcanc = System.DateTime.Now;
                discente.IDutenteultcanc = HttpContext.Session.GetIdUtenteLoggato();

                _context.Discente.Update(discente);
            
            }


            await _context.SaveChangesAsync();

            Utility.SalvaLogDef(tipoOperazione.eliminazione, discente.cf, _context, HttpContext.Session);

            return RedirectToAction("VediAssegnazioni", new { id = progetto });


        }



        
        public async Task<IActionResult> Assegna(int id)
        {

            var discente = await _context.Discente.FindAsync(id);

            var progetto = discente.ProgettoFormativoID;

            var unita = discente.UnitaProduttiva;
            var edizione = discente.EdizioneCorsoID;
            

            TempData["unita"] = unita;
            TempData["edizione"] = edizione;
            


            if(discente.Assegnato == true) {

                discente.Assegnato = false;
                discente.DataAssegnato = null;
                discente.UtenteAss = " ";
                _context.Update(discente);

               

            } else  {

                discente.Assegnato = true;
                discente.DataAssegnato = System.DateTime.Now;
                discente.UtenteAss = _context.Utente.Where(m => m.utente_id == HttpContext.Session.GetIdUtenteLoggato()).Select(m => m.cognome + " " + m.nome).FirstOrDefault();
                _context.Update(discente);

                
            }



            await _context.SaveChangesAsync();




            return RedirectToAction("VediAssegnazioni", new { id = progetto});

        }


     



            [HttpPost]
        public JsonResult JqAJAX(int value1, int value2)
        {
            // Retrieve 'totale' value
            var totale = _context.Pianificazioni
                .Where(m => m.EdizioneCorsoID == value1 && m.ProgettoFormativoID == value2)
                .Select(m => m.NumeroAssegnato)
                .FirstOrDefault();

            // Retrieve 'assegnati' value
            var assegnati = _context.Discente
                .Where(m => m.EdizioneCorsoID == value1 && m.ProgettoFormativoID == value2 && m.Assegnato == true)
                .Count();

            var result = new { totale, assegnati };

            return Json(result);
        }



        //public IActionResult CaricaDip()
        //{








        //}



    }
}

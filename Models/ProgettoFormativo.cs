using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace Formazione.Models
{
    public class ProgettoFormativo
    {
        [Key]


        public int ProgettoFormativoID { get; set; }

        [ForeignKey("TipoCorsoID")]
        [DisplayName("ID del Corso")]
        public int TipoCorsoID { get; set; }

        public string? titolo { get; set; }

        [DisplayName("Direttore Scientifico")]
        public string? direttore_scientifico { get; set; }

        [DisplayName("Tipologia dell'evento")]
        public string? tipologia_evento { get; set; }

        [DisplayName("Tutor")]
        public string? tutor { get; set; }
        [Required, StringLength(10)]
        public String Codice { get; set; }

        [DisplayName("Tutor d'aula")]
        public string? tutor_aula { get; set; }

        [DisplayName("Totale ore edizione")]
        public int? ore_edizione { get; set; }

        [DisplayName("Totale numero partecipanti")]
        public int? numero_partecipanti { get; set; }

        [DisplayName("Sede svolgimento")]
        public string? sede_svolgimento { get; set; }

        [DisplayName("Data ora inserimento")]
        public DateTime? dataorains { get; set; }

        [DisplayName("ID dell'utente inserito")]
        public int? IDutenteins { get; set; }

        [DisplayName("Data dell'ultima modifica")]
        public DateTime? dataoraultmod { get; set; }

        [DisplayName("ID dell'ultimo utente modificato")]
        public int? IDutenteultmod { get; set; }

        public short? eliminato { get; set; }

        [DisplayName("ID dell'ultimo utente cancellato")]
        public int? IDutenteultcanc { get; set; }

        [DisplayName("Data ora ultima cancellazione")]
        public DateTime? dataoraultcanc { get; set; }

        [Required]
        [Range(0, 40)]
        public int NumeroEdizioni { get; set; }


        public virtual ICollection<EdizioneCorso>? edizioneCorso { get; set; }

        public TipoCorso? TipoCorso { get; set; }

        public virtual ICollection<Pianificazione>? pianificazione { get; set; }


        public enum ProgettoFilter
        {
            [Display(Name = "InCorso")]
            InCorso = 0,

            [Display(Name = "Non In Corso")]
            NonInCorso = 1,

            [Display(Name = "Senza Edizioni")]
            SenzaEdizioni = 2,

            [Display(Name = "Tutti")]
            Tutti = 3,
        }

        public bool isInCorso()
        {
            bool result = false;

            bool q = (from p in edizioneCorso
                      from u in p.Moduli
                      where u.data_inizio.Date <= DateTime.Now.Date && u.data_fine.Date >= DateTime.Now.Date
                      select p).ToList().Count() > 0;

            return result;

        }


        /*
        public IQueryable<ProgettoFormativo> Progettos { get; set;}
        public int PageSize { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }
        */

    }
}

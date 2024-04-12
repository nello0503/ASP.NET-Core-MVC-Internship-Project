using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Numerics;

namespace Formazione.Models
{

    public class CustomViewPianificazioneProg
    {
        public int ProgettoFormativoID { get; set; }

        public int TipoCorsoID { get; set; }

        public string? titolo { get; set; }

        public string? direttore_scientifico { get; set; }

        public string? tipologia_evento { get; set; }

        public string? tutor { get; set; }
        [Required, StringLength(10)]
        public String Codice { get; set; }

        public string? tutor_aula { get; set; }

        public int? ore_edizione { get; set; }

        public int? numero_partecipanti { get; set; }

        public string? sede_svolgimento { get; set; }



        public List<EdizioneCorso> EdizioniProgetto { get; set; }

        public List<Pianificazione> PianificazioneProgetto { get; set; }

        public List<UnitaProduttiva> UnitaProduttive { get; set; }

        public CustomViewPianificazioneProg() { }

        public CustomViewPianificazioneProg(int progettoFormativoID, int tipoCorsoID, string? titolo, string? direttore_scientifico, string? tipologia_evento, string? tutor, string codice, string? tutor_aula, int? ore_edizione, int? numero_partecipanti, string? sede_svolgimento, List<EdizioneCorso> edizioniProgetto, List<Pianificazione> pianificazioneProgetto, List<UnitaProduttiva> unitaProduttive)
        {
            ProgettoFormativoID = progettoFormativoID;
            TipoCorsoID = tipoCorsoID;
            this.titolo = titolo;
            this.direttore_scientifico = direttore_scientifico;
            this.tipologia_evento = tipologia_evento;
            this.tutor = tutor;
            Codice = codice;
            this.tutor_aula = tutor_aula;
            this.ore_edizione = ore_edizione;
            this.numero_partecipanti = numero_partecipanti;
            this.sede_svolgimento = sede_svolgimento;
            EdizioniProgetto = edizioniProgetto;
            PianificazioneProgetto = pianificazioneProgetto;
            UnitaProduttive = unitaProduttive;
        }

        public Pianificazione? pianificazione { get; set; }



    }


}

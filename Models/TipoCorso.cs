



using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Formazione.Models

{
    public class TipoCorso

    {
        public int TipoCorsoID { get; set; }

        public string? Codice { get; set; }

        public string? DescrizioneCodice { get; set; }

        [Range(0, 20)]
        [DisplayName("Durata in ore")]
        public int? Durata {  get; set; }
        public int? Sedute { get; set; }
        public int? Moduli { get; set; }

        public int? AnniValidità { get; set; }

        public int? MaxDiscenti { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]

        public DateTime? dataorains { get; set; }

        public int? IDutenteins { get; set; }

        public DateTime? dataoraultmod { get; set; }

        public int? IDutenteultmod { get; set; }

        public short eliminato { get; set; }

        public int? IDutenteultcanc { get; set; }

        public DateTime? dataoraultcanc { get; set; }

        public ICollection<ProgettoFormativo>? progettiFormativi { get; set; }
      

    }
}

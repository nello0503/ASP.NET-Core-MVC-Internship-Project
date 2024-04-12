using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Formazione.Models
{
    public class EdizioneCorso
    {


        [Key]
        public int EdizioneCorsoID { get; set; }

        [ForeignKey("ProgettoFormativo")]
        public int ProgettoFormativoID { get; set; }

        public int ?Anno { get; set; }

        [Required]
        public int NumeroEdizione { get; set; }

        public int ?QuantitàModuli { get; set; }

        public string ? Descrizione { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]

        public DateTime? dataorains { get; set; }

        public int? IDutenteins { get; set; }

        public DateTime? dataoraultmod { get; set; }

        public int? IDutenteultmod { get; set; }

        public short? eliminato { get; set; }

        public int? IDutenteultcanc { get; set; }

        public DateTime? dataoraultcanc { get; set; }

        public ICollection<Modulo> ?Moduli {  get; set; }

        //public virtual ProgettoFormativo? Progetto { get; set; }




    }
}

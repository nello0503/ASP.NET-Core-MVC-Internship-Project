using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Formazione.Models
{
    public class Modulo
    {

        public int ModuloID { get; set; }



        public int EdizioneCorsoID { get; set; }


        public int numero { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dddd, dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime data_inizio { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh:mm}")]
        public DateTime data_fine { get; set; }

        public int totale_ore { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime dataorains { get; set; }

        public int? IDutenteins { get; set; }

        public DateTime? dataoraultmod { get; set; }

        public int? IDutenteultmod { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int eliminato { get; set; }

        public int? IDutenteultcanc { get; set; }

        public DateTime? dataoraultcanc { get; set; }

        public EdizioneCorso ?Edizione { get; set; }
    }
}

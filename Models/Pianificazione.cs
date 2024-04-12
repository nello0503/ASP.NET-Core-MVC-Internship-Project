using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Formazione.Models
{
    public class Pianificazione
    {

        [Key]
        public int PianificazioneID { get; set; }
        
        [ ForeignKey("ProgettoFormativo")]
        public int? ProgettoFormativoID { get; set; }

        [ ForeignKey("EdizioneCorso")]
        public int? EdizioneCorsoID { get; set; }

        public int? UnitaProduttivaID { get; set; }

        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Deve essere un numero")]
        public int NumeroPrevisto { get; set; }

        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Deve essere un numero")]
        public int NumeroAssegnato { get; set; }

        public virtual ProgettoFormativo Progetto { get; set; }


    }
}

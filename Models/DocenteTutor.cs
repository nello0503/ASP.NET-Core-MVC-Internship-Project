using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Formazione.Models
{
    public class DocenteTutor
    {

        [Key]
        public int DocenteTutorID { get; set; }

        public string? Note { get; set; }

        [RegularExpression(@"^[a-zA-Z]*$")]
        [StringLength(50)]
        [Required(ErrorMessage = "Cognome è Richiesto")]
        public string? Cognome { get; set; }

        [RegularExpression(@"^[a-zA-Z]*$")]
        [StringLength(50)]
        public string? Nome { get; set; }

        [DisplayName("Docente/Tutor")]
        public TipologiaDocenteTutor? Tipologia { get; set; }

        public string? Qualifica { get; set; }

        [StringLength(16)]
        [Required(ErrorMessage = "CF è Richiesto")]
        public string? CF { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        //[Range(1, 200,            ErrorMessage = "Price must be between 0.01 and 100.00")]
        [RegularExpression("^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number."), StringLength(10)]
        public string? Telefono { get; set; }
        [RegularExpression("^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number."), StringLength(10)]
        public string? Cellulare { get; set; }


        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime dataorains { get; set; }

        public int? IDutenteins { get; set; }
        
        [HiddenInput]
        public DateTime? dataoraultmod { get; set; }

        [HiddenInput]
        public int? IDutenteultmod { get; set; }

        [HiddenInput]
        public short eliminato { get; set; }

        [HiddenInput]
        public int? IDutenteultcanc { get; set; }

        [HiddenInput]
        public DateTime? dataoraultcanc { get; set; }



    }

    

    public enum TipologiaDocenteTutor:int
    {
        Docente = 0,
        Tutor = 1,
        Docente_Tutor = 2
    }
    public enum Qualifica
    {
        Dott,
        Dottssa,
        Ing,
        Prof,
        Altro
    }
}


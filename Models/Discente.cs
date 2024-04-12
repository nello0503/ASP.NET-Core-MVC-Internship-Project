using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Formazione.Models
{
    public class Discente
    {

        [Key]
        public int DiscenteID { get; set; }
 
        public int? ProgettoFormativoID { get; set; }

        [Required(ErrorMessage = "Scegli l'edizione")]
        public int? EdizioneCorsoID {  get; set; }

        public bool Assegnato { get; set; }

        public string? Matricola { get; set; }

        [Required(ErrorMessage = "*")]
        public string? cognome { get; set; }

        [Required(ErrorMessage = "*")]
        public string? nome { get; set; }

        public string? luogo_di_nascita { get; set; }

        [DisplayName("Data Nascita")]
        [CustomDate]
        [DataType(DataType.Date)]
        public DateTime? data_di_nascita { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DataAssegnato { get; set; }
                        
        [RegularExpression(@"^(?:[A-Z][AEIOU][AEIOUX]|[AEIOU]X{2}|[B-DF-HJ-NP-TV-Z]{2}[A-Z]){2}(?:[\dLMNP-V]{2}(?:[A-EHLMPR-T](?:[04LQ][1-9MNP-V]|[15MR][\dLMNP-V]|[26NS][0-8LMNP-U])|[DHPS][37PT][0L]|[ACELMRT][37PT][01LM]|[AC-EHLMPR-T][26NS][9V])|(?:[02468LNQSU][048LQU]|[13579MPRTV][26NS])B[26NS][9V])(?:[A-MZ][1-9MNP-V][\dLMNP-V]{2}|[A-M][0L](?:[1-9MNP-V][\dLMNP-V]|[0L][1-9MNP-V]))[A-Z]$", ErrorMessage = "CF Formato Errato ")]
        [Required(ErrorMessage = "*")]
        [StringLength(16),MinLength(16) ]
        public string cf { get; set; }


        public string? unita_operativa { get; set; }

        public string? Corso { get; set; }

        [Required(ErrorMessage = "Scegli l'unità")]
        public int? UnitaProduttiva { get; set; }

        public string? mansione { get; set; }

        public string? qualifica { get; set; }

        public string? UtenteAss {  get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime dataorains { get; set; }

        public int? IDutenteins { get; set; }

        public DateTime? dataoraultmod { get; set; }

        public int? IDutenteultmod { get; set; }

        public int eliminato { get; set; }

        public int? IDutenteultcanc { get; set; }

        public DateTime? dataoraultcanc { get; set; }

    }



    public class CustomDateDNAttribute : RangeAttribute
    {
        public CustomDateDNAttribute()
          : base(typeof(DateTime),
                  DateTime.Now.AddYears(-80).ToShortDateString(),
                  DateTime.Now.ToShortDateString())
        { }
    }


    [AttributeUsage(AttributeTargets.Property)]
    public class CustomDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime val = (DateTime)value;
            // This assumes inclusivity, i.e. exactly six years ago is okay
            if (val >= DateTime.Now.Date.AddYears(-110) && val <= DateTime.Now.Date)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Date must be within the last six years!");
            }
        }
    }




}

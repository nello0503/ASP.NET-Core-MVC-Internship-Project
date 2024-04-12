using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Formazione.Models
{
    public class Utente
    {
        [Key]
        public int utente_id { get; set; }

      
        public string cognome { get; set; }

        public string  nome { get; set; }

        [StringLength(200)]
        public string email { get; set; }

        [Required(ErrorMessage = "Username obbligatorio")]
        [StringLength(50)]
        public string username { get; set; }

        [Required(ErrorMessage = "Password obbligatoria")]
        [StringLength(100)]
        public string password { get; set; }

        [Required(ErrorMessage = "Ruolo obbligatorio")]
        public TipologiaUtente ruolo { get; set; }

   
        public string? CF { get; set; }

        public string? unitaprodAbilitate { get; set; }

        public short eliminato { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime dataorains { get; set; }


        public int? IDutenteins { get; set; }

        public DateTime? dataoraultmod { get; set; }

        public int? IDutenteultmod { get; set; }

        public int? IDutenteultcanc { get; set; }

        public DateTime? dataoraultcanc { get; set; }


        public virtual List<string> ListaUnitaAbilitate
        {
            get
            {
                List<String> list = new List<string>();
                if (unitaprodAbilitate!=null)
                    list = unitaprodAbilitate.Split('|').ToList();
                return list;
            }
        }

        public bool UnitaPresente(string codUP)
        {
            return ListaUnitaAbilitate.Contains(codUP);
        }

    }


    public enum TipologiaUtente
    {
        Admin = 99,
        Addetto_SPP = 2,
        Datore_Lavoro = 3
    }
}

using System.ComponentModel.DataAnnotations;

namespace Formazione.Models
{
    public class LogTable
    {

        [Key]
        public int logID { get; set; }

        public DateTime dataOra { get; set; }

        public int? utenteID { get; set; }

        public string? messaggio { get; set; }

        public tipoOperazione operazione { get; set; }


    }

    public enum tipoOperazione
    {
        login =   0,
        logout = 4,
        inserimento = 1,
        modifica = 2,
        eliminazione = 3

    }




    





}


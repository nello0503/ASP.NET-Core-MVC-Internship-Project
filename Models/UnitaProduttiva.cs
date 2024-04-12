namespace Formazione.Models
{
    public class UnitaProduttiva
    {
        public int UnitaProduttivaID { get; set; }

        public string? Codice { get; set; }

        public string? Descrizione { get; set; }

        public string? Email { get; set; }

        public DateTime dataorains { get; set; }

        public int? IDutenteins { get; set; }

        public DateTime? dataoraultmod { get; set; }

        public int? IDutenteultmod { get; set; }

        public int eliminato { get; set; }

        public int? IDutenteultcanc { get; set; }

        public DateTime? dataoraultcanc { get; set; }

    }

   

}

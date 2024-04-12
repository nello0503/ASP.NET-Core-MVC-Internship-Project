using Formazione.Models;
using Microsoft.EntityFrameworkCore;


namespace Formazione.Data

{
    public class ApplicationDbContext : DbContext 

    {

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<TipoCorso>  Corsi {  get; set; }

        public DbSet<ProgettoFormativo> Progetti {  get; set; }

        public DbSet<EdizioneCorso> Edizioni { get; set; }

        public DbSet<Modulo> Moduli { get; set; }

        public DbSet<DocenteTutor> DocenteTutor { get; set; }

        public DbSet<Formazione.Models.Utente>? Utente { get; set; }

        public DbSet<LogTable> LogTables { get; set; }

        public DbSet<UnitaProduttiva> UnitaProduttive { get; set; }

        public DbSet<Pianificazione> Pianificazioni { get; set; }


        public DbSet<Discente> Discente { get; set; }

    }
}

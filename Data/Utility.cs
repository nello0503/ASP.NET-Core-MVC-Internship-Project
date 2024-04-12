using Formazione.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

using Microsoft.AspNetCore.Mvc;

namespace Formazione.Data
{
    public class Utility
    {

        public static void SalvaLogDef(tipoOperazione id, string mess, ApplicationDbContext _context, ISession prmsession)
        {
            LogTable log = new LogTable
            {
                utenteID =prmsession.GetInt32("utente_id"),
                dataOra = System.DateTime.Now,
                operazione = id, // Ensure that this cast is valid
                messaggio = mess
            };

            // Assuming _context is a valid and properly configured DbContext
            _context.LogTables.Add(log);

            _context.SaveChangesAsync();
            // _context.SavedChanges.Add(log);
        }



    }



    public static class SessionExtensions
    {
        public static void SetObject(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

        public static Utente GetUtenteLoggato<utente>(this ISession session)
        {
            return session.GetObject<Utente>("UtenteLoggato");
        }

        public static List<String> GetListaUnitaAbilitate(this ISession session)
        {
            List<String> list = new List<string>();
            if (session.GetString("UnitaProdAbilitate") != null)
                list = session.GetString("UnitaProdAbilitate").Split('|').ToList();
            return list;
        }

        public static int GetIdUtenteLoggato(this ISession session)
        {
            return (int)session.GetInt32("utente_id");
        }

        public static string GetUserNameLoggato(this ISession session)
        {
            return session.GetString("UserName");
        }

        public static TipologiaUtente GetRuoloUtenteLoggato(this ISession session)
        {
            return (TipologiaUtente)session.GetInt32("Ruolo");
        }



        public static bool isUnitaAbilitate(this ISession session, string sCodiceUnita)
        {

            return session.GetListaUnitaAbilitate().Contains(sCodiceUnita);
        }
    }
}

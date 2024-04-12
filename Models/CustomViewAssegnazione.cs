


using Formazione.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Server.IIS;

namespace Formazione.Models
{
    public class CustomViewAssegnazione
    {

         public int? progettoID {  get; set; }

         public List<Pianificazione>? listaPianificazioni {  get; set; }

         public SelectList? unita {  get; set; }

         public SelectList? numeroEdiz { get; set; }

         public Discente? discente { get; set; }

         public List<Discente>? listaDiscenti { get; set; }





    }
}

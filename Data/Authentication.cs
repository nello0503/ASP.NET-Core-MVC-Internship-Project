using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Formazione.Models;
using Formazione.Data;

public class Authentication : ActionFilterAttribute
{

    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        if (filterContext.HttpContext.Session.GetString("UserName") == null)
        {
            filterContext.Result = new RedirectToRouteResult(
            new RouteValueDictionary {
                                { "Controller", "Utente" },
                                { "Action", "Login" }
                        });

        }

        if (filterContext.HttpContext.Session.GetRuoloUtenteLoggato() != TipologiaUtente.Admin &&  filterContext.HttpContext.Session.GetRuoloUtenteLoggato() != TipologiaUtente.Addetto_SPP)
        {
            filterContext.Result = new RedirectToRouteResult(
            new RouteValueDictionary {
                                { "Controller", "Home" },
                                { "Action", "Privacy" }
                        });

        }

        


    }
}
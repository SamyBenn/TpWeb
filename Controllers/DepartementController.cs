using Microsoft.AspNetCore.Mvc;
using System;
using TpWeb.Logics.Controleurs;

namespace TpWeb.Controllers
{
    public class DepartementController : Controller
    {
        //[Route("")]
        [Route("Departement")]
        [Route("Departement/Index")]
        [HttpGet]
        public IActionResult Index([FromQuery] string cegep)
        {
            try
            {
                ViewBag.ListeCegeps = CegepControleur.Instance.ObtenirListeCegep().ToArray();
                if (cegep is null)
                {
                    cegep = CegepControleur.Instance.ObtenirListeCegep()[0].Nom;
                }
                //string nomCegep = "Cégep de Rivière-du-Loup";
                ViewBag.Cegep = CegepControleur.Instance.ObtenirCegep(cegep);
                ViewBag.ListeDepartements = CegepControleur.Instance.ObtenirListeDepartement(cegep).ToArray();
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
            }
            return View();
        }
    }
}

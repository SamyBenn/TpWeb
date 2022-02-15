using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using TpWeb.Logics.Controleurs;
using TpWeb.Models;

namespace TpWeb.Controllers
{
    public class CoursController : Controller
    {
        [Route("Cours")]
        [Route("Cours/Index")]
        [HttpGet]
        public IActionResult Index([FromQuery] string cegep, string departement)
        {
            try
            {
                ViewBag.ListeCegeps = CegepControleur.Instance.ObtenirListeCegep().ToArray();
                if (cegep is null)
                {
                    cegep = CegepControleur.Instance.ObtenirListeCegep()[0].Nom;
                }
                ViewBag.nomCegep = cegep;
                ViewBag.ListeDepartements = CegepControleur.Instance.ObtenirListeDepartement(cegep).ToArray();
                
                if (departement is null)
                {
                    departement = CegepControleur.Instance.ObtenirListeDepartement(cegep)[0].Nom;
                }
                ViewBag.nomDepartement = departement;
                ViewBag.ListeCours = CegepControleur.Instance.ObtenirListeCours(cegep, departement).ToArray();

            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
            }
            return View();
        }

        [Route("Cours/AjouterCours")]
        [HttpPost]
        public IActionResult AjouterCours([FromForm]string nomCegep, [FromForm]string nomDepartement, [FromForm]CoursDTO cours)
        {
            try
            {
                CegepControleur.Instance.AjouterCours(nomCegep, nomDepartement, cours);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
            }
            return RedirectToAction("Index", "Cours", new { cegep = nomCegep, departement = nomDepartement });
        }
    }
}

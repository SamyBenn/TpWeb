using Microsoft.AspNetCore.Mvc;
using System;
using TpWeb.Logics.Controleurs;
using TpWeb.Models;

namespace TpWeb.Controllers
{
    public class DepartementController : Controller
    {
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
                ViewBag.Cegep = CegepControleur.Instance.ObtenirCegep(cegep);
                ViewBag.ListeDepartements = CegepControleur.Instance.ObtenirListeDepartement(cegep).ToArray();
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
            }
            return View();
        }

        [Route("Departement/AjouterDepartement")]
        [HttpPost]
        public IActionResult AjouterDepartement([FromForm]string nomCegep, [FromForm]DepartementDTO departement)
        {
            try
            {
                CegepControleur.Instance.AjouterDepartement(nomCegep, departement);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
            }
            return RedirectToAction("Index", "Departement", new {cegep=nomCegep});
        }
    }
}

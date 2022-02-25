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
        public IActionResult Index([FromQuery] string nomCegep)
        {
            try
            {
                ViewBag.ListeCegeps = CegepControleur.Instance.ObtenirListeCegep().ToArray();
                if (nomCegep is null)
                {
                    nomCegep = CegepControleur.Instance.ObtenirListeCegep()[0].Nom;
                }
                ViewBag.nomCegep = nomCegep;
                ViewBag.ListeDepartements = CegepControleur.Instance.ObtenirListeDepartement(nomCegep).ToArray();
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
            return RedirectToAction("Index", "Departement", new {nomCegep=nomCegep});
        }

        [Route("Departement/FormModifier")]
        [HttpGet]
        public IActionResult FormModifier([FromQuery] string nomCegep, string nomDepartement)
        {
            DepartementDTO departement = new DepartementDTO(); ;
            try
            {
                departement = CegepControleur.Instance.ObtenirDepartement(nomCegep, nomDepartement);
                ViewBag.nomCegep = nomCegep;
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
            }
            return View(departement);
        }

        [Route("Departement/ModifierDepartement")]
        [HttpPost]
        public IActionResult ModifierDepartement([FromForm] string nomCegep, DepartementDTO departement)
        {
            try
            {
                CegepControleur.Instance.ModifierDepartement(nomCegep, departement);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
            }
            return RedirectToAction("Index", "Departement", new { nomCegep = nomCegep });
        }
    }
}

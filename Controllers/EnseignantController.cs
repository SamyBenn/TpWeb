using Microsoft.AspNetCore.Mvc;
using System;
using TpWeb.Logics.Controleurs;
using TpWeb.Models;

namespace TpWeb.Controllers
{
    public class EnseignantController : Controller
    {
        [Route("Enseignant")]
        [Route("Enseignant/Index")]
        [HttpGet]
        public IActionResult Index([FromQuery] string nomCegep, string nomDepartement)
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

                if (nomDepartement is null)
                {
                    nomDepartement = CegepControleur.Instance.ObtenirListeDepartement(nomCegep)[0].Nom;
                }
                ViewBag.nomDepartement = nomDepartement;
                ViewBag.ListeEnseignant = CegepControleur.Instance.ObtenirListeEnseignant(nomCegep, nomDepartement).ToArray();
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
            }
            return View();
        }

        [Route("Enseignant/AjouterEnseignant")]
        [HttpPost]
        public ActionResult AjouterEnseignant([FromForm]string nomCegep, [FromForm]string nomDepartement, [FromForm]EnseignantDTO enseignant)
        {
            try
            {
                CegepControleur.Instance.AjouterEnseignant(nomCegep, nomDepartement, enseignant);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
            }
            return RedirectToAction("Index", "Enseignant", new { nomCegep = nomCegep, nomDepartement = nomDepartement });
        }


        [Route("Enseignant/FormModifier")]
        [HttpGet]
        public IActionResult FormModifier([FromQuery] string nomCegep, string nomDepartement, int noEnseignant)
        {
            EnseignantDTO enseignant = new EnseignantDTO(); ;
            try
            {
                enseignant = CegepControleur.Instance.ObtenirEnseignant(nomCegep, nomDepartement, noEnseignant);
                ViewBag.nomCegep = nomCegep;
                ViewBag.nomDepartement = nomDepartement;
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
            }
            return View(enseignant);
        }


        [Route("Enseignant/ModifierEnseignant")]
        [HttpPost]
        public IActionResult ModifierEnseignant([FromForm] string nomCegep, string nomDepartement, EnseignantDTO enseignant)
        {
            try
            {
                CegepControleur.Instance.ModifierEnseignant(nomCegep, nomDepartement, enseignant);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
            }
            return RedirectToAction("Index", "Enseignant", new { nomCegep = nomCegep, nomDepartement=nomDepartement });
        }
    }
}
using System;
using Microsoft.AspNetCore.Mvc;
using TpWeb.Logics.Controleurs;
using TpWeb.Models;

/// <summary>
/// Namespace pour les controleurs de vue.
/// </summary>
namespace TpWeb.Controllers
{
    /// <summary>
    /// Classe représentant le controleur de vue des Cégeps.
    /// </summary>
    public class CegepController : Controller
    {
        /// <summary>
        /// Méthode de service appelé lors de l'action Index.
        /// Rôles de l'action : 
        ///  -Afficher la liste des Cégeps.
        /// </summary>
        /// <returns>ActionResult suite aux traitements des données.</returns>
        [Route("")]
        [Route("Cegep")]
        [Route("Cegep/Index")]
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                //Préparation des données pour la vue...
                ViewBag.ListeCegeps = CegepControleur.Instance.ObtenirListeCegep().ToArray();
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
            }

            //Retour de la vue...
            return View();
        }

        /// <summary>
        /// Méthode de service appelé lors de l'action Ajouter.
        /// Rôles de l'action : 
        ///  -Ajouter un Cégep a la BD.
        /// </summary>
        /// <param name="cegep">le cegep a ajouter</param>
        /// <returns>ActionResult suite aux traitements des données.</returns>
        [Route("Cegep/AjouterCegep")]
        [HttpPost]
        public IActionResult AjouterCegep([FromForm] CegepDTO cegep)
        {
            try
            {
               CegepControleur.Instance.AjouterCegep(cegep);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
            }
            return RedirectToAction("Index", "Cegep");
        }


        /// <summary>
        /// Méthode de service appelé lors de l'action modifier.
        /// Rôles de l'action : 
        ///  -Lancer le formulaire Modifier
        /// </summary>
        /// <param name="nomCegep">le nom du Cegep a modifier</param>
        /// <returns></returns>
        [Route("Cegep/FormModifier")]
        [HttpGet]
        public IActionResult FormModifier([FromQuery] string nomCegep)
        {
            CegepDTO cegep = new CegepDTO();
            try
            {
                cegep = CegepControleur.Instance.ObtenirCegep(nomCegep);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
            }
            return View(cegep);
        }


        /// <summary>
        /// Méthode de service appelé lors de l'action modifier.
        /// Rôles de l'action : 
        ///  -Modifier un Cegep
        /// </summary>
        /// <param name="cegep">le Cegep a modifier</param>
        /// <returns></returns>
        [Route("Cegep/ModifierCegep")]
        [HttpPost]
        public  IActionResult ModifierCegep([FromForm] CegepDTO cegep)
        {
            try
            {
                CegepControleur.Instance.ModifierCegep(cegep);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
            }
            return RedirectToAction("Index");
        }

        [Route("Cegep/SupprimerCegep")]
        [HttpPost]
        public IActionResult SupprimerCegep([FromForm] string nomCegep)
        {
            try
            {
                CegepControleur.Instance.SupprimerCegep(nomCegep);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
            }
            return RedirectToAction("Index", "Cegep");
        }

        [Route("Cegep/ViderListeCegep")]
        [HttpPost]
        public IActionResult ViderListeCegep()
        {
            try
            {
                CegepControleur.Instance.ViderListeCegep();
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
            }
            return RedirectToAction("Index", "Cegep");
        }
    }
}
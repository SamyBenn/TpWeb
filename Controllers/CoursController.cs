using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using TpWeb.Logics.Controleurs;
using TpWeb.Models;

namespace TpWeb.Controllers
{
    public class CoursController : Controller
    {
        /// <summary>
        /// Méthode de service appelé lors de l'action Index.
        /// Rôles de l'action : 
        ///  -Afficher la liste des Cours.
        /// </summary>
        /// <param name="nomCegep">le nom du Cegep selectionne</param>
        /// <param name="nomDepartement">le nom du Departement selectionne</param>
        /// <returns></returns>
        [Route("Cours")]
        [Route("Cours/Index")]
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
                ViewBag.ListeCours = CegepControleur.Instance.ObtenirListeCours(nomCegep, nomDepartement).ToArray();

            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
            }
            return View();
        }


        /// <summary>
        /// Méthode de service appelé lors de l'action Ajouter.
        /// Rôles de l'action : 
        ///  -Ajouter un Cours a la BD.
        /// </summary>
        /// <param name="nomCegep">le nom du cegep ou ajouter</param>
        /// <param name="nomDepartement">le nom du Departement ou ajouter</param>
        /// <param name="cours">le cours a ajouter</param>
        /// <returns></returns>
        [Route("Cours/AjouterCours")]
        [HttpPost]
        public IActionResult AjouterCours([FromForm]string nomCegep, string nomDepartement, CoursDTO cours)
        {
            try
            {
                CegepControleur.Instance.AjouterCours(nomCegep, nomDepartement, cours);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
            }
            return RedirectToAction("Index", "Cours", new { nomCegep = nomCegep, nomDepartement = nomDepartement });
        }


        /// <summary>
        /// Méthode de service appelé lors de l'action modifier.
        /// Rôles de l'action : 
        ///  -Lancer le formulaire Modifier
        /// </summary>
        /// <param name="nomCegep">le nom du cegep ou modifier</param>
        /// <param name="nomDepartement">le nom du departement ou modifier</param>
        /// <param name="nomCours">le nom du cours a ajouter</param>
        /// <returns></returns>
        [Route("Cours/FormModifier")]
        [HttpGet]
        public IActionResult FormModifier([FromQuery] string nomCegep, string nomDepartement, string nomCours)
        {
            CoursDTO cours = new CoursDTO(); ;
            try
            {
                cours = CegepControleur.Instance.ObtenirCours(nomCegep, nomDepartement, nomCours);
                ViewBag.nomCegep = nomCegep;
                ViewBag.nomDepartement = nomDepartement;
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
            }
            return View(cours);
        }


        /// <summary>
        /// Méthode de service appelé lors de l'action modifier.
        /// Rôles de l'action : 
        ///  -Modifier un Cours
        /// </summary>
        /// <param name="nomCegep">le nom du cegep ou modifier</param>
        /// <param name="nomDepartement">le nom du departement ou modifier</param>
        /// <param name="cours">le cours a ajouter</param>
        /// <returns></returns>
        [Route("Cours/ModifierCours")]
        [HttpPost]
        public IActionResult ModifierCours([FromForm] string nomCegep, string nomDepartement, CoursDTO cours)
        {
            try
            {
                CegepControleur.Instance.ModifierCours(nomCegep, nomDepartement, cours);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
            }
            return RedirectToAction("Index", "Cours", new { nomCegep = nomCegep, nomDepartement = nomDepartement });
        }
    }
}

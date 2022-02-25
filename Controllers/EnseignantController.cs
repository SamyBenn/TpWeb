using Microsoft.AspNetCore.Mvc;
using System;
using TpWeb.Logics.Controleurs;
using TpWeb.Models;

namespace TpWeb.Controllers
{
    public class EnseignantController : Controller
    {
        /// <summary>
        /// Méthode de service appelé lors de l'action Index.
        /// Rôles de l'action : 
        ///  -Afficher la liste des Enseignant.
        /// </summary>
        /// <param name="nomCegep">le nom du Cegep selectionne</param>
        /// <param name="nomDepartement">le nom du Departement selectionne</param>
        /// <returns></returns>
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


        /// <summary>
        /// Méthode de service appelé lors de l'action Ajouter.
        /// Rôles de l'action : 
        ///  -Ajouter un Enseignant a la BD.
        /// </summary>
        /// <param name="nomCegep">le nom du cegep ou ajouter</param>
        /// <param name="nomDepartement">le nom du Departement ou ajouter</param>
        /// <param name="enseignant">l'enseignant a ajouter</param>
        /// <returns></returns>
        [Route("Enseignant/AjouterEnseignant")]
        [HttpPost]
        public ActionResult AjouterEnseignant([FromForm]string nomCegep, string nomDepartement, EnseignantDTO enseignant)
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


        /// <summary>
        /// Méthode de service appelé lors de l'action modifier.
        /// Rôles de l'action : 
        ///  -Lancer le formulaire Modifier
        /// </summary>
        /// <param name="nomCegep">le nom du cegep ou modifier</param>
        /// <param name="nomDepartement">le nom du departement ou modifier</param>
        /// <param name="noEnseignant">le nom de l'enseignant a modifier</param>
        /// <returns></returns>
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


        /// <summary>
        /// Méthode de service appelé lors de l'action modifier.
        /// Rôles de l'action : 
        ///  -Modifier un Enseignant
        /// </summary>
        /// <param name="nomCegep">le nom du cegep ou modifier</param>
        /// <param name="nomDepartement">le nom du departement ou modifier</param>
        /// <param name="enseignant">l'enseignant a modifier</param>
        /// <returns></returns>
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
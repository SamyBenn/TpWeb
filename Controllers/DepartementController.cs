using Microsoft.AspNetCore.Mvc;
using System;
using TpWeb.Logics.Controleurs;
using TpWeb.Models;

namespace TpWeb.Controllers
{
    public class DepartementController : Controller
    {
        /// <summary>
        /// Méthode de service appelé lors de l'action Index.
        /// Rôles de l'action : 
        ///  -Afficher la liste des Departement.
        /// </summary>
        /// <param name="nomCegep">le nom du Cegep selectionne</param>
        /// <returns></returns>
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


        /// <summary>
        /// Méthode de service appelé lors de l'action Ajouter.
        /// Rôles de l'action : 
        ///  -Ajouter un Departement a la BD.
        /// </summary>
        /// <param name="nomCegep">le nom du cegep ou ajouter</param>
        /// <param name="departement">le nom du departement a ajouter</param>
        /// <returns></returns>
        [Route("Departement/AjouterDepartement")]
        [HttpPost]
        public IActionResult AjouterDepartement([FromForm]string nomCegep, DepartementDTO departement)
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


        /// <summary>
        /// Méthode de service appelé lors de l'action modifier.
        /// Rôles de l'action : 
        ///  -Lancer le formulaire Modifier
        /// </summary>
        /// <param name="nomCegep">le nom du cegep ou modifier</param>
        /// <param name="nomDepartement">le nom Departement a modifier</param>
        /// <returns></returns>
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



        /// <summary>
        /// Méthode de service appelé lors de l'action modifier.
        /// Rôles de l'action : 
        ///  -Modifier un Departement
        /// </summary>
        /// <param name="nomCegep">le nom du cegep ou ajouter</param>
        /// <param name="departement">le Departement a modifier</param>
        /// <returns></returns>
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

using System;
using System.Collections.Generic;
using System.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TpWeb.Logics.Controleurs;
using TpWeb.Models;
using TpWeb.Utils;

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
        public async Task<IActionResult> Index()
        {
            try
            {
                //Préparation des données pour la vue...
                //ViewBag.ListeCegeps = CegepControleur.Instance.ObtenirListeCegep().ToArray();
                JsonValue jsonResponse = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Cegep/obtenirListeCegep");
                ViewBag.ListeCegeps = JsonConvert.DeserializeObject<List<CegepDTO>>(jsonResponse.ToString()).ToArray();
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
        public async Task<IActionResult> AjouterCegep([FromForm] CegepDTO cegep)
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Cegep/AjouterCegep", cegep);
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
        public async Task<IActionResult> FormModifier([FromQuery] string nomCegep)
        {
            try
            {
                if (TempData["MessageErreur"] != null)
                    ViewBag.MessageErreur = TempData["MessageErreur"];
                JsonValue jsonResponse = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Cegep/ObtenirCegep?nomCegep=" + nomCegep);
                CegepDTO cegep = JsonConvert.DeserializeObject<CegepDTO>(jsonResponse.ToString());
                return View(cegep);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
            }
            return RedirectToAction("Index");
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
        public async Task<IActionResult> ModifierCegep([FromForm] CegepDTO cegep)
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Cegep/ModifierCegep", cegep);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
            }
            return RedirectToAction("Index");
        }

        [Route("Cegep/SupprimerCegep")]
        [HttpPost]
        public async Task<IActionResult> SupprimerCegep([FromForm] string nomCegep)
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Cegep/SupprimerCegep?nomCegep=" + nomCegep, null);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
            }
            return RedirectToAction("Index", "Cegep");
        }

        [Route("Cegep/ViderListeCegep")]
        [HttpPost]
        public async Task<IActionResult> ViderListeCegep()
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Cegep/ViderListeCegep", null);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
            }
            return RedirectToAction("Index", "Cegep");
        }
    }
}
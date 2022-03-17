using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Json;
using System.Threading.Tasks;
using TpWeb.Logics.Controleurs;
using TpWeb.Models;
using TpWeb.Utils;

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
        public async Task<IActionResult> Index([FromQuery] string nomCegep, string nomDepartement)
        {
            try
            {
                JsonValue jsonListeCegeps = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Cegep/obtenirListeCegep");
                List<CegepDTO> listeCegeps = new(JsonConvert.DeserializeObject<List<CegepDTO>>(jsonListeCegeps.ToString()).ToArray());
                ViewBag.ListeCegeps = listeCegeps;
                if (nomCegep is null)
                {
                    nomCegep = listeCegeps[0].Nom;
                }
                ViewBag.nomCegep = nomCegep;

                JsonValue jsonListeDepartements = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Departement/obtenirListeDepartement?nomCegep=" + nomCegep);
                List<DepartementDTO> listeDepartements = new(JsonConvert.DeserializeObject<List<DepartementDTO>>(jsonListeDepartements.ToString()).ToArray());
                ViewBag.ListeDepartements = listeDepartements;
                if (nomDepartement is null)
                {
                    nomDepartement = listeDepartements[0].Nom;
                }
                ViewBag.nomDepartement = nomDepartement;
                JsonValue jsonListeCours = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Cours/obtenirListeCours?nomCegep=" + nomCegep + "&nomDepartement=" + nomDepartement);
                ViewBag.ListeCours = JsonConvert.DeserializeObject<List<CoursDTO>>(jsonListeCours.ToString()).ToArray();

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
        public async Task<IActionResult> AjouterCours([FromForm]string nomCegep, string nomDepartement, CoursDTO cours)
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Cours/ajouterCours?nomCegep=" + nomCegep + "&nomDepartement=" + nomDepartement, cours);
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
        public async Task<IActionResult> FormModifier([FromQuery] string nomCegep, string nomDepartement, string nomCours)
        {
            CoursDTO cours = new CoursDTO(); ;
            try
            {
                JsonValue jsonCours = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Cours/obtenirCours?nomCegep=" + nomCegep + "&nomDepartement=" + nomDepartement + "&nomCours=" + nomCours);
                cours = JsonConvert.DeserializeObject<CoursDTO>(jsonCours.ToString());
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
        public async Task<IActionResult> ModifierCours([FromForm] string nomCegep, string nomDepartement, CoursDTO cours)
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Cours/modifierCours?nomCegep=" + nomCegep + "&nomDepartement=" + nomDepartement, cours);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
            }
            return RedirectToAction("Index", "Cours", new { nomCegep = nomCegep, nomDepartement = nomDepartement });
        }


        [Route("Cours/SupprimerCours")]
        [HttpPost]
        public async Task<IActionResult> SupprimerCours([FromForm] string nomCegep, string nomDepartement, string nomCours)
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Cours/supprimerCours?nomCegep=" + nomCegep + "&nomDepartement=" + nomDepartement + "&nomCours=" + nomCours, null);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
            }
            return RedirectToAction("Index", "Cours", new { nomCegep = nomCegep, nomDepartement = nomDepartement });
        }

        [Route("Cours/ViderListeCours")]
        [HttpPost]
        public async Task<IActionResult> ViderListeCours([FromForm] string nomCegep, string nomDepartement)
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Cours/viderListeCours?nomCegep=" + nomCegep + "&nomDepartement=" + nomDepartement, null);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
            }
            return RedirectToAction("Index", "Cours", new { nomCegep = nomCegep, nomDepartement = nomDepartement });
        }
    }
}

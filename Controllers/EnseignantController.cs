using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Index([FromQuery] string nomCegep, string nomDepartement)
        {
            try
            {
                JsonValue jsonListeCegeps = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Cegep/obtenirListeCegep");
                List <CegepDTO> listeCegeps = new(JsonConvert.DeserializeObject<List<CegepDTO>>(jsonListeCegeps.ToString()).ToArray());
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
                JsonValue jsonListeEnseignant = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Enseignant/obtenirListeEnseignant?nomCegep=" + nomCegep + "&nomDepartement=" + nomDepartement);
                ViewBag.ListeEnseignant = JsonConvert.DeserializeObject<List<EnseignantDTO>>(jsonListeEnseignant.ToString()).ToArray();
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
        public async Task<IActionResult> AjouterEnseignant([FromForm]string nomCegep, string nomDepartement, EnseignantDTO enseignant)
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Enseignant/ajouterEnseignant?nomCegep=" + nomCegep + "&nomDepartement=" + nomDepartement, enseignant);
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
        public async Task<IActionResult> FormModifier([FromQuery] string nomCegep, string nomDepartement, int noEnseignant)
        {
            EnseignantDTO enseignant = new EnseignantDTO(); ;
            try
            {
                JsonValue jsonEnseignant = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Enseignant/obtenirEnseignant?nomCegep=" + nomCegep + "&nomDepartement=" + nomDepartement + "&noEnseignant=" + noEnseignant);
                enseignant = JsonConvert.DeserializeObject<EnseignantDTO>(jsonEnseignant.ToString());
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
        public async Task<IActionResult> ModifierEnseignant([FromForm] string nomCegep, string nomDepartement, EnseignantDTO enseignant)
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Enseignant/modifierEnseignant?nomCegep=" + nomCegep + "&nomDepartement=" + nomDepartement, enseignant);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
            }
            return RedirectToAction("Index", "Enseignant", new { nomCegep = nomCegep, nomDepartement=nomDepartement });
        }

        [Route("Enseignant/SupprimerEnseignant")]
        [HttpPost]
        public async Task<IActionResult> SupprimerEnseignant([FromForm] string nomCegep, string nomDepartement, int noEnseignant)
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Enseignant/supprimerEnseignant?nomCegep=" + nomCegep + "&nomDepartement=" + nomDepartement + "&noEnseignant=" + noEnseignant, null);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
            }
            return RedirectToAction("Index", "Enseignant", new { nomCegep = nomCegep, nomDepartement = nomDepartement });
        }

        [Route("Enseignant/ViderListeEnseignant")]
        [HttpPost]
        public async Task<IActionResult> ViderListeEnseignant([FromForm] string nomCegep, string nomDepartement)
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Enseignant/viderListeEnseignant?nomCegep=" + nomCegep + "&nomDepartement=" + nomDepartement, null);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
            }
            return RedirectToAction("Index", "Enseignant", new { nomCegep = nomCegep, nomDepartement = nomDepartement });
        }
    }
}
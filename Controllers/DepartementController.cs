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
        public async Task<IActionResult> Index([FromQuery] string nomCegep)
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
                JsonValue jsonListeDep = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Departement/obtenirListeDepartement?nomCegep="+nomCegep);
                ViewBag.ListeDepartements = JsonConvert.DeserializeObject<List<DepartementDTO>>(jsonListeDep.ToString()).ToArray();
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
        public async Task<IActionResult> AjouterDepartement([FromForm]string nomCegep, DepartementDTO departement)
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Departement/AjouterDepartement?nomCegep=" + nomCegep, departement);
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
        public async Task<IActionResult> FormModifier([FromQuery] string nomCegep, string nomDepartement)
        {
            try
            {
                //departement = CegepControleur.Instance.ObtenirDepartement(nomCegep, nomDepartement);
                ViewBag.nomCegep = nomCegep;
                JsonValue jsonResponse = await WebAPI.Instance.ExecuteGetAsync("http://" + Program.HOST + ":" + Program.PORT + "/Departement/obtenirDepartement?nomcegep=" + nomCegep + "&nomDepartement=" + nomDepartement);
                DepartementDTO departement = JsonConvert.DeserializeObject<DepartementDTO>(jsonResponse.ToString());
                return View(departement);
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
        ///  -Modifier un Departement
        /// </summary>
        /// <param name="nomCegep">le nom du cegep ou ajouter</param>
        /// <param name="departement">le Departement a modifier</param>
        /// <returns></returns>
        [Route("Departement/ModifierDepartement")]
        [HttpPost]
        public async Task<IActionResult> ModifierDepartement([FromForm] string nomCegep, DepartementDTO departement)
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Departement/modifierDepartement?nomCegep=" + nomCegep, departement);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
            }
            return RedirectToAction("Index", "Departement", new { nomCegep = nomCegep });
        }

        [Route("Departement/SupprimerDepartement")]
        [HttpPost]
        public async Task<IActionResult> SupprimerDepartement([FromForm] string nomCegep, string nomDepartement)
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Departement/supprimerDepartement?nomCegep=" + nomCegep + "&nomDepartement=" + nomDepartement, null);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
            }
            return RedirectToAction("Index", "Departement", new { nomCegep = nomCegep });
        }

        [Route("Departement/ViderListeDepartement")]
        [HttpPost]
        public async Task<IActionResult> ViderListeDepartement([FromForm] string nomCegep)
        {
            try
            {
                await WebAPI.Instance.PostAsync("http://" + Program.HOST + ":" + Program.PORT + "/Departement/viderListeDepartement?nomCegep=" + nomCegep, null);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
            }
            return RedirectToAction("Index", "Departement", new { nomCegep = nomCegep });
        }
    }
}

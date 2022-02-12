using Microsoft.AspNetCore.Mvc;
using System;
using TpWeb.Logics.Controleurs;

namespace TpWeb.Controllers
{
    public class EnseignantController : Controller
    {
        [Route("Enseignant")]
        [Route("Enseignant/Index")]
        [HttpGet]
        public IActionResult Index([FromQuery] string cegep, string departement)
        {
            try
            {
                ViewBag.ListeCegeps = CegepControleur.Instance.ObtenirListeCegep().ToArray();
                if (cegep is null)
                {
                    cegep = CegepControleur.Instance.ObtenirListeCegep()[0].Nom;
                }
                ViewBag.Cegep = CegepControleur.Instance.ObtenirCegep(cegep);
                ViewBag.ListeDepartements = CegepControleur.Instance.ObtenirListeDepartement(cegep).ToArray();
                if (departement is null)
                {
                    departement = CegepControleur.Instance.ObtenirListeDepartement(cegep)[0].Nom;
                }
                ViewBag.Departement = CegepControleur.Instance.ObtenirDepartement(cegep, departement);
                ViewBag.ListeEnseignant = CegepControleur.Instance.ObtenirListeEnseignant(cegep, departement).ToArray();
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
            }
            return View();
        }
    }
}
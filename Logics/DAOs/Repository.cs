using System.Data.SqlClient;

/// <summary>
/// Namespace pour les classe de type DAO.
/// </summary>
namespace TpWeb.Logics.DAOs
{
    /// <summary>
    /// Classe représentant un repository.
    /// </summary>
    public class Repository
    {
        #region AttributsPropriete

        /// <summary>
        /// La connexion.
        /// </summary>
        protected SqlConnection connexion;

        #endregion AttributsPropriete

        #region Constructeurs
        /// <summary>
        /// Constructeur de la classe.
        /// </summary>
        protected Repository()
        {
            connexion = new SqlConnection("Server = 172.16.201.252; Database = Cegep2; User Id=Samy;Password=Patate123456;");
        }

        #endregion Constructeurs

        #region MethodesService

        /// <summary>
        /// Méthode permettant d'ouvrir la connexion.
        /// </summary>
        protected void OuvrirConnexion()
        {
            connexion.Open();
        }

        /// <summary>
        /// Méthode permettant de fermer la connexion.
        /// </summary>
        protected void FermerConnexion()
        {
            connexion.Close();
        }

        #endregion MethodesService
    }
}

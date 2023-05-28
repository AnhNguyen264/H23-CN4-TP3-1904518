using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaladeurMultiFormats

{
    /// <summary>
    /// Fournit les propriétés et méthodes pour simuler le fonctionnement de Chanson
    /// </summary>
    public interface IChanson
    {
        #region Proprietes,    
        /// <summary>
        /// Obtient l’année de création de la chanson 
        /// </summary> 
        int Annee { get; }
        /// <summary>
        /// Obtient le nom de l’artiste ou du groupe ayant créé la chanson 
        /// </summary> 
        string Artiste { get; }
        /// <summary>
        /// Obtient le format audio de la chanson par exemple AAC, MP3 ou WMA 
        /// </summary> 
        string Format { get; }
        /// <summary>
        /// Obtient le nom de fichier de la chanson
        /// </summary> 
        string NomFichier { get; }
        /// <summary>
        /// Cette propriété calculée va obtenir les paroles de la chanson à partir d’un fichier texte 
        /// </summary> 
        string Paroles { get; }
        /// <summary>
        /// Obtient le titre de la chanson
        /// </summary> 
        string Titre { get; }
        #endregion

        #region Methodes
        /// <summary>
        /// Écrit les paroles passées en paramètre dans le fichier de la chanson. 
        /// </summary> 
        void Ecrire(string pParoles);

        /// <summary>
        /// Obtient le titre de la chanson
        /// </summary> 
        void EcrireEntetes(StreamWriter pobjFichier);

        /// <summary>
        /// Écrit uniquement les paroles de la chanson
        /// </summary> 
        void EcrireParoles(StreamWriter pobjFichier, string pParoles);

        /// <summary>
        /// Lecture de l’en-tête du fichier soit uniquement la première ligne
        /// </summary> 
        void LireEntete();

        /// <summary>
        /// Obtient les paroles à partir d'un fichier binaire déjà ouvert.
        /// </summary> 
        string LireParoles(StreamReader pobjFicher);

        /// <summary>
        /// Lit une ligne à partir du fichier passé en paramètre
        /// </summary> 
        void SauterEntete(StreamReader pobjFichier);
        #endregion

    }
}

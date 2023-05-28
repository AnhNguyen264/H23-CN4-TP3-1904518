using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaladeurMultiFormats
{
    /// ===============================================================================================
    /// <summary>
    /// Représente la chanson,   Les fichiers ont les extensions suivantes soit : .aac, .mp3 où .wma.  
    /// </summary>
    
    public abstract class Chanson : IChanson
    {
        #region Champs
        /// ===============================================================================================
        /// <summary>
        ///Représente l'annee de la chanson.
        /// </summary>
        protected int m_annee;

        /// ===============================================================================================
        /// <summary>
        ///Représente l'artiste de la chanson.
        /// </summary>
        protected string m_artiste;

        /// ===============================================================================================
        /// <summary>
        ///Représente le nom du fichier de la chanson.
        /// </summary>
        protected string m_nomFichier;

        /// ===============================================================================================
        /// <summary>
        ///Représente le titre de la chanson.
        /// </summary>
        protected string m_titre;
        #endregion

        #region Proprietes
        /// <summary>
        /// Obtient l’année de création de la chanson 
        /// </summary> 
        public int Annee 
            {
            get { return m_annee; } 
            }
        /// <summary>
        /// Obtient le nom de l’artiste ou du groupe ayant créé la chanson 
        /// </summary> 
        public string Artiste
        {
            get { return m_artiste; }
        }
        /// <summary>
        /// Obtient le format audio de la chanson par exemple AAC, MP3 ou WMA 
        /// </summary> 
        public abstract string Format
        {
            get;
        }
        /// <summary>
        /// Obtient le nom de fichier de la chanson
        /// </summary> 
        public string NomFichier
        {
            get { return m_nomFichier;}
        }
        /// <summary>
        /// Cette propriété calculée va obtenir les paroles de la chanson à partir d’un fichier texte 
        /// </summary> 
        public string Paroles
        {
            get
            {
                string lesParoles = "";
                if(File.Exists(NomFichier))
                {
                    StreamReader objFichier = new StreamReader(NomFichier);
                    SauterEntete(objFichier);
                    lesParoles = LireParoles(objFichier);
                    objFichier.Close();
                }
                return lesParoles;
            }
        }
        /// <summary>
        /// Obtient le titre de la chanson
        /// </summary> 
        public string Titre
        {
            get { return m_titre; }
        }

        #endregion

        #region Constructeur
        ///==============================================================
        /// <summary>
        ///  Initialise une nouvelle instance de la classe Chanson 
        ///  la donnee de nom du fichier
        /// </summary>
        /// --------------------------------------------------------------
        
        public Chanson (string pNomFichier)
        {
            m_nomFichier = pNomFichier;
            LireEntete();

        }

        ///==============================================================
        /// <summary>
        ///  Initialise une nouvelle instance de la classe Chanson 
        ///  les donnees sont: nom du fichier (repertoire + titre + format), artiste, tirte, annee
        /// </summary>
        /// --------------------------------------------------------------
        public Chanson (string pRepertoire, string pArtiste, string pTitre, int pAnnée)
        {
            m_nomFichier = pRepertoire + "\\" + pTitre + "." + Format.ToLower();
            m_artiste = pArtiste;
            m_titre = pTitre;
            m_annee = pAnnée;

        }
        #endregion

        #region MEthodes

        ///======================================================================
        /// <summary>
        ///  Écrit les paroles passées en paramètre dans le fichier de la chanson. Elle doit d’abord écrire l’en-tête ensuite écrire les paroles.
        /// </summary>
        /// ---------------------------------------------------------------------
        public void Ecrire(string pParoles)
        {
            StreamWriter objFichier = new StreamWriter(NomFichier);
            EcrireEntetes(objFichier);
            EcrireParoles(objFichier, pParoles);
            objFichier .Close();

        }

        ///======================================================================
        /// <summary>
        ///  Une méthode abstraite qui permet d’écrire l’entête de la chanson
        /// </summary>
        /// ---------------------------------------------------------------------
        public abstract void EcrireEntetes(StreamWriter pobjFichier);

        ///======================================================================
        /// <summary>
        ///  Une méthode abstraite qui permet d’écrire les paroles de la chanson
        /// ---------------------------------------------------------------------
        public abstract void EcrireParoles(StreamWriter pobjFichier, string pParoles);

        ///======================================================================
        /// <summary>
        ///  Méthode abstraite à redéfinir dans les classe dérivées
        /// </summary>
        /// ---------------------------------------------------------------------
        public abstract void LireEntete();

        ///======================================================================
        /// <summary>
        ///  Méthode abstraite à redéfinir dans les classe dérivées
        /// </summary>
        /// ---------------------------------------------------------------------
        public abstract string LireParoles(StreamReader pobjFicher);

        ///======================================================================
        /// <summary>
        ///  Lit une ligne à partir du fichier passé en paramètre.
        /// </summary>
        /// ---------------------------------------------------------------------
        public void SauterEntete(StreamReader pobjFichier)
        {
            pobjFichier.ReadLine();
        }
      
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaladeurMultiFormats
{
    public class ChansonWMA:Chanson

    { /// ===============================================================================================
      /// <summary>
      ///represent codage
      /// </summary>
        private int m_codage;
        /// ===============================================================================================
        /// <summary>
        ///Obtient le format du fichier (AAC) 
        /// </summary>
        public override string Format { get { return "WMA"; } }
        ///==============================================================
        /// <summary>
        ///  Initialise une nouvelle instance de la classe ChansonWMA qui la classe hérite de Chanson
        ///  la donnee de nom du fichier, c'est base sur la classe Chanson
        /// </summary>
        /// --------------------------------------------------------------
        public ChansonWMA(string pNomFichier) : base(pNomFichier)
        {

        }
        ///==============================================================
        /// <summary>
        ///  Initialise une nouvelle instance de la classe ChansonWMA qui la classe hérite de Chanson
        ///  les donnees sont: nom du fichier (repertoire + titre + format), artiste, tirte, annee
        /// c'est base sur la classe Chanson
        /// </summary>
        /// --------------------------------------------------------------
        public ChansonWMA(string pRepertoire, string pArtiste, string pTitre, int pAnnée) : base(pRepertoire, pArtiste, pTitre, pAnnée)
        {

        }
        ///==============================================================
        /// <summary>
        /// Écrit une ligne dans le fichier passé en paramètre. 
        /// Cette ligne correspond à l’entête du fichier et contient les informations sur la chanson et représentées
        /// </summary>
        public override void EcrireEntetes(StreamWriter pobjFichier)
        {
            string entete = "codage"+ m_codage + " : ANNEE = " + Annee + "TITRE = " + Titre + " : ARTISE = " + Artiste ;
            pobjFichier.WriteLine(entete);
        }
        ///==============================================================
        /// <summary>
        /// Encode les paroles reçues en paramètre au format AAC
        /// </summary>
        public override void EcrireParoles(StreamWriter pobjFichier, string pParoles)
        {
            pobjFichier.WriteLine(OutilsFormats.EncoderWMA(pParoles, m_codage));
        }
        ///==============================================================
        /// <summary>
        /// Lit la premiere ligne du fichier de la chanson et initialise les champs de la chanson (titre, artiste et année de création de la chanson)
        /// </summary>
        public override void LireEntete()
        {
            StreamReader objFichier = new StreamReader(NomFichier);
            string entete = objFichier.ReadLine();
            string[] champ = entete.Split(':');
            string[] codage = champ[0].Split('=');
            string[] annee = champ[1].Split('=');
            string[] titre = champ[2].Split('=');
            string[] artiste = champ[3].Split('=');

        }
        ///==============================================================
        /// <summary>
        /// Récupère les paroles de la chanson à partir du fichier passé en paramètre, les décode selon le format AAC et les retourne.
        /// </summary>
        public override string LireParoles(StreamReader pobjFicher)
        {
            string encode = pobjFicher.ReadToEnd();
            string decode = OutilsFormats.DecoderMP3(encode);
            return decode;

        }
    }

}

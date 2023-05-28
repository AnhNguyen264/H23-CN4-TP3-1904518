using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaladeurMultiFormats
{ /// ===============================================================================================
  /// <summary>
  /// Représente la chanson du format AAC, 
  /// </summary>
    public class ChansonAAC:Chanson

    {
        /// ===============================================================================================
        /// <summary>
        ///Obtient le format du fichier (AAC) 
        /// </summary>
        public override string Format { get { return "AAC"; } }

        ///==============================================================
        /// <summary>
        ///  Initialise une nouvelle instance de la classe ChansonAAC qui la classe hérite de Chanson
        ///  la donnee de nom du fichier, c'est base sur la classe Chanson
        /// </summary>
        /// --------------------------------------------------------------
        public ChansonAAC(string pNomFichier) : base(pNomFichier)
        {

        }

        ///==============================================================
        /// <summary>
        ///  Initialise une nouvelle instance de la classe ChansonAAC qui la classe hérite de Chanson
        ///  les donnees sont: nom du fichier (repertoire + titre + format), artiste, tirte, annee
        /// c'est base sur la classe Chanson
        /// </summary>
        /// --------------------------------------------------------------
        public ChansonAAC(string pRepertoire, string pArtiste, string pTitre, int pAnnée) : base(pRepertoire, pArtiste,pTitre,pAnnée)
        {

        }

        ///==============================================================
        /// <summary>
        /// Écrit une ligne dans le fichier passé en paramètre. 
        /// Cette ligne correspond à l’entête du fichier et contient les informations sur la chanson et représentées
        /// </summary>
        public override void EcrireEntetes(StreamWriter pobjFichier) {
            string entete = "TITRE = " + Titre +" : ARTISE = " + Artiste + " : ANNEE = " + Annee;
            pobjFichier.WriteLine(entete);
        }

        ///==============================================================
        /// <summary>
        /// Encode les paroles reçues en paramètre au format AAC
        /// </summary>
        
        public override void EcrireParoles(StreamWriter pobjFichier, string pParoles) {
            pobjFichier.WriteLine(OutilsFormats.EncoderAAC(pParoles)); 
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
            string[] titre = champ[0].Split('=');
            string[] artiste = champ[1].Split('=');
            string[] annee = champ[2].Split('=');

        }

        ///==============================================================
        /// <summary>
        /// Récupère les paroles de la chanson à partir du fichier passé en paramètre, les décode selon le format AAC et les retourne.
        /// </summary>
        public override string LireParoles(StreamReader pobjFicher) {
            string encode = pobjFicher.ReadToEnd();
            string decode = OutilsFormats.DecoderAAC(encode);
            return decode;

        }
    }
}

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
    /// Représente la chanson du format MP3, 
    /// </summary>
    public class ChansonMP3:Chanson

    {
        /// ===============================================================================================
        /// <summary>
        ///Obtient le format du fichier (MP3) 
        /// </summary>
        public override string Format { get { return "MP3"; } }
        ///==============================================================
        /// <summary>
        ///  Initialise une nouvelle instance de la classe ChansonMp3 qui la classe hérite de Chanson
        ///  la donnee de nom du fichier, c'est base sur la classe Chanson
        /// </summary>
        /// --------------------------------------------------------------
        public ChansonMP3(string pNomFichier) : base(pNomFichier)
        {

        }
        ///==============================================================
        /// <summary>
        ///  Initialise une nouvelle instance de la classe ChansonMp3 qui la classe hérite de Chanson
        ///  les donnees sont: nom du fichier (repertoire + titre + format), artiste, tirte, annee
        /// c'est base sur la classe Chanson
        /// </summary>
        /// --------------------------------------------------------------
        public ChansonMP3(string pRepertoire, string pArtiste, string pTitre, int pAnnée) : base(pRepertoire, pArtiste, pTitre, pAnnée)
        {

        }

        ///==============================================================
        /// <summary>
        /// Écrit une ligne dans le fichier passé en paramètre. 
        /// Cette ligne correspond à l’entête du fichier et contient les informations sur la chanson et représentées
        /// </summary>
        public override void EcrireEntetes(StreamWriter pobjFichier)
        {
            string entete =  " : ARTISE = " + Artiste + " : ANNEE = " + Annee + "TITRE = " + Titre ;
            pobjFichier.WriteLine(entete);
        }


        ///==============================================================
        /// <summary>
        /// Encode les paroles reçues en paramètre au format MP3
        /// </summary>
        public override void EcrireParoles(StreamWriter pobjFichier, string pParoles)
        {
            pobjFichier.WriteLine(OutilsFormats.EncoderMP3(pParoles));
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
            string[] artiste = champ[0].Split('=');
            string[] annee = champ[1].Split('=');
            string[] titre = champ[2].Split('=');

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

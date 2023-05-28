using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaladeurMultiFormats
{ /// ===============================================================================================
  /// <summary>
  /// Représente la baladeur
  /// </summary>
    public class Baladeur : IBaladeur
    {
        #region Constant
        /// <summary>
        /// Represente constant Nom de repertoire
        /// </summary> 
        private const string NOM_RÉPERTOIRE = "Chansons";
        #endregion

        #region Champs
        private List<Chanson> m_colChansons;
        #endregion

        #region Proprietes
        /// <summary>
        /// Obtient le nombre de chansons.
        /// </summary> 
        public int NbChansons { get { return m_colChansons.Count; } }
        #endregion

        #region Constructeur
        /// <summary>
        /// Initialise une instance de la classe Baladeur. Elle instancie la collection des chansons.
        /// </summary> 
        public Baladeur() 
        { 
            m_colChansons = new List<Chanson>();

        }
        #endregion

        #region methodes
        /// <summary>
        /// Affiche la liste des chansons dans la pListView passée en paramètre
        /// </summary> 
        public void AfficherLesChansons(ListView pListView)
        {
            pListView.Clear();
            foreach (var item in m_colChansons)
            { ListViewItem objItem = new ListViewItem(item.Artiste);

                objItem.SubItems.Add(item.Titre);
                objItem.SubItems.Add(item.Annee.ToString());
                objItem.SubItems.Add(item.Format);
                pListView.Items.Add(objItem);
            }
            
        }

        /// <summary>
        /// Obtient la chanson à l’index pIndex passé en paramètre.
        /// </summary> 
        public Chanson ChansonAt(int pIndex)
        {
            return m_colChansons[pIndex];
        }


        /// <summary>
        /// Charge la liste des chansons dans m_colChansons.
        /// Elle doit vérifier l’existence du répertoire des chansons, ensuite lit chaque fichier et instancie une classe de chanson selon le format et l’ajoute dans la collection des chansons m_colChansons 
        /// </summary> 
        public void ConstruireLaListeDesChansons()
        {
            if(Directory.Exists(NOM_RÉPERTOIRE))
            {
                string[] listeDesChansons = Directory.GetFiles(NOM_RÉPERTOIRE);
                Array.Sort(listeDesChansons);
                int cptErreur = 0;
                foreach (string chansonDansListe in listeDesChansons)
                {
                    try
                    {
                        string[] nomDuFichier = chansonDansListe.Split('.');
                        Chanson chanson;
                        switch (nomDuFichier[1].ToLower())
                        {
                            case "aac":
                                chanson = new ChansonAAC(chansonDansListe);
                                m_colChansons.Add(chanson);
                                break;
                            case "mp3":
                                chanson = new ChansonMP3(chansonDansListe);
                                m_colChansons.Add(chanson);
                                break;
                            case "wma":
                                chanson = new ChansonWMA(chansonDansListe);
                                m_colChansons.Add(chanson);
                                break;

                        }
                    }
                    catch (Exception)
                    {
                        cptErreur++;
                    }
                     if (cptErreur > 0)
                    {
                        MessageBox.Show(cptErreur + "Chansons n'ont pas pu etre chargees correctement", "Baladeur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }


        /// <summary>
        /// un object de ChansonAAC à partir de la chanson à l’index pIndex, enregistre les paroles et supprime le fichier du format précédent.
        ///Elle utilise la méthode Ecrire pour enregistrer les paroles et la méthode File.Delete pour supprimer un
        /// </summary> 
        public void ConvertirVersAAC(int pIndex)
        {
            Chanson objChanson = ChansonAt(pIndex);
            string parole = objChanson.Paroles;
            ChansonAAC chansonAAC = new ChansonAAC(NOM_RÉPERTOIRE, objChanson.Artiste, objChanson.Titre, objChanson.Annee);
            File.Delete(objChanson.NomFichier);
            StreamWriter objFichier = new StreamWriter( chansonAAC.NomFichier);
           chansonAAC.EcrireEntetes(objFichier);
            chansonAAC.EcrireParoles(objFichier, parole);
            objFichier.Close();
            m_colChansons[pIndex] = chansonAAC;
        }


        /// <summary>
        /// un object de ChansonMP3 à partir de la chanson à l’index pIndex, enregistre les paroles et supprime le fichier du format précédent.
        ///Elle utilise la méthode Ecrire pour enregistrer les paroles et la méthode File.Delete pour supprimer un
        /// </summary> 
        public void ConvertirVersMP3(int pIndex)
        {
            Chanson objChanson = ChansonAt(pIndex);
            string parole = objChanson.Paroles;
            ChansonMP3 chansonMP3 = new ChansonMP3(NOM_RÉPERTOIRE, objChanson.Artiste, objChanson.Titre, objChanson.Annee);
            File.Delete(objChanson.NomFichier);

            StreamWriter objFichier = new StreamWriter(chansonMP3.NomFichier);
            chansonMP3.EcrireEntetes(objFichier);
            chansonMP3.EcrireParoles(objFichier, parole);
            objFichier.Close();
            m_colChansons[pIndex] = chansonMP3;
        }


        /// <summary>
        /// un object de ChansonWMA à partir de la chanson à l’index pIndex, enregistre les paroles et supprime le fichier du format précédent.
        ///Elle utilise la méthode Ecrire pour enregistrer les paroles et la méthode File.Delete pour supprimer un
        /// </summary> 
        public void ConvertirVersWMA(int pIndex)
        {
            Chanson objChanson = ChansonAt(pIndex);
            string parole = objChanson.Paroles;
            ChansonWMA chansonWMA = new ChansonWMA(NOM_RÉPERTOIRE, objChanson.Artiste, objChanson.Titre, objChanson.Annee);
            File.Delete(objChanson.NomFichier);

            StreamWriter objFichier = new StreamWriter(chansonWMA.NomFichier);
            chansonWMA.EcrireEntetes(objFichier);
            chansonWMA.EcrireParoles(objFichier, parole);
            objFichier.Close();
            m_colChansons[pIndex] = chansonWMA;
        }
        #endregion
    }
}

using ControleurJSON.JSON;
using Imgur.API;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Models;
using Imgur.API.Models.Impl;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ControleurJSON
{
    class Program
    {
        static void Main(string[] args)
        {
            ControleurGererDB _controleurGererDB = new ControleurGererDB();
            List<KrosCard> cardsToAdd = new List<KrosCard>();
            List<KrosListCardText> cardsTextToAdd = new List<KrosListCardText>();


     



           
            

            //On populate la base
            string[] fileJSONKrosKardPaths = Directory.GetFiles(@"D:\XamarinProject\KrosmagaUniverse\KrosmagaUniverse-master\cards_data\data_0.8.4\json_0.8.4\data\");

            Console.WriteLine("Test ParseJsonToCard \n \n");
            foreach (var jsonFilePath in fileJSONKrosKardPaths)
            {
                var json = System.IO.File.ReadAllText(jsonFilePath);
                cardsToAdd.Add(_controleurGererDB.ParseJsonToKrosCard(json));

            }

            Console.WriteLine("\n \n Partie DB : \n \n");

            

            SQLiteConnection m_dbConnection;
            string dbName = "KrosDB.db";
            string stringConnexion = @"D:\XamarinProject\KrosmagaUniverse\KrosmagaUniverse-master\cards_data\data_0.8.4\Database\" + dbName;

            if (!System.IO.File.Exists(stringConnexion))
            {

                m_dbConnection = new SQLiteConnection(@"Data Source=" + stringConnexion + ";Version=3;");
                m_dbConnection.Open();

                CreateAllTable(m_dbConnection);
                List<KrosImageModel> listImgCards = PrepareAllCard();
                FillAllTable(m_dbConnection, cardsToAdd, listImgCards);

                Console.ReadLine();
            }




        }
    
        private static void FillAllTable(SQLiteConnection m_dbConnection, List<KrosCard> cardsToAdd, List<KrosImageModel> listImgCards)
        {
            StringBuilder sqlInsertKrosCard = new StringBuilder();
            int nbcardsToAdd = cardsToAdd.Count;
            int index = 0;
            foreach (var card in cardsToAdd)
            {


                sqlInsertKrosCard.Append("insert into KrosCard (");
                sqlInsertKrosCard.Append("Id,");
                sqlInsertKrosCard.Append("Name,");
                sqlInsertKrosCard.Append("CardType,");
                sqlInsertKrosCard.Append("CostAP,");
                sqlInsertKrosCard.Append("Life,");
                sqlInsertKrosCard.Append("Attack,");
                sqlInsertKrosCard.Append("MovementPoint,");
                sqlInsertKrosCard.Append("IsToken,");
                sqlInsertKrosCard.Append("Rarity,");
                sqlInsertKrosCard.Append("GodType,");
                sqlInsertKrosCard.Append("Extension,");
                sqlInsertKrosCard.Append("NameFR,");
                sqlInsertKrosCard.Append("DescFR,");
                sqlInsertKrosCard.Append("NameEN,");
                sqlInsertKrosCard.Append("DescEN,");
                sqlInsertKrosCard.Append("NameES,");
                sqlInsertKrosCard.Append("DescES,");
                sqlInsertKrosCard.Append("ImgFRURL");
                sqlInsertKrosCard.Append(") values (");
                sqlInsertKrosCard.Append(card.Id + ",");
                sqlInsertKrosCard.Append("'" + card.Name + "',");
                sqlInsertKrosCard.Append(card.CardType + ",");
                sqlInsertKrosCard.Append(card.CostAP + ",");
                sqlInsertKrosCard.Append(card.Life + ",");
                sqlInsertKrosCard.Append(card.Attack + ",");
                sqlInsertKrosCard.Append(card.MovementPoint + ",");
                sqlInsertKrosCard.Append(0 + ",");  //Extension
                sqlInsertKrosCard.Append(card.Rarity + ",");
                sqlInsertKrosCard.Append(card.GodType + ",");
                sqlInsertKrosCard.Append(card.Extension + ",");
                sqlInsertKrosCard.Append("'" + card.Texts.NameFR.Replace("'", "#QUOTE") + "',");
                sqlInsertKrosCard.Append("'" + card.Texts.DescFR.Replace("'", "#QUOTE") + "',");
                sqlInsertKrosCard.Append("'" + card.Texts.NameEN.Replace("'", "#QUOTE") + "',");
                sqlInsertKrosCard.Append("'" + card.Texts.DescEN.Replace("'", "#QUOTE") + "',");
                sqlInsertKrosCard.Append("'" + card.Texts.NameES.Replace("'", "#QUOTE") + "',");
                sqlInsertKrosCard.Append("'" + card.Texts.DescES.Replace("'", "#QUOTE") + "',");
                sqlInsertKrosCard.Append("'" + listImgCards.Find(x => x.imgName == card.Name+".png").imgURL + "')");

                SQLiteCommand command = new SQLiteCommand(sqlInsertKrosCard.ToString(), m_dbConnection);
                command.ExecuteNonQuery();
                index++;
                Console.Write(index + " / " + nbcardsToAdd + " || " + card.Texts.NameFR + " Ajouté en base \n");
                sqlInsertKrosCard.Clear();



            }



        }

        private static void CreateAllTable(SQLiteConnection m_dbConnection)
        {
            try
            {

                string sqlCreateKrosCard = "CREATE TABLE \"KrosCard\" ( `Id` INTEGER NOT NULL UNIQUE, `Name` TEXT, `CardType` INTEGER NOT NULL, `CostAP` INTEGER, `Life` INTEGER, `Attack` INTEGER, `MovementPoint` INTEGER, `IsToken` INTEGER, `Rarity` INTEGER, `GodType` INTEGER, `Extension` INTEGER, `NameFR` TEXT, `DescFR` TEXT, `NameEN` TEXT, `DescEN` TEXT, `NameES` TEXT, `DescES` TEXT,`ImgFRURL` TEXT, PRIMARY KEY(`Id`) )";

                SQLiteCommand commandCreateKrosCard = new SQLiteCommand(sqlCreateKrosCard, m_dbConnection);
                commandCreateKrosCard.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Message : " + e.Message);
                throw;
            }


            //  string sqlCreateKrosCardText = "CREATE TABLE `KrosCardText` ("
            //                             + "`CardId`	INTEGER UNIQUE,"
            //                             + "`Name`	TEXT,"
            //                             + "`Descr`	TEXT,"
            //                             + "`idLanguage`	INTEGER"
            //                             + ")";


            //SQLiteCommand commandlCreateKrosCardText = new SQLiteCommand(sqlCreateKrosCardText, m_dbConnection);
            //commandlCreateKrosCardText.ExecuteNonQuery();

        }

        public static List<KrosImageModel> PrepareAllCard()
        {
            List<KrosImageModel> listRoReturn = new List<KrosImageModel>();

            #region fill List
            listRoReturn.Add(new KrosImageModel("betty_boubz.png", "http://i.imgur.com/igmdPHh.png", 1));
            listRoReturn.Add(new KrosImageModel("clara_byne.png", "http://i.imgur.com/CXXRRgO.png", 1));
            listRoReturn.Add(new KrosImageModel("criblage.png", "http://i.imgur.com/xJWN2bL.png", 1));
            listRoReturn.Add(new KrosImageModel("dernier_recours.png", "http://i.imgur.com/AICEGVG.png", 1));
            listRoReturn.Add(new KrosImageModel("detournement.png", "http://i.imgur.com/xBJKkUp.png", 1));
            listRoReturn.Add(new KrosImageModel("eksa_soth.png", "http://i.imgur.com/PrCDaYQ.png", 1));
            listRoReturn.Add(new KrosImageModel("esquive.png", "http://i.imgur.com/olTILLU.png", 1));
            listRoReturn.Add(new KrosImageModel("fantome_cra.png", "http://i.imgur.com/8UOdMIq.png", 1));
            listRoReturn.Add(new KrosImageModel("fleche_chercheuse.png", "http://i.imgur.com/hW4DBaD.png", 1));
            listRoReturn.Add(new KrosImageModel("fleche_criblante.png", "http://i.imgur.com/cTMxqMl.png", 1));
            listRoReturn.Add(new KrosImageModel("fleche_d_immolation.png", "http://i.imgur.com/PHBGTs2.png", 1));
            listRoReturn.Add(new KrosImageModel("fleche_de_recul.png", "http://i.imgur.com/5pGZkXF.png", 1));
            listRoReturn.Add(new KrosImageModel("fleche_destructrice.png", "http://i.imgur.com/wpTNAKB.png", 1));
            listRoReturn.Add(new KrosImageModel("fleche_explosive.png", "http://i.imgur.com/cxxwLMu.png", 1));
            listRoReturn.Add(new KrosImageModel("fleche_percante.png", "http://i.imgur.com/IOWiY9v.png", 1));
            listRoReturn.Add(new KrosImageModel("fleche_tempete.png", "http://i.imgur.com/EvKpAjE.png", 1));
            listRoReturn.Add(new KrosImageModel("guy_yomtella.png", "http://i.imgur.com/1tHTA7p.png", 1));
            listRoReturn.Add(new KrosImageModel("harcelement.png", "http://i.imgur.com/fwimZ9B.png", 1));
            listRoReturn.Add(new KrosImageModel("jems_blond.png", "http://i.imgur.com/8vHJxuh.png", 1));
            listRoReturn.Add(new KrosImageModel("lebolas.png", "http://i.imgur.com/z8Khxvi.png", 1));
            listRoReturn.Add(new KrosImageModel("lucie_fhair.png", "http://i.imgur.com/vs0qZTe.png", 1));
            listRoReturn.Add(new KrosImageModel("oeil_de_lynx.png", "http://i.imgur.com/y9XhieO.png", 1));
            listRoReturn.Add(new KrosImageModel("patty_ceriz.png", "http://i.imgur.com/tPiZkG7.png", 1));
            listRoReturn.Add(new KrosImageModel("robin_des_landes.png", "http://i.imgur.com/ZtjNhyY.png", 1));
            listRoReturn.Add(new KrosImageModel("tireur_d_elite.png", "http://i.imgur.com/wb2vOjW.png", 1));
            listRoReturn.Add(new KrosImageModel("arbre_a_chachas.png", "http://i.imgur.com/urCb2Te.png", 1));
            listRoReturn.Add(new KrosImageModel("bas_les_pattes.png", "http://i.imgur.com/kYIRKdp.png", 1));
            listRoReturn.Add(new KrosImageModel("bluff.png", "http://i.imgur.com/CUUMWgL.png", 1));
            listRoReturn.Add(new KrosImageModel("bond_du_felin.png", "http://i.imgur.com/Too2a90.png", 1));
            listRoReturn.Add(new KrosImageModel("bowne_piauch.png", "http://i.imgur.com/o69UHgp.png", 1));
            listRoReturn.Add(new KrosImageModel("chacha.png", "http://i.imgur.com/yUD7zuL.png", 1));
            listRoReturn.Add(new KrosImageModel("chasseur.png", "http://i.imgur.com/tchwLQD.png", 1));
            listRoReturn.Add(new KrosImageModel("chatar.png", "http://i.imgur.com/j1aAzGi.png", 1));
            listRoReturn.Add(new KrosImageModel("craps.png", "http://i.imgur.com/T5JxLye.png", 1));
            listRoReturn.Add(new KrosImageModel("de_du_chacha.png", "http://i.imgur.com/BAHNwOT.png", 1));
            listRoReturn.Add(new KrosImageModel("de_du_chateux.png", "http://i.imgur.com/F09Ym52.png", 1));
            listRoReturn.Add(new KrosImageModel("de_ecaflip.png", "http://i.imgur.com/iuANSnI.png", 1));
            listRoReturn.Add(new KrosImageModel("de_rebondissant.png", "http://i.imgur.com/o2iOvAH.png", 1));
            listRoReturn.Add(new KrosImageModel("defhi_croquets.png", "http://i.imgur.com/wyyj3f1.png", 1));
            listRoReturn.Add(new KrosImageModel("echaenge.png", "http://i.imgur.com/M8QslYO.png", 1));
            listRoReturn.Add(new KrosImageModel("karla_blondie.png", "http://i.imgur.com/aWtKAXt.png", 1));
            listRoReturn.Add(new KrosImageModel("mitaine.png", "http://i.imgur.com/knqczbG.png", 1));
            listRoReturn.Add(new KrosImageModel("nomekop_le_crapoteur.png", "http://i.imgur.com/LXvjiEZ.png", 1));
            listRoReturn.Add(new KrosImageModel("relance.png", "http://i.imgur.com/i5VGdKA.png", 1));
            listRoReturn.Add(new KrosImageModel("roulette_ecaflip.png", "http://i.imgur.com/eErwHwv.png", 1));
            listRoReturn.Add(new KrosImageModel("shafouine.png", "http://i.imgur.com/prfcudn.png", 1));
            listRoReturn.Add(new KrosImageModel("shava_shavien.png", "http://i.imgur.com/yAAu71z.png", 1));
            listRoReturn.Add(new KrosImageModel("tout_ou_rien.png", "http://i.imgur.com/gFZN8PH.png", 1));
            listRoReturn.Add(new KrosImageModel("transchamation.png", "http://i.imgur.com/99EW6bQ.png", 1));
            listRoReturn.Add(new KrosImageModel("will_skass.png", "http://i.imgur.com/oh6jgBu.png", 1));
            listRoReturn.Add(new KrosImageModel("abraxane.png", "http://i.imgur.com/7GKBI1v.png", 1));
            listRoReturn.Add(new KrosImageModel("acide_sandoz.png", "http://i.imgur.com/q2l19CM.png", 1));
            listRoReturn.Add(new KrosImageModel("alargix.png", "http://i.imgur.com/Oi307FB.png", 1));
            listRoReturn.Add(new KrosImageModel("aroma_bome.png", "http://i.imgur.com/djEbkFw.png", 1));
            listRoReturn.Add(new KrosImageModel("asprogik_mils.png", "http://i.imgur.com/Tioy8Ox.png", 1));
            listRoReturn.Add(new KrosImageModel("baraklud.png", "http://i.imgur.com/HX9oVfW.png", 1));
            listRoReturn.Add(new KrosImageModel("ben_debouche.png", "http://i.imgur.com/FpfOHBn.png", 1));
            listRoReturn.Add(new KrosImageModel("dephasage.png", "http://i.imgur.com/U1WO1Yu.png", 1));
            listRoReturn.Add(new KrosImageModel("equite.png", "http://i.imgur.com/U6RHg9O.png", 1));
            listRoReturn.Add(new KrosImageModel("fiole_de_douleur.png", "http://i.imgur.com/LjEjR2W.png", 1));
            listRoReturn.Add(new KrosImageModel("fiole_de_frayeur.png", "http://i.imgur.com/8mioo47.png", 1));
            listRoReturn.Add(new KrosImageModel("fiole_de_psykoz.png", "http://i.imgur.com/qlWLRJl.png", 1));
            listRoReturn.Add(new KrosImageModel("fiole_de_torpeur.png", "http://i.imgur.com/4TvjMJh.png", 1));
            listRoReturn.Add(new KrosImageModel("fiole_infectee.png", "http://i.imgur.com/PBLCXaY.png", 1));
            listRoReturn.Add(new KrosImageModel("lapinos.png", "http://i.imgur.com/VIaWLZj.png", 1));
            listRoReturn.Add(new KrosImageModel("malox_makugen.png", "http://i.imgur.com/4x5REqs.png", 1));
            listRoReturn.Add(new KrosImageModel("mot_de_silence.png", "http://i.imgur.com/N11YFJc.png", 1));
            listRoReturn.Add(new KrosImageModel("mot_reconstituant.png", "http://i.imgur.com/LazXf9N.png", 1));
            listRoReturn.Add(new KrosImageModel("mot_retablissant.png", "http://i.imgur.com/dAOWbL4.png", 1));
            listRoReturn.Add(new KrosImageModel("mot_soignant.png", "http://i.imgur.com/erBURVc.png", 1));
            listRoReturn.Add(new KrosImageModel("renisurrection.png", "http://i.imgur.com/xjUjxa6.png", 1));
            listRoReturn.Add(new KrosImageModel("saizan_zen.png", "http://i.imgur.com/JPbpdWA.png", 1));
            listRoReturn.Add(new KrosImageModel("seduction.png", "http://i.imgur.com/YedmepF.png", 1));
            listRoReturn.Add(new KrosImageModel("transformatose.png", "http://i.imgur.com/gUa4jNP.png", 1));
            listRoReturn.Add(new KrosImageModel("zaldior.png", "http://i.imgur.com/eW2YzLD.png", 1));
            listRoReturn.Add(new KrosImageModel("adamai_niveau_1.png", "http://i.imgur.com/ueyH4ss.png", 1));
            listRoReturn.Add(new KrosImageModel("adamai_niveau_2.png", "http://i.imgur.com/nMBSqQI.png", 1));
            listRoReturn.Add(new KrosImageModel("adamai_niveau_3.png", "http://i.imgur.com/758KXp0.png", 1));
            listRoReturn.Add(new KrosImageModel("alibert_niveau_1.png", "http://i.imgur.com/8PJUdct.png", 1));
            listRoReturn.Add(new KrosImageModel("alibert_niveau_2.png", "http://i.imgur.com/GDrmcSZ.png", 1));
            listRoReturn.Add(new KrosImageModel("alibert_niveau_3.png", "http://i.imgur.com/Yx612jN.png", 1));
            listRoReturn.Add(new KrosImageModel("amalia_niveau_1.png", "http://i.imgur.com/NuYsNhI.png", 1));
            listRoReturn.Add(new KrosImageModel("amalia_niveau_2.png", "http://i.imgur.com/t15D9Vm.png", 1));
            listRoReturn.Add(new KrosImageModel("amalia_niveau_3.png", "http://i.imgur.com/q1OVhO4.png", 1));
            listRoReturn.Add(new KrosImageModel("atcham_niveau_1.png", "http://i.imgur.com/CQMOLU9.png", 1));
            listRoReturn.Add(new KrosImageModel("atcham_niveau_2.png", "http://i.imgur.com/EOX8R5D.png", 1));
            listRoReturn.Add(new KrosImageModel("atcham_niveau_3.png", "http://i.imgur.com/GxERhVO.png", 1));
            listRoReturn.Add(new KrosImageModel("bakara_niveau_1.png", "http://i.imgur.com/t4lJAkw.png", 1));
            listRoReturn.Add(new KrosImageModel("bakara_niveau_2.png", "http://i.imgur.com/AbA0aIt.png", 1));
            listRoReturn.Add(new KrosImageModel("bakara_niveau_3.png", "http://i.imgur.com/xED7t44.png", 1));
            listRoReturn.Add(new KrosImageModel("comte_harebourg_niveau_1.png", "http://i.imgur.com/OzBgJHE.png", 1));
            listRoReturn.Add(new KrosImageModel("comte_harebourg_niveau_2.png", "http://i.imgur.com/6spZEq8.png", 1));
            listRoReturn.Add(new KrosImageModel("comte_harebourg_niveau_3.png", "http://i.imgur.com/5qrv0iV.png", 1));
            listRoReturn.Add(new KrosImageModel("dragon_cochon_niveau_1.png", "http://i.imgur.com/L48FA5N.png", 1));
            listRoReturn.Add(new KrosImageModel("dragon_cochon_niveau_2.png", "http://i.imgur.com/T9R3BUU.png", 1));
            listRoReturn.Add(new KrosImageModel("dragon_cochon_niveau_3.png", "http://i.imgur.com/fhDAFK9.png", 1));
            listRoReturn.Add(new KrosImageModel("evangelyne_niveau_1.png", "http://i.imgur.com/XYpU6T1.png", 1));
            listRoReturn.Add(new KrosImageModel("evangelyne_niveau_2.png", "http://i.imgur.com/GF7XUdi.png", 1));
            listRoReturn.Add(new KrosImageModel("evangelyne_niveau_3.png", "http://i.imgur.com/DSvXx5D.png", 1));
            listRoReturn.Add(new KrosImageModel("goultard_niveau_1.png", "http://i.imgur.com/LMksfHI.png", 1));
            listRoReturn.Add(new KrosImageModel("goultard_niveau_2.png", "http://i.imgur.com/CkyKwbk.png", 1));
            listRoReturn.Add(new KrosImageModel("goultard_niveau_3.png", "http://i.imgur.com/OK9Hbs0.png", 1));
            listRoReturn.Add(new KrosImageModel("grougal_niveau_1.png", "http://i.imgur.com/LQjzhn3.png", 1));
            listRoReturn.Add(new KrosImageModel("grougal_niveau_2.png", "http://i.imgur.com/R3PxI4y.png", 1));
            listRoReturn.Add(new KrosImageModel("grougal_niveau_3.png", "http://i.imgur.com/D08mgf1.png", 1));
            listRoReturn.Add(new KrosImageModel("indie_niveau_1.png", "http://i.imgur.com/A5mnJc3.png", 1));
            listRoReturn.Add(new KrosImageModel("indie_niveau_2.png", "http://i.imgur.com/JmdPQw5.png", 1));
            listRoReturn.Add(new KrosImageModel("indie_niveau_3.png", "http://i.imgur.com/84zn3FS.png", 1));
            listRoReturn.Add(new KrosImageModel("joris_niveau_1.png", "http://i.imgur.com/Npysb1P.png", 1));
            listRoReturn.Add(new KrosImageModel("joris_niveau_2.png", "http://i.imgur.com/DvJGr4v.png", 1));
            listRoReturn.Add(new KrosImageModel("joris_niveau_3.png", "http://i.imgur.com/h07aqyM.png", 1));
            listRoReturn.Add(new KrosImageModel("julith_jurgen_niveau_1.png", "http://i.imgur.com/HPDyWkT.png", 1));
            listRoReturn.Add(new KrosImageModel("julith_jurgen_niveau_2.png", "http://i.imgur.com/rwO4ehQ.png", 1));
            listRoReturn.Add(new KrosImageModel("julith_jurgen_niveau_3.png", "http://i.imgur.com/ro8CcG7.png", 1));
            listRoReturn.Add(new KrosImageModel("justice_niveau_1.png", "http://i.imgur.com/HBr42Ic.png", 1));
            listRoReturn.Add(new KrosImageModel("justice_niveau_2.png", "http://i.imgur.com/BRfxgU9.png", 1));
            listRoReturn.Add(new KrosImageModel("justice_niveau_3.png", "http://i.imgur.com/hWSr8K0.png", 1));
            listRoReturn.Add(new KrosImageModel("kabrok_niveau_1.png", "http://i.imgur.com/3kChvr5.png", 1));
            listRoReturn.Add(new KrosImageModel("kabrok_niveau_2.png", "http://i.imgur.com/YWxkg3w.png", 1));
            listRoReturn.Add(new KrosImageModel("kabrok_niveau_3.png", "http://i.imgur.com/rczM59y.png", 1));
            listRoReturn.Add(new KrosImageModel("katar_niveau_1.png", "http://i.imgur.com/W0jtMQw.png", 1));
            listRoReturn.Add(new KrosImageModel("katar_niveau_2.png", "http://i.imgur.com/dnjn3YJ.png", 1));
            listRoReturn.Add(new KrosImageModel("katar_niveau_3.png", "http://i.imgur.com/Ce2FRqt.png", 1));
            listRoReturn.Add(new KrosImageModel("kerubim_niveau_1.png", "http://i.imgur.com/S2Oo41H.png", 1));
            listRoReturn.Add(new KrosImageModel("kerubim_niveau_2.png", "http://i.imgur.com/3XnckmN.png", 1));
            listRoReturn.Add(new KrosImageModel("kerubim_niveau_3.png", "http://i.imgur.com/wAHKJNA.png", 1));
            listRoReturn.Add(new KrosImageModel("khan_karkass_niveau_1.png", "http://i.imgur.com/czlWqum.png", 1));
            listRoReturn.Add(new KrosImageModel("khan_karkass_niveau_2.png", "http://i.imgur.com/29wpp4D.png", 1));
            listRoReturn.Add(new KrosImageModel("khan_karkass_niveau_3.png", "http://i.imgur.com/fq6z6bA.png", 1));
            listRoReturn.Add(new KrosImageModel("lilotte_niveau_1.png", "http://i.imgur.com/AvjT8ra.png", 1));
            listRoReturn.Add(new KrosImageModel("lilotte_niveau_2.png", "http://i.imgur.com/2QXAQ1b.png", 1));
            listRoReturn.Add(new KrosImageModel("lilotte_niveau_3.png", "http://i.imgur.com/dvH1QzY.png", 1));
            listRoReturn.Add(new KrosImageModel("lou_niveau_1.png", "http://i.imgur.com/6J198u1.png", 1));
            listRoReturn.Add(new KrosImageModel("lou_niveau_2.png", "http://i.imgur.com/893mUGH.png", 1));
            listRoReturn.Add(new KrosImageModel("lou_niveau_3.png", "http://i.imgur.com/JggXfUe.png", 1));
            listRoReturn.Add(new KrosImageModel("marline_niveau_1.png", "http://i.imgur.com/c7cjmkx.png", 1));
            listRoReturn.Add(new KrosImageModel("marline_niveau_2.png", "http://i.imgur.com/OT7xtf9.png", 1));
            listRoReturn.Add(new KrosImageModel("marline_niveau_3.png", "http://i.imgur.com/pasilDz.png", 1));
            listRoReturn.Add(new KrosImageModel("maskemane_niveau_1.png", "http://i.imgur.com/nEFgTkF.png", 1));
            listRoReturn.Add(new KrosImageModel("maskemane_niveau_2.png", "http://i.imgur.com/QfZgAs2.png", 1));
            listRoReturn.Add(new KrosImageModel("maskemane_niveau_3.png", "http://i.imgur.com/xL7J8YA.png", 1));
            listRoReturn.Add(new KrosImageModel("nox_niveau_1.png", "http://i.imgur.com/ToHu6nC.png", 1));
            listRoReturn.Add(new KrosImageModel("nox_niveau_2.png", "http://i.imgur.com/WQJMici.png", 1));
            listRoReturn.Add(new KrosImageModel("nox_niveau_3.png", "http://i.imgur.com/JAc3sW8.png", 1));
            listRoReturn.Add(new KrosImageModel("otomai_niveau_1.png", "http://i.imgur.com/xHw2taN.png", 1));
            listRoReturn.Add(new KrosImageModel("otomai_niveau_2.png", "http://i.imgur.com/7fG63BS.png", 1));
            listRoReturn.Add(new KrosImageModel("otomai_niveau_3.png", "http://i.imgur.com/r2M0cwD.png", 1));
            listRoReturn.Add(new KrosImageModel("qilby_niveau_1.png", "http://i.imgur.com/Ey7aVIG.png", 1));
            listRoReturn.Add(new KrosImageModel("qilby_niveau_2.png", "http://i.imgur.com/XT29ALV.png", 1));
            listRoReturn.Add(new KrosImageModel("qilby_niveau_3.png", "http://i.imgur.com/69OAZf4.png", 1));
            listRoReturn.Add(new KrosImageModel("remington_niveau_1.png", "http://i.imgur.com/oq5iAte.png", 1));
            listRoReturn.Add(new KrosImageModel("remington_niveau_2.png", "http://i.imgur.com/emFSFau.png", 1));
            listRoReturn.Add(new KrosImageModel("remington_niveau_3.png", "http://i.imgur.com/JVsRvVU.png", 1));
            listRoReturn.Add(new KrosImageModel("ruel_niveau_1.png", "http://i.imgur.com/PpTXAJi.png", 1));
            listRoReturn.Add(new KrosImageModel("ruel_niveau_2.png", "http://i.imgur.com/fw6lJVO.png", 1));
            listRoReturn.Add(new KrosImageModel("ruel_niveau_3.png", "http://i.imgur.com/WAfU9hW.png", 1));
            listRoReturn.Add(new KrosImageModel("tristepin_niveau_1.png", "http://i.imgur.com/SYrmDVw.png", 1));
            listRoReturn.Add(new KrosImageModel("tristepin_niveau_2.png", "http://i.imgur.com/NdUPI9X.png", 1));
            listRoReturn.Add(new KrosImageModel("tristepin_niveau_3.png", "http://i.imgur.com/sHRYwUK.png", 1));
            listRoReturn.Add(new KrosImageModel("ush_niveau_1.png", "http://i.imgur.com/ToARCHD.png", 1));
            listRoReturn.Add(new KrosImageModel("ush_niveau_2.png", "http://i.imgur.com/hzMQOUv.png", 1));
            listRoReturn.Add(new KrosImageModel("ush_niveau_3.png", "http://i.imgur.com/ZQdUl5x.png", 1));
            listRoReturn.Add(new KrosImageModel("yugo_niveau_1.png", "http://i.imgur.com/ywouV8K.png", 1));
            listRoReturn.Add(new KrosImageModel("yugo_niveau_2.png", "http://i.imgur.com/otbhxem.png", 1));
            listRoReturn.Add(new KrosImageModel("yugo_niveau_3.png", "http://i.imgur.com/2KLhjOe.png", 1));
            listRoReturn.Add(new KrosImageModel("appel_a_la_baston.png", "http://i.imgur.com/mx5rewv.png", 1));
            listRoReturn.Add(new KrosImageModel("archille.png", "http://i.imgur.com/zWJpOSS.png", 1));
            listRoReturn.Add(new KrosImageModel("autorite.png", "http://i.imgur.com/nyq84LK.png", 1));
            listRoReturn.Add(new KrosImageModel("bond.png", "http://i.imgur.com/GaN4hgD.png", 1));
            listRoReturn.Add(new KrosImageModel("charge.png", "http://i.imgur.com/JW68Y0o.png", 1));
            listRoReturn.Add(new KrosImageModel("chuck_maurice.png", "http://i.imgur.com/PoBeVVs.png", 1));
            listRoReturn.Add(new KrosImageModel("compulsion.png", "http://i.imgur.com/aBJ3SEv.png", 1));
            listRoReturn.Add(new KrosImageModel("do.png", "http://i.imgur.com/NgNel1e.png", 1));
            listRoReturn.Add(new KrosImageModel("ebranler.png", "http://i.imgur.com/Abwsvgh.png", 1));
            listRoReturn.Add(new KrosImageModel("eratz_le_revendicateur.png", "http://i.imgur.com/xDu1eKE.png", 1));
            listRoReturn.Add(new KrosImageModel("eventrail.png", "http://i.imgur.com/qXgEh7m.png", 1));
            listRoReturn.Add(new KrosImageModel("felida.png", "http://i.imgur.com/DBI5sNj.png", 1));
            listRoReturn.Add(new KrosImageModel("gzenah.png", "http://i.imgur.com/PruKAH8.png", 1));
            listRoReturn.Add(new KrosImageModel("heure_de_gloire.png", "http://i.imgur.com/ImXe9bf.png", 1));
            listRoReturn.Add(new KrosImageModel("initiative.png", "http://i.imgur.com/JiG3jUa.png", 1));
            listRoReturn.Add(new KrosImageModel("intimidation.png", "http://i.imgur.com/t8ZKsuS.png", 1));
            listRoReturn.Add(new KrosImageModel("jabs.png", "http://i.imgur.com/9Q0hWow.png", 1));
            listRoReturn.Add(new KrosImageModel("jice_aouaire.png", "http://i.imgur.com/kMU5iFE.png", 1));
            listRoReturn.Add(new KrosImageModel("katsou_mee.png", "http://i.imgur.com/kYRmej3.png", 1));
            listRoReturn.Add(new KrosImageModel("ravage.png", "http://i.imgur.com/hkAJ5X1.png", 1));
            listRoReturn.Add(new KrosImageModel("rocknocerok.png", "http://i.imgur.com/QF69xR8.png", 1));
            listRoReturn.Add(new KrosImageModel("sono_sino.png", "http://i.imgur.com/XHU1yJG.png", 1));
            listRoReturn.Add(new KrosImageModel("tomla_klass.png", "http://i.imgur.com/72gdo1d.png", 1));
            listRoReturn.Add(new KrosImageModel("uppercut.png", "http://i.imgur.com/qDwilyR.png", 1));
            listRoReturn.Add(new KrosImageModel("virilite.png", "http://i.imgur.com/C4Q4Gfx.png", 1));
            listRoReturn.Add(new KrosImageModel("armure_sanguine.png", "http://i.imgur.com/3ZJ5OMG.png", 1));
            listRoReturn.Add(new KrosImageModel("attirance.png", "http://i.imgur.com/TRxbZ3j.png", 1));
            listRoReturn.Add(new KrosImageModel("bould_erdash.png", "http://i.imgur.com/PpTjkVX.png", 1));
            listRoReturn.Add(new KrosImageModel("coup_de_sang.png", "http://i.imgur.com/LfQR6bW.png", 1));
            listRoReturn.Add(new KrosImageModel("dureden_taillair.png", "http://i.imgur.com/Ovxkgs7.png", 1));
            listRoReturn.Add(new KrosImageModel("edass_le_trouble_fete.png", "http://i.imgur.com/812SlRi.png", 1));
            listRoReturn.Add(new KrosImageModel("fulgurance.png", "http://i.imgur.com/35TOFPs.png", 1));
            listRoReturn.Add(new KrosImageModel("fumfamfim.png", "http://i.imgur.com/rDr0lqE.png", 1));
            listRoReturn.Add(new KrosImageModel("globule_la_crapule.png", "http://i.imgur.com/UYple0g.png", 1));
            listRoReturn.Add(new KrosImageModel("jet_le_pied_volant.png", "http://i.imgur.com/ayWKtnK.png", 1));
            listRoReturn.Add(new KrosImageModel("l_enklarveur.png", "http://i.imgur.com/eWY09Tk.png", 1));
            listRoReturn.Add(new KrosImageModel("la_gerbouille.png", "http://i.imgur.com/FVGYoJR.png", 1));
            listRoReturn.Add(new KrosImageModel("laon_epee_filante.png", "http://i.imgur.com/a95LVhm.png", 1));
            listRoReturn.Add(new KrosImageModel("larmes_de_sang.png", "http://i.imgur.com/jhb0rbN.png", 1));
            listRoReturn.Add(new KrosImageModel("mort_proche.png", "http://i.imgur.com/uARu5ll.png", 1));
            listRoReturn.Add(new KrosImageModel("pacte_de_sang.png", "http://i.imgur.com/kdQV6YU.png", 1));
            listRoReturn.Add(new KrosImageModel("padgref_demouelle.png", "http://i.imgur.com/2Tf110j.png", 1));
            listRoReturn.Add(new KrosImageModel("pied_du_sacrieur.png", "http://i.imgur.com/gHNNTiP.png", 1));
            listRoReturn.Add(new KrosImageModel("punition.png", "http://i.imgur.com/fgkeF2P.png", 1));
            listRoReturn.Add(new KrosImageModel("refus_de_mort.png", "http://i.imgur.com/EeX2rFm.png", 1));
            listRoReturn.Add(new KrosImageModel("sacrifice.png", "http://i.imgur.com/085TEJ7.png", 1));
            listRoReturn.Add(new KrosImageModel("sang_brulant.png", "http://i.imgur.com/7EtPBI4.png", 1));
            listRoReturn.Add(new KrosImageModel("sang_meprise.png", "http://i.imgur.com/Q0Vw8dV.png", 1));
            listRoReturn.Add(new KrosImageModel("sang_tatoue.png", "http://i.imgur.com/W9pP6Uo.png", 1));
            listRoReturn.Add(new KrosImageModel("silas_le_solitaire.png", "http://i.imgur.com/C01hzbS.png", 1));
            listRoReturn.Add(new KrosImageModel("buisson.png", "http://i.imgur.com/z6lb3gH.png", 1));
            listRoReturn.Add(new KrosImageModel("canar.png", "http://i.imgur.com/Pwo6tdD.png", 1));
            listRoReturn.Add(new KrosImageModel("dodu.png", "http://i.imgur.com/3ZVZ52q.png", 1));
            listRoReturn.Add(new KrosImageModel("engrais.png", "http://i.imgur.com/FRCtSpR.png", 1));
            listRoReturn.Add(new KrosImageModel("grainaille.png", "http://i.imgur.com/svF767C.png", 1));
            listRoReturn.Add(new KrosImageModel("graines_de_folie.png", "http://i.imgur.com/aakzgJv.png", 1));
            listRoReturn.Add(new KrosImageModel("graines_de_sacrifice.png", "http://i.imgur.com/JMnETKV.png", 1));
            listRoReturn.Add(new KrosImageModel("grine_piz.png", "http://i.imgur.com/06gx79I.png", 1));
            listRoReturn.Add(new KrosImageModel("klore_ofil.png", "http://i.imgur.com/UswDgHD.png", 1));
            listRoReturn.Add(new KrosImageModel("kolo_kolko.png", "http://i.imgur.com/68li4de.png", 1));
            listRoReturn.Add(new KrosImageModel("li_crounch.png", "http://i.imgur.com/uuaTfdI.png", 1));
            listRoReturn.Add(new KrosImageModel("murmures_sauvages.png", "http://i.imgur.com/4mDrjQv.png", 1));
            listRoReturn.Add(new KrosImageModel("necrom_l_ancien.png", "http://i.imgur.com/ueNRygg.png", 1));
            listRoReturn.Add(new KrosImageModel("orma.png", "http://i.imgur.com/AMWmyha.png", 1));
            listRoReturn.Add(new KrosImageModel("pollinisation.png", "http://i.imgur.com/XgLB6yH.png", 1));
            listRoReturn.Add(new KrosImageModel("puissance_sylvestre.png", "http://i.imgur.com/M6RBuAc.png", 1));
            listRoReturn.Add(new KrosImageModel("ronce.png", "http://i.imgur.com/pkVfpdo.png", 1));
            listRoReturn.Add(new KrosImageModel("ronces_agressives.png", "http://i.imgur.com/1JmqIVz.png", 1));
            listRoReturn.Add(new KrosImageModel("ronces_multiples.png", "http://i.imgur.com/2PbR03l.png", 1));
            listRoReturn.Add(new KrosImageModel("sac_de_graines.png", "http://i.imgur.com/03tY0OM.png", 1));
            listRoReturn.Add(new KrosImageModel("sacrifice_poupesque.png", "http://i.imgur.com/71cpKgY.png", 1));
            listRoReturn.Add(new KrosImageModel("savoir_sadida.png", "http://i.imgur.com/3XhBDBz.png", 1));
            listRoReturn.Add(new KrosImageModel("selk_ator.png", "http://i.imgur.com/nJ1CHGy.png", 1));
            listRoReturn.Add(new KrosImageModel("sylvine_folherbe.png", "http://i.imgur.com/RBL0947.png", 1));
            listRoReturn.Add(new KrosImageModel("tremblement_de_terre.png", "http://i.imgur.com/nWqXF88.png", 1));
            listRoReturn.Add(new KrosImageModel("affaiblissement.png", "http://i.imgur.com/obTEj5t.png", 1));
            listRoReturn.Add(new KrosImageModel("amsrad_cepaisset.png", "http://i.imgur.com/rJdUsCm.png", 1));
            listRoReturn.Add(new KrosImageModel("armee_des_ombres.png", "http://i.imgur.com/Uz4eAyc.png", 1));
            listRoReturn.Add(new KrosImageModel("assoiffe.png", "http://i.imgur.com/QKj6AH3.png", 1));
            listRoReturn.Add(new KrosImageModel("azraoul.png", "http://i.imgur.com/aVJk7Lv.png", 1));
            listRoReturn.Add(new KrosImageModel("baron_sramedi.png", "http://i.imgur.com/y4QqH7n.png", 1));
            listRoReturn.Add(new KrosImageModel("demasquer.png", "http://i.imgur.com/CEFltHU.png", 1));
            listRoReturn.Add(new KrosImageModel("ejipe.png", "http://i.imgur.com/MiUSt1H.png", 1));
            listRoReturn.Add(new KrosImageModel("fosscheur.png", "http://i.imgur.com/e6hFCYW.png", 1));
            listRoReturn.Add(new KrosImageModel("funerailles.png", "http://i.imgur.com/1F1AWxX.png", 1));
            listRoReturn.Add(new KrosImageModel("lame_ourduvis.png", "http://i.imgur.com/W4N6XjO.png", 1));
            listRoReturn.Add(new KrosImageModel("maitre_des_ombres.png", "http://i.imgur.com/jEwoYRd.png", 1));
            listRoReturn.Add(new KrosImageModel("ogivol_scalarcin.png", "http://i.imgur.com/tc8DSrg.png", 1));
            listRoReturn.Add(new KrosImageModel("oscar_nak.png", "http://i.imgur.com/juFFeGA.png", 1));
            listRoReturn.Add(new KrosImageModel("peur.png", "http://i.imgur.com/MPzRwkZ.png", 1));
            listRoReturn.Add(new KrosImageModel("pierre_tombale.png", "http://i.imgur.com/FPyjAOc.png", 1));
            listRoReturn.Add(new KrosImageModel("premier_sang.png", "http://i.imgur.com/EzjFecg.png", 1));
            listRoReturn.Add(new KrosImageModel("repos_eternel.png", "http://i.imgur.com/QYWJ2bs.png", 1));
            listRoReturn.Add(new KrosImageModel("rituel_sram.png", "http://i.imgur.com/ViMqpzU.png", 1));
            listRoReturn.Add(new KrosImageModel("saignee_mortelle.png", "http://i.imgur.com/xz94I43.png", 1));
            listRoReturn.Add(new KrosImageModel("sanction.png", "http://i.imgur.com/K9AJSUK.png", 1));
            listRoReturn.Add(new KrosImageModel("second_souffle.png", "http://i.imgur.com/0Y3uw8W.png", 1));
            listRoReturn.Add(new KrosImageModel("sellor_noob.png", "http://i.imgur.com/L6u53RJ.png", 1));
            listRoReturn.Add(new KrosImageModel("sournoiserie.png", "http://i.imgur.com/Vtq9Enr.png", 1));
            listRoReturn.Add(new KrosImageModel("sufod.png", "http://i.imgur.com/nQyHSbB.png", 1));
            listRoReturn.Add(new KrosImageModel("blanquette.png", "http://i.imgur.com/3bvNOdR.png", 1));
            listRoReturn.Add(new KrosImageModel("bombe_token.png", "http://i.imgur.com/12sHu9v.png", 1));
            listRoReturn.Add(new KrosImageModel("buisson_token.png", "http://i.imgur.com/lGYI1BH.png", 1));
            listRoReturn.Add(new KrosImageModel("chacha_noir_token.png", "http://i.imgur.com/Hlrwk9Y.png", 1));
            listRoReturn.Add(new KrosImageModel("chafer_token.png", "http://i.imgur.com/SMCHo9v.png", 1));
            listRoReturn.Add(new KrosImageModel("corbac_token.png", "http://i.imgur.com/NEHFIWx.png", 1));
            listRoReturn.Add(new KrosImageModel("fan_token.png", "http://i.imgur.com/RQ3hFx6.png", 1));
            listRoReturn.Add(new KrosImageModel("gelatine_token.png", "http://i.imgur.com/JoTIn11.png", 1));
            listRoReturn.Add(new KrosImageModel("graine_token.png", "http://i.imgur.com/NvsxDBw.png", 1));
            listRoReturn.Add(new KrosImageModel("la_folle_token.png", "http://i.imgur.com/uHFkwpE.png", 1));
            listRoReturn.Add(new KrosImageModel("la_gonflable_token.png", "http://i.imgur.com/SocYyNI.png", 1));
            listRoReturn.Add(new KrosImageModel("la_sacrifiee_token.png", "http://i.imgur.com/vpHhUzn.png", 1));
            listRoReturn.Add(new KrosImageModel("lait_bambou.png", "http://i.imgur.com/6PvZLZA.png", 1));
            listRoReturn.Add(new KrosImageModel("lapino_token.png", "http://i.imgur.com/qQHB3p5.png", 1));
            listRoReturn.Add(new KrosImageModel("momie_de_quartz_token.png", "http://i.imgur.com/izb23Yw.png", 1));
            listRoReturn.Add(new KrosImageModel("poele.png", "http://i.imgur.com/Uu5CIjc.png", 1));
            listRoReturn.Add(new KrosImageModel("poils_de_jiji.png", "http://i.imgur.com/gaYQbsv.png", 1));
            listRoReturn.Add(new KrosImageModel("prisme_fleau.png", "http://i.imgur.com/6d3Mmyj.png", 1));
            listRoReturn.Add(new KrosImageModel("aiguille.png", "http://i.imgur.com/ttdcbyN.png", 1));
            listRoReturn.Add(new KrosImageModel("brulure_temporelle.png", "http://i.imgur.com/Qm696Yl.png", 1));
            listRoReturn.Add(new KrosImageModel("corum.png", "http://i.imgur.com/4c4z9D2.png", 1));
            listRoReturn.Add(new KrosImageModel("dente_le_remonteur.png", "http://i.imgur.com/fBDqnaZ.png", 1));
            listRoReturn.Add(new KrosImageModel("desynchronisation.png", "http://i.imgur.com/LD2RQiN.png", 1));
            listRoReturn.Add(new KrosImageModel("devouement.png", "http://i.imgur.com/fF3s8Me.png", 1));
            listRoReturn.Add(new KrosImageModel("diod_dewit.png", "http://i.imgur.com/1WUWrXE.png", 1));
            listRoReturn.Add(new KrosImageModel("garde_temps.png", "http://i.imgur.com/dX1SyVb.png", 1));
            listRoReturn.Add(new KrosImageModel("gelure.png", "http://i.imgur.com/jkUrkB2.png", 1));
            listRoReturn.Add(new KrosImageModel("horloge.png", "http://i.imgur.com/3Av1heA.png", 1));
            listRoReturn.Add(new KrosImageModel("instantina.png", "http://i.imgur.com/YzWh0K9.png", 1));
            listRoReturn.Add(new KrosImageModel("many_de_brakmar.png", "http://i.imgur.com/T9yERtd.png", 1));
            listRoReturn.Add(new KrosImageModel("miss_nuit.png", "http://i.imgur.com/UohtWJI.png", 1));
            listRoReturn.Add(new KrosImageModel("missiz_frizz.png", "http://i.imgur.com/05m4E6R.png", 1));
            listRoReturn.Add(new KrosImageModel("patek_tag.png", "http://i.imgur.com/GBgIqKY.png", 1));
            listRoReturn.Add(new KrosImageModel("poussiere_temporelle.png", "http://i.imgur.com/dN1AQ26.png", 1));
            listRoReturn.Add(new KrosImageModel("quartz.png", "http://i.imgur.com/WdoIs8w.png", 1));
            listRoReturn.Add(new KrosImageModel("radoris_montrouge.png", "http://i.imgur.com/td6ePW2.png", 1));
            listRoReturn.Add(new KrosImageModel("ralentissement.png", "http://i.imgur.com/AkU9RJX.png", 1));
            listRoReturn.Add(new KrosImageModel("rollback.png", "http://i.imgur.com/kGPUoWq.png", 1));
            listRoReturn.Add(new KrosImageModel("sablier_de_xelor.png", "http://i.imgur.com/vfTMFvy.png", 1));
            listRoReturn.Add(new KrosImageModel("sarcophage.png", "http://i.imgur.com/yIACO9g.png", 1));
            listRoReturn.Add(new KrosImageModel("sinistro.png", "http://i.imgur.com/kICEcz6.png", 1));
            listRoReturn.Add(new KrosImageModel("teleportation.png", "http://i.imgur.com/PItN88t.png", 1));
            listRoReturn.Add(new KrosImageModel("xelorium.png", "http://i.imgur.com/WEqShlP.png", 1));
            listRoReturn.Add(new KrosImageModel("abragland.png", "http://i.imgur.com/0Wps9TX.png", 1));
            listRoReturn.Add(new KrosImageModel("abraknyde.png", "http://i.imgur.com/lX1SMr1.png", 1));
            listRoReturn.Add(new KrosImageModel("abraknyde_ancestral.png", "http://i.imgur.com/aFuQjx2.png", 1));
            listRoReturn.Add(new KrosImageModel("anathar.png", "http://i.imgur.com/kVc0esb.png", 1));
            listRoReturn.Add(new KrosImageModel("arakne.png", "http://i.imgur.com/QImbk4c.png", 1));
            listRoReturn.Add(new KrosImageModel("arakne_albinos.png", "http://i.imgur.com/40oE5hU.png", 1));
            listRoReturn.Add(new KrosImageModel("arakne_brodeuse.png", "http://i.imgur.com/kySdiFP.png", 1));
            listRoReturn.Add(new KrosImageModel("arakne_majeure.png", "http://i.imgur.com/m6sMLG7.png", 1));
            listRoReturn.Add(new KrosImageModel("arakne_spectrale.png", "http://i.imgur.com/QDicN30.png", 1));
            listRoReturn.Add(new KrosImageModel("araknoplasme_token.png", "http://i.imgur.com/6YOpzsP.png", 1));
            listRoReturn.Add(new KrosImageModel("az.png", "http://i.imgur.com/TSxqXwe.png", 1));
            listRoReturn.Add(new KrosImageModel("bebe_phorreur.png", "http://i.imgur.com/Bxygeem.png", 1));
            listRoReturn.Add(new KrosImageModel("bebe_tofu.png", "http://i.imgur.com/EkuNCpL.png", 1));
            listRoReturn.Add(new KrosImageModel("bellaphone.png", "http://i.imgur.com/TBYcSax.png", 1));
            listRoReturn.Add(new KrosImageModel("bernardo_de_la_carpett.png", "http://i.imgur.com/Hzkirrk.png", 1));
            listRoReturn.Add(new KrosImageModel("black_tiwabbit.png", "http://i.imgur.com/WxdJcxa.png", 1));
            listRoReturn.Add(new KrosImageModel("black_wabbit.png", "http://i.imgur.com/spnZ3mH.png", 1));
            listRoReturn.Add(new KrosImageModel("boo.png", "http://i.imgur.com/CO8aD9F.png", 1));
            listRoReturn.Add(new KrosImageModel("boufette.png", "http://i.imgur.com/xcjAqEu.png", 1));
            listRoReturn.Add(new KrosImageModel("boufton_blanc.png", "http://i.imgur.com/w4vvzZH.png", 1));
            listRoReturn.Add(new KrosImageModel("boufton_noir.png", "http://i.imgur.com/CVSgU2z.png", 1));
            listRoReturn.Add(new KrosImageModel("bouftou_celeste.png", "http://i.imgur.com/iIi3niW.png", 1));
            listRoReturn.Add(new KrosImageModel("bouftou_dominant.png", "http://i.imgur.com/2cklrHu.png", 1));
            listRoReturn.Add(new KrosImageModel("bouftou_du_printemps.png", "http://i.imgur.com/OWB7JGC.png", 1));
            listRoReturn.Add(new KrosImageModel("bouftou_male.png", "http://i.imgur.com/NJdTCyL.png", 1));
            listRoReturn.Add(new KrosImageModel("bouftou_noir.png", "http://i.imgur.com/I8Roy6e.png", 1));
            listRoReturn.Add(new KrosImageModel("bouftou_royal.png", "http://i.imgur.com/gpMS4cO.png", 1));
            listRoReturn.Add(new KrosImageModel("bouftou_sauvage.png", "http://i.imgur.com/ISvW05I.png", 1));
            listRoReturn.Add(new KrosImageModel("bwork_chevaucheur.png", "http://i.imgur.com/68VDghX.png", 1));
            listRoReturn.Add(new KrosImageModel("cactoblong.png", "http://i.imgur.com/gmjERsR.png", 1));
            listRoReturn.Add(new KrosImageModel("cawotte.png", "http://i.imgur.com/iApAJpf.png", 1));
            listRoReturn.Add(new KrosImageModel("chacha_or.png", "http://i.imgur.com/O3QDB7X.png", 1));
            listRoReturn.Add(new KrosImageModel("chafer_archer.png", "http://i.imgur.com/7kGCEMz.png", 1));
            listRoReturn.Add(new KrosImageModel("chafer_d_elite.png", "http://i.imgur.com/LJMyafS.png", 1));
            listRoReturn.Add(new KrosImageModel("chafer_lancier.png", "http://i.imgur.com/04hI9s8.png", 1));
            listRoReturn.Add(new KrosImageModel("chafer_translucide.png", "http://i.imgur.com/WA6F69C.png", 1));
            listRoReturn.Add(new KrosImageModel("chaferfu.png", "http://i.imgur.com/yIboVi7.png", 1));
            listRoReturn.Add(new KrosImageModel("chauve_souris.png", "http://i.imgur.com/MFm1WdN.png", 1));
            listRoReturn.Add(new KrosImageModel("chauve_souris_dodue.png", "http://i.imgur.com/Z1eWcwk.png", 1));
            listRoReturn.Add(new KrosImageModel("chef_de_guerre_bouftou.png", "http://i.imgur.com/qprTelZ.png", 1));
            listRoReturn.Add(new KrosImageModel("chef_grouilleux.png", "http://i.imgur.com/TWlPumz.png", 1));
            listRoReturn.Add(new KrosImageModel("chuchoteurs_arbaletriers.png", "http://i.imgur.com/TVbYhQd.png", 1));
            listRoReturn.Add(new KrosImageModel("chuchoteurs_fantassins.png", "http://i.imgur.com/WcqLFWz.png", 1));
            listRoReturn.Add(new KrosImageModel("chuchoteurs_porte-etendard.png", "http://i.imgur.com/tVc6cLK.png", 1));
            listRoReturn.Add(new KrosImageModel("cochon_token.png", "http://i.imgur.com/JEhlOI4.png", 1));
            listRoReturn.Add(new KrosImageModel("corbac_chef.png", "http://i.imgur.com/sb8xL7i.png", 1));
            listRoReturn.Add(new KrosImageModel("corbacassin.png", "http://i.imgur.com/4sDxPhx.png", 1));
            listRoReturn.Add(new KrosImageModel("crapaud_mufle.png", "http://i.imgur.com/mhd099x.png", 1));
            listRoReturn.Add(new KrosImageModel("craqueboule.png", "http://i.imgur.com/kBoNhGq.png", 1));
            listRoReturn.Add(new KrosImageModel("craqueboule_chuchote.png", "http://i.imgur.com/ob7LJfy.png", 1));
            listRoReturn.Add(new KrosImageModel("craqueboule_or.png", "http://i.imgur.com/bk3Gr3c.png", 1));
            listRoReturn.Add(new KrosImageModel("craqueleur.png", "http://i.imgur.com/PRB6tvb.png", 1));
            listRoReturn.Add(new KrosImageModel("craqueleur_ancestral.png", "http://i.imgur.com/uWgTwKz.png", 1));
            listRoReturn.Add(new KrosImageModel("craqueleur_chuchote.png", "http://i.imgur.com/oSv28oJ.png", 1));
            listRoReturn.Add(new KrosImageModel("craqueleur_royal.png", "http://i.imgur.com/2Wu5NA9.png", 1));
            listRoReturn.Add(new KrosImageModel("dark_vlad.png", "http://i.imgur.com/W9I6Zvk.png", 1));
            listRoReturn.Add(new KrosImageModel("deserboss.png", "http://i.imgur.com/lP6bKXq.png", 1));
            listRoReturn.Add(new KrosImageModel("don_rascailles.png", "http://i.imgur.com/aQnXIlA.png", 1));
            listRoReturn.Add(new KrosImageModel("elely.png", "http://i.imgur.com/WsPd4fZ.png", 1));
            listRoReturn.Add(new KrosImageModel("eliacube.png", "http://i.imgur.com/9BAy98i.png", 1));
            listRoReturn.Add(new KrosImageModel("empereur_gelax.png", "http://i.imgur.com/iSMtxic.png", 1));
            listRoReturn.Add(new KrosImageModel("epouventrail.png", "http://i.imgur.com/HfgmLdd.png", 1));
            listRoReturn.Add(new KrosImageModel("excarnus.png", "http://i.imgur.com/OpjbF1e.png", 1));
            listRoReturn.Add(new KrosImageModel("flaqueux.png", "http://i.imgur.com/S8s5h7W.png", 1));
            listRoReturn.Add(new KrosImageModel("flopin.png", "http://i.imgur.com/60fAfai.png", 1));
            listRoReturn.Add(new KrosImageModel("fripon.png", "http://i.imgur.com/Lpm259u.png", 1));
            listRoReturn.Add(new KrosImageModel("gelee_citron.png", "http://i.imgur.com/9QQ5kLQ.png", 1));
            listRoReturn.Add(new KrosImageModel("gelee_encre.png", "http://i.imgur.com/P27lx2s.png", 1));
            listRoReturn.Add(new KrosImageModel("gelee_fraise.png", "http://i.imgur.com/ufqwxUv.png", 1));
            listRoReturn.Add(new KrosImageModel("gelee_framboise.png", "http://i.imgur.com/QoWvKP4.png", 1));
            listRoReturn.Add(new KrosImageModel("gelee_menthe.png", "http://i.imgur.com/7Rj8KGP.png", 1));
            listRoReturn.Add(new KrosImageModel("glaie.png", "http://i.imgur.com/8XwyCug.png", 1));
            listRoReturn.Add(new KrosImageModel("gligli.png", "http://i.imgur.com/AN0vzf4.png", 1));
            listRoReturn.Add(new KrosImageModel("gligli_agressif.png", "http://i.imgur.com/4GS6rKk.png", 1));
            listRoReturn.Add(new KrosImageModel("gligli_ancestral.png", "http://i.imgur.com/lrXEhlA.png", 1));
            listRoReturn.Add(new KrosImageModel("gligli_royal.png", "http://i.imgur.com/NaaOf1d.png", 1));
            listRoReturn.Add(new KrosImageModel("gloutoblop.png", "http://i.imgur.com/ItvhmOD.png", 1));
            listRoReturn.Add(new KrosImageModel("goule.png", "http://i.imgur.com/p0p5vv7.png", 1));
            listRoReturn.Add(new KrosImageModel("gouloutony.png", "http://i.imgur.com/5ag3JD7.png", 1));
            listRoReturn.Add(new KrosImageModel("grand_craqueleur_chuchote.png", "http://i.imgur.com/Gpuh9SB.png", 1));
            listRoReturn.Add(new KrosImageModel("grany.png", "http://i.imgur.com/dW6ZFuY.png", 1));
            listRoReturn.Add(new KrosImageModel("grimm_beurguen.png", "http://i.imgur.com/4RIGYIt.png", 1));
            listRoReturn.Add(new KrosImageModel("grinch.png", "http://i.imgur.com/wOc1fwH.png", 1));
            listRoReturn.Add(new KrosImageModel("gros_nambourg.png", "http://i.imgur.com/gWbRPnO.png", 1));
            listRoReturn.Add(new KrosImageModel("grouilleux.png", "http://i.imgur.com/LrEB2BC.png", 1));
            listRoReturn.Add(new KrosImageModel("gruffon.png", "http://i.imgur.com/FTUIGz1.png", 1));
            listRoReturn.Add(new KrosImageModel("guy.png", "http://i.imgur.com/UBILcqc.png", 1));
            listRoReturn.Add(new KrosImageModel("gwanpa_wabbit.png", "http://i.imgur.com/EHNBbF4.png", 1));
            listRoReturn.Add(new KrosImageModel("jiji.png", "http://i.imgur.com/b2jH1a2.png", 1));
            listRoReturn.Add(new KrosImageModel("kam_erlite.png", "http://i.imgur.com/uT8ULLc.png", 1));
            listRoReturn.Add(new KrosImageModel("kamasutar.png", "http://i.imgur.com/aXNyfC9.png", 1));
            listRoReturn.Add(new KrosImageModel("kokoko.png", "http://i.imgur.com/ywLaOxF.png", 1));
            listRoReturn.Add(new KrosImageModel("korbax.png", "http://i.imgur.com/Q3vtOAn.png", 1));
            listRoReturn.Add(new KrosImageModel("kralamor.png", "http://i.imgur.com/5JqNmdg.png", 1));
            listRoReturn.Add(new KrosImageModel("kralamour.png", "http://i.imgur.com/EEO3PBU.png", 1));
            listRoReturn.Add(new KrosImageModel("larve_bleue.png", "http://i.imgur.com/vzcgei7.png", 1));
            listRoReturn.Add(new KrosImageModel("larve_orange.png", "http://i.imgur.com/fP0LuuH.png", 1));
            listRoReturn.Add(new KrosImageModel("larve_verte.png", "http://i.imgur.com/wpxE3UA.png", 1));
            listRoReturn.Add(new KrosImageModel("larve_violette.png", "http://i.imgur.com/pDRRG04.png", 1));
            listRoReturn.Add(new KrosImageModel("leila.png", "http://i.imgur.com/rzL6xso.png", 1));
            listRoReturn.Add(new KrosImageModel("lumino.png", "http://i.imgur.com/HpiIwwu.png", 1));
            listRoReturn.Add(new KrosImageModel("magislek.png", "http://i.imgur.com/OoMvYsW.png", 1));
            listRoReturn.Add(new KrosImageModel("magmog_le_boufrog.png", "http://i.imgur.com/gTht9mP.png", 1));
            listRoReturn.Add(new KrosImageModel("maine_cooyne.png", "http://i.imgur.com/lnDfeVc.png", 1));
            listRoReturn.Add(new KrosImageModel("maitre_chuchoku.png", "http://i.imgur.com/8Wkc2Zh.png", 1));
            listRoReturn.Add(new KrosImageModel("maloboss.png", "http://i.imgur.com/UP13mIc.png", 1));
            listRoReturn.Add(new KrosImageModel("malobouc.png", "http://i.imgur.com/wQa984H.png", 1));
            listRoReturn.Add(new KrosImageModel("malocac.png", "http://i.imgur.com/w4oT338.png", 1));
            listRoReturn.Add(new KrosImageModel("malopo.png", "http://i.imgur.com/ds80ZGl.png", 1));
            listRoReturn.Add(new KrosImageModel("maluss.png", "http://i.imgur.com/pwP5Rpo.png", 1));
            listRoReturn.Add(new KrosImageModel("mandale.png", "http://i.imgur.com/Vo3bvn9.png", 1));
            listRoReturn.Add(new KrosImageModel("marcassin_or.png", "http://i.imgur.com/j90TUpJ.png", 1));
            listRoReturn.Add(new KrosImageModel("marcassinet.png", "http://i.imgur.com/4u8bUuR.png", 1));
            listRoReturn.Add(new KrosImageModel("masse.png", "http://i.imgur.com/BQg0xqP.png", 1));
            listRoReturn.Add(new KrosImageModel("megathon.png", "http://i.imgur.com/8We9H5k.png", 1));
            listRoReturn.Add(new KrosImageModel("merkator.png", "http://i.imgur.com/lSt0aNh.png", 1));
            listRoReturn.Add(new KrosImageModel("milimulou.png", "http://i.imgur.com/Ex3GvYS.png", 1));
            listRoReturn.Add(new KrosImageModel("milimulou_garou.png", "http://i.imgur.com/1QtLblY.png", 1));
            listRoReturn.Add(new KrosImageModel("milkar.png", "http://i.imgur.com/Y7NL1WF.png", 1));
            listRoReturn.Add(new KrosImageModel("miranda.png", "http://i.imgur.com/8wwjTmt.png", 1));
            listRoReturn.Add(new KrosImageModel("moogrr.png", "http://i.imgur.com/0VbZZOW.png", 1));
            listRoReturn.Add(new KrosImageModel("moogrr_dominant.png", "http://i.imgur.com/6vrSpMX.png", 1));
            listRoReturn.Add(new KrosImageModel("moogrr_royale.png", "http://i.imgur.com/ziVtRcB.png", 1));
            listRoReturn.Add(new KrosImageModel("moogrron.png", "http://i.imgur.com/3wkYpm9.png", 1));
            listRoReturn.Add(new KrosImageModel("moskito.png", "http://i.imgur.com/9hmdnkP.png", 1));
            listRoReturn.Add(new KrosImageModel("mulou.png", "http://i.imgur.com/zDAMTj0.png", 1));
            listRoReturn.Add(new KrosImageModel("mulou_garou.png", "http://i.imgur.com/IYSBxvv.png", 1));
            listRoReturn.Add(new KrosImageModel("muloune.png", "http://i.imgur.com/7K5fdt4.png", 1));
            listRoReturn.Add(new KrosImageModel("noxine_token.png", "http://i.imgur.com/CRzSoO4.png", 1));
            listRoReturn.Add(new KrosImageModel("ombre.png", "http://i.imgur.com/risiVzI.png", 1));
            listRoReturn.Add(new KrosImageModel("pampactus.png", "http://i.imgur.com/EYyKhIU.png", 1));
            listRoReturn.Add(new KrosImageModel("phorreur.png", "http://i.imgur.com/ZyEzPlc.png", 1));
            listRoReturn.Add(new KrosImageModel("phorreur_domestique.png", "http://i.imgur.com/YqzdSYm.png", 1));
            listRoReturn.Add(new KrosImageModel("phorzerker.png", "http://i.imgur.com/ALrxq2B.png", 1));
            listRoReturn.Add(new KrosImageModel("piou_aux_oeufs_d_or.png", "http://i.imgur.com/LSa5ROs.png", 1));
            listRoReturn.Add(new KrosImageModel("piou_bleu.png", "http://i.imgur.com/lNhvmF9.png", 1));
            listRoReturn.Add(new KrosImageModel("piou_rouge.png", "http://i.imgur.com/b4dhVgz.png", 1));
            listRoReturn.Add(new KrosImageModel("piou_royal.png", "http://i.imgur.com/9jgtyo6.png", 1));
            listRoReturn.Add(new KrosImageModel("piou_vert.png", "http://i.imgur.com/pIoynKi.png", 1));
            listRoReturn.Add(new KrosImageModel("piou_violet.png", "http://i.imgur.com/pje4ILf.png", 1));
            listRoReturn.Add(new KrosImageModel("pioussin.png", "http://i.imgur.com/4NyOEba.png", 1));
            listRoReturn.Add(new KrosImageModel("plante_kanniboul.png", "http://i.imgur.com/w50ub7O.png", 1));
            listRoReturn.Add(new KrosImageModel("polter.png", "http://i.imgur.com/Kf68ZaQ.png", 1));
            listRoReturn.Add(new KrosImageModel("polter_tofu.png", "http://i.imgur.com/O0VzkhB.png", 1));
            listRoReturn.Add(new KrosImageModel("prespic.png", "http://i.imgur.com/pmzpN2w.png", 1));
            listRoReturn.Add(new KrosImageModel("protoflex.png", "http://i.imgur.com/5LUzqlG.png", 1));
            listRoReturn.Add(new KrosImageModel("pupuce.png", "http://i.imgur.com/dF63khU.png", 1));
            listRoReturn.Add(new KrosImageModel("purpuce.png", "http://i.imgur.com/eALvhsj.png", 1));
            listRoReturn.Add(new KrosImageModel("rabet.png", "http://i.imgur.com/Wd9lCTf.png", 1));
            listRoReturn.Add(new KrosImageModel("ragnagna.png", "http://i.imgur.com/T9U0EuX.png", 1));
            listRoReturn.Add(new KrosImageModel("rat_dechant.png", "http://i.imgur.com/2kq5mru.png", 1));
            listRoReturn.Add(new KrosImageModel("rat_devil.png", "http://i.imgur.com/8haGRGb.png", 1));
            listRoReturn.Add(new KrosImageModel("rat_dominant.png", "http://i.imgur.com/tGK0reF.png", 1));
            listRoReturn.Add(new KrosImageModel("rat_tiboiseur.png", "http://i.imgur.com/CjpcM8E.png", 1));
            listRoReturn.Add(new KrosImageModel("ratou.png", "http://i.imgur.com/aNQqwbE.png", 1));
            listRoReturn.Add(new KrosImageModel("raymond_garden.png", "http://i.imgur.com/thgdJux.png", 1));
            listRoReturn.Add(new KrosImageModel("razortemps.png", "http://i.imgur.com/zzqXRdB.png", 1));
            listRoReturn.Add(new KrosImageModel("requin_lancier.png", "http://i.imgur.com/M7CVdmN.png", 1));
            listRoReturn.Add(new KrosImageModel("requin_marteau.png", "http://i.imgur.com/EVtTgiN.png", 1));
            listRoReturn.Add(new KrosImageModel("requinou.png", "http://i.imgur.com/fcreBpV.png", 1));
            listRoReturn.Add(new KrosImageModel("roi_chafer.png", "http://i.imgur.com/V6H2DoR.png", 1));
            listRoReturn.Add(new KrosImageModel("roi_des_bouftous.png", "http://i.imgur.com/6CoZQgi.png", 1));
            listRoReturn.Add(new KrosImageModel("roi_des_truches.png", "http://i.imgur.com/V0saEv0.png", 1));
            listRoReturn.Add(new KrosImageModel("roi_gelax.png", "http://i.imgur.com/NvEOZ6q.png", 1));
            listRoReturn.Add(new KrosImageModel("rose_demoniaque.png", "http://i.imgur.com/l2oaA2A.png", 1));
            listRoReturn.Add(new KrosImageModel("rupuce.png", "http://i.imgur.com/tT2gJ7I.png", 1));
            listRoReturn.Add(new KrosImageModel("salbatroce.png", "http://i.imgur.com/uEapsnN.png", 1));
            listRoReturn.Add(new KrosImageModel("sangsuce_tsu_tsu.png", "http://i.imgur.com/or1umkQ.png", 1));
            listRoReturn.Add(new KrosImageModel("scarador.png", "http://i.imgur.com/vSwPCHa.png", 1));
            listRoReturn.Add(new KrosImageModel("scoreur.png", "http://i.imgur.com/JmM8va8.png", 1));
            listRoReturn.Add(new KrosImageModel("sergent_poiscaille.png", "http://i.imgur.com/UJABhPA.png", 1));
            listRoReturn.Add(new KrosImageModel("shin_larve.png", "http://i.imgur.com/3Hd60iR.png", 1));
            listRoReturn.Add(new KrosImageModel("sir_comte_flex.png", "http://i.imgur.com/GWYnDBu.png", 1));
            listRoReturn.Add(new KrosImageModel("skale.png", "http://i.imgur.com/i3EjJVz.png", 1));
            listRoReturn.Add(new KrosImageModel("sphincter_cell.png", "http://i.imgur.com/PGTU4VX.png", 1));
            listRoReturn.Add(new KrosImageModel("tartanque.png", "http://i.imgur.com/FRccdow.png", 1));
            listRoReturn.Add(new KrosImageModel("taure.png", "http://i.imgur.com/huSv4ds.png", 1));
            listRoReturn.Add(new KrosImageModel("tiwabbit_kiafin.png", "http://i.imgur.com/9qNnV31.png", 1));
            listRoReturn.Add(new KrosImageModel("tiwabbit_tados.png", "http://i.imgur.com/j7DaPtA.png", 1));
            listRoReturn.Add(new KrosImageModel("tofoune.png", "http://i.imgur.com/NzspLCy.png", 1));
            listRoReturn.Add(new KrosImageModel("tofu_celeste.png", "http://i.imgur.com/2BccFJ3.png", 1));
            listRoReturn.Add(new KrosImageModel("tofu_dominant.png", "http://i.imgur.com/Rx8FY88.png", 1));
            listRoReturn.Add(new KrosImageModel("tofu_enrage.png", "http://i.imgur.com/PaX2HZn.png", 1));
            listRoReturn.Add(new KrosImageModel("tofu_explosif.png", "http://i.imgur.com/GEzSKuv.png", 1));
            listRoReturn.Add(new KrosImageModel("tofu_male.png", "http://i.imgur.com/996XjZI.png", 1));
            listRoReturn.Add(new KrosImageModel("tofu_mutant.png", "http://i.imgur.com/Smniumn.png", 1));
            listRoReturn.Add(new KrosImageModel("tofu_noir.png", "http://i.imgur.com/DwbFwW8.png", 1));
            listRoReturn.Add(new KrosImageModel("tofu_royal.png", "http://i.imgur.com/gsysGXk.png", 1));
            listRoReturn.Add(new KrosImageModel("tofu_ventripotent.png", "http://i.imgur.com/ymAZZML.png", 1));
            listRoReturn.Add(new KrosImageModel("tofukaz.png", "http://i.imgur.com/s1FIvXa.png", 1));
            listRoReturn.Add(new KrosImageModel("tristecoeur.png", "http://i.imgur.com/sLrfmVx.png", 1));
            listRoReturn.Add(new KrosImageModel("tronknyde.png", "http://i.imgur.com/xJUe2x9.png", 1));
            listRoReturn.Add(new KrosImageModel("trool.png", "http://i.imgur.com/q0A9Vdj.png", 1));
            listRoReturn.Add(new KrosImageModel("truche.png", "http://i.imgur.com/NVtVB9U.png", 1));
            listRoReturn.Add(new KrosImageModel("truche_foldingue.png", "http://i.imgur.com/mAUgBni.png", 1));
            listRoReturn.Add(new KrosImageModel("truchemuche.png", "http://i.imgur.com/dn4Apti.png", 1));
            listRoReturn.Add(new KrosImageModel("truchideur.png", "http://i.imgur.com/XtpnRS3.png", 1));
            listRoReturn.Add(new KrosImageModel("truchon.png", "http://i.imgur.com/pwmtSwx.png", 1));
            listRoReturn.Add(new KrosImageModel("tsar_tsu_tsu.png", "http://i.imgur.com/61fsTro.png", 1));
            listRoReturn.Add(new KrosImageModel("veuve_noire.png", "http://i.imgur.com/mEg38jm.png", 1));
            listRoReturn.Add(new KrosImageModel("wa_wabbit.png", "http://i.imgur.com/qpyNsvh.png", 1));
            listRoReturn.Add(new KrosImageModel("wabbit.png", "http://i.imgur.com/zvXCZza.png", 1));
            listRoReturn.Add(new KrosImageModel("wabbit_gm.png", "http://i.imgur.com/bvp3VMN.png", 1));
            listRoReturn.Add(new KrosImageModel("wabbit_infernal.png", "http://i.imgur.com/dtBy2oA.png", 1));
            listRoReturn.Add(new KrosImageModel("wabbit_tados.png", "http://i.imgur.com/vfLISG7.png", 1));
            listRoReturn.Add(new KrosImageModel("welsh.png", "http://i.imgur.com/tg4Rwjv.png", 1));
            listRoReturn.Add(new KrosImageModel("wo_wabbit.png", "http://i.imgur.com/h457rev.png", 1));
            listRoReturn.Add(new KrosImageModel("wobot_01.png", "http://i.imgur.com/Nh9dkUB.png", 1));
            listRoReturn.Add(new KrosImageModel("wobot_02.png", "http://i.imgur.com/4uqYvli.png", 1));
            listRoReturn.Add(new KrosImageModel("zespadon.png", "http://i.imgur.com/PM5bYVF.png", 1));
            listRoReturn.Add(new KrosImageModel("zespadon_noir.png", "http://i.imgur.com/RmCAJgG.png", 1));
            #endregion

            return listRoReturn;

        }

    }

   

    class KrosImageModel
    {
        public string imgURL { get; set; }
        public string imgName { get; set; }
        public int imgLang { get; set; }
        public KrosImageModel()
        {

        }
        public KrosImageModel(string name, string url,int lang)
        {
            imgName = name;
            imgURL = url;
            imgLang = lang;
        }
    }
}










//static async void PrepareUpload()
//{
//    List<string> filePNGPath = new List<string>();

//    filePNGPath.AddRange(Directory.GetFiles(@"D:\XamarinProject\KrosmagaUniverse\KrosmagaUniverse-master\cards_data\data_0.8.4\cards33_0.8.4\images_FR\Cra\"));
//    filePNGPath.AddRange(Directory.GetFiles(@"D:\XamarinProject\KrosmagaUniverse\KrosmagaUniverse-master\cards_data\data_0.8.4\cards33_0.8.4\images_FR\Ecaflip\"));
//    filePNGPath.AddRange(Directory.GetFiles(@"D:\XamarinProject\KrosmagaUniverse\KrosmagaUniverse-master\cards_data\data_0.8.4\cards33_0.8.4\images_FR\Eniripsa\"));
//    filePNGPath.AddRange(Directory.GetFiles(@"D:\XamarinProject\KrosmagaUniverse\KrosmagaUniverse-master\cards_data\data_0.8.4\cards33_0.8.4\images_FR\infinite\"));
//    filePNGPath.AddRange(Directory.GetFiles(@"D:\XamarinProject\KrosmagaUniverse\KrosmagaUniverse-master\cards_data\data_0.8.4\cards33_0.8.4\images_FR\Iop\"));
//    filePNGPath.AddRange(Directory.GetFiles(@"D:\XamarinProject\KrosmagaUniverse\KrosmagaUniverse-master\cards_data\data_0.8.4\cards33_0.8.4\images_FR\Neutre\"));
//    filePNGPath.AddRange(Directory.GetFiles(@"D:\XamarinProject\KrosmagaUniverse\KrosmagaUniverse-master\cards_data\data_0.8.4\cards33_0.8.4\images_FR\Sacrieur\"));
//    filePNGPath.AddRange(Directory.GetFiles(@"D:\XamarinProject\KrosmagaUniverse\KrosmagaUniverse-master\cards_data\data_0.8.4\cards33_0.8.4\images_FR\Sadida\"));
//    filePNGPath.AddRange(Directory.GetFiles(@"D:\XamarinProject\KrosmagaUniverse\KrosmagaUniverse-master\cards_data\data_0.8.4\cards33_0.8.4\images_FR\Sram\"));
//    filePNGPath.AddRange(Directory.GetFiles(@"D:\XamarinProject\KrosmagaUniverse\KrosmagaUniverse-master\cards_data\data_0.8.4\cards33_0.8.4\images_FR\tokens\"));
//    filePNGPath.AddRange(Directory.GetFiles(@"D:\XamarinProject\KrosmagaUniverse\KrosmagaUniverse-master\cards_data\data_0.8.4\cards33_0.8.4\images_FR\Xelor\"));

//    Dictionary<string, string> dicoNomPathPNGFile = new Dictionary<string, string>();

//    foreach (string nomPathData in filePNGPath)
//    {
//        int nbCharToKeep = nomPathData.Length - nomPathData.LastIndexOf('\\');
//        // +1 pour le \ , et -4 pour .png
//        dicoNomPathPNGFile.Add(nomPathData.Substring(nomPathData.LastIndexOf('\\') + 1, nbCharToKeep - 5), nomPathData);
//    }

//    foreach (var path in dicoNomPathPNGFile)
//    {
//        Task<int> task = UploadAllKrosImage(path.Value, path.Key);
//        int x = await task;
//    }

//}
//static async Task<int> UploadAllKrosImage(string path, string nom)
//{
//    try
//    {
//        var client = new ImgurClient("210dff9a62ceef5", "ff65883c75c947f43c7880e1c19e7dfd089d28cb");
//        var endpoint = new ImageEndpoint(client);
//        IImage image;

//        using (var fs = new FileStream(path, FileMode.Open))
//        {
//            image = await endpoint.UploadImageStreamAsync(fs);
//        }
//        Console.Write("\n Image uploaded. Image Url: " + image.Link + " Nom : " + nom);

//        return 1;

//    }
//    catch (ImgurException imgurEx)
//    {
//        Console.Write("An error occurred uploading an image to Imgur.");
//        Console.Write(imgurEx.Message);
//        return 0;
//    }
//}

//public static KrosImageModel TestUpload(string path,string fileName)
//{
//    KrosImageModel imgToReturn = new KrosImageModel();


//    using (var w = new WebClient())
//    {
//        string clientID = "0485633930418bb";
//        w.Headers.Add("Authorization", "Client-ID " + clientID);
//        var values = new NameValueCollection
//            {
//                { "image", Convert.ToBase64String(File.ReadAllBytes(path)) }
//            };

//        byte[] response = w.UploadValues("https://api.imgur.com/3/upload.xml", values);

//        XDocument XMLReponse = XDocument.Load(new MemoryStream(response));
//        var linkReponse = from c in XMLReponse.Descendants().Where(p => p.Name.LocalName == "link") select c;

//        Console.WriteLine("\n"+linkReponse.First().FirstNode.ToString());
//         imgToReturn.imgLang = 1;//FR
//        imgToReturn.imgURL = linkReponse.First().FirstNode.ToString();
//        imgToReturn.imgName = fileName;
//        return imgToReturn;
//    }
//}
using Newtonsoft.Json;
using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KrosmagaUniverse.KrosData
{
    public class ControleurGererDataAccess
    {
        SQLiteConnection dbConn;
        public ControleurGererDataAccess()
        {
            dbConn = DependencyService.Get<ISQLite>().GetConnection();
            // create the table(s)
            //dbConn.CreateTable<KrosCard>();
        }
        public List<KrosCard> GetAllCards()
        {
            return dbConn.Query<KrosCard>("Select * From KrosCard");
        }
        public List<KrosCard> GetAllCardsByClass(int idClass)
        {
            if(System.Enum.IsDefined(typeof(EnumHelper.ClasseKrosmaga), idClass))
                return dbConn.Query<KrosCard>("Select * From KrosCard where GodType ="+idClass);
            else
                return dbConn.Query<KrosCard>("Select * From KrosCard");
        }


        internal void FillTheDb()
        {
            KrosCard card = new KrosCard();
            card.Attack = 3;
            card.CardType = 1;
            card.CostAP = 2;
            card.Id = 1;
            card.Life = 3;
            card.MovementPoint = 3;
            card.Name = "Ma Carte";

            if (dbConn.Query<KrosCard>("Select * From KrosCard").Count == 0)

            SaveKrosCard(card);
        }

        public int SaveKrosCard(KrosCard aCard)
        {
            return dbConn.Insert(aCard);
        }
        public int DeleteKrosCard(KrosCard aCard)
        {
            return dbConn.Delete(aCard);
        }
        public int EditKrosCard(KrosCard aCard)
        {
            return dbConn.Update(aCard);
        }

        public KrosCard ParseJsonToKrosCard(string jsonPath)
        {
            KrosCard deserializedCard = new KrosCard();

            deserializedCard = JsonConvert.DeserializeObject<KrosCard>(jsonPath);

            return deserializedCard;
        }

      
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleurJSON.JSON
{
    public class ControleurGererDB
    {
        public ControleurGererDB()
        {
        

        }

      

        public KrosCard ParseJsonToKrosCard(string jsonPath)
        {
            KrosCard deserializedCard = new KrosCard();

            deserializedCard = JsonConvert.DeserializeObject<KrosCard>(jsonPath);

            return deserializedCard;
        }

        public KrosListCardText ParseJsonToKrosCardText(string jsonPath)
        {
            KrosListCardText deserializedCard = new KrosListCardText();

            deserializedCard = JsonConvert.DeserializeObject<KrosListCardText>(jsonPath);

            return deserializedCard;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ControleurJSON.JSON
{
    [XmlRoot("root")]
    public class KrosListCardText
    {
        [XmlElement("Card")]
        public List<KrosCardText> listKrosCard { get; set; }

        public KrosListCardText()
        {
            listKrosCard = new List<KrosCardText>();
        }
      
    }
}

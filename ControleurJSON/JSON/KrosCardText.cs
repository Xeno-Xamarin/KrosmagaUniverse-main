using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ControleurJSON.JSON
{
    public class KrosCardText
    {
        [XmlElement("CardId")]
        public uint CardId { get; set; }
        [XmlElement("Name")]
        public string Name { get; set; }
        [XmlElement("Descr")]
        public string Descr { get; set; }
        [XmlElement("idLanguage")]
        public uint idLanguage { get; set; }

        public override string ToString()
        {
            return "=====" + "id=" + CardId + "=====" + '\n' +
                    "Nom =" + Name + '\n' +
                    "Desc =" + Descr + '\n' +
                    "================" + '\n'
                ;
        }
    }
}

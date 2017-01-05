
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleurJSON.JSON
{
    public class KrosCard
    {

        public uint Id { get; set; }
        public string Name { get; set; }
        public int CardType { get; set; }
        public int CostAP { get; set; }
        public int Life { get; set; }
        public int Attack { get; set; }
        public int MovementPoint { get; set; }
        public uint[] Families { get; set; }
        public bool IsToken { get; set; }
        public uint Rarity { get; set; }
        public uint GodType { get; set; }

        public int Extension { get; set; }
        public uint[] LinkedCards { get; set; }
        public TextData Texts { get; set; }

        public override string ToString()
        {
            return  "=====" + "id=" + Id +"====="+ '\n' +
                    "Type =" + CardType + '\n'+
                    "PA =" + CostAP + '\n' +
                    "Atq =" + Attack + '\n' +
                    "PV =" + Life + '\n' +
                    "PM =" + MovementPoint + '\n' +
                    "NameFR =" + Texts.NameFR + '\n' +
                    "DescFR =" + Texts.DescFR + '\n' +
                    "NameEN =" + Texts.NameEN + '\n' +
                    "DescEN =" + Texts.DescEN + '\n' +
                  
                    "================" + '\n'
                ;
        }
    }
    public class TextData
    {
        public string NameFR;
        public string DescFR;

        public string NameEN;
        public string DescEN;

        public string NameES;
        public string DescES;
    }
}

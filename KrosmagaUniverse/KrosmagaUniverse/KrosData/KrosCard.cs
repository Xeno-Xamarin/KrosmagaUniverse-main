using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrosmagaUniverse.KrosData
{
    public class KrosCard
    {
        [PrimaryKey, Unique, NotNull, Column("Id")]
        public int Id { get; set; }
        public string Name { get; set; }
        public int CardType { get; set; }
        public int CostAP { get; set; }
        public int Life { get; set; }
        public int Attack { get; set; }
        public int MovementPoint { get; set; }
        public int GodType { get; set; }
        public string ImgFRURL { get; set; }
        public int Rarity { get; set; }

        public string NameFR { get; set; }
        public string DescFR { get; set; }
        public string NameEN { get; set; }
        public string DescEN { get; set; }
        public string NameES { get; set; }
        public string DescES { get; set; }
    }
  
}

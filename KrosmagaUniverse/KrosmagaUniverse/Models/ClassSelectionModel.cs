using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrosmagaUniverse.Models
{
    [ImplementPropertyChanged]
    public class ClassSelectionModel
    {
        private string className;

        public string ClassName
        {
            get { return className; }
            set { className = value; }
        }


        private string classImgPath;

        public string ClassImgPath
        {
            get { return classImgPath; }
            set { classImgPath = value; }
        }
    }
}

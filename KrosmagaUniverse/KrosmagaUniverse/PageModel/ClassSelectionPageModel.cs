using KrosmagaUniverse.Models;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using FreshMvvm;

namespace KrosmagaUniverse.PageModel
{
    [ImplementPropertyChanged]
    public class ClassSelectionPageModel : FreshBasePageModel
    {

        public List<ClassModel> ClassList { get; set; }

        public ClassModel ClassDeckBuilderSelected
        {

            get
            {
                return null;
            }
            set
            {
              
                    CoreMethods.PushPageModel<DeckBuilderPageModel>(value);
                    RaisePropertyChanged();
                value = null;

            }
        }
        public ClassSelectionPageModel()
        {
            
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            var list = new List<ClassModel>();

            list.Add(new ClassModel { IdClass = 2, ClassName = "Crâ", ClassImgPath = "WPcra.png"  });
            list.Add(new ClassModel { IdClass = 4, ClassName = "Ecaflip", ClassImgPath = "WPecaflip.png" });
            list.Add(new ClassModel { IdClass = 3, ClassName = "Eniripsa", ClassImgPath = "WPeniripsa.png" });
            list.Add(new ClassModel { IdClass = 1, ClassName = "Iop", ClassImgPath = "WPiop.png" });
            list.Add(new ClassModel { IdClass = 8, ClassName = "Sacrieur", ClassImgPath = "WPsacrieur.png" });
            list.Add(new ClassModel { IdClass = 10, ClassName = "Sadida", ClassImgPath = "WPsadida.png" });
            list.Add(new ClassModel { IdClass = 6, ClassName = "Sram", ClassImgPath = "WPsram.png" });
            list.Add(new ClassModel { IdClass = 7, ClassName = "Xélor", ClassImgPath = "WPxelor.png" });
            list.Add(new ClassModel { IdClass = 0, ClassName = "Neutre", ClassImgPath = "krosmozv2.png" });

            ClassList = list;

        }

        public void FreeResources()
        {
            ClassList = null;
        }

    }
}

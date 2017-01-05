using KrosmagaUniverse.KrosData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace KrosmagaUniverse
{
    public partial class CardListMenuPage : ContentPage
    {
        public CardListMenuPage()
        {

            InitializeComponent();
            var vList = App.DAUtil.GetAllCards();

            if(vList != null || vList.Count == 0)
            {
                ControleurGererDataAccess _controleurGererDataAccess = new ControleurGererDataAccess();
                _controleurGererDataAccess.FillTheDb();
            }

            cardListData.ItemsSource = vList;



        }
        void OnSelection(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
                //ItemSelected is called on deselection, 
                //which results in SelectedItem being set to null
            }
            var vSelUser = (KrosCard)e.SelectedItem;
           // Navigation.PushAsync(new ShowKrosCard(vSelUser));
        }
        public void OnNewClicked(object sender, EventArgs args)
        {
            //Navigation.PushAsync(new AddKrosCard());
        }
    }
}

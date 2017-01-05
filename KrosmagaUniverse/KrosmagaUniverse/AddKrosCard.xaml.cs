using KrosmagaUniverse.KrosData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace KrosmagaUniverse
{
    public partial class AddKrosCard : ContentPage
    {
        public AddKrosCard()
        {
            InitializeComponent();
        }
        public void OnSaveClicked(object sender, EventArgs args)
        {
            var vCard = new KrosCard()
            {
                Id = uint.Parse(txtCardId.Text),
                CardType = txtCardType.Text,
                Rarity = uint.Parse(txtRarity.Text),
              
            };
            App.DAUtil.SaveKrosCard(vCard);
            Navigation.PushAsync(new CardListMenuPage());
        }
    }
}

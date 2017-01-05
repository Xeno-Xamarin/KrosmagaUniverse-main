using KrosmagaUniverse.KrosData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace KrosmagaUniverse
{
    public partial class EditKrosCard : ContentPage
    {
        KrosCard mSelCard;
        public EditKrosCard(KrosCard aSelCard)
        {
            InitializeComponent();
            mSelCard = aSelCard;
            BindingContext = mSelCard;
        }

        public void OnSaveClicked(object sender, EventArgs args)
        {
            mSelCard.Id = uint.Parse(txtCardId.Text);
            mSelCard.CardType = txtCardType.Text;
            mSelCard.Rarity = uint.Parse(txtRarity.Text);
            
            App.DAUtil.EditKrosCard(mSelCard);
            Navigation.PushAsync(new CardListMenuPage());
        }
    }
}

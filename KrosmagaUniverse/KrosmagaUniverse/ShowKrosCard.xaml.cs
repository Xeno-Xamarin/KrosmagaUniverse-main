using KrosmagaUniverse.KrosData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace KrosmagaUniverse
{
    public partial class ShowKrosCard : ContentPage
    {
        KrosCard mSelCard;
        public ShowKrosCard(KrosCard aSelectedCard)
        {
            InitializeComponent();
            mSelCard = aSelectedCard;
            BindingContext = mSelCard;
        }
        public void OnEditClicked(object sender, EventArgs args)
        {
            Navigation.PushAsync(new EditKrosCard(mSelCard));
        }
        public async void OnDeleteClicked(object sender, EventArgs args)
        {
            bool accepted = await DisplayAlert("Confirm", "Are you Sure ?", "Yes", "No");
            if (accepted)
            {
                App.DAUtil.DeleteKrosCard(mSelCard);
            }
            await Navigation.PushAsync(new CardListMenuPage());
        }
    }
}

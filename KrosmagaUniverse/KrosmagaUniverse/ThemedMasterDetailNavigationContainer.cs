using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KrosmagaUniverse
{
    public class ThemedMasterDetailNavigationContainer : FreshMasterDetailNavigationContainer
    {
        List<Models.MenuItem> pageIcons = new List<Models.MenuItem>();

        public void AddPageWithIcon<T>(string title, string icon = "", object data = null) where T : FreshBasePageModel
        {
            base.AddPage<T>(title, data);
            pageIcons.Add(new Models.MenuItem
            {
                Title = title,
                IconSource = icon
            });
        }
        public void AddTitle<T>(string title, string icon = "", object data = null) where T : FreshBasePageModel
        {
            base.AddPage<T>(title, data);
            pageIcons.Add(new Models.MenuItem
            {
                Title = title,
                IconSource = icon,
                bLabel = true

            });
        }
        protected override void CreateMenuPage(string menuPageTitle, string menuIcon = null)
        {
            var listview = new ListView();
            var _menuPage = new ContentPage();
            _menuPage.Title = menuPageTitle;
            _menuPage.BackgroundColor = Color.FromHex("#c8c8c8");

            listview.ItemsSource = pageIcons;

            var cell = new DataTemplate(typeof(ImageCell));
            cell.SetValue(TextCell.TextColorProperty, Color.White);
            cell.SetBinding(ImageCell.TextProperty, "Title");
            cell.SetBinding(ImageCell.ImageSourceProperty, "IconSource");



            listview.ItemTemplate = cell;

            listview.ItemSelected += (sender, args) =>
            {
                if (!((Models.MenuItem)args.SelectedItem).bLabel)
                {
                    if (Pages.ContainsKey(((Models.MenuItem)args.SelectedItem).Title))
                    {

                        Detail = Pages[((Models.MenuItem)args.SelectedItem).Title];
                    }
                    IsPresented = false;
                }
            };

            _menuPage.Content = listview;

            var navPage = new NavigationPage(_menuPage) { Title = "Menu" };

            if (!string.IsNullOrEmpty(menuIcon))
                navPage.Icon = menuIcon;

            Master = navPage;

        }

        protected override Xamarin.Forms.Page CreateContainerPage(Xamarin.Forms.Page page)
        {

            var navigation = new NavigationPage(page);
            navigation.BarTextColor = Color.Black;

            navigation.BarBackgroundColor = Color.White;

            return navigation;

        }

    }
}

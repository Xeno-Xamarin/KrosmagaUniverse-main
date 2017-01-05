using KrosmagaUniverse.KrosData;
using KrosmagaUniverse.PageModel;

using Xamarin.Forms;

namespace KrosmagaUniverse
{
    public class App : Application
    {
        static ControleurGererDataAccess dbUtils;
        public App()
        {
            // The root page of your application
            //MainPage = new Pages.RootPage();

            var masterDetailNav = new ThemedMasterDetailNavigationContainer();
            masterDetailNav.Init("KrosmagaUniverse","menu.png");
            masterDetailNav.AddTitle<ClassSelectionPageModel>("Outils",null);
           // masterDetailNav.AddPageWithIcon<ClassSelectionPageModel>("Rien", "krosmozv2.png");
            masterDetailNav.AddPageWithIcon<ClassSelectionPageModel>("DeckBuilder", "krosmozv2.png");
            masterDetailNav.AddPageWithIcon<CardModernPageModel>("Liste des Cartes", "krosmozv2.png");
           // masterDetailNav.AddPageWithIcon<ClassSelectionPageModel>("Decks", "krosmozv2.png");
            //masterDetailNav.AddTitle<CardPageModel>("Actualités", null);
            //masterDetailNav.AddPageWithIcon<ClassSelectionPageModel>("News & Patch", "krosmozv2.png");
            //masterDetailNav.AddPageWithIcon<ClassSelectionPageModel>("Streams & Vidéos", "krosmozv2.png");
            //masterDetailNav.AddTitle<CardPageModel>("Autres", null);
            //masterDetailNav.AddPageWithIcon<ClassSelectionPageModel>("Note cette app", "krosmozv2.png");
            //masterDetailNav.AddPageWithIcon<ClassSelectionPageModel>("Paramètres", "krosmozv2.png");
            MainPage = masterDetailNav;
            
        }
        public static ControleurGererDataAccess DAUtil
        {
            get
            {
                if (dbUtils == null)
                {
                    dbUtils = new ControleurGererDataAccess();
                }
                return dbUtils;
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        
    }
}

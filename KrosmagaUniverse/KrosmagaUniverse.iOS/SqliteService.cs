
using KrosmagaUniverse;
using KrosmagaUniverse.iOS;
using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;
using System.Text;

namespace KrosmagaUniverse.iOS
{
    [assembly: Dependency(typeof(SqliteService_IOS))]
    public class SqliteService_IOS : ISQLite
    {
        public SqliteService_IOS()
        {
        }
        #region ISQLite implementation
        public SQLite.Net.SQLiteConnection GetConnection()
        {
            var sqliteFilename = "KrosDB.db";
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder
            var path = Path.Combine(libraryPath, sqliteFilename);

            // This is where we copy in the prepopulated database
            Console.WriteLine(path);
            if (!File.Exists(path))
            {
                //Normalement le fichier est là.
                //Sinon faire appel aux webservice pour le reDL.
                //File.Create(path);
            }

            var plat = new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
            var conn = new SQLite.Net.SQLiteConnection(plat, path);

            // Return the database connection 
            return conn;
        }
        #endregion
    }
}

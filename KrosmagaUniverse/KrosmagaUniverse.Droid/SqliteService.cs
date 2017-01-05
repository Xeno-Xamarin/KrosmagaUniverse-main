using System;
using Xamarin.Forms;
using KrosmagaUniverse.Droid;
using System.IO;

[assembly: Dependency(typeof(SqliteService))]
namespace KrosmagaUniverse.Droid
{
    public class SqliteService : ISQLite
    {
        public SqliteService() { }

        #region ISQLite implementation
        public SQLite.Net.SQLiteConnection GetConnection()
        {
            var sqliteFilename = "KrosDB.db";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder

            var path = Path.Combine(documentsPath, sqliteFilename);

            Console.WriteLine(path);

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            var s = Forms.Context.Resources.OpenRawResource(Resource.Raw.KrosDB);  // RESOURCE NAME ###

            // create a write stream
            FileStream writeStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
            // write to the stream
            ReadWriteStream(s, writeStream);


            var plat = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
            var conn = new SQLite.Net.SQLiteConnection(plat, path);
            // Return the database connection 
            return conn;
        }

        #endregion

        /// <summary>
		/// helper method to get the database out of /raw/ and into the user filesystem
		/// </summary>
		void ReadWriteStream(Stream readStream, Stream writeStream)
        {
            int Length = 256;
            Byte[] buffer = new Byte[Length];
            int bytesRead = readStream.Read(buffer, 0, Length);
            // write the required bytes
            while (bytesRead > 0)
            {
                writeStream.Write(buffer, 0, bytesRead);
                bytesRead = readStream.Read(buffer, 0, Length);
            }
            readStream.Close();
            writeStream.Close();
        }
    }
}
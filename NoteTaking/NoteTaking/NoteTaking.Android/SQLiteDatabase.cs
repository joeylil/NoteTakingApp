using SQLite;
using Xamarin.Forms;
using System.IO;
using System;

[assembly: Dependency(typeof(NoteTaking.Droid.SQLiteDatabase))]

namespace NoteTaking.Droid
{
    class SQLiteDatabase : SQLiteInterface
    {

        public SQLiteAsyncConnection GetConnection()
        {
            var documentsPath =
           Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, "Database.db2");
            return new SQLiteAsyncConnection(path);
        }

    }
}
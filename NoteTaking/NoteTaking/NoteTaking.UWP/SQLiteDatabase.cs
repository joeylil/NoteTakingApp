
using SQLite;
using Xamarin.Forms;
[assembly: Dependency(typeof(NoteTaking.UWP.SQLiteDatabase))]


 namespace NoteTaking.UWP
{
    class SQLiteDatabase : SQLiteInterface
    {
        public SQLiteAsyncConnection GetConnection()
        {
            return new SQLiteAsyncConnection("database.db2");
        }
    }
}

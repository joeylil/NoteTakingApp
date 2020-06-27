using SQLite;

namespace NoteTaking
{
    public interface SQLiteInterface
    {
        SQLiteAsyncConnection GetConnection();
    }

}

using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DemoAPP.DBFolder
{
   public class UserDataBase
    {
        readonly SQLiteAsyncConnection _database;

        public UserDataBase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<UserDetailsTable>().Wait();
        }

        public Task<List<UserDetailsTable>> GetNotesAsync()
        {
            return _database.Table<UserDetailsTable>().ToListAsync();
        }

        public Task<UserDetailsTable> GetNoteAsync(int id)
        {
            return _database.Table<UserDetailsTable>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveNoteAsync(UserDetailsTable userDetailsTable)
        {
            if (userDetailsTable.ID != 0)
            {
                return _database.UpdateAsync(userDetailsTable);
            }
            else
            {
                return _database.InsertAsync(userDetailsTable);
            }
        }

        public Task<int> DeleteNoteAsync(UserDetailsTable userDetailsTable)
        {
            return _database.DeleteAsync(userDetailsTable);
        }
    }
}

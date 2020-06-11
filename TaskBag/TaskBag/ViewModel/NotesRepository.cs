using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Xamarin.Essentials;

namespace TaskBag
{
    public class NotesRepository
    {
        public SQLiteConnection database;
        public NotesRepository(string databasePath)
        {
            database = new SQLiteConnection(databasePath);
            database.CreateTable<Note>();
        }
        public IEnumerable<Note> GetItems()
        {
            return database.Table<Note>().ToList();
        }
        public Note GetItem(int id)
        {
            return database.Get<Note>(id);
        }
        public int DeleteItem(int id)
        {
            return database.Delete<Note>(id);
        }
        public int SaveItem(Note item)
        {
            if (item.Id != 0)
            {
                database.Update(item);
                return item.Id;
            }
            else
            {
                return database.Insert(item);
            }
        }
    }
}

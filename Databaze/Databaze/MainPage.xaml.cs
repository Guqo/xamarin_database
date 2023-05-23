using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Databaze
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        static SQLiteAsyncConnection db;

        static async Task Init()
        {
            if (db != null)
                return;

            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MyData.db");

            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<Jidlo>();
        }

        public static async Task AddJidlo(string name)
        {
            await Init();
            var jidlo = new Jidlo
            {
                Name = name
            };

            var id = await db.InsertAsync(jidlo);
        }

        public static async Task RemoveJidlo(int id)
        {
            await Init();

            await db.DeleteAsync<Jidlo>(id);
        }

        public static async Task GetJidlo()
        {
            await Init();

            var jidlo = await db.Table<Jidlo>().ToListAsync();
        }
    }
}

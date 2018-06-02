using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SQLiteDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (SqliteConnection db = new SqliteConnection("Filename=sqliteSample.db"))
            {
                db.Open();
                SqliteCommand cmd = new SqliteCommand();
                cmd.CommandText = "INSERT INTO Items VALUES (NULL,@Item);";
                cmd.Parameters.AddWithValue("@Item", inputBox.Text);
                try
                {
                    cmd.ExecuteReader();
                }
                catch
                {
                    return;
                }
                db.Close();
            }
            ListProduct.ItemsSource = Grab_Entries();
        }

        private List<string> Grab_Entries()
        {
            List<string> entries = new List<string>();
            using (SqliteConnection db = new SqliteConnection("Filename=sqliteSample.db"))
            {
                db.Open();
                SqliteCommand cmd = new SqliteCommand("SELECT name from Items", db);
                SqliteDataReader query;
                try
                {
                    query = cmd.ExecuteReader();
                }
                catch (SqliteException)
                {
                    return entries;
                }
                while (query.Read())
                {
                    entries.Add(query.GetString(0));
                }
                db.Close();
            }
            return entries;
        }
    }
}

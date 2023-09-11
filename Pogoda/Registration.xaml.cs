using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Pogoda
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ForDB.connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = ForDB.connection;
            command.CommandText = "INSERT INTO [Users] (Login,Pass) VALUES('" + login.Text + "','" + password.Password + "')";

            command.ExecuteNonQuery();
            ForDB.connection.Close();

            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}

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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pogoda
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Registration reg = new Registration();
            reg.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string query;
            bool admin = false;

            ForDB.connection.Open();
            if (AD.IsChecked.Value)
            {
                query = "Select * from [Admins] where Login ='" + login.Text + "' and Pass = '" + password.Password + "'";
                admin = true;
            }
            else
            {
                query = "Select * from [Users] where Login ='" + login.Text + "' and Pass = '" + password.Password + "'";                
            }

            OleDbCommand command = new OleDbCommand(query, ForDB.connection);
            OleDbDataReader read = command.ExecuteReader();
            if (read.HasRows)
            {
                MessageBox.Show("Фамилия и имя верны");

                while (read.Read())
                {
                    App.ID = read.GetInt32(0);
                }
                ForDB.connection.Close();

                MessageBox.Show("Дурачок я еще не доделал прогу", "WOW!!!");
                if (admin)
                {
                    Main2 main2 = new Main2();
                    main2.Show();
                    this.Close();
                }
                else
                {
                    main main = new main();
                    main.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Данного пользователя нет в системе!", "Oh no...");
            }
            ForDB.connection.Close();
        }
    }
}

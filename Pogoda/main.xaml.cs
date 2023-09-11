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
    /// Логика взаимодействия для main.xaml
    /// </summary>
    public partial class main : Window
    {
        public main()
        {
            InitializeComponent();
            ForDB.Combobox_update($"Select * from [Cities]", City_name);
            City_name.SelectedIndex = 0;
            Data_now.SelectedDate = DateTime.Now;
        }

        private void City_name_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OleDbCommand command = new OleDbCommand();
            string eng = (Data_now.SelectedDate.HasValue) ? $"'{Data_now.SelectedDate.Value.ToShortDateString()}'" : $"'{DateTime.Now.ToShortDateString()}'";
            command.CommandText = $"Select * from [Weather] where Data = {eng} AND City = '{City_name.SelectedItem.ToString().Trim()}'";
            command.Connection = ForDB.connection;
            ForDB.connection.Open();

            

            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                tempru.Text = reader.GetInt32(1).ToString() + '°';
                wind_.Text = reader.GetInt32(2).ToString() + "м/с";
                prec_.Text = (reader.GetDouble(3) * 100).ToString() + '%';
                hum_.Text = (reader.GetDouble(4) * 100).ToString() + '%';
            }

            ForDB.connection.Close();
        }

        private void Data_now_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

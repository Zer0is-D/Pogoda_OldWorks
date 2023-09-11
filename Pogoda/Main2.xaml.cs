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
    /// Логика взаимодействия для Main2.xaml
    /// </summary>
    public partial class Main2 : Window
    {
        public Main2()
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
                tempru.Text = reader.GetInt32(1).ToString();
                wind_.Text = reader.GetInt32(2).ToString();
                prec_.Text = (reader.GetDouble(3) * 100).ToString();
                hum_.Text = (reader.GetDouble(4) * 100).ToString(); 
            }

            ForDB.connection.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double prec, hum;
            prec = Convert.ToDouble(prec_.Text) / 100;
            hum = Convert.ToDouble(hum_.Text) / 100;

            ForDB.connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = ForDB.connection;
            string eng = $"'{Data_now.SelectedDate.Value.ToShortDateString()}'";
            command.CommandText = $"Select COUNT(*) from [Weather] where Data = {eng} AND City = '{City_name.SelectedItem.ToString().Trim()}'";
            int b = (int)command.ExecuteScalar();

            if (b > 0)
            {
                command.CommandText = "UPDATE [Weather] SET Temperature = " + tempru.Text + ", Wind = " + wind_.Text + ",Precipitation = " + prec.ToString().Replace(',', '.') + ", Humidity = " + hum.ToString().Replace(',', '.') + " WHERE Data = " + eng + " AND City = '" + City_name.SelectedItem.ToString().Trim() + "'";
            }
            else
            {
                command.CommandText = "INSERT INTO [Weather] (Data,Temperature,Wind,Precipitation,Humidity,City) VALUES('" + Data_now.SelectedDate.Value.ToShortDateString() + "'," + tempru.Text + "," + wind_.Text + "," + prec.ToString().Replace(',', '.') + "," + hum.ToString().Replace(',', '.') + ",'" + City_name.Text + "')";
            }

            command.ExecuteNonQuery();
            ForDB.connection.Close();
        }
    }
}

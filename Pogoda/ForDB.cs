using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Pogoda
{
    class ForDB
    {
        public static OleDbConnection connection = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\Users\dick\source\repos\Pogoda\BD.accdb");

        public static void Combobox_update(string query, ComboBox box)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand(query, connection);
            OleDbDataReader read = command.ExecuteReader();
            while (read.Read())
            {
                string item_name = "";
                for (int i = 0; i < read.FieldCount; i++)
                {
                    item_name += read.GetValue(i) + " ";
                }
                box.Items.Add(item_name);
            }
            read.Close();

            connection.Close();
        }
    }
}

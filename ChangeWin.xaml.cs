using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace Final_wpf
{
    /// <summary>
    /// Логика взаимодействия для ChangeWin.xaml
    /// </summary>
    public partial class ChangeWin : Window
    {
        private GeneralWin parent;
        public ChangeWin(GeneralWin parent)
        {
            InitializeComponent();
            this.parent = parent;
        }
        public void Change_but_ON()
        {
            SaveButton.Visibility = Visibility.Hidden;
            ChangeButton.Visibility = Visibility.Visible;

        }
        public void Save_but_ON()
        {
            SaveButton.Visibility = Visibility.Visible;
            ChangeButton.Visibility = Visibility.Hidden;

        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        { string name_nom = textBoxName.Text;
            ///int id_nomenclature = Convert.ToInt32(textBoxID.Text);
            decimal price = Convert.ToDecimal(textBoxPrice.Text);
            DateTime fromDate = fromDatePicker.DisplayDate;
            DateTime toDate = toDatePicker.DisplayDate;

            SqlConnection connection = new SqlConnection("Data Source=DRDI;Initial Catalog=wpf_sql;Integrated Security=True;");

            SqlCommand command = new SqlCommand("INSERT INTO nomenclature (name, price, fromDate, toDate) VALUES ( @NM, @PC, @fD, @tD)", connection);
            /// если таблица БД имеет свойство IDENTITY_INSERT OFF
            ///SqlCommand command = new SqlCommand("INSERT INTO nomenclature (id_nomenclature, name, price, fromDate, toDate) VALUES (@ID, @NM, @PC, @fD, @tD)", connection);
            ///command.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = id_nomenclature;
            command.Parameters.Add("@NM", System.Data.SqlDbType.NVarChar).Value = name_nom;
            command.Parameters.Add("@PC", System.Data.SqlDbType.Decimal).Value = price;
            command.Parameters.Add("@tD", System.Data.SqlDbType.DateTime).Value = toDate;
            command.Parameters.Add("@fD", System.Data.SqlDbType.DateTime).Value = fromDate;
            connection.Open();
            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("ok");
            }

            else
                MessageBox.Show("False");
            connection.Close();

            this.parent.Update();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string name_nom = textBoxName.Text;
            int id_nom = Convert.ToInt32(ID_zn_Label.Content);
            decimal price = Convert.ToDecimal(textBoxPrice.Text);
            DateTime fromDate = Convert.ToDateTime(fromDatePicker.SelectedDate);
            DateTime toDate = Convert.ToDateTime(toDatePicker.SelectedDate);

            SqlConnection connection = new SqlConnection("Data Source=DRDI;Initial Catalog=wpf_sql;Integrated Security=True;");

            SqlCommand command = new SqlCommand("UPDATE nomenclature SET  name = @NM, price = @PC, fromDate = @fD, toDate = @tD WHERE id_nomenclature = @ID", connection);
           
            command.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = id_nom;
            command.Parameters.Add("@NM", System.Data.SqlDbType.NVarChar).Value = name_nom;
            command.Parameters.Add("@PC", System.Data.SqlDbType.Decimal).Value = price;
            command.Parameters.Add("@tD", System.Data.SqlDbType.DateTime).Value = toDate;
            command.Parameters.Add("@fD", System.Data.SqlDbType.DateTime).Value = fromDate;
            connection.Open();
            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("ok");
            }

            else
                MessageBox.Show("False");
            connection.Close();

            this.parent.Update();
        }
    }
}

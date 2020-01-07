using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.MobileControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Final_wpf
{
    /// <summary>
    /// Логика взаимодействия для GeneralWin.xaml
    /// </summary>
    public partial class GeneralWin : Window
    {
        public GeneralWin()
        {
            InitializeComponent();
            Update();

            
        }

        public void Update()
        {
            SqlConnection connection = new SqlConnection("Data Source=DRDI;Initial Catalog=wpf_sql;Integrated Security=True;");
            connection.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM nomenclature", connection);
            DataTable dataTable = new DataTable();
            da.Fill(dataTable);
            nomenclature.ItemsSource = dataTable.DefaultView;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeWin chaingeWin = new ChangeWin(this);
            chaingeWin.Save_but_ON();
            chaingeWin.Show();

            chaingeWin.ID_zn_Label.Content = "1";
        
        }

        private void DeletButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView oDataRowView = nomenclature.SelectedItem as DataRowView;
            string id_nom = "";

            
            if (oDataRowView != null)
            {
                id_nom = oDataRowView.Row.ItemArray.GetValue(0).ToString();
                SqlConnection connection = new SqlConnection("Data Source=DRDI;Initial Catalog=wpf_sql;Integrated Security=True;");
                connection.Open();
                string message = "Действительно хотите удалить?";
                string caption = "Подтверждение удаления";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;


                result = System.Windows.Forms.MessageBox.Show(message, caption, buttons);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    SqlCommand command = new SqlCommand("DELETE FROM nomenclature WHERE id_nomenclature = @ID", connection);
                    command.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = Convert.ToInt32(id_nom);

                    if (command.ExecuteNonQuery() == 1)
                    {
                        connection.Close();

                        Update();
                        System.Windows.MessageBox.Show("Удаление выполнено");
                    }
                }
                else
                    System.Windows.MessageBox.Show("Удаление не выполнено");


            }
            else
                System.Windows.MessageBox.Show("Fail");
             
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView oDataRowView = nomenclature.SelectedItem as DataRowView;
            string id_num = oDataRowView.Row.ItemArray.GetValue(0).ToString();
            string name = oDataRowView.Row.ItemArray.GetValue(1).ToString();
            string price = oDataRowView.Row.ItemArray.GetValue(2).ToString();
            string fromDate = oDataRowView.Row.ItemArray.GetValue(3).ToString();
            string toDate = oDataRowView.Row.ItemArray.GetValue(4).ToString();
            ChangeWin chaingeWin = new ChangeWin(this);
            
            chaingeWin.ID_zn_Label.Content = id_num;
            chaingeWin.textBoxName.Text = name;
            chaingeWin.textBoxPrice.Text = price;
            chaingeWin.fromDatePicker.SelectedDate = Convert.ToDateTime(fromDate);
            chaingeWin.toDatePicker.SelectedDate = Convert.ToDateTime(toDate);
            chaingeWin.Change_but_ON();
            chaingeWin.Show();

        }
    }
}
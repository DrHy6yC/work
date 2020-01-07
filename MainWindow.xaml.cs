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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Final_wpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
           
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            InitializeComponent();
            string Login = LoginText.Text;
            string Pass = PassText.Text;
            SqlConnection connection = new SqlConnection("Data Source=DRDI;Initial Catalog=wpf_sql;Integrated Security=True;");
            DB db = new DB();

            DataTable table = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter();
            
            db.openConn();

            SqlCommand command = new SqlCommand("SELECT * FROM users WHERE login = @LU AND pass = @LP",connection);
            command.Parameters.Add("@LU", System.Data.SqlDbType.VarChar).Value = Login;
            command.Parameters.Add("@LP", System.Data.SqlDbType.VarChar).Value = Pass;
            adapter.SelectCommand = command;
            adapter.Fill(table);
            if (table.Rows.Count > 0)
            {
                MessageBox.Show("вход выполнен");
                GeneralWin generalWin = new GeneralWin();
                generalWin.Show();
                db.closeConn();
                this.Close();
            }
            else
                MessageBox.Show("вход не выполнен");
        }
    }
}

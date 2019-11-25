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

namespace WpfApp10
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        public LoginScreen()
        {
            InitializeComponent();
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=GENTI\SQLEXPRESS01; Initial Catalog=LoginDB; Integrated Security=True;");

            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                    sqlCon.Open();
                string query = "SELECT COUNT(1) FROM tblUsers WHERE Username = @Username AND Password = @Password";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                sqlCmd.Parameters.AddWithValue("@Password", txtPassword.Password);
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());

                if(count == 1)
                {
                    MainWindow faqja = new MainWindow();
                    faqja.Show();
                    this.Close();
                } else
                {
                    MessageBox.Show("Username or Password is incorrect");
                }



            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error in connectin to the Login"); ;
            }
            finally
            {
                sqlCon.Close();
            }
        }
    }
}

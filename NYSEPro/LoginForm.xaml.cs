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

namespace NYSEPro
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        //Based on the user's role, the below button will redirect the user to their panel
        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=ELLIE\SQLEXPRESS;Initial Catalog=StockProj;Integrated Security=True");
            SqlDataAdapter sda = new SqlDataAdapter(@"SELECT *
                                                      FROM [dbo].[Login]
                                                      WHERE [UserName] = '" + Username_txt.Text + "' and [Password] ='" +Password_txt.Password + "'", con);

            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                this.Hide();
                Admin_Panel adminPanel = new Admin_Panel();
                User_Panel userPanel = new User_Panel();
                if (dt.Rows[0]["Role"].ToString() == "Admin") { adminPanel.Show(); }
                else if (dt.Rows[0]["Role"].ToString() == "Client")
                {
                    userPanel.Show();
                }
                else
                {
                    MessageBox.Show("This user's role is not specified!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            else
            {
                MessageBox.Show("Invalid username or password...!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            Username_txt.Clear();
            Password_txt.Clear();
            Username_txt.Focus();
        }

      
    }
    
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NYSEPro
{
   public class User
    {
        public string login (string username, string password)
        {
            SqlConnection con = new SqlConnection(@"Data Source=ELLIE\SQLEXPRESS;Initial Catalog=StockProj;Integrated Security=True");
            SqlDataAdapter sda = new SqlDataAdapter(@"SELECT *
                                                      FROM [dbo].[Login]
                                                      WHERE [UserName] = '" + username + "' and [Password] ='" + password + "'", con);

            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count == 1)
            {
            
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
            string result = dt.Rows[0]["Role"].ToString();
            return result;
        }
    }
}

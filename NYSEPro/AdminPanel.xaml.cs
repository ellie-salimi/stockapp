using System;
using System.Collections.Generic;
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
using System.Data;
using System.Data.SqlClient;

namespace NYSEPro
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Admin_Panel : Window
    {
        public Admin_Panel()
        {
            InitializeComponent();
        }
        //Helper Functions
        //Functon to load data on start up 
        private void loadData()
        {
            SqlConnection con = new SqlConnection(@"Data Source=ELLIE\SQLEXPRESS;Initial Catalog=StockProj;Integrated Security=True");
            SqlDataAdapter sda = new SqlDataAdapter(@"SELECT *
                     FROM [dbo].[Test]", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            AdminDataGrid.ItemsSource = dt.DefaultView;
        }

        //Function to check if a stock exists in the database
        public bool ifExists(SqlConnection con, string stockId)
        {
            con = new SqlConnection(@"Data Source=ELLIE\SQLEXPRESS;Initial Catalog=StockProj;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            SqlDataAdapter sda = new SqlDataAdapter("Select 1 From [dbo].[Test] WHERE [StockID]='" + stockId + "' ", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Function to clear all the text boxes after add/delete/update
        private void resetData()
        {
            StIdTxt.Clear();
            StSymbolTxt.Clear();
            StPriceCloseTxt.Clear();
            StPriceHighTxt.Clear();
            StPriceLowTxt.Clear();
            StPriceOpenTxt.Clear();
            StPriceAdjCloseTxt.Clear();
            StVolumeTxt.Clear();
            StExchangeTxt.Clear();
            StockDatePicker.SelectedDate = null;
        }

        //Main Functions

       //Start up form
            private void AdminPanel_Loaded(object sender, RoutedEventArgs e)
        {
            loadData();

        }

        //Add button
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=ELLIE\SQLEXPRESS;Initial Catalog=StockProj;Integrated Security=True");
            con.Open();
            SqlCommand sc = new SqlCommand(@"INSERT INTO [dbo].[Test]
                   ([StockID]
                   ,[date]
                   ,[StockSymbol]
                   ,[StockPriceOpen]
                   ,[StockPriceClose]
                   ,[StockPriceIo]
                   ,[StockPriceHigh]
                   ,[StockPriceAdjClose]
                   ,[StockVolume]
                   ,[StockExchange])
             VALUES
                   ('" + StIdTxt.Text + "', '" + Convert.ToDateTime(DateTxt.Text) + "', '" + StSymbolTxt.Text + "', '" + StPriceOpenTxt.Text + "', '" + StPriceCloseTxt.Text + "', '" + StPriceLowTxt.Text + "', '" + StPriceHighTxt.Text + "', '" + StPriceAdjCloseTxt.Text + "', '" + StVolumeTxt.Text + "', '" + StExchangeTxt.Text + "')", con);
            sc.ExecuteNonQuery();
            con.Close();
            resetData();
            loadData();
        }

        //Delete Button
        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        //Datagrid built in event to get the data from selected row
        private void AdminDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView selectedRow = dg.SelectedItem as DataRowView;

            if(selectedRow != null)
            {
                StIdTxt.Text = selectedRow["StockID"].ToString();
                StSymbolTxt.Text = selectedRow["StockSymbol"].ToString(); ;
                StPriceCloseTxt.Text = selectedRow["StockPriceClose"].ToString(); ;
                StPriceHighTxt.Text = selectedRow["StockPriceHigh"].ToString(); ;
                StPriceLowTxt.Text = selectedRow["StockPriceIo"].ToString(); ;
                StPriceOpenTxt.Text = selectedRow["StockPriceOpen"].ToString(); ;
                StPriceAdjCloseTxt.Text = selectedRow["StockPriceAdjClose"].ToString(); ;
                StVolumeTxt.Text = selectedRow["StockVolume"].ToString(); ;
                StExchangeTxt.Text = selectedRow["StockExchange"].ToString(); ;
                DateTxt.Text = selectedRow["Date"].ToString();
                //StockDatePicker.SelectedDate = DateTime.Parse(selectedRow["Date"]);
            }

        }

        //Delete button functionality
        private void DelBtn_Click_1(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=ELLIE\SQLEXPRESS;Initial Catalog=StockProj;Integrated Security=True");
            con.Open();
            SqlCommand sc = new SqlCommand(@"DELETE FROM  [dbo].[Test] Where [StockID] = '" + StIdTxt.Text + "'  ",con);
            sc.ExecuteNonQuery();
            con.Close();
            resetData();
            loadData();
        }
        //Update button functionality

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=ELLIE\SQLEXPRESS;Initial Catalog=StockProj;Integrated Security=True");
            con.Open();
            SqlCommand sc = new SqlCommand(@"UPDATE [dbo].[Test] SET [StockID] ='" + StIdTxt.Text + "' ,[date] = '" +Convert.ToDateTime( DateTxt.Text) + "',[StockSymbol] = '" + StSymbolTxt.Text + "',[StockPriceOpen] = '" + StPriceOpenTxt.Text + "',[StockPriceClose] = '" + StPriceCloseTxt.Text + "',[StockPriceIo] = '" + StPriceLowTxt.Text + "',[StockPriceHigh] = '" + StPriceHighTxt.Text + "',[StockPriceAdjClose] = '" + StPriceAdjCloseTxt.Text + "', [StockVolume] = '" + StVolumeTxt.Text + "',[StockExchange] = '" + StExchangeTxt.Text + "'Where [StockID] = '" + StIdTxt.Text + "'", con);
            sc.ExecuteNonQuery();
            con.Close();
            resetData();
            loadData();

        }
    }
}

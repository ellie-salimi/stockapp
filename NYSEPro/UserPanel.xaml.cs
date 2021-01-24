using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
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
using Newtonsoft.Json;
using System.Net.Http;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using Microsoft.Win32;
using System.Collections;
using System.Windows.Controls.Primitives;

namespace NYSEPro
{
    /// <summary>
    /// Interaction logic for User_Panel.xaml
    /// </summary>
    public partial class User_Panel : Window
    {
        HttpClient client = new HttpClient();

        public User_Panel()
        {
            InitializeComponent();
        }
        private DataTable splitString(string jsonString)
        {
            string[] json = jsonString.Split('>');
            string[] finalJson = json[2].Split('<');
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(finalJson[0]);
            return dt;
        }
        private void WebServicesSetting()
        {
            client.BaseAddress = new Uri("https://localhost:44385/clientAPI.asmx/");
        }

        private void SearchBtn_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            WebServicesSetting();
            // serach by symbol
            //HttpResponseMessage message = client.GetAsync("usersDT?symbol=" + textBox1.Text + "").Result;
            //string userJson = message.Content.ReadAsStringAsync().Result;
            //MessageBox.Show(userJson);
            //var table = splitString(userJson);
            //UserDataGrid.ItemsSource = table.DefaultView; 

            //Search by date
            DateTime fromDate = DateTime.Parse(FromTxt.Text);
            DateTime toDate = DateTime.Parse(ToTxt.Text);
            HttpResponseMessage message = client.GetAsync("UsersDateSearch?startDate=" + fromDate + "&endDate=" + toDate + "").Result;
            string userJson = message.Content.ReadAsStringAsync().Result;


            MessageBox.Show(userJson);
            //var table = JsonConvert.DeserializeObject<DataTable>(userJson);
            var table = splitString(userJson);
            UserDataGrid.ItemsSource = table.DefaultView; ;



        }
      

        
        
        void SaveBtn_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // ------------< btnApp_ScreenShot_as_File_Click() > ------------

save_FrameworkElement_as_Screenshot_File(this);

            //------------</ btnApp_ScreenShot_as_File_Click() >------------

        }





        private void save_FrameworkElement_as_Screenshot_File(FrameworkElement element)

        {

            //------------< save_FrameworkElement_as_Screenshot_File() >------------

            //< init >

            String filename = "‪C:\\Users\\Ellie\\Desktop\\test\\ellie-" + DateTime.Now.ToString("ddMMyyyy-hhmmss") + ".png";

            //</ init >



            //< get Screenshot of Element >

            RenderTargetBitmap bmp = new RenderTargetBitmap((int)element.ActualWidth, (int)element.ActualHeight, 96, 96, PixelFormats.Pbgra32);

            bmp.Render(element);

            //</ get Screenshot of Element >



            //< create Encoder >

            PngBitmapEncoder encoder = new PngBitmapEncoder();

            encoder.Frames.Add(BitmapFrame.Create(bmp));

            //</ create Encoder >



            //< save >

            FileStream fs = new FileStream(filename, FileMode.Create);

            encoder.Save(fs);

            fs.Close();

            //</ save >

            //------------</ save_FrameworkElement_as_Screenshot_File() >-
            //SaveFileDialog saveFileDialog = new SaveFileDialog();
            //saveFileDialog.DefaultExt = ".txt";
            //saveFileDialog.Filter = "Text documents (.txt)|*.txt";
            //if (saveFileDialog.ShowDialog() == true)
            //    File.WriteAllText(saveFileDialog.FileName, UserDataGrid.Items.SourceCollection.ToString());
            //if (UserDataGrid.Items.Count > 0)
            //{
            //    SaveFileDialog sfd = new SaveFileDialog();
            //    sfd.Filter = "PDF (*.pdf)|*.pdf";
            //    sfd.FileName = "Output.pdf";
            //    bool fileError = false;
            //    Nullable<bool> result = sfd.ShowDialog();
            //    //MessageBoxResult result = MessageBox.Show("Insert with Logo?", "Logo", MessageBoxButton.YesNoCancel);

            //    if (result == true)
            //    {
            //        if (File.Exists(sfd.FileName))
            //        {
            //            try
            //            {
            //                File.Delete(sfd.FileName);
            //            }
            //            catch (IOException ex)
            //            {
            //                fileError = true;
            //                MessageBox.Show("It wasn't possible to write the data to the disk." + ex.Message);
            //            }
            //        }
            //        if (!fileError)
            //        {
            //            try
            //            {
            //                PdfPTable pdfTable = new PdfPTable(UserDataGrid.Columns.Count);
            //                pdfTable.DefaultCell.Padding = 3;
            //                pdfTable.WidthPercentage = 100;
            //                pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

            //                foreach (DataGridColumn column in UserDataGrid.Columns)
            //                {
            //                    PdfPCell cell = new PdfPCell(new Phrase(column.HeaderStringFormat));
            //                    pdfTable.AddCell(cell);
            //                }

            //                foreach (DataGridRow row in UserDataGrid.Items)
            //                {
            //                    //foreach (DataGridCell cell in row.Selected)
            //                    //{
            //                    //    if (cell.Content == null)
            //                    //    {
            //                    //        pdfTable.AddCell("0");
            //                    //    }
            //                    //    else
            //                    //    {
            //                    //        pdfTable.AddCell(cell.Content.ToString());

            //                    //    }
            //                    //}
            //                    int i = row.GetIndex();
            //                    pdfTable.GetRow(i).ToString();
            //                }

            //                using (FileStream stream = new FileStream(sfd.FileName, FileMode.Create))
            //                {
            //                    Document pdfDoc = new Document(PageSize.A4, 10f, 20f, 20f, 10f);
            //                    PdfWriter.GetInstance(pdfDoc, stream);
            //                    pdfDoc.Open();
            //                    pdfDoc.Add(pdfTable);
            //                    pdfDoc.Close();
            //                    stream.Close();
            //                }

            //                MessageBox.Show("Data Exported Successfully !!!", "Info");
            //            }
            //            catch (Exception ex)
            //            {
            //                MessageBox.Show("Error :" + ex.Message);
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("No Record To Export !!!", "Info");
            //}
        }
    }
}
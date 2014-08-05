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
using System.Configuration;
using CRM_BAL;
using CRM_DAL;

namespace CRM_User_Interface
{
    /// <summary>
    /// Interaction logic for ADD_Tax.xaml
    /// </summary>
    public partial class ADD_Tax : Window
    {
        public SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["ConstCRM"].ToString());
        SqlCommand cmd;

        public ADD_Tax()
        {
            InitializeComponent();
        }
        BAL_Tax baltax = new BAL_Tax();
        DAL_Tax daltax = new DAL_Tax();
        private void btnTaxMain_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnTax_AddTax_Click(object sender, RoutedEventArgs e)
        {
            if(btnTax_AddTax .Content =="Add Tax")
            {
                baltax.Flag = 1;
                baltax.Tax_Type = txtTax_TName.Text;
                baltax.Tax_Percentage =Convert.ToDouble ( txtTax_TPercent.Text);
                baltax.S_Status = "Active";
                baltax.C_Date = System.DateTime.Now.ToShortDateString();
            }
        }
    }
}

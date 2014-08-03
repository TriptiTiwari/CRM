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

namespace CRM_User_Interface
{
    /// <summary>
    /// Interaction logic for frmCRM_DealerDetailsEdit.xaml
    /// </summary>
    public partial class frmCRM_DealerDetailsEdit : Window
    {
        public SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["ConstCRM"].ToString());
        SqlCommand cmd;
        SqlDataReader dr;
        string caption = "Green Future Glob";
        static int PK_ID;

        public frmCRM_DealerDetailsEdit()
        {
            InitializeComponent();
            //FillData();
        }

        public void DealerID(string id)
        {
            txtDealerID.Text = id;
        }

        public void FillData()
        {
            try
            {
                con.Open();
                string sqlquery = "SELECT * FROM tbl_DealerEntry where ID='" + txtDealerID.Text + "' ";
                SqlCommand cmd = new SqlCommand(sqlquery, con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    lbl_Edit_DealerID.Content = dt.Rows[0]["DealerEntryID"].ToString();
                    txtAdmEdit_CompanyName.Text = dt.Rows[0]["CompanyName"].ToString();
                    txtAdmEdit_DealerFirstName.Text = dt.Rows[0]["DealerFirstName"].ToString();
                    txtAdmEdit_DealerLastName.Text = dt.Rows[0]["DealerLaseName"].ToString();
                    dtpAdmEdit_Dealer_DOB.SelectedDate = Convert.ToDateTime(dt.Rows[0]["DateOfBirth"].ToString());
                    txtAdmEdit_Dealer_MobileNo.Text = dt.Rows[0]["MobileNo"].ToString();
                    txtAdmEdit_Dealer_PhoneNo.Text = dt.Rows[0]["PhoneNo"].ToString();
                    txtAdmEdit_Dealer_Address.Text = dt.Rows[0]["DealerAddress"].ToString();
                    txtAdmEdit_Dealer_City.Text = dt.Rows[0]["City"].ToString();
                    txtAdmEdit_Dealer_Zip.Text = dt.Rows[0]["Zip"].ToString();
                    txtAdmEdit_Dealer_State.Text = dt.Rows[0]["DState"].ToString();
                    txtAdmEdit_Dealer_Country.Text = dt.Rows[0]["Country"].ToString();
                }
            }
            catch(Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            btnAdmEdit_Dealer_Save.Content = "Update";
        }

        #region Button Event
        private void btnAdmEdit_Dealer_Save_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAdmEdit_Dealer_Clear_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAdmEdit_Dealer_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion Button Event
    }
}

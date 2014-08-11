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
using System.Globalization;
using Microsoft.Win32;
using CRM_BAL;
using CRM_DAL;


namespace CRM_User_Interface
{
    /// <summary>
    /// Interaction logic for CRM_AdminDashbord.xaml
    /// </summary>
    public partial class CRM_AdminDashbord : Window
    {
        NumberFormatInfo nfi = CultureInfo.CurrentCulture.NumberFormat;
        public SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["ConstCRM"].ToString());
        SqlCommand cmd;
        SqlDataReader dr;
        string caption = "Green Future Glob";

        static int PK_ID;

        public CRM_AdminDashbord()
        {
            InitializeComponent();
        }
        BAL_EmployeeEntry bempetr = new BAL_EmployeeEntry();
        DAL_EmployeeEntry dempetr = new DAL_EmployeeEntry();
        BAL_DealerEntry bdealeretr = new BAL_DealerEntry();
        DAL_DealerEntry ddealeretr = new DAL_DealerEntry();
        BAL_StockDetails bstockDet = new BAL_StockDetails();
        DAL_StockDetails dstockDet = new DAL_StockDetails();
        DAL_StaockDetailsUpdate dstUpdate = new DAL_StaockDetailsUpdate();
        BAL_FinalDealer bfinaldealer1 = new BAL_FinalDealer();
        DAL_FinalDealer dfinaldealer = new DAL_FinalDealer();
        DAL_StockAddQty daddqty = new DAL_StockAddQty();
        DAL_FinalDealerUpdate dFup = new DAL_FinalDealerUpdate();

        private void btnadminexit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void smaddproducts_Click(object sender, RoutedEventArgs e)
        {
           
        }

        #region Employee Function
        #region EmployeeEntry Button Event
        private void btnAdm_Emp_Save_Click(object sender, RoutedEventArgs e)
        {
            if (Employee_Validation() == true)
                return;

            try
            {
                bempetr.Flag = 1;
                bempetr.EmployeeID = lblEmpID.Content.ToString();
                bempetr.EmployeeName = txtAdm_EmpName.Text;
                bempetr.DateOfBirth = Convert.ToDateTime(dtpAdm_Emp_DOB.SelectedDate);
                bempetr.EmpAddress = txtAdm_Emp_Address.Text;
                bempetr.MobileNo = txtAdm_Emp_MobileNo.Text;
                bempetr.PhoneNo = txtAdm_Emp_PhoneNo.Text;
                bempetr.Designation = txtAdm_Emp_Designation.Text;
                bempetr.DateOfJoining = Convert.ToDateTime(dtpAdm_Emp_DOJ.SelectedDate);
                bempetr.NoOfYears = cmbAdm_Emp_YearExp.SelectedItem.ToString();
                bempetr.Years = lblYears.Content.ToString();
                bempetr.NoOfMonths = cmbAdm_Emp_Months.SelectedItem.ToString();
                bempetr.Months = lblMonths.Content.ToString();
                bempetr.Salary = Convert.ToDouble(txtAdm_Emp_Salary.Text);
                bempetr.S_Status = "Active";

                //string STRTODAYDATE = System.DateTime.Now.ToShortDateString();
                //string time = System.DateTime.Now.ToShortTimeString();
                //string[] STRVAL = STRTODAYDATE.Split('-');
                //string STR_DATE1 = STRVAL[0];
                //string STR_MONTH = STRVAL[1];
                //string STR_YEAR = STRVAL[2];
                //string DATE = STR_DATE1 + "-" + STR_MONTH + "-" + STR_YEAR;
                ////txtdate.Text = DATE;
                ////txttime.Text = time;

                //baddprd.C_Date =Convert .ToDateTime( DATE);
                bempetr.C_Date = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
                dempetr.EmployeeEntry_Insert_Update_Delete(bempetr);
                MessageBox.Show("Data Save Successfully");
                ResetText();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }

            EEMPLOYEEid();
        }

        private void btnAdm_Emp_Clear_Click(object sender, RoutedEventArgs e)
        {
            ResetText();
        }

        private void smemployee_Click(object sender, RoutedEventArgs e)
        {
            grd_EmployeeDetails.Visibility = System.Windows.Visibility.Visible;
            EEMPLOYEEid();
            LoadNoOfYears();
            LoadNoOfMonths();
        }

        private void btnAdm_Emp_Exit_Click(object sender, RoutedEventArgs e)
        {
            grd_EmployeeDetails.Visibility = System.Windows.Visibility.Hidden;

        }
        #endregion EmployeeEntry Button Event

        #region EmployeeEntry Function
        public bool Employee_Validation()
        {
            bool result = false;
            if (txtAdm_EmpName.Text == "")
            {
                result = true;
                MessageBox.Show("Please Enter Employee Name", caption, MessageBoxButton.OK);
            }
            else if (txtAdm_Emp_Address.Text == "")
            {
                result = true;
                MessageBox.Show("Please Enter Employee Address", caption, MessageBoxButton.OK);
            }
            else if (txtAdm_Emp_MobileNo.Text == "")
            {
                result = true;
                MessageBox.Show("Please Enter Employee Mobile No", caption, MessageBoxButton.OK);
            }
            else if (txtAdm_Emp_Designation.Text == "")
            {
                result = true;
                MessageBox.Show("Please Enter Employee Designation", caption, MessageBoxButton.OK);
            }
            else if (dtpAdm_Emp_DOJ.Text == "")
            {
                result = true;
                MessageBox.Show("Please Select Employee Joining Date", caption, MessageBoxButton.OK);
            }
            else if (cmbAdm_Emp_YearExp.SelectedItem == null)
            {
                result = true;
                MessageBox.Show("Please Select Employee Experience Year", caption, MessageBoxButton.OK);
            }
            else if (cmbAdm_Emp_Months.SelectedItem == null)
            {
                result = true;
                MessageBox.Show("Please Select Employee Experience Month", caption, MessageBoxButton.OK);
            }
            else if (txtAdm_Emp_Salary.Text == "")
            {
                result = true;
                MessageBox.Show("Please Enter Employee Salary", caption, MessageBoxButton.OK);
            }
            return result;
        }

        public void ResetText()
        {
            //txtAdm_EmpID.Text = "";
            txtAdm_EmpName.Text = "";
            txtAdm_Emp_MobileNo.Text = "";
            txtAdm_Emp_PhoneNo.Text = "";
            txtAdm_Emp_Designation.Text = "";
            //txtAdm_Emp_Experience.Text = "";
            txtAdm_Emp_Salary.Text = "";
            dtpAdm_Emp_DOB.SelectedDate = null;
            dtpAdm_Emp_DOJ.SelectedDate = null;
            cmbAdm_Emp_YearExp.Text = "Select Year";
            cmbAdm_Emp_Months.Text = "Select Months";
            txtAdm_Emp_Address.Text = "";
            cmbAdm_Emp_Months.Visibility = System.Windows.Visibility.Hidden;
            lblMonths.Visibility = System.Windows.Visibility.Hidden;
        }

        public void LoadNoOfYears()
        {
            cmbAdm_Emp_YearExp.Text = "Select Year";
            cmbAdm_Emp_YearExp.Items.Add("0");
            cmbAdm_Emp_YearExp.Items.Add("1");
            cmbAdm_Emp_YearExp.Items.Add("2");
            cmbAdm_Emp_YearExp.Items.Add("3");
            cmbAdm_Emp_YearExp.Items.Add("4");
            cmbAdm_Emp_YearExp.Items.Add("5");
            cmbAdm_Emp_YearExp.Items.Add("6");
            cmbAdm_Emp_YearExp.Items.Add("7");
            cmbAdm_Emp_YearExp.Items.Add("8");
            cmbAdm_Emp_YearExp.Items.Add("9");
            cmbAdm_Emp_YearExp.Items.Add("10");
            cmbAdm_Emp_YearExp.Items.Add("11");
            cmbAdm_Emp_YearExp.Items.Add("12");
            cmbAdm_Emp_YearExp.Items.Add("13");
            cmbAdm_Emp_YearExp.Items.Add("14");
            cmbAdm_Emp_YearExp.Items.Add("15");
        }

        public void LoadNoOfMonths()
        {
            cmbAdm_Emp_Months.Text = "Select Months";
            cmbAdm_Emp_Months.Items.Add("0");
            cmbAdm_Emp_Months.Items.Add("1");
            cmbAdm_Emp_Months.Items.Add("2");
            cmbAdm_Emp_Months.Items.Add("3");
            cmbAdm_Emp_Months.Items.Add("4");
            cmbAdm_Emp_Months.Items.Add("5");
            cmbAdm_Emp_Months.Items.Add("6");
            cmbAdm_Emp_Months.Items.Add("7");
            cmbAdm_Emp_Months.Items.Add("8");
            cmbAdm_Emp_Months.Items.Add("9");
            cmbAdm_Emp_Months.Items.Add("10");
            cmbAdm_Emp_Months.Items.Add("11");
        }

        public void EEMPLOYEEid()
        {

            int id1 = 0;
            // SqlConnection con = new SqlConnection(constring);
            con.Open();
            SqlCommand cmd = new SqlCommand("select (COUNT(ID)) from tbl_Employee", con);
            id1 = Convert.ToInt32(cmd.ExecuteScalar());
            id1 = id1 + 1;
            lblEmpID.Content = "# Emp /" + id1.ToString();
            con.Close();


        }

        private void cmbAdm_Emp_YearExp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbAdm_Emp_Months.Visibility = System.Windows.Visibility.Visible;
            lblMonths.Visibility = System.Windows.Visibility.Visible;
        }
        #endregion EmployeeEntry Function

        #region EmployeeEdit Fun
        public void GetData_EmployeeDetails()
        {
            try
            {
                String str;
                //con.Open();
                DataSet ds = new DataSet();
                str = "SELECT [ID],[EmployeeID],[EmployeeName],[DateOfBirth],[EmpAddress],[MobileNo],[Designation],[DateOfJoining],[NoOfYears] + ' ' + [Years] + ' , ' + [NoOfMonths] + ' ' + [Months] AS [Experience],[Salary] " +
                      "FROM [tbl_Employee] " +
                      "WHERE ";
                if (txtAdm_EmployeeName_Search.Text.Trim() != "")
                {
                    str = str + "[EmployeeName] LIKE ISNULL('" + txtAdm_EmployeeName_Search.Text.Trim() + "',[EmployeeName]) + '%' AND ";
                }
                if (txtAdm_EmployeeMN_Search.Text.Trim() != "")
                {
                    str = str + "[MobileNo] LIKE ISNULL('" + txtAdm_EmployeeMN_Search.Text.Trim() + "',[MobileNo]) + '%' AND ";
                }
                str = str + " [S_Status] = 'Active' ORDER BY [EmployeeName] ASC ";
                //str = str + " S_Status = 'Active' ";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                //if (ds.Tables[0].Rows.Count > 0)
                //{
                dgvAdm_EmployeeDetails.ItemsSource = ds.Tables[0].DefaultView;
                //}
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        private void smemployeedetails_Click(object sender, RoutedEventArgs e)
        {
            grd_EmployeeDet.Visibility = System.Windows.Visibility.Visible;
            GetData_EmployeeDetails();
        }
        #endregion EmployeeEdit Fun

        #region EmployeeEdit Button Event
        private void btnAdm_EmployeeExit_Click(object sender, RoutedEventArgs e)
        {
            grd_EmployeeDet.Visibility = System.Windows.Visibility.Hidden;
        }

        private void btndgv_EmployeeDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var id1 = (DataRowView)dgvAdm_EmployeeDetails.SelectedItem;  //Get specific ID From DataGrid after click on Delete Button.

                PK_ID = Convert.ToInt32(id1.Row["Id"].ToString());
                //SqlConnection con = new SqlConnection(sqlstring);
                con.Open();
                string sqlquery = "UPDATE tbl_Employee SET S_Status='DeActive' where ID='" + PK_ID + "' ";
                SqlCommand cmd = new SqlCommand(sqlquery, con);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Data Deleted Successfully...", caption, MessageBoxButton.OK, MessageBoxImage.Error);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            GetData_EmployeeDetails();
        }

        private void btndgv_EmployeeEditUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var id1 = (DataRowView)dgvAdm_EmployeeDetails.SelectedItem; //get specific ID from          DataGrid after click on Edit button in DataGrid   
                PK_ID = Convert.ToInt32(id1.Row["Id"].ToString());
                con.Open();
                string sqlquery = "SELECT * FROM tbl_Employee where Id='" + PK_ID + "' ";
                SqlCommand cmd = new SqlCommand(sqlquery, con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    txtAdm_EmployeeID.Text = dt.Rows[0]["ID"].ToString();
                }

                frmCRM_EmpDetailsEdit obj = new frmCRM_EmpDetailsEdit();
                obj.EmployeeID(txtAdm_EmployeeID.Text.Trim());
                obj.FillData();
                obj.LoadNoOfYears1();
                obj.LoadNoOfMonths1();
                obj.ShowDialog();

                // con.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            GetData_EmployeeDetails();
        }
        #endregion EmployeeEdit Button Event

        #region EmployeeEdit Event
        private void txtAdm_EmployeeName_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            GetData_EmployeeDetails();
        }

        private void txtAdm_EmployeeMN_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            GetData_EmployeeDetails();
        }
        #endregion EmployeeEdit Event
        #endregion Employee Function

        #region Dealer Function

        #region Dealer Button Event
        private void btnAdm_Dealer_Save_Click(object sender, RoutedEventArgs e)
        {
            if (Dealer_Validation() == true)
                return;

            try
            {
                bdealeretr.Flag = 1;
                bdealeretr.DealerEntryID = lblDealerID.Content.ToString();
                bdealeretr.CompanyName = txtAdm_CompanyName.Text;
                bdealeretr.DealerFirstName = txtAdm_DealerFirstName.Text;
                bdealeretr.DealerLastName = txtAdm_DealerLastName.Text;
                bdealeretr.DateOfBirth = Convert.ToDateTime(dtpAdm_Dealer_DOB.SelectedDate);
                bdealeretr.MobileNo = txtAdm_Dealer_MobileNo.Text;
                bdealeretr.PhoneNo = txtAdm_Dealer_PhoneNo.Text;
                bdealeretr.DealerAddress = txtAdm_Dealer_Address.Text;
                bdealeretr.City = txtAdm_Dealer_City.Text;
                bdealeretr.Zip = txtAdm_Dealer_Zip.Text;
                bdealeretr.DState = txtAdm_Dealer_State.Text;
                bdealeretr.Country = txtAdm_Dealer_Country.Text;
                bdealeretr.S_Status = "Active";

                //string STRTODAYDATE = System.DateTime.Now.ToShortDateString();
                //string time = System.DateTime.Now.ToShortTimeString();
                //string[] STRVAL = STRTODAYDATE.Split('-');
                //string STR_DATE1 = STRVAL[0];
                //string STR_MONTH = STRVAL[1];
                //string STR_YEAR = STRVAL[2];
                //string DATE = STR_DATE1 + "-" + STR_MONTH + "-" + STR_YEAR;
                ////txtdate.Text = DATE;
                ////txttime.Text = time;

                //baddprd.C_Date =Convert .ToDateTime( DATE);
                bdealeretr.C_Date = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
                ddealeretr.EmployeeEntry_Insert_Update_Delete(bdealeretr);
                MessageBox.Show("Data Save Successfully");
                Dealer_ResetText();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            Dealerid();
        }
        
        private void btnAdm_Dealer_Clear_Click(object sender, RoutedEventArgs e)
        {
            Dealer_ResetText();
        }

        private void smdealerentry_Click(object sender, RoutedEventArgs e)
        {
            grd_DealerEntry.Visibility = System.Windows.Visibility.Visible;
            Dealerid();
        }

        private void btnAdm_Dealer_Exit_Click(object sender, RoutedEventArgs e)
        {
            grd_DealerEntry.Visibility = System.Windows.Visibility.Hidden;
        }

        private void btnAdm_DealerRefresh_Click(object sender, RoutedEventArgs e)
        {
            txtAdm_CompName_Search.Text = "";
            txtAdm_DealerMN_Search.Text = "";
            txtAdm_DealerName_Search.Text = "";
            DealerDetails_LoadData();
        }
        
        private void btndgv_DealerEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var id1 = (DataRowView)dgvAdm_Dealerdetails.SelectedItem; //get specific ID from          DataGrid after click on Edit button in DataGrid   
                PK_ID = Convert.ToInt32(id1.Row["Id"].ToString());
                con.Open();
                string sqlquery = "SELECT * FROM tbl_DealerEntry where Id='" + PK_ID + "' ";
                SqlCommand cmd = new SqlCommand(sqlquery, con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    txtAdm_DealerID1.Text = dt.Rows[0]["ID"].ToString();
                }

                frmCRM_DealerDetailsEdit obj = new frmCRM_DealerDetailsEdit();
                obj.DealerID(txtAdm_DealerID1.Text.Trim());
                obj.FillData();
                obj.ShowDialog();
                
               // con.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            DealerDetails_LoadData();
        }

        private void btndgv_DealerDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var id1 = (DataRowView)dgvAdm_Dealerdetails.SelectedItem;  //Get specific ID From DataGrid after click on Delete Button.

                PK_ID = Convert.ToInt32(id1.Row["Id"].ToString());
                //SqlConnection con = new SqlConnection(sqlstring);
                con.Open();
                string sqlquery = "UPDATE tbl_DealerEntry SET S_Status='DeActive' where ID='" + PK_ID + "' ";
                SqlCommand cmd = new SqlCommand(sqlquery, con);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Data Deleted Successfully...", caption, MessageBoxButton.OK);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            DealerDetails_LoadData();
        }

        private void btnAdm_DealerExit_Click(object sender, RoutedEventArgs e)
        {
            grd_DealerDetails.Visibility = System.Windows.Visibility.Hidden;
        }

        private void btnAdm_FinalProcurment_Click(object sender, RoutedEventArgs e)
        {
            grd_FinalProcurment.Visibility = System.Windows.Visibility.Hidden;
        }

        private void smviewprocurement_Click(object sender, RoutedEventArgs e)
        {
            grd_FinalProcurment.Visibility = System.Windows.Visibility.Visible;
            LoadFinal();
            Final_PreProcurement();
        }

        private void txtAdm_Dealer_Filter_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Final_PreProcurement();
        }

        private void dtpAdmTo_Dealer_Search_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Final_PreProcurement();
        }

        private void dtpAdmBetween_Dealer_Search_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Final_PreProcurement();
        }

        private void btnAdm_FinalRefresh_Click(object sender, RoutedEventArgs e)
        {
            dtpAdmTo_Dealer_Search.SelectedDate = null;
            dtpAdmBetween_Dealer_Search.SelectedDate = null;
            cmbAdm_DealerFilter_Search.Text = "Select";
            txtAdm_Dealer_Filter_Search.Text = "";
            grd_FinalizeProducts.Visibility = System.Windows.Visibility.Hidden;
        }
        #endregion Dealer Button Event

        #region Dealer Fun
        public bool Dealer_Validation()
        {
            bool result = false;
            if(txtAdm_CompanyName.Text == "")
            {
                result = true;
                MessageBox.Show("Please Enter Company Name", caption, MessageBoxButton.OK);
            }
            else if (txtAdm_DealerFirstName.Text == "")
            {
                result = true;
                MessageBox.Show("Please Enter Dealer First Name", caption, MessageBoxButton.OK);
            }
            else if (txtAdm_DealerLastName.Text == "")
            {
                result = true;
                MessageBox.Show("Please Enter Dealer Last Name", caption, MessageBoxButton.OK);
            }
            else if (dtpAdm_Dealer_DOB.Text == "")
            {
                result = true;
                MessageBox.Show("Please Select Dealer Date Of Birth", caption, MessageBoxButton.OK);
            }
            else if (txtAdm_Dealer_MobileNo.Text == "")
            {
                result = true;
                MessageBox.Show("Please Enter Dealer Mobile No", caption, MessageBoxButton.OK);
            }
            else if(txtAdm_Dealer_PhoneNo.Text == "")
            {
                result = true;
                MessageBox.Show("Please Enter Dealer Phone No", caption, MessageBoxButton.OK);
            }
            else if (txtAdm_Dealer_Address.Text == "")
            {
                result = true;
                MessageBox.Show("Please Enter Dealer Address", caption, MessageBoxButton.OK);
            }
            return result;
        }

        public void Dealer_ResetText()
        {
            txtAdm_CompanyName.Text = "";
            txtAdm_DealerFirstName.Text = "";
            txtAdm_DealerLastName.Text = "";
            dtpAdm_Dealer_DOB.SelectedDate = null;
            txtAdm_Dealer_MobileNo.Text = "";
            txtAdm_Dealer_PhoneNo.Text = "";
            txtAdm_Dealer_Address.Text = "";
            txtAdm_Dealer_City.Text = "";
            txtAdm_Dealer_Zip.Text = "";
        }

        public void Dealerid()
        {

            int id1 = 0;
            // SqlConnection con = new SqlConnection(constring);
            con.Open();
            SqlCommand cmd = new SqlCommand("select (COUNT(ID)) from tbl_DealerEntry", con);
            id1 = Convert.ToInt32(cmd.ExecuteScalar());
            id1 = id1 + 1;
            lblDealerID.Content = "# Dealer /" + id1.ToString();
            con.Close();


        }
       
        public void DealerDetails_LoadData()
        {
            try
            {
                String str;
                //con.Open();
                DataSet ds = new DataSet();
                str = "SELECT [ID],[DealerEntryID],[CompanyName],[DealerFirstName] + ' ' + [DealerLastName] AS [DealerName],[DateOfBirth],[MobileNo],[PhoneNo],[DealerAddress] " +
                             "FROM [tbl_DealerEntry] " +
                             "WHERE ";
                if (txtAdm_CompName_Search.Text.Trim() != string.Empty)
                {
                    str = str + "[CompanyName] LIKE ISNULL('" + txtAdm_CompName_Search.Text.Trim() + "',CompanyName) + '%' AND ";
                }
                if (txtAdm_DealerName_Search.Text.Trim() != string.Empty)
                {
                    str = str + "[DealerFirstName] LIKE ISNULL('" + txtAdm_DealerName_Search.Text.Trim() + "',DealerFirstName) + '%' AND ";
                }
                if (txtAdm_DealerMN_Search.Text.Trim() != string.Empty)
                {
                    str = str + "[MobileNo] LIKE ISNULL('" + txtAdm_DealerMN_Search.Text.Trim() + "',MobileNo) + '%' AND ";
                }
                str = str + " S_Status = 'Active' ORDER BY DealerName ASC ";
                SqlCommand cmd = new SqlCommand(str,con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                //if (ds.Tables[0].Rows.Count > 0)
                //{
                    dgvAdm_Dealerdetails.ItemsSource = ds.Tables[0].DefaultView;
                //}
            }
            catch(Exception)
            {
                throw;
            }
            finally 
            { 
                con.Close(); 
            }
        }
        #endregion Fun

        #region Dealer Event
        private void smdealerDetails_Click(object sender, RoutedEventArgs e)
        {
            grd_DealerDetails.Visibility = System.Windows.Visibility.Visible;
            DealerDetails_LoadData();
        }

        private void txtAdm_DealerName_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            DealerDetails_LoadData();
        }

        private void txtAdm_DealerMN_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            DealerDetails_LoadData();
        }

        private void txtAdm_CompName_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            DealerDetails_LoadData();
        }

        private void dgvAdm_Dealerdetails_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
        #endregion Dealer Event

        #endregion Function

        #region Final Pro
        private bool FinalPro_Validation()
        {
            bool result = false;
            if (txtPrice.Text == "")
            {
                result = true;
                MessageBox.Show("Please Enter Price", caption, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (txtQuantity.Text == "")
            {
                result = true;
                MessageBox.Show("Please Enter Quantity", caption, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (dtpFinalDate.Text == "")
            {
                result = true;
                MessageBox.Show("Please Select Date", caption, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            return result;
        }
        
        public void Final_PreProcurement()
        {
            try
            {
                String str;
                //con.Open();
                DataSet ds = new DataSet();
                str = "SELECT P.[ID],P.[DealerID],P.[Domain_ID],P.[Product_ID],P.[Brand_ID],P.[P_Category],P.[Model_No_ID],P.[Color_ID],P.[Warranty],P.[Quantity],P.[C_Date] " +
                      ",D.[DealerFirstName] + '' + D.[DealerLastName] AS [DealerName],D.[MobileNo],D.[PhoneNo] " +
                      ",DM.[Domain_Name] + ' , ' +  PM.[Product_Name] + ' , ' + B.[Brand_Name] + ' , ' + PC.[Product_Category] + ' , ' + MN.[Model_No] + ' , ' + C.[Color] AS [Products]" +
                      ",PP.[Price] " +
                      "FROM [Pre_Procurement] P " +
                      "INNER JOIN [tbl_DealerEntry] D ON D.[ID] = P.[DealerID] " +
                      "INNER JOIN [tb_Domain] DM ON DM.[ID]=P.[Domain_ID] " +
                      "INNER JOIN [tlb_Products] PM ON PM.[ID]=P.[Product_ID] " +
                      "INNER JOIN [tlb_Brand] B ON B.[ID]=P.[Brand_ID] " +
                      "INNER JOIN [tlb_P_Category] PC ON PC.[ID]=P.[P_Category]" +
                      "INNER JOIN [tlb_Model] MN ON MN.[ID]=P.[Model_No_ID] " +
                      "INNER JOIN [tlb_Color] C ON C.[ID]=P.[Color_ID] " +
                      "INNER JOIN [Pre_Products] PP ON PP.[Model_No_ID]=P.[Model_No_ID] " +
                      "WHERE ";
                if ((dtpAdmTo_Dealer_Search.SelectedDate != null) && (dtpAdmBetween_Dealer_Search.SelectedDate != null))
                {
                    DateTime StartDate = Convert.ToDateTime(dtpAdmTo_Dealer_Search.Text.Trim() + " 00:00:00.000");
                    DateTime EndDate = Convert.ToDateTime(dtpAdmBetween_Dealer_Search.Text.Trim() + " 23:59:59.999");
                    str = str + "P.[C_Date] Between '" + StartDate + "' AND '" + EndDate + "'  AND ";
                }

                if (cmbAdm_DealerFilter_Search.Text.Equals("Domain"))
                {
                    if (txtAdm_Dealer_Filter_Search.Text.Trim() != "")
                    {
                        str = str + "DM.[Domain_Name] LIKE ISNULL('" + txtAdm_Dealer_Filter_Search.Text.Trim() + "',DM.[Domain_Name]) + '%' AND ";
                    }
                }
                if (cmbAdm_DealerFilter_Search.Text.Equals("Product Type"))
                {
                    if (txtAdm_Dealer_Filter_Search.Text.Trim() != "")
                    {
                        str = str + "PM.[Product_Name] LIKE ISNULL('" + txtAdm_Dealer_Filter_Search.Text.Trim() + "',PM.[Product_Name]) + '%' AND ";
                    }
                }
                if (cmbAdm_DealerFilter_Search.Text.Equals("Brand"))
                {
                    if (txtAdm_Dealer_Filter_Search.Text.Trim() != "")
                    {
                        str = str + "B.[Brand_Name] LIKE ISNULL('" + txtAdm_Dealer_Filter_Search.Text.Trim() + "',B.[Brand_Name]) + '%' AND ";
                    }
                }
                if (cmbAdm_DealerFilter_Search.Text.Equals("Product Category"))
                {
                    if (txtAdm_Dealer_Filter_Search.Text.Trim() != "")
                    {
                        str = str + "PC.[Product_Category] LIKE ISNULL('" + txtAdm_Dealer_Filter_Search.Text.Trim() + "',PC.[Product_Category]) + '%' AND ";
                    }
                }
                if (cmbAdm_DealerFilter_Search.Text.Equals("Model"))
                {
                    if (txtAdm_Dealer_Filter_Search.Text.Trim() != "")
                    {
                        str = str + "MN.[Model_No] LIKE ISNULL('" + txtAdm_Dealer_Filter_Search.Text.Trim() + "',MN.[Model_No]) + '%' AND ";
                    }
                }
                if (cmbAdm_DealerFilter_Search.Text.Equals("Color"))
                {
                    if (txtAdm_Dealer_Filter_Search.Text.Trim() != "")
                    {
                        str = str + "C.[Color] LIKE ISNULL('" + txtAdm_Dealer_Filter_Search.Text.Trim() + "',C.[Color]) + '%' AND ";
                    }
                }
                if (cmbAdm_DealerFilter_Search.Text.Equals("Products / Services"))
                {
                    if (txtAdm_Dealer_Filter_Search.Text.Trim() != "")
                    {
                        str = str + "[Products] LIKE ISNULL('" + txtAdm_Dealer_Filter_Search.Text.Trim() + "',[Products]) + '%' AND ";
                    }
                }
                str = str + " P.[S_Status] = 'Active' ORDER BY P.[C_Date] ASC ";
                //str = str + " S_Status = 'Active' ";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                //if (ds.Tables[0].Rows.Count > 0)
                //{
                dgvAdm_FinalProcurement.ItemsSource = ds.Tables[0].DefaultView;
                //}
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public void LoadFinal()
        {
            cmbAdm_DealerFilter_Search.Text = "Select";
            cmbAdm_DealerFilter_Search.Items.Add("Domain");
            cmbAdm_DealerFilter_Search.Items.Add("Product Type");
            cmbAdm_DealerFilter_Search.Items.Add("Brand");
            cmbAdm_DealerFilter_Search.Items.Add("Product Category");
            cmbAdm_DealerFilter_Search.Items.Add("Model");
            cmbAdm_DealerFilter_Search.Items.Add("Color");
            cmbAdm_DealerFilter_Search.Items.Add("Products / Services");
        }

        private bool CheckProduct()
        {
            try
            {
                bool result = false;
                string str = "SELECT * FROM [StockDetails] WHERE [S_Status] = 'Active' ";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows.Count > 0)
                    {
                        if (txtAdm_DomainID.Text.Trim() == dt.Rows[i]["Domain_ID"].ToString())
                        {
                            if (txtAdm_ProductID.Text.Trim() == dt.Rows[i]["Product_ID"].ToString())
                            {
                                if (txtAdm_BrandID.Text.Trim() == dt.Rows[i]["Brand_ID"].ToString())
                                {
                                    if (txtAdm_ProductCatID.Text.Trim() == dt.Rows[i]["P_Category"].ToString())
                                    {
                                        if (txtAdm_ModelID.Text.Trim() == dt.Rows[i]["Model_No_ID"].ToString())
                                        {
                                            if (txtAdm_ColorID.Text.Trim() == dt.Rows[i]["Color_ID"].ToString())
                                            {
                                                //if ((txtAdm_DomainID.Text.Trim() == dt.Rows[i]["Domain_ID"].ToString()) && (txtAdm_ProductID.Text.Trim() == dt.Rows[i]["Product_ID"].ToString()) &&
                                                //    (txtAdm_BrandID.Text.Trim() == dt.Rows[i]["Brand_ID"].ToString()) && (txtAdm_ProductCatID.Text.Trim() == dt.Rows[i]["P_Category"].ToString()) &&
                                                //    (txtAdm_ModelID.Text.Trim() == dt.Rows[i]["Model_No_ID"].ToString()) && (txtAdm_ColorID.Text.Trim() == dt.Rows[i]["Color_ID"].ToString()))
                                                //string qry = "Select [ID],[Domain_ID],[Product_ID],[Brand_ID],[P_Category],[Model_No_ID],[Color_ID] From [StockDetails] Where [Domain_ID] = '" + txtAdm_DomainID.Text.Trim() + "' And [Product_ID] = '" + txtAdm_ProductID.Text.Trim() + "' And [Brand_ID] = '" + txtAdm_BrandID.Text.Trim() + "' And [P_Category] = '" + txtAdm_ProductCatID.Text.Trim() + "' And [Model_No_ID] = '" + txtAdm_ModelID.Text.Trim() + "' And [Color_ID] = '" + txtAdm_ColorID.Text.Trim() + "' ";
                                                string qry = "Select [ID],[Products123] From [StockDetails] Where  [Products123] = '" + lblProducts.Content.ToString() + "' ";
                                                
                                                SqlCommand cmd1 = new SqlCommand(qry, con);
                                                SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
                                                DataTable dt1 = new DataTable();
                                                adp.Fill(dt1);
                                                if (dt.Rows.Count > 0)
                                                {
                                                    txtAdm_StockID.Text = dt.Rows[0]["ID"].ToString();
                                                }
                                                

                                                result = true;
                                                return result;
                                            }
                                            else
                                            {
                                                result = false;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public void Final_DealerDetails()
        {
            try
            {
                String str;
                //con.Open();
                DataSet ds = new DataSet();
                str = "SELECT P.[ID],P.[SalesID],P.[Dealer_ID],P.[Domain_ID],P.[Product_ID],P.[Brand_ID],P.[P_Category],P.[Model_No_ID],P.[Color_ID],P.[FinalQty],P.[NetAmt],P.[SDefault],P.[ServiceIntervalMonth],P.[C_Date] " +
                      ",D.[DealerFirstName] + '' + D.[DealerLastName] AS [DealerName],D.[MobileNo],D.[PhoneNo] " +
                      ",DM.[Domain_Name] + ' , ' +  PM.[Product_Name] + ' , ' + B.[Brand_Name] + ' , ' + PC.[Product_Category] + ' , ' + MN.[Model_No] + ' , ' + C.[Color] AS [Products]" +
                      ",PP.[Price] " +
                      "FROM [Final_DealerDetails] P " +
                      "INNER JOIN [tbl_DealerEntry] D ON D.[ID] = P.[Dealer_ID] " +
                      "INNER JOIN [tb_Domain] DM ON DM.[ID]=P.[Domain_ID] " +
                      "INNER JOIN [tlb_Products] PM ON PM.[ID]=P.[Product_ID] " +
                      "INNER JOIN [tlb_Brand] B ON B.[ID]=P.[Brand_ID] " +
                      "INNER JOIN [tlb_P_Category] PC ON PC.[ID]=P.[P_Category]" +
                      "INNER JOIN [tlb_Model] MN ON MN.[ID]=P.[Model_No_ID] " +
                      "INNER JOIN [tlb_Color] C ON C.[ID]=P.[Color_ID] " +
                      "INNER JOIN [Pre_Products] PP ON PP.[Model_No_ID]=P.[Model_No_ID] " +
                      "WHERE ";
                if ((dtpAdm_From_FinalDealer.SelectedDate != null) && (dtpAdm_To_FinalDealer.SelectedDate != null))
                {
                    DateTime StartDate = Convert.ToDateTime(dtpAdm_From_FinalDealer.Text.Trim() + " 00:00:00.000");
                    DateTime EndDate = Convert.ToDateTime(dtpAdm_To_FinalDealer.Text.Trim() + " 23:59:59.999");
                    str = str + "P.[C_Date] Between '" + StartDate + "' AND '" + EndDate + "'  AND ";
                }

                if (txtAdm_FDealerName_Search.Text.Trim() != "")
                {
                    str = str + "D.[DealerFirstName] LIKE ISNULL('" + txtAdm_FDealerName_Search.Text.Trim() + "',D.[DealerFirstName]) + '%' AND ";
                }

                if (txtAdm_Dealer_Filter_Search.Text.Trim() != "")
                {
                    str = str + "D.[MobileNo] LIKE ISNULL('" + txtAdm_FDealerMN_Search.Text.Trim() + "',D.[MobileNo]) + '%' AND ";
                }
                str = str + " P.[S_Status] = 'Active' ORDER BY P.[C_Date] ASC ";
                //str = str + " S_Status = 'Active' ";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                //if (ds.Tables[0].Rows.Count > 0)
                //{
                dgvAdm_FDealerDetails.ItemsSource = ds.Tables[0].DefaultView;
                //}
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        #region Final Product Event
        private void dgvAdm_FinalProcurement_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                //var id1 = (DataRowView)dgvAdm_FinalProcurement.SelectedItem; //get specific ID from          DataGrid after click on Edit button in DataGrid   
                //PK_ID = Convert.ToInt32(id1.Row["Id"].ToString());
                //con.Open();
                ////string sqlquery = "SELECT * FROM tbl_DealerEntry where Id='" + PK_ID + "' ";

                //string sqlquery = "SELECT P.[ID],P.[DealerID],P.[Domain_ID],P.[Product_ID],P.[Brand_ID],P.[P_Category],P.[Model_No_ID],P.[Color_ID],P.[Warranty],P.[Quantity],P.[C_Date],P.[Net_Amount] " +
                //      ",D.[DealerFirstName] + '' + D.[DealerLastName] AS [DealerName],D.[MobileNo],D.[PhoneNo] " +
                //      ",DM.[Domain_Name] + ' , ' +  PM.[Product_Name] + ' , ' + B.[Brand_Name] + ' , ' + PC.[Product_Category] + ' , ' + MN.[Model_No] + ' , ' + C.[Color] AS [Products]" +
                //      ",PP.[Price] " +
                //      "FROM [Pre_Procurement] P " +
                //      "INNER JOIN [tbl_DealerEntry] D ON D.[ID] = P.[DealerID] " +
                //      "INNER JOIN [tb_Domain] DM ON DM.[ID]=P.[Domain_ID] " +
                //      "INNER JOIN [tlb_Products] PM ON PM.[ID]=P.[Product_ID] " +
                //      "INNER JOIN [tlb_Brand] B ON B.[ID]=P.[Brand_ID] " +
                //      "INNER JOIN [tlb_P_Category] PC ON PC.[ID]=P.[P_Category]" +
                //      "INNER JOIN [tlb_Model] MN ON MN.[ID]=P.[Model_No_ID] " +
                //      "INNER JOIN [tlb_Color] C ON C.[ID]=P.[Color_ID] " +
                //      "INNER JOIN [Pre_Products] PP ON PP.[Model_No_ID]=P.[Model_No_ID] " +
                //      "WHERE P.[ID]='" + PK_ID + "' ";
       
                //SqlCommand cmd = new SqlCommand(sqlquery, con);
                //SqlDataAdapter adp = new SqlDataAdapter(cmd);
                //DataTable dt = new DataTable();
                //adp.Fill(dt);
                //if (dt.Rows.Count > 0)
                //{
                //    txtAdm_DealerID.Text = dt.Rows[0]["DealerID"].ToString();
                //    txtAdm_DomainID.Text = dt.Rows[0]["Domain_ID"].ToString();
                //    txtAdm_ProductID.Text = dt.Rows[0]["Product_ID"].ToString();
                //    txtAdm_BrandID.Text = dt.Rows[0]["Brand_ID"].ToString();
                //    txtAdm_ProductCatID.Text = dt.Rows[0]["P_Category"].ToString();
                //    txtAdm_ModelID.Text = dt.Rows[0]["Model_No_ID"].ToString();
                //    txtAdm_ColorID.Text = dt.Rows[0]["Color_ID"].ToString();

                //    lblProcDate.Content = dt.Rows[0]["C_Date"].ToString();
                //    lblProducts.Content = dt.Rows[0]["Products"].ToString();
                //    double Abc = Convert.ToDouble(dt.Rows[0]["Net_Amount"].ToString());
                //    lblProceNetAmt.Content = Convert.ToDouble(Microsoft.VisualBasic.Strings.Format(Abc, "##,###.00"));
                //    double price = Convert.ToDouble(dt.Rows[0]["Price"].ToString());
                //    lblProcePrice.Content = Convert.ToDouble(Microsoft.VisualBasic.Strings.Format(price, "##,###.00"));
                //    double qt = Convert.ToDouble(dt.Rows[0]["Quantity"].ToString());
                //    lblProceQty.Content = Convert.ToDouble(Microsoft.VisualBasic.Strings.Format(qt, "##,###.00"));
                //}

                //grd_FinalizeProducts.Visibility = System.Windows.Visibility.Visible;
            }
            catch(Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            Salesid();

            //Final_PreProcurement();
        }

        private void txtQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtPrice.Text == "")
            {
                //MessageBox.Show("Please Insert Price", caption, MessageBoxButton.OK);
                txtQuantity.Text = 0.ToString();

            }
            else if (txtQuantity.Text == "")
            {
                txtTotalPrice.Text = txtPrice.Text;
            }
            else if (txtPrice.Text != "" && txtQuantity.Text != "")
            {
                double tamt1;
                nfi = (NumberFormatInfo)nfi.Clone();
                nfi.CurrencySymbol = "";

                double prc = Convert.ToDouble(txtPrice.Text);
                double qty = Convert.ToDouble(txtQuantity.Text);
                double tamt = (prc * qty);
                txtTotalPrice.Text = tamt.ToString();
                //  txtpreroundoff.Text = Math.Round(tamt).ToString();
                //roundoff Method
                if (txtTotalPrice.Text.Trim().Length > 0)
                {
                    tamt1 = Convert.ToDouble(txtTotalPrice.Text);
                }
                else
                {
                    tamt1 = 0;
                }
                double netAmt = Math.Round(tamt1);
                double roundDiff = netAmt - tamt1;
                double roundDiff1 = Math.Round(roundDiff, 2);

                txtNetAmount.Text = String.Format(nfi, "{0:C}", Convert.ToDouble(netAmt));
                //txtRoundUp.Text = String.Format(nfi, "{0:C}", Convert.ToDouble(roundDiff));
                txtpreroundoff.Text = Convert.ToString(roundDiff1);

            }

        }

        private void chkaddToSale_Checked(object sender, RoutedEventArgs e)
        {
            object item = dgvAdm_FinalProcurement.SelectedItem;
            string ID = (dgvAdm_FinalProcurement.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            //MessageBox.Show(ID);
            grd_FinalizeProducts.Visibility = Visibility;
            Salesid();

            try
            {
                con.Open();
                string sqlquery = "SELECT P.[ID],P.[DealerID],P.[Domain_ID],P.[Product_ID],P.[Brand_ID],P.[P_Category],P.[Model_No_ID],P.[Color_ID],P.[Warranty],P.[Quantity],P.[C_Date],P.[Net_Amount] " +
                      ",D.[DealerFirstName] + '' + D.[DealerLastName] AS [DealerName],D.[MobileNo],D.[PhoneNo] " +
                      ",DM.[Domain_Name] + ' , ' +  PM.[Product_Name] + ' , ' + B.[Brand_Name] + ' , ' + PC.[Product_Category] + ' , ' + MN.[Model_No] + ' , ' + C.[Color] AS [Products]" +
                      ",PP.[Price] " +
                      "FROM [Pre_Procurement] P " +
                      "INNER JOIN [tbl_DealerEntry] D ON D.[ID] = P.[DealerID] " +
                      "INNER JOIN [tb_Domain] DM ON DM.[ID]=P.[Domain_ID] " +
                      "INNER JOIN [tlb_Products] PM ON PM.[ID]=P.[Product_ID] " +
                      "INNER JOIN [tlb_Brand] B ON B.[ID]=P.[Brand_ID] " +
                      "INNER JOIN [tlb_P_Category] PC ON PC.[ID]=P.[P_Category]" +
                      "INNER JOIN [tlb_Model] MN ON MN.[ID]=P.[Model_No_ID] " +
                      "INNER JOIN [tlb_Color] C ON C.[ID]=P.[Color_ID] " +
                      "INNER JOIN [Pre_Products] PP ON PP.[Model_No_ID]=P.[Model_No_ID] " +
                      "WHERE P.[ID]='" + ID + "' ";

                SqlCommand cmd = new SqlCommand(sqlquery, con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    txtAdm_DealerID.Text = dt.Rows[0]["DealerID"].ToString();
                    txtAdm_DomainID.Text = dt.Rows[0]["Domain_ID"].ToString();
                    txtAdm_ProductID.Text = dt.Rows[0]["Product_ID"].ToString();
                    txtAdm_BrandID.Text = dt.Rows[0]["Brand_ID"].ToString();
                    txtAdm_ProductCatID.Text = dt.Rows[0]["P_Category"].ToString();
                    txtAdm_ModelID.Text = dt.Rows[0]["Model_No_ID"].ToString();
                    txtAdm_ColorID.Text = dt.Rows[0]["Color_ID"].ToString();

                    lblProcDate.Content = dt.Rows[0]["C_Date"].ToString();
                    lblProducts.Content = dt.Rows[0]["Products"].ToString();
                    double Abc = Convert.ToDouble(dt.Rows[0]["Net_Amount"].ToString());
                    lblProceNetAmt.Content = Convert.ToDouble(Microsoft.VisualBasic.Strings.Format(Abc, "##,###.00"));
                    double price = Convert.ToDouble(dt.Rows[0]["Price"].ToString());
                    lblProcePrice.Content = Convert.ToDouble(Microsoft.VisualBasic.Strings.Format(price, "##,###.00"));
                    double qt = Convert.ToDouble(dt.Rows[0]["Quantity"].ToString());
                    lblProceQty.Content = Convert.ToDouble(Microsoft.VisualBasic.Strings.Format(qt, "##,###.00"));
                }

                //grd_FinalizeProducts.Visibility = System.Windows.Visibility.Visible;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            //Salesid();
        }

        private void txtAdm_FDealerName_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Final_DealerDetails();
        }
        
        private void txtAdm_FDealerMN_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Final_DealerDetails();
        }

        private void dtpAdm_From_FinalDealer_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Final_DealerDetails();
        }

        private void dtpAdm_To_FinalDealer_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Final_DealerDetails();
        }
        #endregion Final Product Event

        #region DealerSales
        public void Salesid()
        {

            int id1 = 0;
            // SqlConnection con = new SqlConnection(constring);
            con.Open();
            SqlCommand cmd = new SqlCommand("select (COUNT(ID)) from Final_DealerDetails", con);
            id1 = Convert.ToInt32(cmd.ExecuteScalar());
            id1 = id1 + 1;
            lblSalesNo.Content = "# Sales /" + id1.ToString();
            con.Close();


        }
        #endregion DealerSales

        #region FinalProcurement Button Event
        private void finaldealerDetails_Click(object sender, RoutedEventArgs e)
        {
            grd_FinalDealerDetails.Visibility = System.Windows.Visibility.Visible;
            Final_DealerDetails();
        }

        private void btnAdm_FinalDealerExit_Click(object sender, RoutedEventArgs e)
        {
            grd_FinalDealerDetails.Visibility = System.Windows.Visibility.Hidden;
        }

        private void btnAdm_FDealerRefresh_Click(object sender, RoutedEventArgs e)
        {
            txtAdm_FDealerMN_Search.Text = "";
            txtAdm_FDealerName_Search.Text = "";
            Final_DealerDetails();
        }

        private void btnFinalProcurement_Close_Click(object sender, RoutedEventArgs e)
        {
            grd_FinalizeProducts.Visibility = System.Windows.Visibility.Hidden;
            txtAdm_BrandID.Text = "";
            txtAdm_AvilableQty.Text = "";
            txtAdm_ColorID.Text = "";
            txtAdm_DomainID.Text = "";
            txtAdm_ProductCatID.Text = "";
            txtAdm_ProductID.Text = "";
            txtAdm_DealerID.Text = "";
            lblSalesNo.Content = "";
            lblProcDate.Content = "";
            lblProducts.Content = "";
            lblProceNetAmt.Content = "";
            lblProcePrice.Content = "";
            txtPrice.Text = "";
            txtQuantity.Text = "";
            dtpFinalDate.Text = "";
            txtTotalPrice.Text = "";
            txtpreroundoff.Text = "";
            txtNetAmount.Text = "";
            txtAdm_StockID.Text = "";

        }

        private void btnFinalProcurement_Click(object sender, RoutedEventArgs e)
        {
            if (FinalPro_Validation() == true)
                return;

            if (CheckProduct() == true)
            {
                try
                {
                    bstockDet.Flag = 1;
                    bstockDet.SID = Convert.ToInt32(txtAdm_StockID.Text);
                    //bstockDet.Products123 = lblProducts.Content.ToString();
                    bstockDet.NewQty = txtQuantity.Text;
                    bstockDet.FinalPrice = Convert.ToDouble(txtPrice.Text);
                    bstockDet.S_Status = "Active";
                    bstockDet.C_Date = Convert.ToString(System.DateTime.Now.ToShortDateString());
                    dstUpdate.AddStockDetailsUp_Insert_Update_Delete(bstockDet);
                    MessageBox.Show("Data Save Successfully", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    con.Close();
                }
            }
            else
            {
                try
                {
                    bstockDet.Flag = 1;
                    bstockDet.DomainID = Convert.ToInt32(txtAdm_DomainID.Text);
                    bstockDet.ProductID = Convert.ToInt32(txtAdm_ProductID.Text);
                    bstockDet.BrandID = Convert.ToInt32(txtAdm_BrandID.Text);
                    bstockDet.ProductCatID = Convert.ToInt32(txtAdm_ProductCatID.Text);
                    bstockDet.ModelID = Convert.ToInt32(txtAdm_ModelID.Text);
                    bstockDet.ColorId = Convert.ToInt32(txtAdm_ColorID.Text);
                    bstockDet.Products1234 = lblProducts.Content.ToString();

                    // bstockDet.Products123= lblProducts.Content.ToString();


                    bstockDet.Products1234 = lblProducts.Content.ToString();
                    //bstockDet.Products123= lblProducts.Content.ToString();
                    bstockDet.AvilableQty = txtQuantity.Text;
                    bstockDet.SaleQty = txtSaleQuantity.Text;
                    bstockDet.NewQty = txtQuantity.Text;
                    bstockDet.FinalPrice = Convert.ToDouble(txtPrice.Text);
                    bstockDet.S_Status = "Active";

                    //string STRTODAYDATE = System.DateTime.Now.ToShortDateString();
                    //string time = System.DateTime.Now.ToShortTimeString();
                    //string[] STRVAL = STRTODAYDATE.Split('-');
                    //string STR_DATE1 = STRVAL[0];
                    //string STR_MONTH = STRVAL[1];
                    //string STR_YEAR = STRVAL[2];
                    //string DATE = STR_DATE1 + "-" + STR_MONTH + "-" + STR_YEAR;
                    ////txtdate.Text = DATE;
                    ////txttime.Text = time;

                    //baddprd.C_Date =Convert .ToDateTime( DATE);
                    bstockDet.C_Date = Convert.ToString(System.DateTime.Now.ToShortDateString());
                    dstockDet.AddStockDetails_Insert_Update_Delete(bstockDet);
                    MessageBox.Show("Data Save Successfully", caption, MessageBoxButton.OK, MessageBoxImage.Information);

                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    con.Close();
                }
            }

            string abc;

            if (chbDefault.IsChecked == true)
            {
                abc = "Default";
            }
            else
            {
                abc = "No";
            }

            //final dealer
            try
            {
                bfinaldealer1.Flag = 1;
                bfinaldealer1.FDealerID = Convert.ToInt32(txtAdm_DealerID.Text);
                bfinaldealer1.SalesID = lblSalesNo.Content.ToString();
                bfinaldealer1.Domain_ID = Convert.ToInt32(txtAdm_DomainID.Text);
                bfinaldealer1.Product_ID = Convert.ToInt32(txtAdm_ProductID.Text);
                bfinaldealer1.Brand_ID = Convert.ToInt32(txtAdm_BrandID.Text);
                bfinaldealer1.P_Category = Convert.ToInt32(txtAdm_ProductCatID.Text);
                bfinaldealer1.Model_No_ID = Convert.ToInt32(txtAdm_ModelID.Text);
                bfinaldealer1.Color_ID = Convert.ToInt32(txtAdm_ColorID.Text);
                bfinaldealer1.ProcNetAmt = Convert.ToDouble(lblProceNetAmt.Content.ToString());
                bfinaldealer1.ProcPrice = Convert.ToDouble(lblProcePrice.Content.ToString());
                bfinaldealer1.ProcQty = lblProceQty.Content.ToString();
                bfinaldealer1.FinalPrice = Convert.ToDouble(txtPrice.Text);
                bfinaldealer1.FinalQty = txtQuantity.Text;
                bfinaldealer1.SubTotal = Convert.ToDouble(txtTotalPrice.Text);
                bfinaldealer1.RoundUp = Convert.ToDouble(txtpreroundoff.Text);
                bfinaldealer1.NetAmt = Convert.ToDouble(txtNetAmount.Text);
                //bfinaldealer.FinalDate = Convert.ToString(dtpFinalDate.Text);
                bfinaldealer1.SDefault = abc;
                bfinaldealer1.ServiceIntervalMonth = txtAdm_FinalMonths.Text;
                //bfinaldealer1.FMonths = lblFinal_Months.Content.ToString();
                bfinaldealer1.S_Status = "Active";

                //string STRTODAYDATE = System.DateTime.Now.ToShortDateString();
                //string time = System.DateTime.Now.ToShortTimeString();
                //string[] STRVAL = STRTODAYDATE.Split('-');
                //string STR_DATE1 = STRVAL[0];
                //string STR_MONTH = STRVAL[1];
                //string STR_YEAR = STRVAL[2];
                //string DATE = STR_DATE1 + "-" + STR_MONTH + "-" + STR_YEAR;
                ////txtdate.Text = DATE;
                ////txttime.Text = time;

                //bfinaldealer.C_Date =Convert .ToDateTime(dtpFinalDate.SelectedDate.ToString);
                bfinaldealer1.C_Date = Convert.ToString(System.DateTime.Now.ToShortDateString());
                dfinaldealer.FinalDealer_Insert_Update_Delete(bfinaldealer1);
                MessageBox.Show("Data Save Successfully", caption, MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }

            AddQuantity_Check();
            AddQuantity();

            try
            {
                bstockDet.Flag = 1;
                bstockDet.SID = Convert.ToInt32(txtAdm_StockID.Text);
                //bstockDet.Products123 = lblProducts.Content.ToString();
                bstockDet.AvilableQty = Convert.ToString(add);
                daddqty.AddQtyStockDetails_Insert_Update_Delete(bstockDet);
                MessageBox.Show("Quantity Save Succesfully...", caption, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }


            try
            {
                bstockDet.Flag = 1;
                bstockDet.SID = Convert.ToInt32(txtAdm_StockID.Text);
                //bstockDet.Products123 = lblProducts.Content.ToString();
                bstockDet.AvilableQty = Convert.ToString(add);
                daddqty.AddQtyStockDetails_Insert_Update_Delete(bstockDet);
                MessageBox.Show("Quantity Save Succesfully...", caption, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }

            try
            {
                bfinaldealer1.Flag = 1;
                bfinaldealer1.FDealerID = Convert.ToInt32(txtAdm_DealerID.Text);
                bfinaldealer1.S_Status = "DeActive";
                dFup.FinalUpdateD_Insert_Update_Delete(bfinaldealer1);
                MessageBox.Show("Update Final Dealer Succesfully...", caption, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }

            //txtAdm_DomainID.Text = "";
            //txtAdm_ProductID.Text = "";
            txtAdm_StockID.Text = "";
            txtAdm_BrandID.Text = "";
            txtAdm_AvilableQty.Text = "";
            txtAdm_ColorID.Text = "";
            txtAdm_DomainID.Text = "";
            txtAdm_ProductCatID.Text = "";
            txtAdm_ProductID.Text = "";
            txtAdm_DealerID.Text = "";
            lblSalesNo.Content = "";
            lblProcDate.Content = "";
            lblProducts.Content = "";
            lblProceNetAmt.Content = "";
            lblProcePrice.Content = "";
            txtPrice.Text = "";
            txtQuantity.Text = "";
            dtpFinalDate.Text = "";
            txtTotalPrice.Text = "";
            txtpreroundoff.Text = "";
            txtNetAmount.Text = "";

            Final_PreProcurement();

            Salesid();
        }
        #endregion FinalProcuremrnt Button Event
        #endregion Final Pro

        #region Stock Details
        #region StockDetails Button Event
        private void smstock_Click(object sender, RoutedEventArgs e)
        {
            grd_StockDetails.Visibility = System.Windows.Visibility.Visible;
            LoadStockDetails();
            StockDetails();
        }

        private void btnAdm_StockDetails_Exit_Click(object sender, RoutedEventArgs e)
        {
            grd_StockDetails.Visibility = System.Windows.Visibility.Hidden;
        }

        private void btnAdm_StockD_Refresh_Click(object sender, RoutedEventArgs e)
        {
            txtAdm_Stock_Filter_Search.Text = "";
            txtAdm_Stock_Filter_Search_Price.Text = "";
            cmbAdm_StockFilter_Search.Text = "Select";
            StockDetails();
        }
        #endregion StockDetails Button Event

        #region StockDet
        int aviQty;
        int newQty;
        int add;

        public void AddQuantity_Check()
        {
            try
            {
                String str;
                //con.Open();
                DataSet ds = new DataSet();
                str = "SELECT [ID],[AvilableQty] From [StockDetails] Where [ID]='" + txtAdm_StockID.Text.Trim() + "' ";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if(dt.Rows.Count > 0)
                {
                    txtAdm_AvilableQty.Text = dt.Rows[0]["AvilableQty"].ToString();
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
        }

        public void AddQuantity()
        {
            try
            {         
                aviQty = Convert.ToInt32(txtAdm_AvilableQty.Text);
                newQty = Convert.ToInt32(txtQuantity.Text);
                add = aviQty + newQty;
            }
            catch(Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public void StockDetails()
        {
            try
            {
                String str;
                //con.Open();
                DataSet ds = new DataSet();
                str = "SELECT P.[ID],P.[Domain_ID],P.[Product_ID],P.[Brand_ID],P.[P_Category],P.[Model_No_ID],P.[Color_ID],P.[Products123],P.[AvilableQty],P.[SaleQty],P.[NewQty],P.[FinalPrice] " +
                      ",DM.[Domain_Name],PM.[Product_Name], B.[Brand_Name] , PC.[Product_Category] ,MN.[Model_No] ,C.[Color] " +
                      ",PP.[Price] " +
                      "FROM [StockDetails] P " +
                      "INNER JOIN [tb_Domain] DM ON DM.[ID]=P.[Domain_ID] " +
                      "INNER JOIN [tlb_Products] PM ON PM.[ID]=P.[Product_ID] " +
                      "INNER JOIN [tlb_Brand] B ON B.[ID]=P.[Brand_ID] " +
                      "INNER JOIN [tlb_P_Category] PC ON PC.[ID]=P.[P_Category]" +
                      "INNER JOIN [tlb_Model] MN ON MN.[ID]=P.[Model_No_ID] " +
                      "INNER JOIN [tlb_Color] C ON C.[ID]=P.[Color_ID] " +
                      "INNER JOIN [Pre_Products] PP ON PP.[Model_No_ID]=P.[Model_No_ID] " +
                      "WHERE ";

                if (txtAdm_Stock_Filter_Search_Price.Text.Trim() != "")
                {
                    str = str + "P.[FinalPrice] LIKE ISNULL('" + txtAdm_Stock_Filter_Search_Price.Text.Trim() + "',P.[FinalPrice]) + '%' AND ";
                }
                if (cmbAdm_StockFilter_Search.Text.Equals("Domain"))
                {
                    if (txtAdm_Stock_Filter_Search.Text.Trim() != "")
                    {
                        str = str + "DM.[Domain_Name] LIKE ISNULL('" + txtAdm_Stock_Filter_Search.Text.Trim() + "',DM.[Domain_Name]) + '%' AND ";
                    }
                }
                if (cmbAdm_StockFilter_Search.Text.Equals("Product Type"))
                {
                    if (txtAdm_Stock_Filter_Search.Text.Trim() != "")
                    {
                        str = str + "PM.[Product_Name] LIKE ISNULL('" + txtAdm_Stock_Filter_Search.Text.Trim() + "',PM.[Product_Name]) + '%' AND ";
                    }
                }
                if (cmbAdm_StockFilter_Search.Text.Equals("Brand"))
                {
                    if (txtAdm_Stock_Filter_Search.Text.Trim() != "")
                    {
                        str = str + "B.[Brand_Name] LIKE ISNULL('" + txtAdm_Stock_Filter_Search.Text.Trim() + "',B.[Brand_Name]) + '%' AND ";
                    }
                }
                if (cmbAdm_StockFilter_Search.Text.Equals("Product Category"))
                {
                    if (txtAdm_Stock_Filter_Search.Text.Trim() != "")
                    {
                        str = str + "PC.[Product_Category] LIKE ISNULL('" + txtAdm_Stock_Filter_Search.Text.Trim() + "',PC.[Product_Category]) + '%' AND ";
                    }
                }
                if (cmbAdm_StockFilter_Search.Text.Equals("Model"))
                {
                    if (txtAdm_Stock_Filter_Search.Text.Trim() != "")
                    {
                        str = str + "MN.[Model_No] LIKE ISNULL('" + txtAdm_Stock_Filter_Search.Text.Trim() + "',MN.[Model_No]) + '%' AND ";
                    }
                }
                if (cmbAdm_StockFilter_Search.Text.Equals("Color"))
                {
                    if (txtAdm_Stock_Filter_Search.Text.Trim() != "")
                    {
                        str = str + "C.[Color] LIKE ISNULL('" + txtAdm_Stock_Filter_Search.Text.Trim() + "',C.[Color]) + '%' AND ";
                    }
                }
                
                str = str + " P.[S_Status] = 'Active' ORDER BY P.[C_Date] ASC ";
                //str = str + " S_Status = 'Active' ";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                //if (ds.Tables[0].Rows.Count > 0)
                //{
                dgvAdm_StockDetails.ItemsSource = ds.Tables[0].DefaultView;
                //}
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public void LoadStockDetails()
        {
            cmbAdm_StockFilter_Search.Text = "Select";
            cmbAdm_StockFilter_Search.Items.Add("Domain");
            cmbAdm_StockFilter_Search.Items.Add("Product Type");
            cmbAdm_StockFilter_Search.Items.Add("Brand");
            cmbAdm_StockFilter_Search.Items.Add("Product Category");
            cmbAdm_StockFilter_Search.Items.Add("Model");
            cmbAdm_StockFilter_Search.Items.Add("Color");
       }

        //private void ChangeColor()
        //{
        //    DataSet ds = new DataSet();
        //    for (int i = 0; i < dgvAdm_StockDetails.Rows.Count; i++)
        //    {
        //        try
        //        {
        //            if (Convert.ToDouble(dgvAdm_StockDetails.Rows[i].Cells["BalanceQuantity"].Value.ToString()) <= Convert.ToDouble(dgvDetails.Rows[i].Cells["ReorderQuantity"].Value.ToString()))
        //            {
        //                dgvDetails.Rows[i].DefaultCellStyle.BackColor = Color.Salmon;
        //                //dgvDetails.Rows[i].DefaultCellStyle.ForeColor = Color.White;
        //            }

        //            if (Convert.ToDouble(dgvDetails.Rows[i].Cells["BalanceQuantity"].Value.ToString()) > Convert.ToDouble(dgvDetails.Rows[i].Cells["MaxQuantity"].Value.ToString()))
        //            {
        //                dgvDetails.Rows[i].DefaultCellStyle.BackColor = Color.YellowGreen;
        //                //dgvDetails.Rows[i].DefaultCellStyle.ForeColor = Color.White;
        //            }

        //        }
        //        catch { }
        //    }
        //}
        #endregion StockDet

        #region Stock Event
        private void txtAdm_Stock_Filter_Search_Price_TextChanged(object sender, TextChangedEventArgs e)
        {
            StockDetails();
        }

        private void txtAdm_Stock_Filter_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            StockDetails();
        }
        #endregion Stock Event       
        #endregion StockDetails

        #region AllProduct Function
        #region AllProducts Fun
        public void AllProducts_Details()
        {
            try
            {
                String str;
                //con.Open();
                DataSet ds = new DataSet();
                str = "SELECT P.[ID],P.[Domain_ID],P.[Product_ID],P.[Brand_ID],P.[P_Category],P.[Model_No_ID],P.[Color_ID],P.[Price] " +
                      ",DM.[Domain_Name],PM.[Product_Name], B.[Brand_Name] , PC.[Product_Category] ,MN.[Model_No] ,C.[Color] " +
                      "FROM [Pre_Products] P " +
                      "INNER JOIN [tb_Domain] DM ON DM.[ID]=P.[Domain_ID] " +
                      "INNER JOIN [tlb_Products] PM ON PM.[ID]=P.[Product_ID] " +
                      "INNER JOIN [tlb_Brand] B ON B.[ID]=P.[Brand_ID] " +
                      "INNER JOIN [tlb_P_Category] PC ON PC.[ID]=P.[P_Category]" +
                      "INNER JOIN [tlb_Model] MN ON MN.[ID]=P.[Model_No_ID] " +
                      "INNER JOIN [tlb_Color] C ON C.[ID]=P.[Color_ID] " +
                      "WHERE ";

                if (txtAdm_AllProducts_Search_Price.Text.Trim() != "")
                {
                    str = str + "P.[Price] LIKE ISNULL('" + txtAdm_AllProducts_Search_Price.Text.Trim() + "',P.[Price]) + '%' AND ";
                }
                if (cmbAdm_AllProducts_Search.Text.Equals("Domain"))
                {
                    if (txtAdm_AllProducts_Search.Text.Trim() != "")
                    {
                        str = str + "DM.[Domain_Name] LIKE ISNULL('" + txtAdm_AllProducts_Search.Text.Trim() + "',DM.[Domain_Name]) + '%' AND ";
                    }
                }
                if (cmbAdm_AllProducts_Search.Text.Equals("Product Type"))
                {
                    if (txtAdm_AllProducts_Search.Text.Trim() != "")
                    {
                        str = str + "PM.[Product_Name] LIKE ISNULL('" + txtAdm_AllProducts_Search.Text.Trim() + "',PM.[Product_Name]) + '%' AND ";
                    }
                }
                if (cmbAdm_AllProducts_Search.Text.Equals("Brand"))
                {
                    if (txtAdm_AllProducts_Search.Text.Trim() != "")
                    {
                        str = str + "B.[Brand_Name] LIKE ISNULL('" + txtAdm_AllProducts_Search.Text.Trim() + "',B.[Brand_Name]) + '%' AND ";
                    }
                }
                if (cmbAdm_AllProducts_Search.Text.Equals("Product Category"))
                {
                    if (txtAdm_AllProducts_Search.Text.Trim() != "")
                    {
                        str = str + "PC.[Product_Category] LIKE ISNULL('" + txtAdm_AllProducts_Search.Text.Trim() + "',PC.[Product_Category]) + '%' AND ";
                    }
                }
                if (cmbAdm_AllProducts_Search.Text.Equals("Model"))
                {
                    if (txtAdm_AllProducts_Search.Text.Trim() != "")
                    {
                        str = str + "MN.[Model_No] LIKE ISNULL('" + txtAdm_AllProducts_Search.Text.Trim() + "',MN.[Model_No]) + '%' AND ";
                    }
                }
                if (cmbAdm_AllProducts_Search.Text.Equals("Color"))
                {
                    if (txtAdm_AllProducts_Search.Text.Trim() != "")
                    {
                        str = str + "C.[Color] LIKE ISNULL('" + txtAdm_AllProducts_Search.Text.Trim() + "',C.[Color]) + '%' AND ";
                    }
                }

                str = str + " P.[S_Status] = 'Active' ORDER BY DM.[Domain_Name] ASC ";
                //str = str + " S_Status = 'Active' ";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                //if (ds.Tables[0].Rows.Count > 0)
                //{
                dgvAdm_AllProducts.ItemsSource = ds.Tables[0].DefaultView;
                //}
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public void Load_AllProducts()
        {
            cmbAdm_AllProducts_Search.Text = "Select";
            cmbAdm_AllProducts_Search.Items.Add("Domain");
            cmbAdm_AllProducts_Search.Items.Add("Product Type");
            cmbAdm_AllProducts_Search.Items.Add("Brand");
            cmbAdm_AllProducts_Search.Items.Add("Product Category");
            cmbAdm_AllProducts_Search.Items.Add("Model");
            cmbAdm_AllProducts_Search.Items.Add("Color");
        }
        #endregion AllProducts Fun

        #region AllProducts_Button Event
        private void btnAdm_AllProducts_Exit_Click(object sender, RoutedEventArgs e)
        {
            grd_AllProduct_Details.Visibility = System.Windows.Visibility.Hidden;
        }

        private void btnAdm_AllProducts_Refresh_Click(object sender, RoutedEventArgs e)
        {
            txtAdm_AllProducts_Search_Price.Text = "";
            txtAdm_AllProducts_Search.Text = "";
            cmbAdm_AllProducts_Search.Text = "Select";
            AllProducts_Details();
        }
        #endregion AllProducts_Button Event

        #region AllProduct Event
        private void smviewproducts_Click(object sender, RoutedEventArgs e)
        {
            grd_AllProduct_Details.Visibility = System.Windows.Visibility.Visible;
            AllProducts_Details();
            Load_AllProducts();
        }

        private void txtAdm_AllProducts_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            AllProducts_Details();
        }

        private void txtAdm_AllProducts_Search_Price_TextChanged(object sender, TextChangedEventArgs e)
        {
            AllProducts_Details();
        }
        #endregion AllProduct Event
        #endregion AllProduct Function
    }
}


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
    /// Interaction logic for CRM_AdminDashbord.xaml
    /// </summary>
    public partial class CRM_AdminDashbord : Window
    {
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
            txtAdm_Emp_Address.Text = "";
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

        private void btnAdm_FinalProcurment_Click(object sender, RoutedEventArgs e)
        {
            grd_FinalProcurment.Visibility = System.Windows.Visibility.Hidden;
        }

        public void Final_PreProcurement()
        {
            try
            {
                String str;
                //con.Open();
                DataSet ds = new DataSet();
                str = "SELECT P.[ID],P.[DealerID],P.[Domain_ID],P.[Product_ID],P.[Brand_ID],P.[P_Category],P.[Model_No_ID],P.[Color_ID] " +
                      ",D.[DealerFirstName] + '' + D.[DealerLastName] AS [DealerName],D.[MobileNo],D.[PhoneNo] " +
                      ",DM.[Domain_Name] + '' + PM.[Product_Name] + '' + B.[Brand_Name] + '' + PC.[Product_Category] + '' + MN.[Model_No] + '' + C.[Color] AS [Products]" +
                      "FROM [Pre_Procurement] P" +
                      "INNER JOIN [tbl_DealerEntry] D ON D.[ID] = P.[DealerID] " +
                      "INNER JOIN [tb_Domain] DM ON DM.[ID]=P.[Domain_ID] " +
                      "INNER JOIN [tlb_Products] PM ON PM.[ID]=P.[Product_ID] " +
                      "INNER JOIN [tlb_Brand] B ON B.[ID]=P.[Brand_ID] " +
                      "INNER JOIN [tlb_P_Category] PC ON PC.[ID]=P.[P_Category]" +
                      "INNER JOIN [tlb_Model] MN ON MN.[ID]=P.[Model_No_ID] " +
                      "INNER JOIN [tlb_Color] C ON C.[ID]=P.[Color_ID] " +
                      "WHERE ";
                if(cmbAdm_DealerFilter_Search.Text.Equals("Select"))
                {
                    if (cmbAdm_DealerFilter_Search.Text.Equals("Domain"))
                    {
                        if (txtAdm_Dealer_Filter_Search.Text.Trim() == "")
                        {
                            str = str + "DM.[Domain_Name] LIKE ISNULL('" + txtAdm_Dealer_Filter_Search.Text.Trim() + "',DM.[Domain_Name]) + '%' AND ";
                        }
                    }
                    if (cmbAdm_DealerFilter_Search.Text.Equals("Product Type"))
                    {
                        if (txtAdm_Dealer_Filter_Search.Text.Trim() == "")
                        {
                            str = str + "PM.[Product_Name] LIKE ISNULL('" + txtAdm_Dealer_Filter_Search.Text.Trim() + "',PM.[Product_Name]) + '%' AND ";
                        }
                    }
                    if (cmbAdm_DealerFilter_Search.Text.Equals("Brand"))
                    {
                        if (txtAdm_Dealer_Filter_Search.Text.Trim() == "")
                        {
                            str = str + "B.[Brand_Name] LIKE ISNULL('" + txtAdm_Dealer_Filter_Search.Text.Trim() + "',B.[Brand_Name]) + '%' AND ";
                        }
                    }
                    if (cmbAdm_DealerFilter_Search.Text.Equals("Product Category"))
                    {
                        if (txtAdm_Dealer_Filter_Search.Text.Trim() == "")
                        {
                            str = str + "PC.[Product_Category] LIKE ISNULL('" + txtAdm_Dealer_Filter_Search.Text.Trim() + "',PC.[Product_Category]) + '%' AND ";
                        }
                    }
                    if (cmbAdm_DealerFilter_Search.Text.Equals("Model"))
                    {
                        if (txtAdm_Dealer_Filter_Search.Text.Trim() == "")
                        {
                            str = str + "MN.[Model_No] LIKE ISNULL('" + txtAdm_Dealer_Filter_Search.Text.Trim() + "',MN.[Model_No]) + '%' AND ";
                        }
                    }
                    if (cmbAdm_DealerFilter_Search.Text.Equals("Color"))
                    {
                        if (txtAdm_Dealer_Filter_Search.Text.Trim() == "")
                        {
                            str = str + "C.[Color] LIKE ISNULL('" + txtAdm_Dealer_Filter_Search.Text.Trim() + "',C.[Color]) + '%' AND ";
                        }
                    }
                   if (cmbAdm_DealerFilter_Search.Text.Equals("Products / Services"))
                    {
                        if (txtAdm_Dealer_Filter_Search.Text.Trim() == "")
                        {
                            str = str + "[Products] LIKE ISNULL('" + txtAdm_Dealer_Filter_Search.Text.Trim() + "',[Products]) + '%' AND ";
                        }
                    }
                }
                str = str + " P.S_Status = 'Active' ORDER BY D.DealerFirstName ASC ";
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

        private void smviewprocurement_Click(object sender, RoutedEventArgs e)
        {
            grd_FinalProcurment.Visibility = System.Windows.Visibility.Visible;
            LoadFinal();
            Final_PreProcurement();
        }
    }
}


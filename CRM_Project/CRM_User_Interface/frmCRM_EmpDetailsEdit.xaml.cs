﻿using System;
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
    /// Interaction logic for frmCRM_EmpDetailsEdit.xaml
    /// </summary>
    public partial class frmCRM_EmpDetailsEdit : Window
    {
        public SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["ConstCRM"].ToString());
        SqlCommand cmd;
        SqlDataReader dr;
        string caption = "Green Future Glob";
        static int PK_ID;

        public frmCRM_EmpDetailsEdit()
        {
            InitializeComponent();
        }

        BAL_EmployeeEntry bempupd = new BAL_EmployeeEntry();
        DAL_EmployeeUpdate dempupd = new DAL_EmployeeUpdate();

        public void EmployeeID(string id)
        {
            txtAdm_EmployeeID.Text = id;
        }

        public void FillData()
        {
            try
            {
                con.Open();
                string sqlquery = "SELECT * FROM tbl_Employee where ID='" + txtAdm_EmployeeID.Text + "' ";
                SqlCommand cmd = new SqlCommand(sqlquery, con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    lblEmpID.Content = dt.Rows[0]["EmployeeID"].ToString();
                    txtAdm_EmpName.Text = dt.Rows[0]["EmployeeName"].ToString();
                    dtpAdm_Emp_DOB.SelectedDate = Convert.ToDateTime(dt.Rows[0]["DateOfBirth"].ToString());
                    txtAdm_Emp_Address.Text = dt.Rows[0]["EmpAddress"].ToString();
                    txtAdm_Emp_MobileNo.Text = dt.Rows[0]["MobileNo"].ToString();
                    txtAdm_Emp_PhoneNo.Text = dt.Rows[0]["PhoneNo"].ToString();
                    txtAdm_Emp_Designation.Text = dt.Rows[0]["Designation"].ToString();
                    dtpAdm_Emp_DOJ.SelectedDate = Convert.ToDateTime(dt.Rows[0]["DateOfJoining"].ToString());
                    cmbAdm_Emp_YearExp.SelectedItem = dt.Rows[0]["NoOfYears"].ToString();
                    lblYears.Content = dt.Rows[0]["Years"].ToString();
                    cmbAdm_Emp_Months.SelectedItem = dt.Rows[0]["NoOfMonths"].ToString();
                    lblMonths.Content = dt.Rows[0]["Months"].ToString();
                    txtAdm_Emp_Salary.Text = dt.Rows[0]["Salary"].ToString();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
            btnAdm_Emp_Save.Content = "Update";
        }

        public void LoadNoOfYears1()
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

        public void LoadNoOfMonths1()
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

        //private void btnAdm_Emp_Save_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        bempupd.Flag = 2;
        //        bdealerupd.id = Convert.ToInt32(txtDealerID.Text);
        //        bdealerupd.DealerEntryID = lbl_Edit_DealerID.Content.ToString();
        //        bdealerupd.CompanyName = txtAdmEdit_CompanyName.Text;
        //        bdealerupd.DealerFirstName = txtAdmEdit_DealerFirstName.Text;
        //        bdealerupd.DealerLastName = txtAdmEdit_DealerLastName.Text;
        //        bdealerupd.DateOfBirth = Convert.ToDateTime(dtpAdmEdit_Dealer_DOB.SelectedDate);
        //        bdealerupd.MobileNo = txtAdmEdit_Dealer_MobileNo.Text;
        //        bdealerupd.PhoneNo = txtAdmEdit_Dealer_PhoneNo.Text;
        //        bdealerupd.DealerAddress = txtAdmEdit_Dealer_Address.Text;
        //        bdealerupd.City = txtAdmEdit_Dealer_City.Text;
        //        bdealerupd.Zip = txtAdmEdit_Dealer_Zip.Text;
        //        bdealerupd.DState = txtAdmEdit_Dealer_State.Text;
        //        bdealerupd.Country = txtAdmEdit_Dealer_Country.Text;
        //        bdealerupd.S_Status = "Active";

        //        //string STRTODAYDATE = System.DateTime.Now.ToShortDateString();
        //        //string time = System.DateTime.Now.ToShortTimeString();
        //        //string[] STRVAL = STRTODAYDATE.Split('-');
        //        //string STR_DATE1 = STRVAL[0];
        //        //string STR_MONTH = STRVAL[1];
        //        //string STR_YEAR = STRVAL[2];
        //        //string DATE = STR_DATE1 + "-" + STR_MONTH + "-" + STR_YEAR;
        //        ////txtdate.Text = DATE;
        //        ////txttime.Text = time;

        //        //baddprd.C_Date =Convert .ToDateTime( DATE);
        //        bdealerupd.C_Date = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
        //        ddealerupd.EmployeeEntry_Insert_Update_Delete(bdealerupd);
        //        MessageBox.Show("Updated Data Successfully", caption, MessageBoxButton.OK, MessageBoxImage.Information);

        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //}
    }
}

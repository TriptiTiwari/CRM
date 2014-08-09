using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using CRM_BAL;

namespace CRM_DAL
{
   public  class DAL_PaymentMode
   {
       public SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["ConstCRM"].ToString());
       SqlCommand cmd;

       public int Save_Cash(BAL_PaymentModes   bpm)
       {
           try
           {

               con.Open();
               cmd = new SqlCommand("SP_PaymentMode", con);
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.AddWithValue("@Flag", 1);
               cmd.Parameters.AddWithValue("@Customer_ID", bpm.Customer_ID);
               cmd.Parameters.AddWithValue("@Bill_No", bpm.Bill_No);
               cmd.Parameters.AddWithValue("@Total_Price", bpm.Total_Price);
               cmd.Parameters.AddWithValue("@Paid_Amount", bpm.Paid_Amount);
               cmd.Parameters.AddWithValue("@Balance_Amount", bpm.Balance_Amount);
               cmd.Parameters.AddWithValue("@S_Status", bpm.S_Status);
               cmd.Parameters.AddWithValue("@C_Date", bpm.C_Date);
               int i = cmd.ExecuteNonQuery();
               return i;


           }
           catch (Exception)
           {

               throw;
           }
           finally { con.Close(); }
       }
    }
}

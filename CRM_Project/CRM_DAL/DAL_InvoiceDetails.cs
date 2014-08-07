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
   public  class DAL_InvoiceDetails
   {
       public SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["ConstCRM"].ToString());
       SqlCommand cmd;
       public int InvoiceDetails_Save(BAL_InvoiceDetails  balid)
       {
           try
           {

               con.Open();
               cmd = new SqlCommand("SP_InvoiceDetails", con);
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.AddWithValue("@Flag", 1);
               cmd.Parameters.AddWithValue("@Customer_ID", balid.Customer_ID);
               cmd.Parameters.AddWithValue("@Bill_No", balid.Bill_No);
               cmd.Parameters.AddWithValue("@Domain_ID", balid.Domain_ID);
               cmd.Parameters.AddWithValue("@Product_ID", balid.Product_ID);
               cmd.Parameters.AddWithValue("@Email_ID", balid.Brand_ID );
               cmd.Parameters.AddWithValue("@Address", balid.P_Category );
               cmd.Parameters.AddWithValue("@Occupation", balid.Model_No_ID );
               cmd.Parameters.AddWithValue("@Product_Details", balid.Color_ID );
               //  cmd.Parameters.AddWithValue("@Followup_Walkin_Option", balfp.Followup_Walkin_Option);

               cmd.Parameters.AddWithValue("@Followup_Type", balid.Per_Product_Price );
               cmd.Parameters.AddWithValue("@F_Date", balid.Qty );
               cmd.Parameters.AddWithValue("@F_Message", balid.C_Price );
               // cmd.Parameters.AddWithValue("@Walkins", balfp.Walkins);
               cmd.Parameters.AddWithValue("@Folloup_Update", balid.Tax_Name );
               cmd.Parameters.AddWithValue("@Folloup_Update", balid.Tax );
               cmd.Parameters.AddWithValue("@Folloup_Update", balid.Total_Price );
              
               cmd.Parameters.AddWithValue("@S_Status", balid.S_Status);
               cmd.Parameters.AddWithValue("@C_Date", balid.C_Date);
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

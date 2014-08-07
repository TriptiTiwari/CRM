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
    public class DAL_FinalDealer
    {
        public SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["ConstCRM"].ToString());
        SqlCommand cmd;
        BAL_FinalDealer bfinaldealer = new BAL_FinalDealer();

        public int FinalDealer_Insert_Update_Delete(BAL_FinalDealer bfinaldealer)
        {
            try
            {

                con.Open();
                cmd = new SqlCommand("SP_FinalDealer", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", 1);
                cmd.Parameters.AddWithValue("@SalesID", bfinaldealer.SalesID);
                cmd.Parameters.AddWithValue("@Domain_ID", bfinaldealer.Domain_ID);
                cmd.Parameters.AddWithValue("@Product_ID", bfinaldealer.Product_ID);
                cmd.Parameters.AddWithValue("@Brand_ID", bfinaldealer.Brand_ID);
                cmd.Parameters.AddWithValue("@P_Category", bfinaldealer.P_Category);
                cmd.Parameters.AddWithValue("@Model_No_ID", bfinaldealer.Model_No_ID);
                cmd.Parameters.AddWithValue("@Color_ID", bfinaldealer.Color_ID);
                cmd.Parameters.AddWithValue("@ProcNetAmt", bfinaldealer.ProcNetAmt);
                cmd.Parameters.AddWithValue("@ProcPrice", bfinaldealer.ProcPrice);
                cmd.Parameters.AddWithValue("@ProcQty", bfinaldealer.ProcQty);
                cmd.Parameters.AddWithValue("@FinalPrice", bfinaldealer.FinalPrice);
                cmd.Parameters.AddWithValue("@FinalQty", bfinaldealer.FinalQty);
                cmd.Parameters.AddWithValue("@SubTotal", bfinaldealer.SubTotal);
                cmd.Parameters.AddWithValue("@RoundUp", bfinaldealer.RoundUp);
                cmd.Parameters.AddWithValue("@NetAmt", bfinaldealer.NetAmt);
                //cmd.Parameters.AddWithValue("@FinalDate", bfinaldealer.FinalDate);
                cmd.Parameters.AddWithValue("@SDefault", bfinaldealer.SDefault);
                cmd.Parameters.AddWithValue("@ServiceIntervalMonth", bfinaldealer.ServiceIntervalMonth);
                //cmd.Parameters.AddWithValue("@Months,", bfinaldealer.Months);
                cmd.Parameters.AddWithValue("@S_Status", bfinaldealer.S_Status);
                cmd.Parameters.AddWithValue("@C_Date", bfinaldealer.C_Date);
                int i = cmd.ExecuteNonQuery();
                return i;
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
    }
}

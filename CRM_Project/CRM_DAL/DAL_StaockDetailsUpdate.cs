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
    public class DAL_StaockDetailsUpdate
    {
        public SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings["ConstCRM"].ToString());
        SqlCommand cmd;
        BAL_StockDetails bstockdet = new BAL_StockDetails();

        public int AddStockDetailsUp_Insert_Update_Delete(BAL_StockDetails bstockdet)
        {
            try
            {

                con.Open();
                cmd = new SqlCommand("SP_StockDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", 2);
                cmd.Parameters.AddWithValue("@id", bstockdet.SID);
                cmd.Parameters.AddWithValue("@AvilableQty", bstockdet.AvilableQty);
                cmd.Parameters.AddWithValue("@S_Status", bstockdet.S_Status);
                cmd.Parameters.AddWithValue("@C_Date", bstockdet.C_Date);
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

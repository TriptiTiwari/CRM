using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace CRM_BAL
{
    public class BAL_StockDetails
    {
        public int Flag { get; set; }

        public int DomainID { get; set; }

        public int ProductID { get; set; }
        
        public int BrandID { get; set; }

        public int ProductCatID { get; set; }

        public int ModelID { get; set; }

        public int ColorId { get; set; }

        public int AvilableQty { get; set; }

        public int SaleQty { get; set; }

        public string S_Status { get; set; }

        public string C_Date { get; set; }
    }
}

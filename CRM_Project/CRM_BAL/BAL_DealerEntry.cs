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
    public class BAL_DealerEntry
    {
        public int Flag { get; set; }

        public string DealerEntryID { get; set; }

        public string CompanyName { get; set; }

        public string DealerFirstName { get; set; }

        public string DealerLaseName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string MobileNo { get; set; }

        public string PhoneNo { get; set; }

        public string DealerAddress { get; set; }

        public string City { get; set; }

        public string Zip { get; set; }

        public string DState { get; set; }

        public string Country { get; set; }

        public string S_Status { get; set; }

        public DateTime C_Date { get; set; }
    }
}

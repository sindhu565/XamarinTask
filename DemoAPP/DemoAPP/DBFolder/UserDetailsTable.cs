using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoAPP.DBFolder
{
    public class UserDetailsTable
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string userName { get; set; }
        public string designation { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string thumbNail { get; set; }

    }
}

using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressMarket.Model
{
    public class User
    {
        public Int16 User_ID { get; set; }
        public String UserName { get; set; }
        public String UserPassword { get; set; }
        public Int16 UserAccessType { get; set; }   
       

    }
}

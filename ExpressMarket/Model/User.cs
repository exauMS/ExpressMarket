using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressMarket.Model
{
    internal class User
    {
        [PrimaryKey, AutoIncrement] 
        public int Id { get; set; }
        [MaxLength(50), Unique]
        public string UserName { get; set; }
        [Unique]
        public string Email { get; set; }
        public string Password { get; set; }

    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace my_new_app.Models
{
    public partial class User
    {
        public User()
        {
            Reserves = new HashSet<Reserve>();
            Tokens = new HashSet<Token>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreateDate { get; set; }
        public string Password { get; set; }
        public string NationalityCode { get; set; }

        public virtual ICollection<Reserve> Reserves { get; set; }
        public virtual ICollection<Token> Tokens { get; set; }
    }
}

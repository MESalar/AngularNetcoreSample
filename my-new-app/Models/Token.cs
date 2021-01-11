using System;
using System.Collections.Generic;

#nullable disable

namespace my_new_app.Models
{
    public partial class Token
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ExpiredDate { get; set; }
        public Guid Token1 { get; set; }

        public virtual User User { get; set; }
    }
}

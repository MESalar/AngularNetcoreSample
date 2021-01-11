using System;
using System.Collections.Generic;

#nullable disable

namespace my_new_app.Models
{
    public partial class Reserve
    {
        public Reserve()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime ReserveDate { get; set; }
        public int? TotalPrice { get; set; }
        public int? TransactionId { get; set; }
        public int State { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}

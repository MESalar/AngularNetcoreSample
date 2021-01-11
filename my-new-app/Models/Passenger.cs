using System;
using System.Collections.Generic;

#nullable disable

namespace my_new_app.Models
{
    public partial class Passenger
    {
        public Passenger()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public string NationalityCode { get; set; }
        public DateTime CreateDate { get; set; }
        public int Age { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}

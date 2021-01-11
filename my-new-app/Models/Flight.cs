using System;
using System.Collections.Generic;

#nullable disable

namespace my_new_app.Models
{
    public partial class Flight
    {
        public Flight()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int SourceId { get; set; }
        public int DestinationId { get; set; }
        public DateTime Date { get; set; }
        public int TarifId { get; set; }
        public int Quantity { get; set; }
        public virtual AirPort Destination { get; set; }
        public virtual AirPort Source { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
        public int GetPrice()
        {
            return 150000;
        }
    }
}

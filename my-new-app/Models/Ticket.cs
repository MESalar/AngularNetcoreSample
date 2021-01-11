using System;
using System.Collections.Generic;

#nullable disable

namespace my_new_app.Models
{
    public partial class Ticket
    {
        public int Id { get; set; }
        public int PassengerId { get; set; }
        public int Code { get; set; }
        public int State { get; set; }
        public int ReseveId { get; set; }
        public int FlightId { get; set; }
        public int Price { get; set; }

        public virtual Flight Flight { get; set; }
        public virtual Passenger Passenger { get; set; }
        public virtual Reserve Reseve { get; set; }
    }
}

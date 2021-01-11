using System;
using System.Collections.Generic;

#nullable disable

namespace my_new_app.Models
{
    public partial class AirPort
    {
        public AirPort()
        {
            FlightDestinations = new HashSet<Flight>();
            FlightSources = new HashSet<Flight>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string CityTitle { get; set; }
        public override string ToString()
        {
            return CityTitle + " , فرودگاه " + Title;
        }

        public virtual ICollection<Flight> FlightDestinations { get; set; }
        public virtual ICollection<Flight> FlightSources { get; set; }
    }
}

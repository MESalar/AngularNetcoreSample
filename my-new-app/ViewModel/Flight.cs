using System;
using System.Collections.Generic;
using my_new_app.Infrastructure;

#nullable disable

namespace my_new_app.ViewModel
{
    public partial class FlightVM
    {
        public FlightVM(Models.Flight flight)
        {
            Id = flight.Id;
            Title = flight.Title;
            Source = flight.Source.ToString();
            Destination = flight.Destination.ToString();
            Date = flight.Date.ToPersianDateString();
            Quantity = flight.Quantity;
            Price = 120000;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public string Date { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public override string ToString()
        {
            string result = $"پرواز شماره  { Title} : از مبداء {Source} به مقصد: {Destination} در تاریخ {Date} .";
            return result;
        }
    }
}

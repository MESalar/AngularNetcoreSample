using System.Collections.Generic;

namespace my_new_app.ViewModel
{
    public class ReseveTicketsVM
    {
        public int ReserveId { get; set; }
        public string userName{get;set;}
        public string ReseveDate { get; set; }
        public List<TicketsVM> Tickets { get; set; }
    }
    public class TicketsVM
    {
        public string FullName { get; set; }
        public int Price { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public string flightDate { get; set; }
        public string TicketCode { get; set; }
    }
}
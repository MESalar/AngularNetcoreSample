using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace my_new_app.Models
{
    public partial class FlightContext : FlightContextBase
    {

        public FlightContext()
        {
        }

        public FlightContext(DbContextOptions<FlightContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AirPort>().HasData(
                new AirPort() { CityTitle = "تهران", Title = "مهرآباد", Id = 1 },
                new AirPort() { CityTitle = "تهران", Title = "امام خمینی", Id = 2 },
                new AirPort() { CityTitle = "مشهد", Title = "هاشمی نژاد", Id = 3 },
                new AirPort() { CityTitle = "اصفهان", Title = "شهید بهشتی", Id = 4 },
                new AirPort() { CityTitle = "بوشهر", Title = "خلیج فارس", Id = 5 },
                new AirPort() { CityTitle = "کرج", Title = "پیام", Id = 6 },
                new AirPort() { CityTitle = "شیراز", Title = "شهید دستغیب", Id = 7 },
                new AirPort() { CityTitle = "تبریز", Title = "شهید مدنی", Id = 8 },
                new AirPort() { CityTitle = "قم", Title = "سلفچگان", Id = 9 }
                );
            List<Flight> seedFlights = new List<Flight>();
            int id = 1;
            for (int i = 1; i < 10; i++)
            {
                for (int j = 1; j < 10; j++)
                {
                    if (i == j)
                        continue;

                    for (int d = 1; d < 10; d++)
                    {

                        for (int c = 1; c < 10; c++)
                        {
                            var code = Math.Abs(Guid.NewGuid().GetHashCode());
                            string title = code.ToString().Length > 5 ? code.ToString().Substring(0, 5) : code.ToString().PadLeft(5, '0');

                            Flight flight = new Flight() { Id = id, Date = DateTime.Now.AddDays(d), Title = title, Quantity = c, SourceId = i, DestinationId = j, TarifId = 1 };
                            id++;
                            seedFlights.Add(flight);
                        }
                    }

                }
            }
            modelBuilder.Entity<Flight>().HasData(seedFlights);
            // modelBuilder.Entity<Flight>().HasData(
            //     new Flight() { Id = 1, Date = DateTime.Now.AddDays(10), Title = "235", Quantity = 5, SourceId = 1, DestinationId = 2, TarifId = 1 },
            //     new Flight() { Id = 2, Date = DateTime.Now.AddDays(11), Title = "23513", Quantity = 5, SourceId = 2, DestinationId = 3, TarifId = 1 },
            //     new Flight() { Id = 3, Date = DateTime.Now.AddDays(12), Title = "23514", Quantity = 5, SourceId = 3, DestinationId = 4, TarifId = 1 },
            //     new Flight() { Id = 4, Date = DateTime.Now.AddDays(13), Title = "23512", Quantity = 5, SourceId = 4, DestinationId = 5, TarifId = 1 },
            //     new Flight() { Id = 5, Date = DateTime.Now.AddDays(14), Title = "23511", Quantity = 5, SourceId = 5, DestinationId = 6, TarifId = 1 },
            //     new Flight() { Id = 6, Date = DateTime.Now.AddDays(9), Title = "23510", Quantity = 5, SourceId = 6, DestinationId = 7, TarifId = 1 },
            //     new Flight() { Id = 7, Date = DateTime.Now.AddDays(8), Title = "2359", Quantity = 5, SourceId = 7, DestinationId = 8, TarifId = 1 },
            //     new Flight() { Id = 8, Date = DateTime.Now.AddDays(7), Title = "2358", Quantity = 5, SourceId = 8, DestinationId = 9, TarifId = 1 },
            //     new Flight() { Id = 9, Date = DateTime.Now.AddDays(6), Title = "2357", Quantity = 5, SourceId = 9, DestinationId = 1, TarifId = 1 },
            //     new Flight() { Id = 10, Date = DateTime.Now.AddDays(5), Title = "2356", Quantity = 5, SourceId = 8, DestinationId = 2, TarifId = 1 },
            //     new Flight() { Id = 11, Date = DateTime.Now.AddDays(10), Title = "2355", Quantity = 5, SourceId = 1, DestinationId = 2, TarifId = 1 },
            //     new Flight() { Id = 12, Date = DateTime.Now.AddDays(11), Title = "2354", Quantity = 5, SourceId = 2, DestinationId = 3, TarifId = 1 },
            //     new Flight() { Id = 13, Date = DateTime.Now.AddDays(12), Title = "2353", Quantity = 5, SourceId = 3, DestinationId = 4, TarifId = 1 },
            //     new Flight() { Id = 14, Date = DateTime.Now.AddDays(13), Title = "2352", Quantity = 5, SourceId = 4, DestinationId = 5, TarifId = 1 },
            //     new Flight() { Id = 15, Date = DateTime.Now.AddDays(14), Title = "241", Quantity = 5, SourceId = 5, DestinationId = 6, TarifId = 1 },
            //     new Flight() { Id = 16, Date = DateTime.Now.AddDays(9), Title = "240", Quantity = 5, SourceId = 6, DestinationId = 7, TarifId = 1 },
            //     new Flight() { Id = 17, Date = DateTime.Now.AddDays(8), Title = "239", Quantity = 5, SourceId = 7, DestinationId = 8, TarifId = 1 },
            //     new Flight() { Id = 18, Date = DateTime.Now.AddDays(7), Title = "238", Quantity = 5, SourceId = 8, DestinationId = 9, TarifId = 1 },
            //     new Flight() { Id = 19, Date = DateTime.Now.AddDays(6), Title = "237", Quantity = 5, SourceId = 9, DestinationId = 1, TarifId = 1 },
            //     new Flight() { Id = 20, Date = DateTime.Now.AddDays(5), Title = "236", Quantity = 5, SourceId = 8, DestinationId = 2, TarifId = 1 }
            //     );
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

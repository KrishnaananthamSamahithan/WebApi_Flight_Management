using WebApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace WebApi.Data
{
    public class FlightData
    {
        private readonly List<Flight> flights = new();

        

        public List<Flight> GetAllFlights() => flights;

        public Flight GetFlightById(int id) => flights.FirstOrDefault(f => f.Id == id);

        public void AddFlight(Flight flight) => flights.Add(flight);

        public void UpdateFlight(Flight flight)
        {
            var existingFlight = GetFlightById(flight.Id);
            if (existingFlight != null)
            {
                flights.Remove(existingFlight);
                flights.Add(flight);
            }
        }

        public void DeleteFlight(int id)
        {
            var flight = GetFlightById(id);
            if (flight != null) flights.Remove(flight);
        }
    }
}

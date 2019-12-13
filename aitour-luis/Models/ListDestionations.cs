using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aitour_luis.Models
{
    public class ListDestionations
    {
        private IList<Destination> _list;
        public ListDestionations()
        {
            _list = new List<Destination>();

            _list.Add(new Destination() { Name = "Mar del Plata", DestinationType = "Playa" });            
            _list.Add(new Destination() { Name = "San Bernando", DestinationType = "Playa" });
            _list.Add(new Destination() { Name = "Villa Gessel", DestinationType = "Playa" });
            _list.Add(new Destination() { Name = "San Clemente del Tuyu", DestinationType = "Playa" });
            _list.Add(new Destination() { Name = "Las Toninas", DestinationType = "Playa" });
            _list.Add(new Destination() { Name = "Santa Teresita", DestinationType = "Playa" });
            _list.Add(new Destination() { Name = "Pinamar", DestinationType = "Playa" });
            _list.Add(new Destination() { Name = "Miramar", DestinationType = "Playa" });



        }
        public string[] GetDestination() {

            return _list.Select(p => p.Name).ToArray();
        }
    }

    public class Destination {
        public string Name { get; set; }
        public string DestinationType { get; set; }
    }
}

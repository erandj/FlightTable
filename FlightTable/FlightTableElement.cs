using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTable
{
    internal class FlightTableElement
    {
        private int id;
        private string airline;
        private string time;
        private string flight;
        private string aircraft;
        private string destination;
        private string status;
        private string terminal;
        private string gate;

        public int Id
        {
            get => id;
            set => id = value;
        }
        public string Airline
        {
            get => airline;
            set => airline = value;
        }
        public string Time
        {
            get => time;
            set => time = value;
        }
        public string Flight
        {
            get => flight;
            set => flight = value;
        }
        public string Destination
        {
            get => destination;
            set => destination = value;
        }
        public string Status
        {
            get => status;
            set => status = value;
        }
        public string Terminal
        {
            get => terminal;
            set => terminal = value;
        }
        public string Gate
        {
            get => gate;
            set => gate = value;
        }
        public string Aircraft { get => aircraft; set => aircraft = value; }
    }
}

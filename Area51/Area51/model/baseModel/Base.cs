using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Area51
{
    internal class Base
    {
        public string Floor { get; set; }
        public string FloorName { get; set; }
        public int FloorSecLvl { get; set; }

        public Base (string floor, string floorName, int floorSecLvl)
        {
            Floor = floor;
            FloorName = floorName;
            FloorSecLvl = floorSecLvl;  
        }
    }
}

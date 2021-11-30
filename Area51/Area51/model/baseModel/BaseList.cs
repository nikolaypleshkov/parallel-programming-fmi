using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Area51.model.baseModel
{
    internal class BaseList
    {
        List<Base> baseFloors = new List<Base>();
        public BaseList()
        {
            Base G = new Base("G", "Ground Floor", 1);
            Base S = new Base("S", "Secret Floor - Armageddon's Floor", 2);
            Base T1 = new Base("T1", "Secret Floor - Testing Toys Floor", 3);
            Base T2 = new Base("T2", "Top-Secret Floor - Alien(maybe) Storage", 4);

            baseFloors.Add(G);
            baseFloors.Add(S);
            baseFloors.Add(T1);
            baseFloors.Add(T2);
        }

        public List<Base> GetFloor()
        {
            return baseFloors;
        }
    }
}

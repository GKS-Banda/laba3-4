using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GKS
{
    class Calculation4PVBrigada
    {
        private string[][] distinctGroups;
        private Dictionary<string, Dictionary<string, int>>[] relationMatrix;
        private string[][][] MGroup;

        public Calculation4PVBrigada(string[][] distinctGroups, Dictionary<string, Dictionary<string, int>>[] relationMatrix)
        {
          
        }

        public void StartCalculation(out string[][][] MGroup)
        {
            MGroup = this.MGroup;
        }

    }
}

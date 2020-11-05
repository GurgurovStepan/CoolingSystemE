using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CoolingSystemElips
{
    class TestControl
    {
        /// <summary>
        /// температур масла
        /// </summary>
        List<sbyte> tempOil = new List<sbyte>() { 73, 73, 72, 72, 72, 71, 71, 70, 70, 69, 69, 68, 68, 68, 68 };
        /// <summary>
        /// температур воды
        /// </summary>
        List<sbyte> tempWater = new List<sbyte> { 82, 82, 81, 81, 81, 80, 79, 78, 77, 76, 75, 74, 73, 73, 73 };

        /// <summary>
        /// пара температур масла - воды
        /// </summary>
        List<TestData> temps = new List<TestData> { };

        public void InitTemps() 
        {
            for (int i = 0; i < tempOil.Count; i++)
            {
                temps.Add(new TestData(tempOil[i], tempWater[i]));
            }
        }

    }

    class TestData 
    {
        private sbyte tempOil;
        private sbyte tempWater;

        public TestData(sbyte to, sbyte tw) 
        {
            tempOil = to;
            tempWater = tw;
        }
    }

}

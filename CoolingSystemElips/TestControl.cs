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
        #region Поля

        /// <summary>
        /// температура масла
        /// </summary>
        private List<sbyte> tempOil = new List<sbyte>() { 76, 75, 74, 73, 72, 71, 70, 69, 68, 67, 66, 65, 64, 63, 62 };
        /// <summary>
        /// температура воды
        /// </summary>
        private List<sbyte> tempWater = new List<sbyte> { 86, 85, 84, 83, 82, 81, 80, 79, 78, 77, 76, 75, 74, 73, 72 };
        
        /// <summary>
        /// пара значений температур масла - вода
        /// </summary>
        private List<TestData> temps = new List<TestData> { };

        /// <summary>
        /// указатель на прочитанный элемент списка 
        /// </summary>
        private int counter;

        #endregion

        #region Конструкторы

        public TestControl()
        {
            InitTemps();
            counter = 0;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Заполнить коллекцию значениями температур масла и воды
        /// </summary>
        private void InitTemps()
        {
            for (int i = 0; i < tempOil.Count; i++)
            {
                temps.Add(new TestData(tempOil[i], tempWater[i]));
            }
        }

        /// <summary>
        /// Установить значения температур согласно значению указателя
        /// </summary>
        /// <param name="to">температур масла</param>
        /// <param name="tw">температур воды</param>
        /// <returns>исчерпано число элементов спика</returns>
        public bool GetTemps(ref sbyte to, ref sbyte tw)
        {
            if (counter < temps.Count()) 
            {
                to = temps[counter].TempOil;
                tw = temps[counter].TempWater;
                counter++;
                return false;
            }
            else 
            {
                return true;
            }
        }

        #endregion
    }

    class TestData
    {
        public sbyte TempWater { get; set; }
        public sbyte TempOil { get; set; }
        public TestData(sbyte to, sbyte tw)
        {
            TempOil = to;
            TempWater = tw;
        }
    }

}

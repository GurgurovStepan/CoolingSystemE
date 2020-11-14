using System;
using System.Collections.Generic;
using System.Linq;

using ExselDate;

namespace CoolingSystemElips
{
    class TestControl
    {
        WorkExсel exsel = new WorkExсel();

        #region Поля

        /// <summary>
        /// температура масла
        /// </summary>
        private List<sbyte> tempOil = new List<sbyte>() { };
        /// <summary>
        /// температура воды
        /// </summary>
        private List<sbyte> tempWater = new List<sbyte> { };
        
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
            InitTempsExs();
            InitTemps();
            counter = 0;
        }

        #endregion

        #region Методы

        private void InitTempsExs() 
        {
            var excelData = exsel.ReceiveData();

            if (excelData!=null)
            {
                for (int i = 2; i < excelData.GetUpperBound(0)+1; i++)
                {
                    tempOil.Add((sbyte)excelData[i, 1]);
                    tempWater.Add((sbyte)excelData[i, 2]);
                }
            }
        }

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

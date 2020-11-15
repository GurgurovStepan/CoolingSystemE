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

        /// <summary>
        /// число элементов в списке
        /// </summary>
        private int countList;

        #endregion

        #region Свойства

        /// <summary>
        /// Указатель на прочитанный элемент списка 
        /// </summary>
        public int Counter 
        {
            get 
            {
                return counter;
            }
            private set 
            {
                counter = value;
            }
        }

        /// <summary>
        /// Число элементов в списке
        /// </summary>
        public int CountList 
        {
            get 
            {
                return countList;
            }
            private set 
            {
                countList = value;
            }
        }

        #endregion

        #region Конструкторы

        public TestControl()
        {     
            InitTempsExs();
            InitTemps();
            Counter = 0;
            CountList = temps.Count();
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
            if (Counter < temps.Count()) 
            {
                to = temps[Counter].TempOil;
                tw = temps[Counter].TempWater;
                Counter++;
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

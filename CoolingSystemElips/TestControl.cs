using System;
using System.Collections.Generic;
using System.Linq;

using ExselDate;

namespace CoolingSystemElips
{
    class TestControl
    {
        private WorkExсel exsel = new WorkExсel();

        private struct TestData
        {
            public sbyte TempWater { get; set; }
            public sbyte TempOil { get; set; }
            public TestData(sbyte to, sbyte tw)
            {
                TempOil = to;
                TempWater = tw;
            }
        }

        #region Поля

        /// <summary>
        /// пара значений температур масла - вода
        /// </summary>
        private List<TestData> temps = new List<TestData> { };

        #endregion

        #region Свойства

        /// <summary>
        /// Указатель на прочитанный элемент списка 
        /// </summary>
        public int Counter { get; private set; }

        /// <summary>
        /// Число элементов в списке
        /// </summary>
        public int CountList { get; private set; }

        /// <summary>
        /// Данные загружены и готовы к использованию
        /// </summary>
        public static bool ReadComplit { get; private set; }

        #endregion

        #region Конструкторы

        public TestControl()
        {
            // прочитать файл один раз
            if (!ReadComplit)
            {
                ReadComplit = InitTempsExs();
            }

            CountList = temps.Count;
            Counter = 0;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Заполнить коллекцию значениями температур масла и воды из файла
        /// </summary>
        public bool InitTempsExs()
        {
            // получить данные из файла
            if (exsel.ReceiveData() != null)
            {
                var excelData = exsel.ReceiveData();

                for (int i = 2; i < excelData.GetUpperBound(0) + 1; i++)
                {
                    temps.Add(new TestData((sbyte)excelData[i, 1], (sbyte)excelData[i, 2]));
                }

                // файл прочитан
                return true;
            }
            else
                // файл не прочитан
                return false;
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

}

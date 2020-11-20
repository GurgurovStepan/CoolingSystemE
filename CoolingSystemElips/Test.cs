using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolingSystemElips
{
    class Test
    {
        private TestControl testControl;

        #region Поля

        /// <summary>
        /// интенсивность выполнения теста
        /// </summary>
        private sbyte testRate;

        #endregion

        #region Свойства

        /// <summary>
        /// запустить тест
        /// </summary>
        public bool Start { get; set; }

        /// <summary>
        /// завершить тест
        /// </summary>
        public bool Stop { get; set; }

        /// <summary>
        /// Интенсивность выполнения теста
        /// </summary>
        public sbyte TestRate
        {
            get
            {
                return testRate;
            }
            set
            {
                if (value >= 0 && value <= 100) testRate = value;
            }
        }

        /// <summary>
        /// Тест готов к выполнению
        /// </summary>
        public bool Ready { get; private set; }

        /// <summary>
        /// Тест выполнен
        /// </summary>
        public bool Completed { get; private set; }

        /// <summary>
        /// Текущее значение температуры масла
        /// </summary>
        public int CurTempOil { get; private set; }

        /// <summary>
        /// Текущее значение температуры воды
        /// </summary>
        public int CurTempWater { get; private set; }

        #endregion

        #region Конструкторы

        public Test() : this(0) { }

        public Test(sbyte _testRate)
        {
            testControl = new TestControl();

            Start = false;
            Stop = true;
            TestRate = _testRate;
            Ready = TestControl.ReadComplit;
            Completed = false;
            CurTempOil = 0;
            CurTempWater = 0;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Получить текущие значения температур масла и воды
        /// </summary>
        public void GetCurTemps()
        {
            if (!Completed)
            {
                sbyte to = 0;
                sbyte tw = 0;
                Completed = testControl.GetTemps(ref to, ref tw);
                CurTempOil = to;
                CurTempWater = tw;
            }
        }

        /// <summary>
        /// Узнать число элементов в списке
        /// </summary>
        /// <returns>число элементов в списке</returns>
        public int GetMaxNumberElements()
        {
            return testControl.CountList;
        }

        /// <summary>
        /// Узнать значение указателя 
        /// </summary>
        /// <returns>Указатель на прочитанный элемент списка </returns>
        public int GetCurrentCounter()
        {
            return testControl.Counter;
        }

        #endregion
    }
}

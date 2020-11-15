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
        /// запустить тест
        /// </summary>
        private bool start;
        
        /// <summary>
        /// завершить тест
        /// </summary>
        private bool stop;
        
        /// <summary>
        /// тест готов к выполнению
        /// </summary>
        private bool ready;
        
        /// <summary>
        /// тест выполнен
        /// </summary>
        private bool completed;
        
        /// <summary>
        /// интенсивность выполнения теста
        /// </summary>
        private sbyte testRate;

        #endregion

        #region Свойства
        public bool Start
        {
            get
            {
                return start;
            }
            set
            {
                start = value;
            }
        }

        public bool Stop
        {
            get
            {
                return stop;
            }
            set
            {
                stop = value;
            }
        }
        
        /// <summary>
        /// Интенсивность выполнения теста
        /// </summary>
        public sbyte TestRate
        {
            get
            {
                return testRate;
            }
            private set
            {
                testRate = value;
            }
        }

        /// <summary>
        /// Тест готов к выполнению
        /// </summary>
        public bool Ready
        {
            get
            {
                return ready;
            }
            private set
            {
                ready = value;
            }
        }

        /// <summary>
        /// Тест выполнен
        /// </summary>
        public bool Completed
        {
            get
            {
                return completed;
            }
            private set
            {
                completed = value;
            }
        }

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
            Start = false;
            Stop = true;
            TestRate = _testRate;
            Ready = false;
            Completed = false;
            CurTempOil = 0;
            CurTempWater = 0;

            testControl = new TestControl();
        }

        #endregion

        #region Методы

        /// <summary>
        /// Получить текущие значения температур масла и воды
        /// </summary>
        public void GetCurTemps()
        {
            if (!completed)
            {
                sbyte to = 0;
                sbyte tw = 0;
                completed = testControl.GetTemps(ref to, ref tw);
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

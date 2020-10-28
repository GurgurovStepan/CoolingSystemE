using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolingSystemElips
{
    class Test
    {
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
        /// номер теста
        /// </summary>
        private byte lvl;
        /// <summary>
        /// тест  готов к выполнению
        /// </summary>
        private bool ready;
        /// <summary>
        /// тест завершен
        /// </summary>
        private bool completed;

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
    
        public byte LvL 
        {
            get 
            {
                return lvl;
            }
            set 
            {
                lvl = value;
            }
        }
    
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
    
        public bool Completed 
        {
            get 
            {
                return completed;    
            }
            set 
            {
                completed = value;
            }
        }

        #endregion

        #region Конструкторы

        public Test() 
        {
            Start = false;
            Stop = true;
            LvL = 0;
            Ready = false;
            Completed = false;
        }

        #endregion
    }
}

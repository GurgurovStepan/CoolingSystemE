using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolingSystemElips
{
    class Motor
    {
        #region Константы
        
        /// <summary>
        /// Включен - on = 1  
        /// </summary>
        public readonly byte on = 1;
        /// <summary>
        /// Отключен - off = 0
        /// </summary>
        public readonly byte off = 0;

        #endregion

        #region Поля

        /// <summary>
        /// Номер
        /// </summary>
        private byte number;

        /// <summary>
        /// Состояние (вкл./откл.)
        /// </summary>
        private byte status;

        /// <summary>
        /// время работы
        /// </summary>
        private UInt64 workTime;

        /// <summary>
        /// количество включений
        /// </summary>
        private UInt32 numberTurnOn;

        /// <summary>
        ///  Время включения 
        /// </summary>
        private DateTime startTime;
        
        /// <summary>
        ///  Время отключения 
        /// </summary>
        private DateTime stopTime;

        /// <summary>
        /// Время работы мотора за цикл вкл./откл.
        /// </summary>
        private TimeSpan workInterval;


        #endregion

        #region Свойства

        public byte Number
        {
            get
            {
                return number;
            }
            set
            {
                number = value;
            }
        }
        
        /// <summary>
        /// Состояние: 1 - on, 0 - off
        /// </summary>
        public byte Status
        {
            get
            {
                return status;
            }
            private set
            {
               status = value;
            }
        }
        public UInt64 WorkTime
        {
            get
            {
                return workTime;
            }
            private set
            {
                workTime = value;
            }
        }
        public UInt32 NumberTurnOn
        {
            get
            {
                return numberTurnOn;
            }
            private set
            {
                numberTurnOn = value;
            }
        }

        #endregion

        #region Конструкторы

        public Motor() : this (0) { }
        public Motor(byte num)
        {
            Number = num;
            Status = off;
            WorkTime = 0;
            NumberTurnOn = 0;

            startTime = DateTime.Now;
            stopTime = DateTime.Now;
            workInterval = TimeSpan.Zero;
        }

        #endregion

        #region Методы

        #region public

        /// <summary>
        /// Включить мотор
        /// </summary>
        public void TurnOn()
        {
            Status = on;
            startTime = DateTime.Now;
        }

        /// <summary>
        /// Отключить мотор
        /// </summary>
        public void TurnOff()
        {
            CalculationNumberStarts();
            Status = off;
            stopTime = DateTime.Now;
            CalculationWorkTime();
        }

        #endregion

        #region private
        /// <summary>
        /// Расчет времени работы мотора
        /// </summary>
        private void CalculationWorkTime()
        {
            workInterval = stopTime - startTime;
            WorkTime = (UInt64)(workInterval.TotalSeconds) + WorkTime;
        }
        /// <summary>
        /// Расчет количесвтва включений мотора
        /// </summary>
        private void CalculationNumberStarts()
        {
            if (Status == on)
            {
                NumberTurnOn++;
            }
        }
        #endregion

        #endregion
    }
}

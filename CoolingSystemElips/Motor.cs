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
        /// Включить - on = 1  
        /// </summary>
        private readonly byte on = 1;
        /// <summary>
        /// Отключить - off = 0
        /// </summary>
        private readonly byte off = 0;

        #endregion

        #region Поля

        /// <summary>
        /// Порядковый номер
        /// </summary>
        private byte number;

        /// <summary>
        /// Команда: вкл. - 1 / откл. - 0 
        /// </summary>
        private byte status;

        /// <summary>
        /// Состояние включен
        /// </summary>
        private bool statusOn;
        
        /// <summary>
        /// Состояние отключен
        /// </summary>
        private bool statusOff;

        /// <summary>
        /// Температура включение по маслу
        /// </summary>
        private sbyte tempOilOn;

        /// <summary>
        /// Температура отключения по маслу
        /// </summary>
        private sbyte tempOilOff;

        /// <summary>
        /// Температура включения по воде
        /// </summary>
        private sbyte tempWaterOn;

        /// <summary>
        /// Температура отключения по воде
        /// </summary>
        private sbyte tempWaterOff;

        /// <summary>
        /// Время работы
        /// </summary>
        private UInt64 workTime;

        /// <summary>
        /// Количество включений
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
        /// Команда : вкл. - 1 / откл. - 0
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
       
        /// <summary>
        /// Состояние включен
        /// </summary>
        public bool StatusOn 
        {
            get 
            {
                return statusOn;
            }
            private set 
            {
                statusOn = value;
            }
        }

        /// <summary>
        /// Состояние отключен
        /// </summary>
        public bool StatusOff 
        { 
            get 
            {
                return statusOff;
            }
            private set 
            {
                statusOff = value;
            }
        }

        /// <summary>
        /// Температура включение по маслу
        /// </summary>
        public sbyte TempOilOn
        {
            get 
            { 
                return tempOilOn; 
            }
            set 
            {
                if (value > -80 && value < 120)
                    tempOilOn = value;
                else tempOilOn = 0;        
            }
        }

        /// <summary>
        /// Температура отключения по маслу
        /// </summary>
        public sbyte TempOilOff
        {
            get
            {
                return tempOilOff;
            }
            set
            {
                if (value > -80 && value < 120)
                    tempOilOff = value;
                else tempOilOff = 0;
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
            StatusOn = false;
            StatusOff = true;
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
            if (StatusOff)
            {
                Status = on;
                StatusOn = true;
                StatusOff = false;
                startTime = DateTime.Now;
            }
        }

        /// <summary>
        /// Отключить мотор
        /// </summary>
        public void TurnOff()
        {
            if (StatusOn)
            {
                Status = off;
                StatusOn = false;
                StatusOff = true;
                stopTime = DateTime.Now;
                CalculationWorkTime();
                CalculationNumberStarts();
            }
        }

        #endregion

        #region private
        /// <summary>
        /// Расчет времени работы мотора
        /// </summary>
        private void CalculationWorkTime()
        {
            if (stopTime > startTime) 
            {
                workInterval = stopTime - startTime;
            }
            else 
            {
                workInterval = TimeSpan.Zero;
            }
            
            WorkTime = (UInt64)(workInterval.TotalSeconds) + WorkTime;
        }
        /// <summary>
        /// Расчет количесвтва включений мотора
        /// </summary>
        private void CalculationNumberStarts()
        { 
            NumberTurnOn++;
        }
        #endregion

        #endregion
    }
}

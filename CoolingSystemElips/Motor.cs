using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CoolingSystemElips
{
    class Motor
    {
        public event EventHandler StatusOnChanged;

        #region Константы

        /// <summary>
        /// Включить - on = 1  
        /// </summary>
        //private readonly byte on = 1;
        ///// <summary>
        ///// Отключить - off = 0
        ///// </summary>
        //private readonly byte off = 0;

        /// <summary>
        /// Минимальная температура охладителя  
        /// </summary>
        private readonly sbyte minTemp = -87;

        /// <summary>
        /// Максимальная температура охладителя  
        /// </summary>
        private readonly sbyte maxTemp = 127;

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
        private sbyte? tempOilOn;

        /// <summary>
        /// Температура отключения по маслу
        /// </summary>
        private sbyte? tempOilOff;

        /// <summary>
        /// Температура включения по воде
        /// </summary>
        private sbyte? tempWaterOn;

        /// <summary>
        /// Температура отключения по воде
        /// </summary>
        private sbyte? tempWaterOff;

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

        /// <summary>
        /// Порядковый номер
        /// </summary>
        public byte Number
        {
            get
            {
                return number;
            }
            set
            {
                if (value > 0)
                    number = value;
                else number = 0;
            }
        }

        /// <summary>
        /// Команда : вкл. - 1 / откл. - 0 / error - 2
        /// </summary>
        //public byte Status
        //{
        //    get
        //    {
        //        return status;
        //    }
        //    private set
        //    {
        //        if (value == 0 || value == 1)
        //            status = value;
        //        else status = 2;    // error
        //    }
        //}
       
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

                if (StatusOnChanged != null)
                {
                    StatusOnChanged(this, new EventArgs());
                }
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
        public sbyte? TempOilOn
        {
            get 
            { 
                return tempOilOn; 
            }
            set 
            {
                if (value > minTemp && value < maxTemp)
                    tempOilOn = value;
                else tempOilOn = maxTemp;        
            }
        }

        /// <summary>
        /// Температура отключения по маслу
        /// </summary>
        public sbyte? TempOilOff
        {
            get
            {
                return tempOilOff;
            }
            set
            {
                if (value > minTemp && value < maxTemp)
                    tempOilOff = value;
                else tempOilOff = maxTemp;
            }
        }

        /// <summary>
        /// Температура включение по воде
        /// </summary>
        public sbyte? TempWaterOn
        {
            get
            {
                return tempWaterOn;
            }
            set
            {
                if (value > minTemp && value < maxTemp)
                    tempWaterOn = value;
                else tempWaterOn = maxTemp;
            }
        }

        /// <summary>
        /// Температура отключения по воде
        /// </summary>
        public sbyte? TempWaterOff
        {
            get
            {
                return tempWaterOff;
            }
            set
            {
                if (value > minTemp && value < maxTemp)
                    tempWaterOff = value;
                else tempWaterOff = maxTemp;
            }
        }
        
        /// <summary>
        /// Время работы (в секундах)
        /// </summary>
        public UInt64 WorkTime
        {
            get
            {
                return workTime;
            }
            private set
            {
                if (value >= UInt64.MinValue && value <= UInt64.MaxValue)
                    workTime = value;
                else workTime = 0;
            }
        }

        /// <summary>
        /// Количество включений
        /// </summary>
        public UInt32 NumberTurnOn
        {
            get
            {
                return numberTurnOn;
            }
            private set
            {
                if (value >= UInt32.MinValue && value <= UInt32.MaxValue)
                    numberTurnOn = value;
                else numberTurnOn = 0;
            }
        }

        #endregion

        #region Конструкторы

        public Motor() : this (0) { }
        public Motor(byte num) : this (num, null, null, null, null) { }
        /// <summary>
        /// Создать мотор с номером и температурными уставками
        /// </summary>
        /// <param name="num">порядковый номер</param>
        /// <param name="toon">Температура включение по маслу</param>
        /// <param name="tooff">Температура отключения по маслу</param>
        /// <param name="twon">Температура включение по воде</param>
        /// <param name="twoff">Температура отключения по воде</param>
        public Motor(byte num, sbyte? toon, sbyte? tooff, sbyte? twon, sbyte? twoff) 
        {
            Number = num;
            //Status = off;
            StatusOn = false;
            StatusOff = true;
            WorkTime = 0;
            NumberTurnOn = 0; 
            TempOilOn = toon;
            TempOilOff = tooff;
            TempWaterOn = twon;
            TempWaterOff = twoff;

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
                //Status = on;
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
                //Status = off;
                StatusOn = false;
                StatusOff = true;
                stopTime = DateTime.Now;
                CalculationWorkTime();
                //CalculationNumberStarts();
            }
        }

        public void ResStatsMotor() 
        {
            //Status = off;
            StatusOn = false;
            StatusOff = true;
            WorkTime = 0;
            NumberTurnOn = 0;

            startTime = DateTime.Now;
            stopTime = DateTime.Now;
            workInterval = TimeSpan.Zero;
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
        public void CalculationNumberStarts()
        { 
            NumberTurnOn++;
        }
        #endregion

        #endregion
    }
}

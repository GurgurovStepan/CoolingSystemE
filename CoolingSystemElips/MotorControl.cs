using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolingSystemElips
{
    class MotorControl : Motor
    {

        #region Поля

        private int? tempOilValue;
        private int? tempWaterValue;

        #endregion

        #region Методы

        /// <summary>
        /// Контроль вкл./откл. МВ в зависимости от температуры ОЖ
        /// </summary>
        /// <param name="to">Температура масла</param>
        /// <param name="tw">Температура воды</param>
        public void tempControl(int? to, int? tw, Motor mt)
        {
            bool allowTurnOn = false;
            bool allowTurnOff = false;

            

        }

        #endregion

    }
}

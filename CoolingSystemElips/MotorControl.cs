using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolingSystemElips
{
    class MotorControl
    {

        #region Поля
        #endregion

        #region Методы

        /// <summary>
        /// Контроль вкл./откл. МВ в зависимости от температуры ОЖ
        /// </summary>
        /// <param name="to">Температура масла</param>
        /// <param name="tw">Температура воды</param>
        public void tempControl(int? to, int? tw, Motor mt)
        {
            if (mt != null && to != null && tw != null)
            {
                // Температура масла или воды превышена
                if (to > mt.TempOilOn || tw > mt.TempWaterOn)
                {
                    mt.TurnOn();
                }
                else
                {
                    // Температура масла и воды в норме
                    if (to < mt.TempOilOff && tw < mt.TempWaterOff)
                    {
                        mt.TurnOff();
                    }
                }
            }
        }

        #endregion

    }
}

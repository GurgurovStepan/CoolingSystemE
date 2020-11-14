using System;
using Microsoft.Office.Interop.Excel;

namespace ExselDate
{
    class WorkExсel
    {
        #region Поля

        /// <summary>
        /// Имя Excel файла
        /// </summary>
        private string xlFileName = "Data\\test.xlsx";

        // Для работы с Excel
        private Range Rng;
        private Application xlApp;
        private Workbook xlWB;
        private Worksheet xlSht;
        private int iLastRow, iLastCol;
        
        /// <summary>
        /// Путь до файла
        /// </summary>
        private string pathToFile;

        #endregion

        #region Свойства
        #endregion

        #region Методы

        /// <summary>
        /// Получить данные из файла
        /// </summary>
        /// <returns>двухмерный массив объектов (строка/столбец), error = null</returns>
        public dynamic ReceiveData()
        {
            try
            {                
                pathToFile = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory);     // Текущая директория                                                                                            
                pathToFile = pathToFile + xlFileName;                                           // Директрория до файла

                xlApp = new Application();                  // Создаём приложение Excel
                xlWB = xlApp.Workbooks.Open(pathToFile);    // Открываем файл           
                xlSht = xlWB.ActiveSheet;                   // Активный лист

                iLastRow = xlSht.Cells[xlSht.Rows.Count, "A"].End[XlDirection.xlUp].Row;            // Последняя заполненная строка в столбце А
                iLastCol = xlSht.Cells[1, xlSht.Columns.Count].End[XlDirection.xlToLeft].Column;    // Последний заполненный столбец в 1-й строке
                
                                                                                                    // или
                // var lrow = xlApp.WorksheetFunction.CountA(xlSht.Columns[1]);                     // Последняя заполненная строка в столбце А
                // var lcol = xlApp.WorksheetFunction.CountA(xlSht.Rows[1]);                        // Последний заполненный столбец в 1-й строке

                Rng = (Range)xlSht.Range["A1", xlSht.Cells[iLastRow, iLastCol]];    //  Запись диапазона ячеек в переменную Rng

                var dataArr = (object[,])Rng.Value;     // Чтение данных из ячеек в массив  
               
                xlWB.Close(false);     // Закрыть файл без сохранения
                xlApp.Quit();          // Закрытие Excel

                // Освододить ресурс
                releaseObject(xlSht);
                releaseObject(xlWB);
                releaseObject(xlApp);

                return dataArr;
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                System.Windows.Forms.MessageBox.Show(" Неисправен Microsoft Excel или \n отсутствует файл test.xlsx!\n\n" +
                    " Создайте файл test.xlsx в директории:\n\n" + pathToFile + "\n\n"+
                    " Дополнительная информация об исключении:\n\n"+ ex.ToString(), "COM Error");

                if (xlSht != null) xlSht = null;
                if (xlWB != null) xlWB = null;
                if (xlApp != null) xlApp = null;

                GC.Collect();
                
                return null;
            }
        }

        /// <summary>
        /// Освободить ресурс
        /// </summary>
        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                System.Windows.Forms.MessageBox.Show("Unable to release the Object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        #endregion
    }
}

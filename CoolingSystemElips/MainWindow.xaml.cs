﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CoolingSystemElips
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        Motor[] motor = new Motor[5];
        MotorControl motorControl = new MotorControl();
        Test mainTest = new Test();

        private void tempOil_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (tempOil.Value != null && tempWater.Value != null) 
            {
                TurnOnMotors((int)tempOil.Value,(int)tempWater.Value);
            }
        }

        private void tempWater_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (tempOil.Value != null && tempWater.Value != null)
            {
                TurnOnMotors((int)tempOil.Value, (int)tempWater.Value);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // Создать моторы с температурными уставками
                motor[0] = new Motor(0);                        // Мотор Null               
                motor[1] = new Motor(1, 75, 72, null, null);    // Мотор 1
                motor[2] = new Motor(2, 79, 75, null, null);    // Мотор 2
                motor[3] = new Motor(3, 79, 75, 83, 75);        // Мотор 3
                motor[4] = new Motor(4, null, null, 79, 76);    // Мотор 4

                // Добавить коллекцию в ComboBox  
                string[] testNumber = new string[4] { "1", "2", "3", "4" };
                selectTest.Items.Clear();

                foreach (string tn in testNumber)
                {
                    selectTest.Items.Add(tn);
                }

                // Инициализируем NumericUpDown.Value
                tempOil.Value = 0;
                tempWater.Value = 0;
            }
            catch (IndexOutOfRangeException ex)
            {
                string mes = ex.Message;
                string cap = "Ошибка";
                System.Windows.Forms.MessageBoxButtons but = System.Windows.Forms.MessageBoxButtons.OK;

                System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show(mes, cap, but);
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    this.Close();
                }
            }
        }

        /// <summary>
        /// Выбор теста
        /// </summary>
        private void selectTest_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (selectTest.SelectedItem != null)
            {
                mainTest.LvL = byte.Parse(selectTest.SelectedItem.ToString());
            }
        }

        private void startStopTest_Checked(object sender, RoutedEventArgs e)
        {
            startStopTest.Content = "Завершить тест";
            tempOil.IsEnabled = false;
            tempWater.IsEnabled = false;
        }

        private void startStopTest_Unchecked(object sender, RoutedEventArgs e)
        {
            startStopTest.Content = "Запустить тест";
            tempOil.IsEnabled = true;
            tempWater.IsEnabled = true;
        }

        /// <summary>
        /// Отобразить состояние моторов (вкл - красный, отключен - синий)
        /// </summary>
        private void DrawMotors()
        {
            var red = Brushes.Red;
            var blue = Brushes.Blue;

            if (motor[1].StatusOn) fan1.Fill = red;
            if (motor[1].StatusOff) fan1.Fill = blue;

            if (motor[2].StatusOn) fan2.Fill = red;
            if (motor[2].StatusOff) fan2.Fill = blue;

            if (motor[3].StatusOn) fan3.Fill = red;
            if (motor[3].StatusOff) fan3.Fill = blue;


            if (motor[4].StatusOn) fan4.Fill = red;
            if (motor[4].StatusOff) fan4.Fill = blue;
        }

        /// <summary>
        /// Отобразить статистику
        /// </summary>
        private void DrawStatistics()
        {
            WorkTimeFan1.Text = motor[1].WorkTime.ToString();
            NumberTurnOnFan1.Text = motor[1].NumberTurnOn.ToString();

            WorkTimeFan2.Text = motor[2].WorkTime.ToString();
            NumberTurnOnFan2.Text = motor[2].NumberTurnOn.ToString();

            WorkTimeFan3.Text = motor[3].WorkTime.ToString();
            NumberTurnOnFan3.Text = motor[3].NumberTurnOn.ToString();

            WorkTimeFan4.Text = motor[4].WorkTime.ToString();
            NumberTurnOnFan4.Text = motor[4].NumberTurnOn.ToString();
        }

        private void TurnOnMotors(int tempOilValue, int tempWaterValue)
        {
            for (int i = 1; i < motor.Length; i++)
            {
                motorControl.tempControl(tempOilValue, tempWaterValue, motor[i]);
            }

            DrawMotors();
            DrawStatistics();
        }
    }
}

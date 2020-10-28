using System;
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

        byte maxNumberMotor = 4;

        Motor[] motor = new Motor[4];
        List<Motor> mot = new List<Motor>();

        private void tempOil_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var value = (((Xceed.Wpf.Toolkit.CommonNumericUpDown<int>)(sender)).Value);

            if (value != null)
            {
                if (value > 72)
                {
                    if (motor[1].Status == motor[1].off) 
                    {
                        fan1.Fill = System.Windows.Media.Brushes.Red;
                        motor[1].TurnOn();
                    }
                }
                else
                {
                    if (motor[1].Status == motor[1].on)
                    {
                        fan1.Fill = System.Windows.Media.Brushes.Blue;
                        motor[1].TurnOff();                        
                    }
                }

                WorkTimeFan1.Text = motor[1].WorkTime.ToString();
                NumberTurnOnFan1.Text = motor[1].NumberTurnOn.ToString();
            }

        }

        private void tempWater_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                for (byte i = 1; i <= motor.Length; i++)
                {
                    motor[i - 1] = new Motor(i);
                }

                for (byte i = 0; i < maxNumberMotor; i++)
                {
                    Motor m = new Motor(i);
                    mot.Add(m);
                }

                string[] testNumber = new string[4] { "1", "2", "3", "4" };
                selectTest.Items.Clear();

                foreach (string tn in testNumber)
                {
                    selectTest.Items.Add(tn);
                }
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

        private void selectTest_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (selectTest.SelectedItem != null)
            {
                // MainTest.LvL = byte.Parse(selectTest.SelectedItem.ToString());
            }
        }

        private void startStopTest_Checked(object sender, RoutedEventArgs e)
        {
            startStopTest.Content = "Завершить тест";
        }

        private void startStopTest_Unchecked(object sender, RoutedEventArgs e)
        {
            startStopTest.Content = "Запустить тест";
        }
    }
}

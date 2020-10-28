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
      
        Motor[] motor = new Motor[5];
        
        private void tempOil_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var value = (((Xceed.Wpf.Toolkit.CommonNumericUpDown<int>)(sender)).Value);
            var red = Brushes.Red;
            var blue = Brushes.Blue; 
           
            if (value != null)
            {
                if (value > 75)
                {
                    fan1.Fill = red;
                    motor[1].TurnOn();
                }
                else
                {
                    if (value < 72) 
                    {
                        fan1.Fill = blue;
                        motor[1].TurnOff();
                    }
                }
                
                if (value > 79) 
                {
                    fan2.Fill = red;
                    motor[2].TurnOn();

                    fan3.Fill = red;
                    motor[3].TurnOn();
                }
                else 
                {
                    if (value < 75)
                    {
                        fan2.Fill = blue;
                        motor[2].TurnOff();

                        fan3.Fill = blue;
                        motor[3].TurnOff();
                    }
                }

                Draw();

            }
        }

        private void tempWater_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var value = (((Xceed.Wpf.Toolkit.CommonNumericUpDown<int>)(sender)).Value);
            var red = Brushes.Red;
            var blue = Brushes.Blue;

            if (value != null) 
            { 
                if (value > 79) 
                {
                    fan4.Fill = red;
                    motor[4].TurnOn();
                }
                else 
                { 
                    if (value < 76) 
                    {
                        fan4.Fill = blue;
                        motor[4].TurnOff();
                    }
                }

                if (value > 83) 
                {
                    fan3.Fill = red;
                    motor[3].TurnOn();
                }
                else 
                {
                    if (value < 75) 
                    {
                        fan3.Fill = blue;
                        motor[3].TurnOff();
                    }
                }

                Draw();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                for (byte i = 1; i <= motor.Length; i++)
                {
                    motor[i - 1] = new Motor(i);
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

        private void Draw() 
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
    }
}

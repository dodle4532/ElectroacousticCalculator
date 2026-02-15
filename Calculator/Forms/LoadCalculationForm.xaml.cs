using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Calculator.Class;

namespace Calculator.Forms
{
    /// <summary>
    /// Логика взаимодействия для LoadCalculationForm.xaml
    /// </summary>
    public partial class LoadCalculationForm : Window
    {
        MainWindow window;
        public LoadCalculationForm(MainWindow window)
        {
            InitializeComponent();
            this.window = window;
            CalculationDBHelp dBHelp = new CalculationDBHelp();
            List<Calculation> list = dBHelp.GetAllCalculations();
            cbName.ItemsSource = list;
        }

        private void btLoad_Click(object sender, RoutedEventArgs e)
        {
            if (cbName.SelectedItem == null)
            {
                MessageBox.Show("Выберите рассчет");
                return;
            }
            Calculation calc = (Calculation)cbName.SelectedItem;
            window.LoadLastCalculation(calc);
            Close();
        }
    }
}

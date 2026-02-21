using Calculator.Class;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator.Views
{
    /// <summary>
    /// Логика взаимодействия для LoadCalculationControl.xaml
    /// </summary>
    public partial class LoadCalculationControl : UserControl
    {
        MainWindow window;
        public LoadCalculationControl(MainWindow window)
        {
            InitializeComponent();
            this.window = window;
            Update();
        }

        public void Update()
        {
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
            window.LoadCalculation(calc);
            window.LoadStartPage();
        }
    }
}

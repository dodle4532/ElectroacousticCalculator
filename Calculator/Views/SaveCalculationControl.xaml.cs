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
using System.Xml.Linq;

namespace Calculator.Views
{
    /// <summary>
    /// Логика взаимодействия для SaveCalculation.xaml
    /// </summary>
    public partial class SaveCalculationControl : UserControl
    {
        MainWindow window;
        public SaveCalculationControl(MainWindow window)
        {
            InitializeComponent();
            this.window = window;
        }
        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            if (tbName.Text == "")
            {
                MessageBox.Show("Введите имя");
                return;
            }
            window.SaveCurCalculation(tbName.Text);
            MessageBox.Show("Рассчет сохранен");
            tbName.Text = "";
            window.ShowCalculatedParams();
        }
    }
}

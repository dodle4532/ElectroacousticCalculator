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

namespace Calculator.Forms
{
    /// <summary>
    /// Логика взаимодействия для SaveCalculationForm.xaml
    /// </summary>
    public partial class SaveCalculationForm : Window
    {
        MainWindow window;
        public SaveCalculationForm(MainWindow window)
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
            MessageBox.Show("Успешно");
            Close();
         }
    }
}

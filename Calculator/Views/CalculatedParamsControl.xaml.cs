using System;
using System.Windows;
using System.Windows.Controls;
using Calculator.Class;  // Добавить для NumMethods

namespace Calculator.Views
{
    public partial class CalculatedParamsControl : UserControl
    {
        private MainWindow parent;

        // Добавляем ссылки на другие контролы
        private SpeakerParamsControl speakerControl;
        private MountParamsControl mountControl;
        private RoomParamsControl roomControl;

        public CalculatedParamsControl(MainWindow mainWindow)
        {
            InitializeComponent();
            parent = mainWindow;
        }

        private void btnCalc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверяем выбран ли громкоговоритель
                if (parent.GetSelectedSpeaker() == null)
                {
                    MessageBox.Show("Выберите громкоговоритель");
                    return;
                }

                // Получаем значения из контролов
                double H = parent.GetH();
                double UH = parent.GetUH();
                double U_vh = parent.GetUvh();
                double delta = parent.GetDelta();
                double USH_p = parent.GetUSH();
                double a = parent.GetA();
                double b = parent.Get_b();
                double h = parent.Get_h();
                double N = parent.GetN();
                double a1 = parent.GetA1();
                double a2 = parent.GetA2();

                // Проверка заполнения полей
                if (H == 0 || UH == 0 || U_vh == 0 || delta == 0 ||
                    USH_p == 0 || a == 0 || b == 0 || h == 0 ||
                    N == 0 || a1 == 0 || a2 == 0)
                {
                    MessageBox.Show("Заполните все поля");
                    return;
                }

                // Расчеты
                double V = a * b * h;
                double S = 2 * (a * b + a * h + b * h);
                double a_ekv = 1 / S * ((S - 0.17 * N) * a1 + 0.17 * N * a2);
                double S_sr = S * a_ekv;
                double B = a_ekv * S / (1 - a_ekv);
                double t_r = 0.164 * V / (-S * Math.Log(1 - a_ekv));

                // Вывод результатов
                tb_V.Text = NumMethods.FormatDouble(V);
                tb_S.Text = NumMethods.FormatDouble(S);
                tb_a_ekv.Text = NumMethods.FormatDouble(a_ekv);
                tb_S_sr.Text = NumMethods.FormatDouble(S_sr);
                tb_B.Text = NumMethods.FormatDouble(B);
                tb_t_r.Text = NumMethods.FormatDouble(t_r);
            }
            catch
            {
                MessageBox.Show("Проверьте правильность введенных данных");
            }
        }

        // Остальные методы (GetV, GetS, SetValues, ClearFields) остаются без изменений
        

        public void SetValues(double v, double s, double aEkv, double sSr, double b, double tr)
        {
            tb_V.Text = NumMethods.FormatDouble(v);
            tb_S.Text = NumMethods.FormatDouble(s);
            tb_a_ekv.Text = NumMethods.FormatDouble(aEkv);
            tb_S_sr.Text = NumMethods.FormatDouble(sSr);
            tb_B.Text = NumMethods.FormatDouble(b);
            tb_t_r.Text = NumMethods.FormatDouble(tr);
        }

        public void ClearFields()
        {
            tb_V.Text = "";
            tb_S.Text = "";
            tb_a_ekv.Text = "";
            tb_S_sr.Text = "";
            tb_B.Text = "";
            tb_t_r.Text = "";
        }
    }
}
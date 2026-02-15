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
using Calculator.Class;
using Calculator.Forms;
using AppContext = Calculator.Class.AppContext;

namespace Calculator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SpeakerDBHelper speakerDBHelper = new SpeakerDBHelper();
        CalculationDBHelp calculationDBHelp = new CalculationDBHelp();
        List<Speaker> speakers;
        int selectedSpeakerId = -1;
        Calculation curCalculation = new Calculation();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cbType.Items.Add("Потолочный");
            cbType.Items.Add("Настенный");
            UpdateSpeakers();
        }

        public void LoadLastCalculation(Calculation calculation)
        {
            if (calculation == null)
            {
                return;
            }
            tb_H.Text = NumMethods.FormatDouble(calculation.H);
            tb_UH.Text = NumMethods.FormatDouble(calculation.UH);
            tb_U_vh.Text = NumMethods.FormatDouble(calculation.U_vh);
            tb_delta.Text = NumMethods.FormatDouble(calculation.delta);
            tb_USH_p.Text = NumMethods.FormatDouble(calculation.USH_p);
            tb_a.Text = NumMethods.FormatDouble(calculation.a);
            tb_b.Text = NumMethods.FormatDouble(calculation.b_);
            tb_h.Text = NumMethods.FormatDouble(calculation.h_);
            tb_N.Text = NumMethods.FormatDouble(calculation.N);
            tb_a1.Text = NumMethods.FormatDouble(calculation.a1);
            tb_a2.Text = NumMethods.FormatDouble(calculation.a2);
            tb_V.Text = NumMethods.FormatDouble(calculation.V);
            tb_S.Text = NumMethods.FormatDouble(calculation.S);
            tb_a_ekv.Text = NumMethods.FormatDouble(calculation.a_ekv);
            tb_S_sr.Text = NumMethods.FormatDouble(calculation.S_sr);
            tb_B.Text = NumMethods.FormatDouble(calculation.B);
            tb_t_r.Text = NumMethods.FormatDouble(calculation.t_r);
            cbType.Text = calculation.SpeakerType.ToString();
            cbModel.SelectedItem = speakers.Find(x => x.Id == calculation.SpeakerId);
            if (cbModel.SelectedItem != null)
            {
                selectedSpeakerId = ((Speaker)cbModel.SelectedItem).Id;
            }
        }

        private void MenuSpeaker_Click(object sender, RoutedEventArgs e)
        {
            SpeakerConfig speakerConfig = new SpeakerConfig(this);
            speakerConfig.Show();
        }

        public void UpdateSpeakers()
        {
            speakers = speakerDBHelper.GetAllSpeakers();
            int id = selectedSpeakerId;
            cbModel.ItemsSource = null;
            cbModel.ItemsSource = speakers;
            selectedSpeakerId = id;
            if (selectedSpeakerId != -1)
            {
                cbModel.SelectedIndex = speakers.FindIndex(x => x.Id == selectedSpeakerId);
                UpdateCurSpeaker();
            }
        }
        public void UpdateCurSpeaker()
        {
            Speaker speaker = (Speaker)cbModel.SelectedItem;
            if (speaker == null)
            {
                return;
            }
            tb_P_Vt.Text = NumMethods.FormatDouble(speaker.P_Vt);
            tb_P_01.Text = NumMethods.FormatDouble(speaker.P_01[2]);
            tb_SHDN_v.Text = NumMethods.FormatDouble(speaker.SHDN_v[4]);
            tb_SHDN_g.Text = NumMethods.FormatDouble(speaker.SHDN_g[4]);
        }
        public void cbModel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateCurSpeaker();
            if (cbModel.SelectedItem != null)
            {
                selectedSpeakerId = ((Speaker)cbModel.SelectedItem).Id;
            }
            else
            {
                ButtonClear_Click(sender, null);
            }
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            cbType.Text = "";
            tb_P_Vt.Text = "";
            tb_P_01.Text = "";
            tb_SHDN_v.Text = "";
            tb_SHDN_g.Text = "";
            tb_H.Text = "";
            tb_UH.Text = "";
            tb_U_vh.Text = "";
            tb_delta.Text = "";
            tb_USH_p.Text = "";
            tb_a.Text = "";
            tb_b.Text = "";
            tb_h.Text = "";
            tb_N.Text = "";
            tb_a1.Text = "";
            tb_a2.Text = "";
            tb_V.Text = "";
            tb_S.Text = "";
            tb_a_ekv.Text = "";
            tb_S_sr.Text = "";
            tb_B.Text = "";
            tb_t_r.Text = "";
            cbModel.SelectedItem = null;
            selectedSpeakerId = -1;
        }

        private void ButtonCalc_Click(object sender, RoutedEventArgs e)
        {
            if (selectedSpeakerId == -1)
            {
                MessageBox.Show("Выберите громкоговоритель");
                return;
            }
            if (!Double.TryParse(tb_H.Text.Replace(".", ","), out double H)) { MessageBox.Show("Не введена высота установки громкоговорителя"); return; }
            if (!Double.TryParse(tb_UH.Text.Replace(".", ","), out double UH)) { MessageBox.Show("Не введен угол наклона громкоговорителя"); return; }
            if (!Double.TryParse(tb_U_vh.Text.Replace(".", ","), out double U_vh)) { MessageBox.Show("Не введено напряжение на входе линии оповещения"); return; }
            if (!Double.TryParse(tb_delta.Text.Replace(".", ","), out double delta)) { MessageBox.Show("Не введены максимальные потери по напряжению"); return; }
            if (!Double.TryParse(tb_USH_p.Text.Replace(".", ","), out double USH_p)) { MessageBox.Show("Не введен средний уровень шума в помещении"); return; }
            if (!Double.TryParse(tb_a.Text.Replace(".", ","), out double a)) { MessageBox.Show("Не введена длина помещения"); return; }
            if (!Double.TryParse(tb_b.Text.Replace(".", ","), out double b)) { MessageBox.Show("Не введена ширина помещения "); return; }
            if (!Double.TryParse(tb_h.Text.Replace(".", ","), out double h)) { MessageBox.Show("Не введена высота потолков"); return; }
            if (!Double.TryParse(tb_N.Text.Replace(".", ","), out double N)) { MessageBox.Show("Не введено количество людей"); return; }
            if (!Double.TryParse(tb_a1.Text.Replace(".", ","), out double a1)) { MessageBox.Show("Не введен средний коэффициент звукопоглощения поверхности"); return; }
            if (!Double.TryParse(tb_a2.Text.Replace(".", ","), out double a2)) { MessageBox.Show("Не введен коэффициент звукопоглощения 1 чел"); return; }
            double V = a * b * h;
            double S = 2 * (a * b + a* h + b * h);
            double a_ekv = 1 / S * ((S - 0.17 * N) * a1 + 0.17 * N * a2);
            double S_sr = S * a_ekv;
            double B = a_ekv * S / (1 - a_ekv);
            double t_r = 0.164 * V / (-S * Math.Log(1 - a_ekv));
            tb_V.Text = NumMethods.FormatDouble(V);
            tb_S.Text = NumMethods.FormatDouble(S);
            tb_a_ekv.Text = NumMethods.FormatDouble(a_ekv);
            tb_S_sr.Text = NumMethods.FormatDouble(S_sr);
            tb_B.Text = NumMethods.FormatDouble(B);
            tb_t_r.Text = NumMethods.FormatDouble(t_r);
    }

        

        private void MenuRoom_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (selectedSpeakerId == -1)
            {
                MessageBox.Show("Выберите громкоговоритель");
                return;
            }
            if (!Double.TryParse(tb_H.Text.Replace(".", ","), out double H)) { MessageBox.Show("Не введена высота установки громкоговорителя"); return; }
            if (!Double.TryParse(tb_UH.Text.Replace(".", ","), out double UH)) { MessageBox.Show("Не введен угол наклона громкоговорителя"); return; }
            if (!Double.TryParse(tb_U_vh.Text.Replace(".", ","), out double U_vh)) { MessageBox.Show("Не введено напряжение на входе линии оповещения"); return; }
            if (!Double.TryParse(tb_delta.Text.Replace(".", ","), out double delta)) { MessageBox.Show("Не введены максимальные потери по напряжению"); return; }
            if (!Double.TryParse(tb_USH_p.Text.Replace(".", ","), out double USH_p)) { MessageBox.Show("Не введен средний уровень шума в помещении"); return; }
            if (!Double.TryParse(tb_a.Text.Replace(".", ","), out double a)) { MessageBox.Show("Не введена длина помещения"); return; }
            if (!Double.TryParse(tb_b.Text.Replace(".", ","), out double b)) { MessageBox.Show("Не введена ширина помещения "); return; }
            if (!Double.TryParse(tb_h.Text.Replace(".", ","), out double h)) { MessageBox.Show("Не введена высота потолков"); return; }
            if (!Double.TryParse(tb_N.Text.Replace(".", ","), out double N)) { MessageBox.Show("Не введено количество людей"); return; }
            if (!Double.TryParse(tb_a1.Text.Replace(".", ","), out double a1)) { MessageBox.Show("Не введен средний коэффициент звукопоглощения поверхности"); return; }
            if (!Double.TryParse(tb_a2.Text.Replace(".", ","), out double a2)) { MessageBox.Show("Не введен коэффициент звукопоглощения 1 чел"); return; }
            double V = a * b * h;
            double S = 2 * (a * b + a * h + b * h);
            double a_ekv = 1 / S * ((S - 0.17 * N) * a1 + 0.17 * N * a2);
            double S_sr = S * a_ekv;
            double B = a_ekv * S / (1 - a_ekv);
            double t_r = 0.164 * V / (-S * Math.Log(1 - a_ekv));
            Calculation calculation = new Calculation();
            calculation.H = H;
            calculation.UH = UH;
            calculation.U_vh = U_vh;
            calculation.delta = delta;
            calculation.USH_p = USH_p;
            calculation.a = a;
            calculation.b_ = b;
            calculation.h_ = h;
            calculation.N = N;
            calculation.a1 = a1;
            calculation.a2 = a2;
            calculation.V = V;
            calculation.S = S;
            calculation.a_ekv = a_ekv;
            calculation.S_sr = S_sr;
            calculation.B = B;
            calculation.t_r = t_r;
            calculation.SpeakerId = selectedSpeakerId;
            calculation.SpeakerType = (DataTypes.SpeakerType)Enum.Parse(typeof(DataTypes.SpeakerType), cbType.Text);
            curCalculation = calculation;
            SaveCalculationForm saveCalculationForm = new SaveCalculationForm(this);
            saveCalculationForm.Show();
        }

        public void SaveCurCalculation(string name)
        {
            curCalculation.Name = name;
            calculationDBHelp.Add(curCalculation);
        }

        private void ButtonLoad_Click(object sender, RoutedEventArgs e)
        {
            LoadCalculationForm loadCalculationForm = new LoadCalculationForm(this);
            loadCalculationForm.Show();
        }
    }
}

using Calculator.Class;
using System.Windows;
using System.Windows.Controls;

namespace Calculator.Views
{
    public partial class MountParamsControl : UserControl
    {
        MainWindow parent;
        public MountParamsControl(MainWindow window)
        {
            parent = window;
            InitializeComponent();
            cbType.Items.Add("Потолочный");
            cbType.Items.Add("Настенный");
        }

        public void SetValues(double h, double uh, double uvh, double delta)
        {
            tb_H.Text = NumMethods.FormatDouble(h);
            tb_UH.Text = NumMethods.FormatDouble(uh);
            tb_U_vh.Text = NumMethods.FormatDouble(uvh);
            tb_delta.Text = NumMethods.FormatDouble(delta);
        }
        public DataTypes.SpeakerType GetSpeakerType()
        {
            if (cbType.Text == "")
            {
                throw new Exception("Не задан тип громкоговорителя");
            }
            return (DataTypes.SpeakerType)Enum.Parse(typeof(DataTypes.SpeakerType), cbType.Text);
        }

        public void SetSpeakerType(string type)
        {
            cbType.Text = type;
        }
        public void ClearFields()
        {
            tb_H.Text = "";
            tb_UH.Text = "";
            tb_U_vh.Text = "";
            tb_delta.Text = "";
            cbType.Text = "";
        }
        public void btnSave_Click(object sender, RoutedEventArgs e)
        {
            double H = parent.GetH();
            double UH = parent.GetUH();
            double U_vh = parent.GetUvh();
            double delta = parent.GetDelta();

            // Проверка заполнения полей
            if (H == 0 || UH == 0 || U_vh == 0 || delta == 0)
            {
                MessageBox.Show("Заполните все поля");
                return;
            }
            parent.NextSection();
        }
    }
}
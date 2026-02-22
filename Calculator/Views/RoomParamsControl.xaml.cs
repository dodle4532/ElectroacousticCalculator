using Calculator.Class;
using Calculator.Class;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Calculator.Views
{
    public partial class RoomParamsControl : UserControl
    {

        public class NoiseItem
        {
            public int val1 { get; set; }
            public int val2 { get; set; }
            public int val3 { get; set; }
            public int val4 { get; set; }
            public int val5 { get; set; }
            public string Metric { get; set; }
        }
        public class SurfaceItem
        {
            public double val1 { get; set; }
            public double val2 { get; set; }
            public double val3 { get; set; }
            public double val4 { get; set; }
            public double val5 { get; set; }
            public string Metric { get; set; }
        }

        int curNoiseLevel = -1;
        List<NoiseLevel> noiseLevels = new List<NoiseLevel>();
        CalculationDBHelp calculationDBHelp = new CalculationDBHelp();
        NoiseLevelDBHelp noiseLevelDBHelp = new NoiseLevelDBHelp();
        SurfaceDBHelp surfaceDBHelp = new SurfaceDBHelp();
        AcousticConstantDBHelp acousticConstantDBHelp = new AcousticConstantDBHelp();
        ApprSoundAbsorpDBHelp apprSoundAbsorpDBHelp = new ApprSoundAbsorpDBHelp();
        List<AcousticConstant> acousticConstants = new List<AcousticConstant>();
        List<ApprSoundAbsorp> apprSoundAbsorps = new List<ApprSoundAbsorp>();
        List<Surface> surfaces = new List<Surface>();
        ObservableCollection<SurfaceItem> surfaceItems = new ObservableCollection<SurfaceItem>();
        MainWindow parent;
        public RoomParamsControl(MainWindow window)
        {
            parent = window;
            InitializeComponent();
            noiseLevels = noiseLevelDBHelp.GetAllNoiseLevels();
            surfaces = surfaceDBHelp.GetAllSurfaces();
            acousticConstants = acousticConstantDBHelp.GetAllAcousticConstants();
            apprSoundAbsorps = apprSoundAbsorpDBHelp.GetAllApprSoundAbsorps();
            cbNoiseLevel.ItemsSource = noiseLevels;
            cbAcousticConstant.ItemsSource = acousticConstants;
            cbApprSoundAbsorp.ItemsSource = apprSoundAbsorps;
            cbBack.ItemsSource = surfaces;
            cbOpp.ItemsSource = surfaces;
            cbLeft.ItemsSource = surfaces;
            cbRight.ItemsSource = surfaces;
            cbBottom.ItemsSource = surfaces;
            cbCeil.ItemsSource = surfaces;

            DataGridSurface.ItemsSource = surfaceItems;
            LoadNoiseLevel(null);
            LoadSurfaces(null);
        }


        private void LoadNoiseLevel(NoiseLevel noiseLevel)
        {
            DataGridNoise.Items.Clear();
            if (noiseLevel == null)
            {
                DataGridNoise.Items.Add(new NoiseItem { val1 = 0, val2 = 0, val3 = 0, val4 = 0, val5 = 0, Metric="дБ" });
            }
            else
            {
                int[] vals = SpecialMathHelper.Proc_USH(noiseLevel.Ush);
                tb_USH_p.Text = noiseLevel.Ush.ToString();
                DataGridNoise.Items.Add(new NoiseItem { val1 = vals[0], val2 = vals[1], val3 = vals[2], val4 = vals[3], val5 = vals[4], Metric = "дБ" });
            }
        }

        private void LoadSurfaces(List<Surface> surfaces)
        {
            if (surfaces == null)
            {
                surfaceItems.Clear();
                surfaceItems.Add(new SurfaceItem { val1 = 0, val2 = 0, val3 = 0, val4 = 0, val5 = 0, Metric = "дБ" });
                surfaceItems.Add(new SurfaceItem { val1 = 0, val2 = 0, val3 = 0, val4 = 0, val5 = 0, Metric = "дБ" });
                surfaceItems.Add(new SurfaceItem { val1 = 0, val2 = 0, val3 = 0, val4 = 0, val5 = 0, Metric = "дБ" });
                surfaceItems.Add(new SurfaceItem { val1 = 0, val2 = 0, val3 = 0, val4 = 0, val5 = 0, Metric = "дБ" });
                surfaceItems.Add(new SurfaceItem { val1 = 0, val2 = 0, val3 = 0, val4 = 0, val5 = 0, Metric = "дБ" });
                surfaceItems.Add(new SurfaceItem { val1 = 0, val2 = 0, val3 = 0, val4 = 0, val5 = 0, Metric = "дБ" });
            }
            else
            {
                cbCeil.SelectedItem = surfaces[0];
                cbBack.SelectedItem = surfaces[1];
                cbOpp.SelectedItem = surfaces[2];
                cbLeft.SelectedItem = surfaces[3];
                cbRight.SelectedItem = surfaces[4];
                cbBottom.SelectedItem = surfaces[5];
            }
        }

        

        public void SetValues(double ush, double a, double b, double h, double n, double a1, double a2, int noiseLevelId, List<int> surfacesList, int ApprId, int AccConstId)
        {
            tb_USH_p.Text = NumMethods.FormatDouble(ush);
            tb_a.Text = NumMethods.FormatDouble(a);
            tb_b.Text = NumMethods.FormatDouble(b);
            tb_h.Text = NumMethods.FormatDouble(h);
            tb_N.Text = NumMethods.FormatDouble(n);
            tb_a1.Text = NumMethods.FormatDouble(a1);
            tb_a2.Text = NumMethods.FormatDouble(a2);
            NoiseLevel noiseLevel = noiseLevels.Find(x => x.Id ==  noiseLevelId);
            if (noiseLevel != null)
            {
                cbNoiseLevel.SelectedItem = noiseLevel;
            }
            else
            {
                LoadNoiseLevel(null);
            }
            List<Surface> list = new List<Surface>();
            if (surfacesList.Count == 0)
            {
                surfacesList.Add(-1);
                surfacesList.Add(-1);
                surfacesList.Add(-1);
                surfacesList.Add(-1);
                surfacesList.Add(-1);
                surfacesList.Add(-1);
            }
            foreach (var item in surfacesList)
            {
                Surface surface = surfaces.Find(x => x.Id == item);
                list.Add(surface);
            }
            LoadSurfaces(list);
            if (ApprId != -1)
            {
                cbApprSoundAbsorp.SelectedItem = apprSoundAbsorps.Find(x => x.Id == ApprId);
                RadioButton1.IsChecked = true;
            }
            if (AccConstId != -1)
            {
                cbAcousticConstant.SelectedItem = acousticConstants.Find(x => x.Id == AccConstId);
                RadioButton2.IsChecked = true;
            }
        }

        public void ClearFields()
        {
            tb_USH_p.Text = "";
            tb_a.Text = "";
            tb_b.Text = "";
            tb_h.Text = "";
            tb_N.Text = "";
            tb_a1.Text = "";
            tb_a2.Text = "";
            tb_a1_back_wall.Text = "";
            tb_a1_bottom.Text = "";
            tb_a1_left_wall.Text = "";
            tb_a1_right_wall.Text = "";
            tb_a1_opp_wall.Text = "";
            tb_a1_potolok.Text = "";
            cbNoiseLevel.SelectedIndex = -1;
            cbBack.SelectedIndex = -1;
            cbOpp.SelectedIndex = -1;
            cbLeft.SelectedIndex = -1;
            cbRight.SelectedIndex = -1;
            cbBottom.SelectedIndex = -1;
            cbCeil.SelectedIndex = -1;
            cbAcousticConstant.SelectedIndex = -1;
            cbApprSoundAbsorp.SelectedIndex = -1;
            RadioButton1.IsChecked = false;
            RadioButton2.IsChecked = false;
        }

        private void cbNoiseLevel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var row = cbNoiseLevel.SelectedItem as NoiseLevel;
            LoadNoiseLevel(row);
        }

        public int GetCurNoiseLevel()
        {
            var row = cbNoiseLevel.SelectedItem as NoiseLevel;
            if (row ==  null)
            {
                return -1;
            }
            return row.Id;
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataGridSurface.Items.Count > 0)
            {
                double rowHeight = (DataGridSurface.ActualHeight) / DataGridSurface.Items.Count;
                DataGridSurface.RowHeight = rowHeight;
            }
        }

        private void DataGrid_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            e.Handled = true;
        }

        private void cbCeil_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var row = cbCeil.SelectedItem as Surface;
            if (row == null)
            {
                surfaceItems[0] = new SurfaceItem { val1 = 0, val2 = 0, val3 = 0, val4 = 0, val5 = 0, Metric = "дБ" };
                return;
            }
            surfaceItems[0] = new SurfaceItem { val1 = row.F[0], val2 = row.F[1], val3 = row.F[2], val4 = row.F[3], val5 = row.F[4], Metric = "дБ" };
            parent.ScrollToRight();
            tb_a1_potolok.Text = NumMethods.FormatDouble(row.Ush);
        }
       


        private void cbBack_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var row = cbBack.SelectedItem as Surface;
            if (row == null)
            {
                surfaceItems[1] = new SurfaceItem { val1 = 0, val2 = 0, val3 = 0, val4 = 0, val5 = 0, Metric = "дБ" };
                return; 
            }
            surfaceItems[1] = new SurfaceItem { val1 = row.F[0], val2 = row.F[1], val3 = row.F[2], val4 = row.F[3], val5 = row.F[4], Metric = "дБ" };
            parent.ScrollToRight();
            tb_a1_back_wall.Text = NumMethods.FormatDouble(row.Ush);
        }

        private void cbOpp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var row = cbOpp.SelectedItem as Surface;
            if (row == null)
            {
                surfaceItems[2] = new SurfaceItem { val1 = 0, val2 = 0, val3 = 0, val4 = 0, val5 = 0, Metric = "дБ" };
                return;
            }
            surfaceItems[2] = new SurfaceItem { val1 = row.F[0], val2 = row.F[1], val3 = row.F[2], val4 = row.F[3], val5 = row.F[4], Metric = "дБ" };
            parent.ScrollToRight();
            tb_a1_opp_wall.Text = NumMethods.FormatDouble(row.Ush);
        }

        private void cbLeft_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var row = cbLeft.SelectedItem as Surface;
            if (row == null)
            {
                surfaceItems[3] = new SurfaceItem { val1 = 0, val2 = 0, val3 = 0, val4 = 0, val5 = 0, Metric = "дБ" };
                return;
            }
            surfaceItems[3] = new SurfaceItem { val1 = row.F[0], val2 = row.F[1], val3 = row.F[2], val4 = row.F[3], val5 = row.F[4], Metric = "дБ" };
            parent.ScrollToRight();
            tb_a1_left_wall.Text = NumMethods.FormatDouble(row.Ush);
        }

        private void cbRight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var row = cbRight.SelectedItem as Surface;
            if (row == null)
            {
                surfaceItems[4] = new SurfaceItem { val1 = 0, val2 = 0, val3 = 0, val4 = 0, val5 = 0, Metric = "дБ" };
                return;
            }
            surfaceItems[4] = new SurfaceItem { val1 = row.F[0], val2 = row.F[1], val3 = row.F[2], val4 = row.F[3], val5 = row.F[4], Metric = "дБ" };
            parent.ScrollToRight();
            tb_a1_right_wall.Text = NumMethods.FormatDouble(row.Ush);
        }

        private void cbBottom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var row = cbBottom.SelectedItem as Surface;
            if (row == null)
            {
                surfaceItems[5] = new SurfaceItem { val1 = 0, val2 = 0, val3 = 0, val4 = 0, val5 = 0, Metric = "дБ" };
                return;
            }
            surfaceItems[5] = new SurfaceItem { val1 = row.F[0], val2 = row.F[1], val3 = row.F[2], val4 = row.F[3], val5 = row.F[4], Metric = "дБ" };
            parent.ScrollToRight();
            tb_a1_bottom.Text = NumMethods.FormatDouble(row.Ush);
        }
        public List<int> GetSelectedSurfacesId()
        {
            List<int> res = new List<int>();
            var row = cbCeil.SelectedItem as Surface;
            if (row == null)
            {
                res.Add(-1);
            }
            else
            {
                res.Add(row.Id);
            }
            row = cbBack.SelectedItem as Surface;
            if (row == null)
            {
                res.Add(-1);
            }
            else
            {
                res.Add(row.Id);
            }
            row = cbOpp.SelectedItem as Surface;
            if (row == null)
            {
                res.Add(-1);
            }
            else
            {
                res.Add(row.Id);
            }
            row = cbLeft.SelectedItem as Surface;
            if (row == null)
            {
                res.Add(-1);
            }
            else
            {
                res.Add(row.Id);
            }
            row = cbRight.SelectedItem as Surface;
            if (row == null)
            {
                res.Add(-1);
            }
            else
            {
                res.Add(row.Id);
            }
            row = cbBottom.SelectedItem as Surface;
            if (row == null)
            {
                res.Add(-1);
            }
            else
            {
                res.Add(row.Id);
            }
            return res;
        }

        public int GetSelectedApprId()
        {
            var row = cbApprSoundAbsorp.SelectedItem as ApprSoundAbsorp;
            if (row == null)
            {
                return -1;
            }
            return row.Id;
        }
        public int GetSelectedAccConstId()
        {
            var row = cbAcousticConstant.SelectedItem as AcousticConstant;
            if (row == null)
            {
                return -1;
            }
            return row.Id;
        }

        private void cbNApprSoundAbsorp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var row = cbApprSoundAbsorp.SelectedItem as ApprSoundAbsorp;
            if (row != null)
            {
                tb_a1.Text = NumMethods.FormatDouble(row.A1);
            }
        }

        private void cbNAcousticConstant_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var row = cbAcousticConstant.SelectedItem as AcousticConstant;
            if (row != null)
            {
                tb_a1.Text = NumMethods.FormatDouble(row.A1);
            }
        }

        private void RadioButton1_Checked(object sender, RoutedEventArgs e)
        {
            cbApprSoundAbsorp.Visibility = Visibility.Visible;
            cbApprSoundAbsorp.IsEnabled = true;
            cbAcousticConstant.Visibility = Visibility.Collapsed;
            cbAcousticConstant.SelectedIndex = -1;
            RadioButton2.IsChecked = false;
        }
        private void RadioButton2_Checked(object sender, RoutedEventArgs e)
        {
            cbAcousticConstant.Visibility = Visibility.Visible;
            cbAcousticConstant.IsEnabled = true;
            cbApprSoundAbsorp.Visibility = Visibility.Collapsed;
            cbApprSoundAbsorp.SelectedIndex = -1;
            RadioButton1.IsChecked = false;
        }

        private void tb_t_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tb_t.Text == "")
            {
                btnCalc.Content = "Рассчитать";
            }
            else
            {
                if (!Double.TryParse(tb_t.Text.Replace(".", ","), out double val))
                {
                    tb_t.Text = tb_t.Text.Substring(0, tb_t.Text.Count() - 1);
                    return;
                }
                btnCalc.Content = "Сохранить";
            }
        }

        private void btnCalc_Click(object sender, RoutedEventArgs e)
        {
            if (btnCalc.Content.ToString() == "Рассчитать")
            {
                calcGrid.IsEnabled = true;
            }
            else
            {
                if (Double.TryParse(tb_t.Text.Replace(".", ","), out double val))
                {
                    parent.SetTime(val);
                }
            }
        }
    }
}
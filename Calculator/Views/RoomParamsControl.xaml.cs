using Calculator.Class;
using Calculator.Class;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using System.Xml.Linq;

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
        RoomDBHelp roomDBHelp = new RoomDBHelp();
        ObservableCollection<SurfaceItem> surfaceItems = new ObservableCollection<SurfaceItem>();
        List<Room> rooms = new List<Room>();
        MainWindow parent;
        int curRoomId = -1;
        public RoomParamsControl(MainWindow window)
        {
            parent = window;
            InitializeComponent();
            noiseLevels = noiseLevelDBHelp.GetAllNoiseLevels();
            surfaces = surfaceDBHelp.GetAllSurfaces();
            acousticConstants = acousticConstantDBHelp.GetAllAcousticConstants();
            apprSoundAbsorps = apprSoundAbsorpDBHelp.GetAllApprSoundAbsorps();
            cbNoiseLevel.ItemsSource = noiseLevels;
            LoadList();
            LoadNoiseLevel(null);
        }


        private void LoadNoiseLevel(NoiseLevel noiseLevel)
        {
            DataGridNoise.Items.Clear();
            if (noiseLevel == null)
            {
                DataGridNoise.Items.Add(new NoiseItem { val1 = 0, val2 = 0, val3 = 0, val4 = 0, val5 = 0, Metric="дБ" });
                int curNoiseLevel = -1;
                RadioButton3.IsChecked = true;
            }
            else
            {
                int[] vals = SpecialMathHelper.Proc_USH(noiseLevel.Ush);
                int curNoiseLevel = noiseLevel.Id;
                tb_USH_p.Text = noiseLevel.Ush.ToString();
                DataGridNoise.Items.Add(new NoiseItem { val1 = vals[0], val2 = vals[1], val3 = vals[2], val4 = vals[3], val5 = vals[4], Metric = "дБ" });
                if (noiseLevel.Sp == "51")
                {
                    RadioButton1.IsChecked = true;
                }
                else
                {
                    RadioButton2.IsChecked = true;
                }
                cbNoiseLevel.SelectedItem = noiseLevel;
            }
        }

        

        

        public void SetValues(double ush, double a, double b, double h, double n, int noiseLevelId, int roomId)
        {
            cbRoom.SelectedItem = rooms.Where(x => x.Id == roomId).FirstOrDefault();
            tb_USH_p.Text = NumMethods.FormatDouble(ush);
            tb_a.Text = NumMethods.FormatDouble(a);
            tb_b.Text = NumMethods.FormatDouble(b);
            tb_h.Text = NumMethods.FormatDouble(h);
            tb_N.Text = NumMethods.FormatDouble(n);
            NoiseLevel noiseLevel = noiseLevels.Find(x => x.Id ==  noiseLevelId);
            if (noiseLevel != null)
            {
                LoadNoiseLevel(noiseLevel);
            }
            else
            {
                LoadNoiseLevel(null);
            }
        }

        public void ClearFields()
        {
            Clear();
            cbRoom.SelectedIndex = -1;
            RadioButton1.IsChecked = false;
            RadioButton2.IsChecked = false;
            tbRoom.Text = "";
        }
        public void Clear()
        {
            tb_USH_p.Text = "";
            tb_a.Text = "";
            tb_b.Text = "";
            tb_h.Text = "";
            tb_N.Text = "";
            cbNoiseLevel.SelectedIndex = -1;
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

        public void btnSave_Click(object sender, RoutedEventArgs e)
        {
            double USH_p = parent.GetUSH();
            double a = parent.GetA();
            double b = parent.Get_b();
            double h = parent.Get_h();
            double N = parent.GetN();

            // Проверка заполнения полей
            if (USH_p == 0 || a == 0 || b == 0 || h == 0 ||
                N == 0)
            {
                MessageBox.Show("Заполните все поля");
                return;
            }
            parent.NextSection();
        }

        private void DataGrid_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            e.Handled = true;
        }

        public int GetCurRoomId()
        {
            var room = cbRoom.SelectedItem as Room;
            if (room == null)
            {
                return -1;
            }
            return room.Id;
        }

        void LoadList()
        {
            rooms = roomDBHelp.GetAllRooms();
            if (rooms == null)
            {
                ButtonAdd_Click(null, null);
                return;
            }
            cbRoom.ItemsSource = rooms;
        }

        public void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            StartEdit();
        }

        public void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            Room room = (Room)cbRoom.SelectedItem;
            if (room == null)
            {
                MessageBox.Show("Выберите помещение");
            }
            roomDBHelp.Remove(room);
            MessageBox.Show("Помещение удален");
            parent.UpdateRooms();
            LoadList();
        }


        private void StartEdit()
        {
            cbRoom.Visibility = Visibility.Collapsed;
            tbRoom.Visibility = Visibility.Visible;
            tbRoom.Text = "";
            parent.ButtonBack.Visibility = Visibility.Visible;
            parent.ButtonAdd.Visibility = Visibility.Collapsed;
            parent.ButtonDel.Visibility = Visibility.Collapsed;
            //if (curRoomId != -1)
            //{
            //    LoadCurRoom();
            //}
            //else
            //{
            //}
            Clear();
        }

        public void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            tbRoom.Visibility = Visibility.Collapsed;
            cbRoom.Visibility = Visibility.Visible;
            //cbRoom.SelectedIndex = 0;
            parent.ButtonAdd.Visibility = Visibility.Visible;
            parent.ButtonDel.Visibility = Visibility.Visible;
            parent.ButtonBack.Visibility = Visibility.Collapsed;
            if (curRoomId != -1)
            {
                LoadCurRoom();
            }
            //ClearEditGrid();
            //LoadList();
        }


        
        private void LoadCurRoom()
        {
            if (curRoomId != -1)
            {
                Room room = rooms.Find(x => x.Id == curRoomId);
                SetRoom(room);
            }
        }
        public void SetRoom(Room room)
        {
            tbRoom.Text = room.Name;
            SetValues(room.USH_p, room.a, room.b_, room.h_,
                                       room.N, room.NoiseLevel, room.Id);
            parent.SetAccousticParamControlValues(room.a1, room.Surfaces, room.ApprId, room.AccConstId, room.t_r);
        }


        


        public void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            Room room = GetCurRoom();
            if (room == null)
            {
                return;
            }
            if (tbRoom.Visibility == Visibility.Visible)
            {
                roomDBHelp.Add(room);
                parent.selectedRoomId = roomDBHelp.GetAllRooms().OrderBy(x => x.Id).Last().Id;
            }
            else
            {
                roomDBHelp.Update(curRoomId, room);
            }
            parent.UpdateRooms();
            MessageBox.Show("Параметры сохранены");
            ButtonBack_Click(sender, e);
        }

        

        private void cbRoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbRoom.SelectedItem != null)
            {
                curRoomId = ((Room)cbRoom.SelectedItem).Id;
                LoadCurRoom();
                parent.selectedRoomId = curRoomId;
            }
            else
            {
                curRoomId = -1;
                Clear();
            }
        }
        public Room GetSelectedRoom()
        {
            if (cbRoom.SelectedItem == null)
            {
                return new Room { Id = 0 };
            }
            return cbRoom.SelectedItem as Room;
        }
        public void UpdateRoomsList(List<Room> roomsList, int currentId)
        {
            rooms = roomsList;
            parent?.selectedRoomId = currentId;
            cbRoom.ItemsSource = rooms;

            if (parent?.selectedRoomId != -1)
            {
                cbRoom.SelectedIndex = rooms.FindIndex(x => x.Id == parent?.selectedRoomId);
            }
        }
        public void SetSelectedRoom(Room room)
        {
            cbRoom.SelectedIndex = rooms.FindIndex(x => x.Id == room.Id);
        }
        

        public Room GetCurRoom()
        {
            string name = tbRoom.Visibility == Visibility.Visible ? tbRoom.Text : cbRoom.Text;
            if (name == "")
            {
                MessageBox.Show("Введите наименование помещения");
                return null;
            }
            if ((tbRoom.Visibility == Visibility.Visible || name != rooms.Find(x => x.Id == curRoomId).Name) && rooms.Select(x => x.Name).Contains(name))
            {
                MessageBox.Show("Уже есть помещение с таким именем");
                return null;
            }
            if (!int.TryParse(tb_a.Text.Replace(".", ","), out int a))
            {
                MessageBox.Show("Неверный формат для длины");
                return null;
            }
            if (!int.TryParse(tb_b.Text.Replace(".", ","), out int b))
            {
                MessageBox.Show("Неверный формат для ширины");
                return null;
            }
            if (!int.TryParse(tb_h.Text.Replace(".", ","), out int h))
            {
                MessageBox.Show("Неверный формат для высоты");
                return null;
            }
            if (!int.TryParse(tb_N.Text.Replace(".", ","), out int N))
            {
                MessageBox.Show("Неверный формат для кол-ва людей");
                return null;
            }
            if (!int.TryParse(tb_USH_p.Text.Replace(".", ","), out int USH_p))
            {
                MessageBox.Show("Неверный формат для уровня шума");
                return null;
            }
            double t = parent.GetTr();
            //if (t == 0)
            //{
            //    MessageBox.Show("Не введено время реверберации");
            //    return null;
            //}


            Room room = new Room();
            room.Name = name;
            room.a = a;
            room.b_ = b;
            room.AccConstId = parent.GetSelectedAccConstId();
            room.ApprId = parent.GetSelectedApprId();
            room.NoiseLevel = GetCurNoiseLevel();
            room.h_ = h;
            room.N = N;
            room.Surfaces = parent.GetSelectedSurfacesId();
            room.t_r = parent.GetTr();
            room.a1 = parent.GetA1();
            room.USH_p = USH_p;
            return room;
        }

        private void RadioButton1_Click(object sender, RoutedEventArgs e)
        {
            RadioButton2.IsChecked = false;
            RadioButton3.IsChecked = false;
            cbNoiseLevel.IsEnabled = true;
            cbNoiseLevel.ItemsSource = noiseLevels.Where(x => x.Sp == "51");
            tb_USH_p.Background = new SolidColorBrush(Colors.LightGray);
            tb_USH_p.IsReadOnly = true;
            tb_USH_p.Text = "";
        }

        private void RadioButton2_Click(object sender, RoutedEventArgs e)
        {
            RadioButton1.IsChecked = false;
            RadioButton3.IsChecked = false;
            cbNoiseLevel.IsEnabled = true;
            cbNoiseLevel.ItemsSource = noiseLevels.Where(x => x.Sp == "3");
            tb_USH_p.Background = new SolidColorBrush(Colors.LightGray);
            tb_USH_p.IsReadOnly = true;
            tb_USH_p.Text = "";
        }

        private void RadioButton3_Click(object sender, RoutedEventArgs e)
        {
            RadioButton1.IsChecked = false;
            RadioButton2.IsChecked = false;
            cbNoiseLevel.IsEnabled = false;
            tb_USH_p.Background = new SolidColorBrush(Colors.White);
            tb_USH_p.IsReadOnly = false;
            
        }
    }
}
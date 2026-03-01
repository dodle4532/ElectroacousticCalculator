using Calculator.Class;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Логика взаимодействия для SpeakerConfigControl.xaml
    /// </summary>
    public partial class SpeakerConfigControl : UserControl
    {
        SpeakerDBHelper dBHelper = new SpeakerDBHelper();
        MainWindow parent;
        List<Speaker> speakers;
        int curSpeakerId = -1;
        bool isReadyForCopy = false;
        bool isNew = false;
        private class DataGridItem
        {
            public string Name { get; set; }
            public string Value1 { get; set; }
            public string Value2 { get; set; }
            public string Value3 { get; set; }
            public string Value4 { get; set; }
            public string Value5 { get; set; }
            public string Metric { get; set; }

        }
        string[] rows = { "Чувствительность громкоговорителя (1Вт/м)", "Угол раскрыва (-6дБ) по вертикали (ШДН на -6дБ)", "Угол раскрыва (-6дБ) по горизонтали (ШДН на -6дБ)" };
        string[] metrics = { "дБ", "Град", "Град" };
        public SpeakerConfigControl(MainWindow window)
        {
            this.parent = window;
            InitializeComponent();
            LoadList();
            CreateDataGrid(); // Создаем по умолчанию 5x3

        }



        void LoadList()
        {
            speakers = dBHelper.GetAllSpeakers();
            if (speakers == null)
            {
                ButtonAdd_Click(null, null);
                return;
            }
            cbName.ItemsSource = speakers;
        }

        public void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            StartEdit();
        }

        public void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            Speaker speaker = (Speaker)cbName.SelectedItem;
            if (speaker == null)
            {
                MessageBox.Show("Выберите громкоговоритель");
            }
            dBHelper.Remove(speaker);
            MessageBox.Show("Громкоговоритель удален");
            parent.UpdateSpeakers();
            LoadList();
        }


        private void StartEdit()
        {
            cbName.Visibility = Visibility.Collapsed;
            tbName.Visibility = Visibility.Visible;
            tbName.Text = "";
            parent.ButtonBack.Visibility = Visibility.Visible;
            parent.ButtonAdd.Visibility = Visibility.Collapsed;
            parent.ButtonDel.Visibility = Visibility.Collapsed;
            //if (curSpeakerId != -1)
            //{
            //    LoadCurSpeaker();
            //}
            //else
            //{
            //}
            Clear();
        }

        public void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            tbName.Visibility = Visibility.Collapsed;
            cbName.Visibility = Visibility.Visible;
            //cbName.SelectedIndex = 0;
            parent.ButtonAdd.Visibility = Visibility.Visible;
            parent.ButtonDel.Visibility = Visibility.Visible;
            parent.ButtonBack.Visibility = Visibility.Collapsed;
            if (curSpeakerId != -1)
            {
                LoadCurSpeaker();
            }
            //ClearEditGrid();
            //LoadList();
        }

        public void Clear()
        {
            ClearEditGrid();
            tbName.Text = "";
            tb_P_Vt.Text = "";
        }

        private void CreateDataGrid()
        {

            List<DataGridItem> list = new List<DataGridItem>();
            for (int row = 0; row < 3; row++)
            {
                DataGridItem item = new DataGridItem();
                item.Name = rows[row];
                item.Metric = metrics[row];
                item.Value1 = "";
                item.Value2 = "";
                item.Value3 = "";
                item.Value4 = "";
                item.Value5 = "";
                list.Add(item);
            }
            DataGrid.ItemsSource = list;

            // При изменении размера окна
            DataGrid.SizeChanged += (s, e) =>
            {
                if (DataGrid.Items.Count > 0)
                {
                    double rowHeight = (DataGrid.ActualHeight - 30) / DataGrid.Items.Count;
                    DataGrid.RowHeight = rowHeight;
                }
            };
        }
        private void LoadCurSpeaker()
        {
            if (curSpeakerId != -1)
            {
                Speaker speaker = speakers.Find(x => x.Id == curSpeakerId);
                SetSpeaker(speaker);
            }
        }

        // Добавь поля для Label
        private Label[] leftLabels;
        private Label[] rightLabels;

        public void SetSpeaker(Speaker speaker)
        {
            tbName.Text = speaker.Name;
            tb_P_Vt.Text = NumMethods.FormatDouble(speaker.P_Vt);
            List<DataGridItem> list = new List<DataGridItem>();
            for (int i = 0; i < 3; ++i)
            {
                DataGridItem item = new DataGridItem();
                item.Name = rows[i];
                item.Metric = metrics[i];
                switch (i)
                {
                    case 0:
                        {
                            item.Value1 = NumMethods.FormatDouble(speaker.P_01[0]);
                            item.Value2 = NumMethods.FormatDouble(speaker.P_01[1]);
                            item.Value3 = NumMethods.FormatDouble(speaker.P_01[2]);
                            item.Value4 = NumMethods.FormatDouble(speaker.P_01[3]);
                            item.Value5 = NumMethods.FormatDouble(speaker.P_01[4]);
                            break;
                        }
                    case 1:
                        {
                            item.Value1 = NumMethods.FormatDouble(speaker.SHDN_v[0]);
                            item.Value2 = NumMethods.FormatDouble(speaker.SHDN_v[1]);
                            item.Value3 = NumMethods.FormatDouble(speaker.SHDN_v[2]);
                            item.Value4 = NumMethods.FormatDouble(speaker.SHDN_v[3]);
                            item.Value5 = NumMethods.FormatDouble(speaker.SHDN_v[4]);
                            break;
                        }
                    case 2:
                        {
                            item.Value1 = NumMethods.FormatDouble(speaker.SHDN_g[0]);
                            item.Value2 = NumMethods.FormatDouble(speaker.SHDN_g[1]);
                            item.Value3 = NumMethods.FormatDouble(speaker.SHDN_g[2]);
                            item.Value4 = NumMethods.FormatDouble(speaker.SHDN_g[3]);
                            item.Value5 = NumMethods.FormatDouble(speaker.SHDN_g[4]);
                            break;
                        }
                }
                list.Add(item);
            }
            DataGrid.ItemsSource = list;
        }


        private void ClearEditGrid()
        {
            List<DataGridItem> list = new List<DataGridItem>();
            for (int row = 0; row < 3; row++)
            {
                DataGridItem item = new DataGridItem();
                item.Name = rows[row];
                item.Metric = metrics[row];
                item.Value1 = "";
                item.Value2 = "";
                item.Value3 = "";
                item.Value4 = "";
                item.Value5 = "";
                list.Add(item);
            }
            DataGrid.ItemsSource = list;
            tbName.Text = "";
            //curSpeakerId = -1;
            tb_P_Vt.Text = "";
        }


        public void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            Speaker speaker = GetCurSpeaker();
            if (speaker == null)
            {
                return;
            }
            if (tbName.Visibility == Visibility.Visible)
            {
                dBHelper.Add(speaker);
                parent.selectedSpeakerId = dBHelper.GetAllSpeakers().OrderBy(x => x.Id).Last().Id;
            }
            else
            {
                dBHelper.Update(curSpeakerId, speaker);
            }
            parent.UpdateSpeakers();
            MessageBox.Show("Параметры сохранены");
            ButtonBack_Click(sender, e);
        }

        private DataGridCell GetCellAtPosition(Point position)
        {
            var hit = DataGrid.InputHitTest(position) as DependencyObject;

            while (hit != null && !(hit is DataGridCell))
            {
                hit = VisualTreeHelper.GetParent(hit);
            }

            return hit as DataGridCell;
        }

        private void DataGrid_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            e.Handled = true;
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataGrid.Items.Count > 0)
            {
                double rowHeight = (DataGrid.ActualHeight - 30) / DataGrid.Items.Count; // 30 - высота заголовка
                DataGrid.RowHeight = rowHeight;
            }
        }

        private void cbName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbName.SelectedItem != null)
            {
                curSpeakerId = ((Speaker)cbName.SelectedItem).Id;
                LoadCurSpeaker();
                parent.selectedSpeakerId = curSpeakerId;
            }
            else
            {
                curSpeakerId = -1;
                Clear();
            }
        }

        // Обработчик нажатия клавиш в DataGrid
        private void UserControl_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                e.Handled = true; // Отменяем стандартное поведение

                // Получаем текущую ячейку
                var currentCell = DataGrid.CurrentCell;
                if (currentCell.Column != null)
                {
                    // Определяем следующую колонку
                    int nextColumnIndex = currentCell.Column.DisplayIndex + 1;

                    // Если дошли до конца строки, переходим на следующую строку
                    if (nextColumnIndex >= DataGrid.Columns.Count - 1)
                    {
                        // Переходим на следующую строку, первый столбец
                        var nextRowIndex = DataGrid.Items.IndexOf(currentCell.Item) + 1;
                        if (nextRowIndex == 3)
                        {
                            nextRowIndex = 0;
                        }
                        if (nextColumnIndex == DataGrid.Columns.Count - 1 && nextRowIndex == 2)
                        {
                            isReadyForCopy = true;
                        }
                        if (nextRowIndex < DataGrid.Items.Count)
                        {
                            var nextItem = DataGrid.Items[nextRowIndex];
                            var firstColumn = DataGrid.Columns[2];

                            // Устанавливаем текущую ячейку
                            DataGrid.CurrentCell = new DataGridCellInfo(nextItem, firstColumn);

                            // Запускаем редактирование
                            Dispatcher.BeginInvoke(new Action(() => {
                                DataGrid.BeginEdit();
                                DataGrid.UnselectAllCells();
                            }));
                        }
                    }
                    else
                    {
                        // Переходим на следующую колонку в той же строке
                        var nextColumn = DataGrid.Columns[Math.Max(2, nextColumnIndex)];
                        DataGrid.CurrentCell = new DataGridCellInfo(currentCell.Item, nextColumn);

                        Dispatcher.BeginInvoke(new Action(() => {
                            DataGrid.BeginEdit();
                            DataGrid.UnselectAllCells();
                        }));
                    }
                }
            }
        }

        // Когда DataGrid получает фокус
        private void DataGrid_GotFocus(object sender, RoutedEventArgs e)
        {
            // Если есть текущая ячейка, начинаем редактирование
            if (DataGrid.CurrentCell.Column != null)
            {
                Dispatcher.BeginInvoke(new Action(() => {
                    DataGrid.BeginEdit();
                    DataGrid.UnselectAllCells();
                }));
            }
        }

        // В DataGrid_PreviewMouseLeftButtonDown тоже обновим
        private void DataGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var cell = GetCellAtPosition(e.GetPosition(DataGrid));

            if (cell != null && cell.Column.DisplayIndex >= 0)
            {
                DataGrid.CurrentCell = new DataGridCellInfo(cell.DataContext, cell.Column);

                Dispatcher.BeginInvoke(new Action(() => {
                    DataGrid.BeginEdit();
                    DataGrid.UnselectAllCells();
                }));
            }

            e.Handled = true;
        }

        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (!isReadyForCopy)
            {
                return;
            }
                    
            // Разрешаем завершить редактирование
            e.Cancel = false;

            Dispatcher.BeginInvoke(new Action(() =>
            {
                // Принудительно завершаем редактирование

                var list = (List<DataGridItem>)DataGrid.ItemsSource;

                if (IsVerticalFill() && IsHorizontalEmpty())
                {
                    list[2].Value1 = list[1].Value1;
                    list[2].Value2 = list[1].Value2;
                    list[2].Value3 = list[1].Value3;
                    list[2].Value4 = list[1].Value4;
                    list[2].Value5 = list[1].Value5;
                    DataGrid.CommitEdit(DataGridEditingUnit.Row, true);

                    // Просто обновляем представление
                    DataGrid.Items.Refresh();
                    DataGrid.BeginEdit();
                }
            }), System.Windows.Threading.DispatcherPriority.Background);
        }
        public Speaker GetSelectedSpeaker()
        {
            if (cbName.SelectedItem == null)
            {
                return new Speaker { Id = 0};
            }
            return cbName.SelectedItem as Speaker;
        }
        public void UpdateSpeakersList(List<Speaker> speakersList, int currentId)
        {
            speakers = speakersList;
            parent?.selectedSpeakerId = currentId;
            cbName.ItemsSource = speakers;

            if (parent?.selectedSpeakerId != -1)
            {
                cbName.SelectedIndex = speakers.FindIndex(x => x.Id == parent?.selectedSpeakerId);
            }
        }
        public void SetSelectedSpeaker(Speaker speaker)
        {
            cbName.SelectedIndex = speakers.FindIndex(x => x.Id == speaker.Id);
        }
        /// <summary>
        /// Проверка, полностью ли заполнена строка угла раскрыва по горизонтали
        /// </summary>
        /// <returns></returns>
        private bool IsVerticalFill()
        {
            var row = ((DataGridItem)DataGrid.Items[1]);
            if (row.Value1 == "" || row.Value2 == "" || row.Value3 == "" || row.Value4 == "" || row.Value5 == "")
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Проверка, пустая ли строка угла раскрыва по горизонтали
        /// </summary>
        /// <returns></returns>
        private bool IsHorizontalEmpty()
        {
            var row = ((DataGridItem)DataGrid.Items[2]);
            if (row.Value1 == "" &&row.Value2 == "" &&row.Value3 == "" &&row.Value4 == "" &&row.Value5 == "")
            {
                return true;
            }
            return false;
        }

        public void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (curSpeakerId == -1 || tbName.Visibility == Visibility.Visible)
            {
                MessageBox.Show("Выберите громкоговоритель");
                return;
            }
            parent.NextSection();
        }

        public Speaker GetCurSpeaker()
        {
            DataGrid.CommitEdit(DataGridEditingUnit.Row, true);
            var allValues = new List<string>();
            List<DataGridItem> list = (List<DataGridItem>)DataGrid.ItemsSource;
            List<double[]> parametrs = new List<double[]>();
            string name = tbName.Visibility == Visibility.Visible ? tbName.Text : cbName.Text;
            if (name == "")
            {
                MessageBox.Show("Введите наименование громкоговорителя");
                return null;
            }
            if ((tbName.Visibility == Visibility.Visible || name != speakers.Find(x => x.Id == curSpeakerId).Name) && speakers.Select(x => x.Name).Contains(name))
            {
                MessageBox.Show("Уже есть громкоговоритель с таким именем");
                return null;
            }
            if (!Double.TryParse(tb_P_Vt.Text.Replace(".", ","), out double P_Vt))
            {
                MessageBox.Show("Неверный формат для мощности");
                return null;
            }
            if (P_Vt <= 0 && P_Vt > 500)
            {
                MessageBox.Show("Мощность громкоговорителя может находиться в пределах: >0 <=500Вт");
                return null;
            }
            if (list[0].Value3 == "")
            {
                MessageBox.Show("Заполните " + rows[0] + " для частоты 1 кГц");
                return null;
            }
            if (list[1].Value5 == "")
            {
                MessageBox.Show("Заполните " + rows[1] + " для частоты 4 кГц");
                return null;
            }
            if (list[2].Value5 == "")
            {
                MessageBox.Show("Заполните " + rows[2] + " для частоты 4 кГц");
                return null;
            }
            foreach (DataGridItem item in list)
            {
                if (!Double.TryParse(item.Value1 == "" ? "0" : item.Value1.Replace(".", ","), out double val1))
                {
                    MessageBox.Show("Неверный формат для одной из ячеек");
                    return null;
                }
                if (!Double.TryParse(item.Value2 == "" ? "0" : item.Value2.Replace(".", ","), out double val2))
                {
                    MessageBox.Show("Неверный формат для одной из ячеек");
                    return null;
                }
                if (!Double.TryParse(item.Value3 == "" ? "0" : item.Value3.Replace(".", ","), out double val3))
                {
                    MessageBox.Show("Неверный формат для одной из ячеек");
                    return null;
                }
                if (!Double.TryParse(item.Value4 == "" ? "0" : item.Value4.Replace(".", ","), out double val4))
                {
                    MessageBox.Show("Неверный формат для одной из ячеек");
                    return null;
                }
                if (!Double.TryParse(item.Value5 == "" ? "0" : item.Value5.Replace(".", ","), out double val5))
                {
                    MessageBox.Show("Неверный формат для одной из ячеек");
                    return null;
                }
                parametrs.Add(new double[] { val1, val2, val3, val4, val5 });
                if (parametrs.Count == 1)
                {
                    if ((val1 < 1 || val1 > 140) || (val2 < 1 || val2 > 140) || (val3 < 1 || val3 > 140) || (val4 < 1 || val4 > 140) || (val5 < 1 || val5 > 140))
                    {
                        MessageBox.Show("Чувствительность громкоговорителя не должна превышать 140дБ");
                        return null;
                    }
                }
                if (parametrs.Count == 2 || parametrs.Count == 3)
                {
                    if ((val1 < 10 || val1 > 360) || (val2 < 10 || val2 > 360) || (val3 < 10 || val3 > 360) || (val4 < 10 || val4 > 360) || (val5 < 10 || val5 > 360))
                    {
                        MessageBox.Show("Угол раскрыва должен находится в границах >= 10Град и <= 360Град");
                        return null;
                    }
                }
            }
            Speaker speaker = new Speaker();
            speaker.Name = name;
            speaker.P_Vt = P_Vt;
            speaker.P_01 = parametrs[0];
            speaker.SHDN_v = parametrs[1];
            speaker.SHDN_g = parametrs[2];
            return speaker;
        }
    }
}

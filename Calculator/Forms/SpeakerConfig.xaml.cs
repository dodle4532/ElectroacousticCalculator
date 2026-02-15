using Calculator.Class;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace Calculator.Forms
{
    /// <summary>
    /// Логика взаимодействия для SpeakerConfig.xaml
    /// </summary>
    public partial class SpeakerConfig : Window
    {
        SpeakerDBHelper dBHelper = new SpeakerDBHelper();
        MainWindow parent;
        List<Speaker> speakers;
        int curSpeakerId = -1;
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
        string[] rows = { "Чувствительность громкоговорителя", "Угол раскрыва (-6дБ) по вертикали", "Угол раскрыва (-6дБ) по горизонтали" };
        string[] metrics = { "дБ", "Град", "Град" };
        public SpeakerConfig(MainWindow parent)
        {
            this.parent = parent;
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
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

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            curSpeakerId = -1;
            StartEdit();
            //Speaker speaker = new Speaker();
            //speaker.Name = tbName.Text;
            //if (speakers.Select(x => x.Name).Contains(tbName.Text))
            //{
            //    MessageBox.Show("Уже есть громкоговоритель с таким именем");
            //    return;
            //}
            //speaker.P_Vt = Convert.ToDouble(tb_P_Vt.Text);
            //speaker.P_01 = Convert.ToDouble(tb_P_01.Text);
            //speaker.SHDN_v = Convert.ToDouble(tb_SHDN_v.Text);
            //speaker.SHDN_g = Convert.ToDouble(tb_SHDN_g.Text);
            //dBHelper.Add(speaker);
            //MessageBox.Show("Успешно");
            //LoadList();
        }

        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            Speaker speaker = (Speaker)cbName.SelectedItem;
            if (speaker == null)
            {
                MessageBox.Show("Выберите громкоговоритель");
            }
            dBHelper.Remove(speaker);
            MessageBox.Show("Успешно");
            parent.UpdateSpeakers();
            LoadList();
        }


        private void StartEdit()
        {
            cbName.Visibility = Visibility.Collapsed;
            tbName.Visibility = Visibility.Visible;
            ButtonBack.Visibility = Visibility.Visible;
            ButtonAdd.Visibility = Visibility.Collapsed;
            ButtonDel.Visibility = Visibility.Collapsed;
            if (curSpeakerId != -1)
            {
                LoadCurSpeaker();
            }
            else
            {
                Clear();
            }
        }

        private void LoadCurSpeaker()
        {
            if (curSpeakerId != -1)
            {
                Speaker speaker = speakers.Find(x => x.Id == curSpeakerId);
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
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            tbName.Visibility = Visibility.Collapsed;
            cbName.Visibility = Visibility.Visible;
            cbName.SelectedIndex = 0;
            ButtonBack.Visibility = Visibility.Collapsed;
            ButtonAdd.Visibility = Visibility.Visible;
            ButtonDel.Visibility = Visibility.Visible;
            ClearEditGrid();
            LoadList();
        }

        private void ClearEditGrid()
        {
            DataGrid.ItemsSource = null;
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
            curSpeakerId = -1;
            cbName.SelectedItem = null;
            tb_P_Vt.Text = "";
        }

        private void Clear()
        {
            ClearEditGrid();
            tbName.Text = "";
            tb_P_Vt.Text = "";
        }

        private void CreateDataGrid()
        {
            DataGrid.Columns.Clear();
            string[] freqs = { "0.25", "0.5", "1", "2", "4" };
            DataGrid.Columns.Add(new DataGridTextColumn
            {
                Header = "Название параметра",
                Binding = new Binding("Name"),
                Width = new DataGridLength(3, DataGridLengthUnitType.Star),
                ElementStyle = new Style(typeof(TextBlock))
                {
                    Setters = {
                            new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center),
                            new Setter(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Center),
                            new Setter(TextBlock.MarginProperty, new Thickness(5, 0, 5, 0))
                        }
                },
                IsReadOnly = true
            });
            for (int col = 0; col < 5; col++)
            {
                DataGrid.Columns.Add(new DataGridTextColumn
                {
                    Header = $"{freqs[col]} кГц",
                    Binding = new System.Windows.Data.Binding("Value" + (col + 1).ToString()),
                    Width = new DataGridLength(1, DataGridLengthUnitType.Star),
                    ElementStyle = new Style(typeof(TextBlock))
                    {
                        Setters = {
                            new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center),
                            new Setter(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Center),
                            new Setter(TextBlock.MarginProperty, new Thickness(5, 0, 5, 0))
                        }
                    }
                });
            }
            DataGrid.Columns.Add(new DataGridTextColumn
            {
                Header = "Ед.изм.",
                Binding = new Binding("Metric"),
                Width = new DataGridLength(0.7, DataGridLengthUnitType.Star),
                ElementStyle = new Style(typeof(TextBlock))
                {
                    Setters = {
                            new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center),
                            new Setter(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Center),
                            new Setter(TextBlock.MarginProperty, new Thickness(5, 0, 5, 0))
                        }
                },
                IsReadOnly = true
            });
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


        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            DataGrid.CommitEdit(DataGridEditingUnit.Row, true);
            var allValues = new List<string>();
            List<DataGridItem> list = (List<DataGridItem>)DataGrid.ItemsSource;
            List<double[]> parametrs = new List<double[]>();
            string name = curSpeakerId == -1 ? tbName.Text : cbName.Text;
            if (name == "")
            {
                MessageBox.Show("Введите имя");
                return;
            }
            if ((curSpeakerId == -1 || name != speakers.Find(x => x.Id == curSpeakerId).Name) && speakers.Select(x => x.Name).Contains(name))
            {
                MessageBox.Show("Уже есть громкоговоритель с таким именем");
                return;
            }
            if (!Double.TryParse(tb_P_Vt.Text.Replace(".", ","), out double P_Vt))
            {
                MessageBox.Show("Неверный формат для мощности");
                return;
            }
            if (list[0].Value3 == "")
            {
                MessageBox.Show("Заполните " + rows[0] + " для частоты 1 кГц");
                return;
            }
            if (list[1].Value5 == "")
            {
                MessageBox.Show("Заполните " + rows[1] + " для частоты 4 кГц");
                return;
            }
            if (list[2].Value5 == "")
            {
                MessageBox.Show("Заполните " + rows[2] + " для частоты 4 кГц");
                return;
            }
            foreach (DataGridItem item in list)
            {
                if (!Double.TryParse(item.Value1 == "" ? "0" : item.Value1.Replace(".", ","), out double val1))
                {
                    MessageBox.Show("Неверный формат для одной из ячеек");
                    return;
                }
                if (!Double.TryParse(item.Value2 == "" ? "0" : item.Value2.Replace(".", ","), out double val2))
                {
                    MessageBox.Show("Неверный формат для одной из ячеек");
                    return;
                }
                if (!Double.TryParse(item.Value3 == "" ? "0" : item.Value3.Replace(".", ","), out double val3))
                {
                    MessageBox.Show("Неверный формат для одной из ячеек");
                    return;
                }
                if (!Double.TryParse(item.Value4 == "" ? "0" : item.Value4.Replace(".", ","), out double val4))
                {
                    MessageBox.Show("Неверный формат для одной из ячеек");
                    return;
                }
                if (!Double.TryParse(item.Value5 == "" ? "0" : item.Value5.Replace(".", ","), out double val5))
                {
                    MessageBox.Show("Неверный формат для одной из ячеек");
                    return;
                }
                parametrs.Add(new double[] { val1, val2, val3, val4, val5 });
            }
            Speaker speaker = new Speaker();
            speaker.Name = name;
            speaker.P_Vt = P_Vt;
            speaker.P_01 = parametrs[0];
            speaker.SHDN_v = parametrs[1];
            speaker.SHDN_g = parametrs[2];
            if (curSpeakerId == -1)
            {
                dBHelper.Add(speaker);
            }
            else
            {
                dBHelper.Update(curSpeakerId, speaker);
            }
            parent.UpdateSpeakers();
            ButtonBack_Click(sender, e);
        }

        private void DataGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Получаем ячейку под курсором
            var cell = GetCellAtPosition(e.GetPosition(DataGrid));

            if (cell != null && cell.Column.DisplayIndex > 0) // Не первый столбец
            {
                // Устанавливаем текущую ячейку
                DataGrid.CurrentCell = new DataGridCellInfo(cell.DataContext, cell.Column);

                // Запускаем редактирование
                DataGrid.BeginEdit();
            }
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
            }
            else
            {
                curSpeakerId = -1;
                Clear();
            }
        }
    }
}

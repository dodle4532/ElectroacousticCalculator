using Calculator.Class;
using Calculator.Forms;
using Calculator.Views;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Linq;

namespace Calculator
{
    public partial class MainWindow : Window
    {
        SpeakerDBHelper speakerDBHelper = new SpeakerDBHelper();
        RoomDBHelp roomDBHelp = new RoomDBHelp();
        CalculationDBHelp calculationDBHelp = new CalculationDBHelp();
        List<Speaker> speakers;
        List<Room> rooms;
        public int selectedSpeakerId = -1;
        public int selectedRoomId = -1;
        public Speaker copyingSpeaker;
        public Room copyingRoom;
        Calculation curCalculation = new Calculation();

        // User Controls
        SpeakerConfigControl speakerConfigControl;
        MountParamsControl mountParamsControl;
        RoomParamsControl roomParamsControl;
        AccousticParamsControl accousticParamsControl;
        CalculatedParamsControl calculatedParamsControl;
        LoadCalculationControl loadCalculationControl;
        SaveCalculationControl saveCalculationControl;
        string curSection;

        List<string> sections = new List<string> { "SpeakerParams", "RoomParams", "AccousticParams", "MountParams", "CalculatedParams" };

        public MainWindow()
        {
            InitializeComponent();
            InitializeControls();
        }

        private void InitializeControls()
        {
            mountParamsControl = new MountParamsControl(this);
            roomParamsControl = new RoomParamsControl(this);
            calculatedParamsControl = new CalculatedParamsControl(this);
            accousticParamsControl = new AccousticParamsControl(this);
            loadCalculationControl = new LoadCalculationControl(this);
            saveCalculationControl = new SaveCalculationControl(this);
            speakerConfigControl = new SpeakerConfigControl(this);

            ShowSection("SpeakerParams");
        }

        private void ShowSection(string sectionTag)
        {
            switch (sectionTag)
            {
                case "SpeakerParams":
                    MainContentArea.Content = speakerConfigControl;
                    curSection = sectionTag;
                    ButtonAdd.Visibility = Visibility.Visible;
                    ButtonSave.Visibility = Visibility.Visible;
                    ButtonDel.Visibility = Visibility.Visible;
                    ButtonCopy.Visibility = Visibility.Visible;
                    ButtonPaste.Visibility = Visibility.Visible;
                    break;
                case "MountParams":
                    MainContentArea.Content = mountParamsControl;
                    curSection = sectionTag;
                    ButtonAdd.Visibility = Visibility.Collapsed;
                    ButtonSave.Visibility = Visibility.Collapsed;
                    ButtonDel.Visibility = Visibility.Collapsed;
                    ButtonCopy.Visibility = Visibility.Collapsed;
                    ButtonPaste.Visibility = Visibility.Collapsed;
                    break;
                case "RoomParams":
                    MainContentArea.Content = roomParamsControl;
                    curSection = sectionTag;
                    ButtonAdd.Visibility = Visibility.Visible;
                    ButtonSave.Visibility = Visibility.Visible;
                    ButtonDel.Visibility = Visibility.Visible;
                    ButtonCopy.Visibility = Visibility.Visible;
                    ButtonPaste.Visibility = Visibility.Visible;
                    break;
                case "AccousticParams":
                    MainContentArea.Content = accousticParamsControl;
                    curSection = sectionTag;
                    ButtonAdd.Visibility = Visibility.Visible;
                    ButtonSave.Visibility = Visibility.Visible;
                    ButtonDel.Visibility = Visibility.Visible;
                    ButtonCopy.Visibility = Visibility.Visible;
                    ButtonPaste.Visibility = Visibility.Visible;
                    break;
                case "CalculatedParams":
                    MainContentArea.Content = calculatedParamsControl;
                    ButtonAdd.Visibility = Visibility.Collapsed;
                    ButtonSave.Visibility = Visibility.Collapsed;
                    ButtonDel.Visibility = Visibility.Collapsed;
                    ButtonCopy.Visibility = Visibility.Collapsed;
                    ButtonPaste.Visibility = Visibility.Collapsed;
                    break;
                case "LoadCalculation":
                    MainContentArea.Content = loadCalculationControl;
                    ButtonAdd.Visibility = Visibility.Collapsed;
                    ButtonSave.Visibility = Visibility.Collapsed;
                    ButtonDel.Visibility = Visibility.Collapsed;
                    ButtonCopy.Visibility = Visibility.Collapsed;
                    ButtonPaste.Visibility = Visibility.Collapsed;
                    break;
                case "SaveCalculation":
                    MainContentArea.Content = saveCalculationControl;
                    ButtonAdd.Visibility = Visibility.Collapsed;
                    ButtonSave.Visibility = Visibility.Collapsed;
                    ButtonDel.Visibility = Visibility.Collapsed;
                    ButtonCopy.Visibility = Visibility.Collapsed;
                    ButtonPaste.Visibility = Visibility.Collapsed;
                    break;
                default:
                    MainContentArea.Content = null;
                    ButtonAdd.Visibility = Visibility.Collapsed;
                    ButtonSave.Visibility = Visibility.Collapsed;
                    ButtonDel.Visibility = Visibility.Collapsed;
                    ButtonCopy.Visibility = Visibility.Collapsed;
                    ButtonPaste.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        public void NextSection()
        {
            int index = sections.FindIndex(x => x == curSection);
            ShowSection(sections[index + 1]);
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                ShowSection(button.Tag.ToString());
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateSpeakers();
        }

        public void UpdateSpeakers()
        {
            speakers = speakerDBHelper.GetAllSpeakers();
            speakerConfigControl.UpdateSpeakersList(speakers, selectedSpeakerId);
        }
        public void UpdateRooms()
        {
            rooms = roomDBHelp.GetAllRooms();
            roomParamsControl.UpdateRoomsList(rooms, selectedRoomId);
        }

        public void UpdateCurSpeaker()
        {
            if (speakerConfigControl != null)
            {
                var speaker = speakerConfigControl.GetSelectedSpeaker();
                if (speaker != null)
                {
                    selectedSpeakerId = speaker.Id;
                }
            }
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            speakerConfigControl.Clear();
            mountParamsControl.ClearFields();
            roomParamsControl.ClearFields();
            accousticParamsControl.ClearFields();
            calculatedParamsControl.ClearFields();
            selectedSpeakerId = -1;
            ShowSection("SpeakerParams");
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (calculatedParamsControl.tb_a_ekv.Text == "")
            {
                MessageBox.Show("Сначала проведите рассчет");
                return;
            }

            try
            {
                var calculation = new Calculation
                {
                    H = GetH(),
                    UH = GetUH(),
                    U_vh = GetUvh(),
                    delta = GetDelta(),
                    V = GetV(),
                    S = GetS(),
                    a_ekv = GetAEkv(),
                    S_sr = GetSSr(),
                    B = GetB(),
                    SpeakerId = selectedSpeakerId,
                    SpeakerType = mountParamsControl.GetSpeakerType(),
                    Room = roomParamsControl.GetCurRoomId()
                };

                curCalculation = calculation;
                loadCalculationControl.Update();
                ShowSection("SaveCalculation");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}");
            }
        }
        /// <summary>
        /// Установить время реверберации
        /// </summary>
        /// <param name="time"></param>
        public void SetTime(double time)
        {
            calculatedParamsControl.SetT_R(time);
            ShowSection("CalculatedParams");
        }
        public Speaker GetSelectedSpeaker()
        {
            return speakerConfigControl.GetSelectedSpeaker();
        }

        private void ButtonLoad_Click(object sender, RoutedEventArgs e)
        {
            ShowSection("LoadCalculation");
        }

        public void SaveCurCalculation(string name)
        {
            curCalculation.Name = name;
            calculationDBHelp.Add(curCalculation);
            loadCalculationControl.Update();
        }

        public void LoadCalculation(Calculation calculation)
        {
            if (calculation == null) return;
            Room room = roomDBHelp.GetRoom(calculation.Room);
            mountParamsControl.SetValues(calculation.H, calculation.UH, calculation.U_vh, calculation.delta);
            roomParamsControl.SetValues(room.USH_p, room.a, room.b_, room.h_,
                                       room.N, room.NoiseLevel, room.Id);
            accousticParamsControl.SetValues(room.a1, room.Surfaces, room.ApprId, room.AccConstId, room.t_r);
            calculatedParamsControl.SetValues(calculation.V, calculation.S, calculation.a_ekv,
                                            calculation.S_sr, calculation.B, room.t_r);

            var speaker = speakers?.Find(x => x.Id == calculation.SpeakerId);
            if (speaker != null)
            {
                speakerConfigControl.SetSelectedSpeaker(speaker);
                selectedSpeakerId = speaker.Id;
            }
            mountParamsControl.SetSpeakerType(calculation.SpeakerType.ToString());
        }

        private void MenuSpeaker_Click(object sender, RoutedEventArgs e)
        {
            SpeakerConfig speakerConfig = new SpeakerConfig(this);
            speakerConfig.Owner = this;
            speakerConfig.ShowDialog();
        }

        public void LoadStartPage()
        {
            ShowSection("SpeakerParams");
        }

        public void UpdateCalculations()
        {
            loadCalculationControl.Update();
        }
        public void ShowCalculatedParams()
        {
            ShowSection("CalculatedParams");
        }

        public void ScrollToRight()
        {
            MainContentScroll.ScrollToRightEnd();
        }

        public double GetH()
        {
            return double.TryParse(mountParamsControl.tb_H.Text.Replace(".", ","), out double result) ? result : 0;
        }

        public double GetUH()
        {
            return double.TryParse(mountParamsControl.tb_UH.Text.Replace(".", ","), out double result) ? result : 0;
        }

        public double GetUvh()
        {
            return double.TryParse(mountParamsControl.tb_U_vh.Text.Replace(".", ","), out double result) ? result : 0;
        }

        public double GetDelta()
        {
            return double.TryParse(mountParamsControl.tb_delta.Text.Replace(".", ","), out double result) ? result : 0;
        }

        public double GetUSH()
        {
            return double.TryParse(roomParamsControl.tb_USH_p.Text.Replace(".", ","), out double result) ? result : 0;
        }

        public double GetA()
        {
            return double.TryParse(roomParamsControl.tb_a.Text.Replace(".", ","), out double result) ? result : 0;
        }

        public double Get_b()
        {
            return double.TryParse(roomParamsControl.tb_b.Text.Replace(".", ","), out double result) ? result : 0;
        }

        public double Get_h()
        {
            return double.TryParse(roomParamsControl.tb_h.Text.Replace(".", ","), out double result) ? result : 0;
        }

        public double GetN()
        {
            return double.TryParse(roomParamsControl.tb_N.Text.Replace(".", ","), out double result) ? result : 0;
        }

        public double GetA1()
        {
            return double.TryParse(accousticParamsControl.tb_a1.Text.Replace(".", ","), out double result) ? result : 0;
        }

        public double GetV() => double.TryParse(calculatedParamsControl.tb_V.Text.Replace(".", ","), out double result) ? result : 0;
        public double GetS() => double.TryParse(calculatedParamsControl.tb_S.Text.Replace(".", ","), out double result) ? result : 0;
        public double GetAEkv() => double.TryParse(calculatedParamsControl.tb_a_ekv.Text.Replace(".", ","), out double result) ? result : 0;
        public double GetSSr() => double.TryParse(calculatedParamsControl.tb_S_sr.Text.Replace(".", ","), out double result) ? result : 0;
        public double GetB() => double.TryParse(calculatedParamsControl.tb_B.Text.Replace(".", ","), out double result) ? result : 0;
        public double GetTr() => double.TryParse(accousticParamsControl.tb_t.Text.Replace(".", ","), out double result) ? result : 0;

        private void ButtonCalculate_Click(object sender, RoutedEventArgs e)
        {
            calculatedParamsControl.btnCalc_Click(sender, e);
            ShowSection("CalculatedParams");
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            if (curSection == "SpeakerParams")
                speakerConfigControl.ButtonBack_Click(sender, e);
            else roomParamsControl.ButtonBack_Click(sender, e);
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (curSection == "SpeakerParams")
                speakerConfigControl.ButtonAdd_Click(sender, e);
            else roomParamsControl.ButtonAdd_Click(sender, e);
        }

        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            if (curSection == "SpeakerParams")
                speakerConfigControl.ButtonDel_Click(sender, e);
            else roomParamsControl.ButtonDel_Click(sender, e);
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            switch (curSection)
            {
                case "SpeakerParams":
                    speakerConfigControl.btnSave_Click(sender, e);
                    break;
                case "MountParams":
                    mountParamsControl.btnSave_Click(sender, e);
                    break;
                case "AccousticParams":
                    accousticParamsControl.btnSave_Click(sender, e);
                    break;
                case "RoomParams":
                    roomParamsControl.btnSave_Click(sender, e);
                    break;
                default:
                    return;
            }
        }


        private void Button_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Button button = sender as Button;
            button.Background = new SolidColorBrush(Colors.LightBlue);
        }


        private void Button_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Button button = sender as Button;
            button.Background = new SolidColorBrush(Colors.Transparent);
        }

        private void ButtonPaste_Click(object sender, RoutedEventArgs e)
        {
            if (curSection == "SpeakerParams")
            {
                if (copyingSpeaker != null)
                {
                    speakerConfigControl.SetSpeaker(copyingSpeaker);
                }
            }
            else
            {
                if (copyingRoom != null)
                {
                    roomParamsControl.SetRoom(copyingRoom);
                }
            }
            
        }
        public void SetAccousticParamControlValues(double a1, List<int> surfacesList, int ApprId, int AccConstId, double t_r)
        {
            accousticParamsControl.SetValues(a1, surfacesList, ApprId, AccConstId, t_r);
        }

        private void ButtonCopy_Click(object sender, RoutedEventArgs e)
        {
            if (curSection == "SpeakerParams")
                copyingSpeaker = speakerConfigControl.GetCurSpeaker();
            else copyingRoom = roomParamsControl.GetCurRoom();
        }

        private void bntSave_Click(object sender, RoutedEventArgs e)
        {
            if (curSection == "SpeakerParams")
                speakerConfigControl.ButtonSave_Click(sender, e);
            else roomParamsControl.ButtonSave_Click(sender, e);
        }
        public List<int> GetSelectedSurfacesId()
        {
            return accousticParamsControl.GetSelectedSurfacesId();
        }
        public int GetSelectedApprId()
        {
            return accousticParamsControl.GetSelectedApprId();
        }
        public int GetSelectedAccConstId()
        {
            return accousticParamsControl.GetSelectedAccConstId();
        }
    }
}
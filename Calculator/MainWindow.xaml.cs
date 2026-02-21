using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Calculator.Class;
using Calculator.Forms;
using Calculator.Views;

namespace Calculator
{
    public partial class MainWindow : Window
    {
        SpeakerDBHelper speakerDBHelper = new SpeakerDBHelper();
        CalculationDBHelp calculationDBHelp = new CalculationDBHelp();
        List<Speaker> speakers;
        public int selectedSpeakerId = -1;
        Calculation curCalculation = new Calculation();

        // User Controls
        SpeakerConfigControl speakerConfigControl;
        MountParamsControl mountParamsControl;
        RoomParamsControl roomParamsControl;
        CalculatedParamsControl calculatedParamsControl;
        LoadCalculationControl loadCalculationControl;
        SaveCalculationControl saveCalculationControl;

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
                    break;
                case "MountParams":
                    MainContentArea.Content = mountParamsControl;
                    break;
                case "RoomParams":
                    MainContentArea.Content = roomParamsControl;
                    break;
                case "CalculatedParams":
                    MainContentArea.Content = calculatedParamsControl;
                    break;
                case "LoadCalculation":
                    MainContentArea.Content = loadCalculationControl;
                    break;
                case "SaveCalculation":
                    MainContentArea.Content = saveCalculationControl;
                    break;
                default:
                    MainContentArea.Content = null;
                    break;
            }
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
                    USH_p = GetUSH(),
                    a = GetA(),
                    b_ = Get_b(),
                    h_ = GetH(),
                    N = GetN(),
                    a1 = GetA1(),
                    a2 = GetA2(),
                    V = GetV(),
                    S = GetS(),
                    a_ekv = GetAEkv(),
                    S_sr = GetSSr(),
                    B = GetB(),
                    t_r = GetTr(),
                    SpeakerId = selectedSpeakerId,
                    SpeakerType = mountParamsControl.GetSpeakerType(),
                    NoiseLevel = roomParamsControl.GetCurNoiseLevel(),
                    Surfaces = roomParamsControl.GetSelectedSurfacesId()
                };

                curCalculation = calculation;
                ShowSection("SaveCalculation");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}");
            }
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
        }

        public void LoadCalculation(Calculation calculation)
        {
            if (calculation == null) return;

            mountParamsControl.SetValues(calculation.H, calculation.UH, calculation.U_vh, calculation.delta);
            roomParamsControl.SetValues(calculation.USH_p, calculation.a, calculation.b_, calculation.h_,
                                       calculation.N, calculation.a1, calculation.a2, calculation.NoiseLevel, calculation.Surfaces);
            calculatedParamsControl.SetValues(calculation.V, calculation.S, calculation.a_ekv,
                                            calculation.S_sr, calculation.B, calculation.t_r);

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
            return double.TryParse(roomParamsControl.tb_a1.Text.Replace(".", ","), out double result) ? result : 0;
        }

        public double GetA2()
        {
            return double.TryParse(roomParamsControl.tb_a2.Text.Replace(".", ","), out double result) ? result : 0;
        }
        public double GetV() => double.TryParse(calculatedParamsControl.tb_V.Text.Replace(".", ","), out double result) ? result : 0;
        public double GetS() => double.TryParse(calculatedParamsControl.tb_S.Text.Replace(".", ","), out double result) ? result : 0;
        public double GetAEkv() => double.TryParse(calculatedParamsControl.tb_a_ekv.Text.Replace(".", ","), out double result) ? result : 0;
        public double GetSSr() => double.TryParse(calculatedParamsControl.tb_S_sr.Text.Replace(".", ","), out double result) ? result : 0;
        public double GetB() => double.TryParse(calculatedParamsControl.tb_B.Text.Replace(".", ","), out double result) ? result : 0;
        public double GetTr() => double.TryParse(calculatedParamsControl.tb_t_r.Text.Replace(".", ","), out double result) ? result : 0;
    }
}
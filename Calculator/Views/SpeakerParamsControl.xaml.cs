using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Calculator.Class;
using Calculator.Forms;

namespace Calculator.Views
{
    public partial class SpeakerParamsControl : UserControl
    {
        public MainWindow parent;
        private List<Speaker> speakers;
        SpeakerConfigControl speakerConfigControl;

        public SpeakerParamsControl(MainWindow mainWindow)
        {
            InitializeComponent();
            parent = mainWindow;
            //speakerConfigControl = new SpeakerConfigControl(this);
        }

        public void UpdateSpeakersList(List<Speaker> speakersList, int currentId)
        {
            speakers = speakersList;
            parent?.selectedSpeakerId = currentId;
            cbModel.ItemsSource = speakers;

            if (parent?.selectedSpeakerId != -1)
            {
                cbModel.SelectedIndex = speakers.FindIndex(x => x.Id == parent?.selectedSpeakerId);
            }
        }

        private void cbModel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbModel.SelectedItem is Speaker speaker)
            {
                tb_P_Vt.Text = NumMethods.FormatDouble(speaker.P_Vt);
                tb_P_01.Text = NumMethods.FormatDouble(speaker.P_01[2]);
                tb_SHDN_v.Text = NumMethods.FormatDouble(speaker.SHDN_v[4]);
                tb_SHDN_g.Text = NumMethods.FormatDouble(speaker.SHDN_g[4]);
                parent?.selectedSpeakerId = speaker.Id;
            }
        }

        public Speaker GetSelectedSpeaker()
        {
            return cbModel.SelectedItem as Speaker;
        }

        public void SetSelectedSpeaker(Speaker speaker)
        {
            cbModel.SelectedItem = speaker;
        }

        

        public void ClearFields()
        {
            tb_P_Vt.Text = "";
            tb_P_01.Text = "";
            tb_SHDN_v.Text = "";
            tb_SHDN_g.Text = "";
            cbModel.SelectedItem = null;
        }

        private void setting_Click(object sender, RoutedEventArgs e)
        {
            parent.MainContentArea.Content = speakerConfigControl;
        }
    }
}
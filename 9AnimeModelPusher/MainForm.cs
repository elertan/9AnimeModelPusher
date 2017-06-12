using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _9AnimeModelPusher
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            ModelPusher.Instance.MadeProgress += Instance_MadeProgress;
        }

        private void Instance_MadeProgress(object sender, ModelPusherProgressEventArgs e)
        {
            ProgressBar.Value = Convert.ToInt32(e.ProgressPercentage);
            if (e.Status != null) StatusLabel.Text = e.Status;
        }

        private async void StartButton_Click(object sender, EventArgs e)
        {
            StartButton.Enabled = false;
            await ModelPusher.Instance.Push();
        }
    }
}

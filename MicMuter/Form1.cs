using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MicMuter
{
    public partial class Form1 : Form
    {
        WindowsMicrophoneMuteLibrary.WindowsMicMute micMute = null;
        public Form1()
        {
            InitializeComponent();
            micMute = new WindowsMicrophoneMuteLibrary.WindowsMicMute();
            Search();
        }

        void Search()
        {
            String[] devices = micMute.SearchDevices();
            listView1.Items.Clear();
            for (int i = 0; i < devices.Length; i++)
            {
                bool bMuted = micMute.GetMute(i);
                ListViewItem item = new ListViewItem(devices[i]);
                item.SubItems.Add(bMuted ? "Muted" : "Unmuted");
                listView1.Items.Add(item);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count == 0)
            {
                muteBtn.Enabled = false;
                return;
            }

            int index = listView1.SelectedIndices[0];
            if (listView1.Items[index].SubItems[1].Text.Equals("Muted"))
                muteBtn.Text = "Unmute";
            else
                muteBtn.Text = "Mute";
            muteBtn.Enabled = true;
        }

        private void muteBtn_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count == 0)
            {
                muteBtn.Enabled = false;
                return;
            }

            int index = listView1.SelectedIndices[0];
            bool bMuted;
            if (listView1.Items[index].SubItems[1].Text.Equals("Muted"))
                bMuted = true;
            else
                bMuted = false;

            if (micMute.MuteMic(index, !bMuted))
            {
                if (bMuted)
                {
                    muteBtn.Text = "Mute";
                    listView1.Items[index].SubItems[1].Text = "Unmuted";
                }
                else
                {
                    muteBtn.Text = "Unmute";
                    listView1.Items[index].SubItems[1].Text = "Muted";
                }
            }
        }
    }
}

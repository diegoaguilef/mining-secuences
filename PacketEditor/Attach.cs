﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace PacketEditor
{
    public partial class Attach : Form
    {
        public int pID = 0;
        private string procPath = "";

        public string GetpPath()
        {
            return procPath;
        }

        public Attach()
        {
            InitializeComponent();
        }

        private void frmAttach_Load(object sender, EventArgs e)
        {
            int i;
            
            Process[] processlist = Process.GetProcesses();
            i = 0;
            foreach(Process theprocess in processlist) {
                dgridAttach.Rows.Add();
                dgridAttach.Rows[i].Selected = false;
                dgridAttach.Rows[i].Cells["id"].Value = theprocess.Id.ToString("X8");
                dgridAttach.Rows[i].Cells["name"].Value = theprocess.ProcessName;
                dgridAttach.Rows[i].Cells["window"].Value = theprocess.MainWindowTitle;
                try
                {
                    dgridAttach.Rows[i].Cells["path"].Value = theprocess.MainModule.FileName;
                }
                catch {}
                if (dgridAttach.Rows[i].Cells["path"].Value == null)
                    dgridAttach.Rows.RemoveAt(i);
                else
                    i++;
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnAttach_Click(object sender, EventArgs e)
        {
            if (dgridAttach.SelectedRows.Count != 0)
            {
                pID = int.Parse((string)dgridAttach.SelectedRows[0].Cells["id"].Value,System.Globalization.NumberStyles.HexNumber);
                procPath = (string)dgridAttach.SelectedRows[0].Cells["path"].Value;
                this.Close();
            }
            else
            {
                MessageBox.Show(this, "You must select a process.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        private void dgridAttach_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27)
            {
                this.Close();
            }
        }
        private void frmAttach_Activated(object sender, EventArgs e)
        {
            if (this.TopMost == true)
            {
                this.Opacity = 1;
            }
        }
        private void frmAttach_Deactivate(object sender, EventArgs e)
        {
            if (this.TopMost == true)
            {
                this.Opacity = .5;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace PacketEditor
{
    public partial class EditFilter : Form
    {
      
        public bool retval;

        DataRow drFilters;
        Main.SockInfo sinfo;

        void UpdateUI(DataRow dr)
        {
            this.txtMsgReplace.TextChanged -= new System.EventHandler(this.txtMsgReplace_TextChanged);
            this.txtAPIReplace.TextChanged -= new System.EventHandler(this.txtAPIReplace_TextChanged);
            this.txtDNSReplace.TextChanged -= new System.EventHandler(this.txtMsgReplace_TextChanged);
            this.cmbMsgActionE.SelectedIndexChanged -= new System.EventHandler(this.cmbMsgActionE_SelectedIndexChanged);
            this.cmbAPIActionE.SelectedIndexChanged -= new System.EventHandler(this.cmbAPIActionE_SelectedIndexChanged);
            this.cmbDNSActionE.SelectedIndexChanged -= new System.EventHandler(this.cmbDNSActionE_SelectedIndexChanged);

            if (dr["id"].ToString() != String.Empty)
            {
                txtName.Text = dr["id"].ToString();
                chkEnabled.Checked = (bool)dr["enabled"];
                foreach (byte b in (byte[])dr["MsgFunction"])
                {
                    chkMsg.SetItemChecked(chkMsg.FindStringExact(sinfo.msg(b)), true);
                }
                txtMsgCatch.Text = dr["MsgCatch"].ToString();
                switch ((byte)dr["MsgAction"])
                {
                    case Glob.ActionReplaceString:
                        rdoMsgActionR.Checked = true;
                        break;
                    case Glob.ActionReplaceStringH:
                        rdoMsgActionR.Checked = true;
                        rdoMsgMethodH.Checked = true;
                        break;
                    case Glob.ActionError:
                        rdoMsgActionE.Checked = true;
                        break;
                    case Glob.ActionErrorH:
                        rdoMsgActionE.Checked = true;
                        rdoMsgMethodH.Checked = true;
                        break;
                }
                txtMsgReplace.Text = dr["MsgReplace"].ToString();
                cmbMsgActionE.Text = sinfo.error((int)dr["MsgError"]);
                foreach (byte b in (byte[])dr["APIFunction"])
                {
                    chkAPI.SetItemChecked(chkAPI.FindStringExact(sinfo.api(b)), true);
                }
                txtAPICatch.Text = dr["APICatch"].ToString();
                switch ((byte)dr["APIAction"])
                {
                    case Glob.ActionReplaceString:
                        rdoAPIActionR.Checked = true;
                        break;
                    case Glob.ActionReplaceStringH:
                        rdoAPIActionR.Checked = true;
                        rdoAPIMethodH.Checked = true;
                        break;
                    case Glob.ActionError:
                        rdoAPIActionE.Checked = true;
                        break;
                    case Glob.ActionErrorH:
                        rdoAPIActionE.Checked = true;
                        rdoAPIMethodH.Checked = true;
                        break;
                }
                txtAPIReplace.Text = dr["APIReplace"].ToString();
                cmbAPIActionE.Text = sinfo.error((int)dr["APIError"]);
                foreach (byte b in (byte[])dr["DNSFunction"])
                {
                    chkDNS.SetItemChecked(chkDNS.FindStringExact(sinfo.api(b)), true);
                }
                txtDNSCatch.Text = dr["DNSCatch"].ToString();
                switch ((byte)dr["DNSAction"])
                {
                    case Glob.ActionReplaceString:
                        rdoDNSActionR.Checked = true;
                        break;
                    case Glob.ActionReplaceStringH:
                        rdoDNSActionR.Checked = true;
                        rdoDNSMethodH.Checked = true;
                        break;
                    case Glob.ActionError:
                        rdoDNSActionE.Checked = true;
                        break;
                    case Glob.ActionErrorH:
                        rdoDNSActionE.Checked = true;
                        rdoDNSMethodH.Checked = true;
                        break;
                }
                txtDNSReplace.Text = dr["DNSReplace"].ToString();
                cmbDNSActionE.Text = sinfo.error((int)dr["DNSError"]);
            }
            else
            {
                cmbMsgActionE.Text = "NO_ERROR";
                cmbAPIActionE.Text = "NO_ERROR";
                cmbDNSActionE.Text = "NO_ERROR";
            }
            this.txtMsgReplace.TextChanged += new System.EventHandler(this.txtMsgReplace_TextChanged);
            this.txtAPIReplace.TextChanged += new System.EventHandler(this.txtAPIReplace_TextChanged);
            this.txtDNSReplace.TextChanged += new System.EventHandler(this.txtMsgReplace_TextChanged);
            this.cmbMsgActionE.SelectedIndexChanged += new System.EventHandler(this.cmbMsgActionE_SelectedIndexChanged);
            this.cmbAPIActionE.SelectedIndexChanged += new System.EventHandler(this.cmbAPIActionE_SelectedIndexChanged);
            this.cmbDNSActionE.SelectedIndexChanged += new System.EventHandler(this.cmbDNSActionE_SelectedIndexChanged);
        }
        void UpdateDR(DataRow dr)
        {
            byte t;
            dr["id"] = txtName.Text;
            dr["enabled"] = chkEnabled.Checked;
            byte[] b = new byte[chkMsg.CheckedItems.Count];
            for (int i = 0; i < chkMsg.CheckedItems.Count; i++) 
            {
                b[i] = sinfo.msgnum(chkMsg.CheckedItems[i].ToString());
            }
            dr["MsgFunction"] = b;
            dr["MsgCatch"] = txtMsgCatch.Text;
            if (rdoMsgActionR.Checked == true)
                t = Glob.ActionReplaceString;
            else
                t = Glob.ActionError;
            if (rdoMsgMethodH.Checked == true)
                t++;
            dr["MsgAction"] = t;
            dr["MsgReplace"] = txtMsgReplace.Text;
            dr["MsgError"] = sinfo.errornum(cmbMsgActionE.Text);
            b = new byte[chkAPI.CheckedItems.Count];
            for (int i = 0; i < chkAPI.CheckedItems.Count; i++)
            {
                b[i] = sinfo.apinum(chkAPI.CheckedItems[i].ToString());
            }
            dr["APIFunction"] = b;
            dr["APICatch"] = txtAPICatch.Text;
            if (rdoAPIActionR.Checked == true)
                t = Glob.ActionReplaceString;
            else
                t = Glob.ActionError;
            if (rdoAPIMethodH.Checked == true)
                t++;
            dr["APIAction"] = t;
            dr["APIReplace"] = txtAPIReplace.Text;
            dr["APIError"] = sinfo.errornum(cmbAPIActionE.Text);
            b = new byte[chkDNS.CheckedItems.Count];
            for (int i = 0; i < chkDNS.CheckedItems.Count; i++)
            {
                b[i] = sinfo.apinum(chkDNS.CheckedItems[i].ToString());
            }
            dr["DNSFunction"] = b;
            dr["DNSCatch"] = txtDNSCatch.Text;
            if (rdoDNSActionR.Checked == true)
                t = Glob.ActionReplaceString;
            else
                t = Glob.ActionError;
            if (rdoDNSMethodH.Checked == true)
                t++;
            dr["DNSAction"] = t;
            dr["DNSReplace"] = txtDNSReplace.Text;
            dr["DNSError"] = sinfo.errornum(cmbDNSActionE.Text);
        }

        public EditFilter(DataRow dr, Main.SockInfo si)
        {
            InitializeComponent();
            drFilters = dr;
            sinfo = si;
            UpdateUI(dr);
        }
        private void frmEditFilters_Activated(object sender, EventArgs e)
        {
            if (this.TopMost == true)
            {
                this.Opacity = 1;
            }
        }
        private void frmEditFilters_Deactivate(object sender, EventArgs e)
        {
            if (this.TopMost == true)
            {
                this.Opacity = .5;
            }
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            txtName.Text = txtName.Text.Trim();
            
            if (txtName.Text != String.Empty)
            {
                if (txtMsgCatch.Text != String.Empty)
                {
                        try { Regex.Match("", txtMsgCatch.Text); }
                        catch (ArgumentException)
                        {
                            tabControl1.SelectedTab = this.tabPage1;
                            txtMsgCatch.Focus();
                            MessageBox.Show("Invalid expression.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                }
                if (txtAPICatch.Text != String.Empty)
                {
                    try { Regex.Match("", txtAPICatch.Text); }
                    catch (ArgumentException)
                    {
                        tabControl1.SelectedTab = this.tabPage2;
                        txtAPICatch.Focus();
                        MessageBox.Show("Invalid expression.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (txtDNSCatch.Text != String.Empty)
                {
                    try { Regex.Match("", txtDNSCatch.Text); }
                    catch (ArgumentException)
                    {
                        tabControl1.SelectedTab = this.tabPage3;
                        txtDNSCatch.Focus();
                        MessageBox.Show("Invalid expression.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                UpdateDR(drFilters);
                retval = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("You must enter a filter name.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtName.Focus();
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            retval = false;
            this.Close();
        }
        private void txtMsgReplace_TextChanged(object sender, EventArgs e)
        {
            if (rdoMsgActionR.Checked == false)
                rdoMsgActionR.Checked = true;
        }
        private void txtAPIReplace_TextChanged(object sender, EventArgs e)
        {
            if (rdoAPIActionR.Checked == false)
                rdoAPIActionR.Checked = true;
        }
        private void txtDNSReplace_TextChanged(object sender, EventArgs e)
        {
            if (rdoDNSActionR.Checked == false)
                rdoDNSActionR.Checked = true;
        }
        private void cmbMsgActionE_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdoMsgActionE.Checked == false)
                rdoMsgActionE.Checked = true;
        }
        private void cmbAPIActionE_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdoAPIActionE.Checked == false)
                rdoAPIActionE.Checked = true;
        }
        private void cmbDNSActionE_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdoDNSActionE.Checked == false)
                rdoDNSActionE.Checked = true;
        }
    }
}

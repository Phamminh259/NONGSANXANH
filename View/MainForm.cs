using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NONGSANXANH
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnNhaCungCap_Click(object sender, EventArgs e)
        {
            var control = new UserControlNHACUNGCAP();
            panelUserControl.Controls.Clear();
            panelUserControl.Controls.Add(control);

        }

        private void btnPhieuNhap_Click(object sender, EventArgs e)
        {
            var control = new UserControlPHIEUNHAP();
            panelUserControl.Controls.Clear();
            panelUserControl.Controls.Add(control);

        }

        private void btnChiTietPhieuNhap_Click(object sender, EventArgs e)
        {
            var control = new UserControlCHITIETPHIEUNHAP();
            panelUserControl.Controls.Clear();
            panelUserControl.Controls.Add(control);

        }
    }
}

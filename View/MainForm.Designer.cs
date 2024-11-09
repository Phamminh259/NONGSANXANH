namespace NONGSANXANH
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnNhaCungCap = new System.Windows.Forms.Button();
            this.btnPhieuNhap = new System.Windows.Forms.Button();
            this.btnChiTietPhieuNhap = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelUserControl = new System.Windows.Forms.Panel();
            this.userControlPHIEUNHAP1 = new NONGSANXANH.UserControlPHIEUNHAP();
            this.userControlNHACUNGCAP1 = new NONGSANXANH.UserControlNHACUNGCAP();
            this.userControlCHITIETPHIEUNHAP1 = new NONGSANXANH.UserControlCHITIETPHIEUNHAP();
            this.panel1.SuspendLayout();
            this.panelUserControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnNhaCungCap
            // 
            this.btnNhaCungCap.Location = new System.Drawing.Point(26, 43);
            this.btnNhaCungCap.Name = "btnNhaCungCap";
            this.btnNhaCungCap.Size = new System.Drawing.Size(142, 46);
            this.btnNhaCungCap.TabIndex = 0;
            this.btnNhaCungCap.Text = "NHÀ CUNG CẤP";
            this.btnNhaCungCap.UseVisualStyleBackColor = true;
            this.btnNhaCungCap.Click += new System.EventHandler(this.btnNhaCungCap_Click);
            // 
            // btnPhieuNhap
            // 
            this.btnPhieuNhap.Location = new System.Drawing.Point(26, 125);
            this.btnPhieuNhap.Name = "btnPhieuNhap";
            this.btnPhieuNhap.Size = new System.Drawing.Size(142, 46);
            this.btnPhieuNhap.TabIndex = 1;
            this.btnPhieuNhap.Text = "PHIẾU NHẬP";
            this.btnPhieuNhap.UseVisualStyleBackColor = true;
            this.btnPhieuNhap.Click += new System.EventHandler(this.btnPhieuNhap_Click);
            // 
            // btnChiTietPhieuNhap
            // 
            this.btnChiTietPhieuNhap.Location = new System.Drawing.Point(26, 202);
            this.btnChiTietPhieuNhap.Name = "btnChiTietPhieuNhap";
            this.btnChiTietPhieuNhap.Size = new System.Drawing.Size(142, 46);
            this.btnChiTietPhieuNhap.TabIndex = 2;
            this.btnChiTietPhieuNhap.Text = "CHI TIẾT PHIẾU NHẬP";
            this.btnChiTietPhieuNhap.UseVisualStyleBackColor = true;
            this.btnChiTietPhieuNhap.Click += new System.EventHandler(this.btnChiTietPhieuNhap_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnNhaCungCap);
            this.panel1.Controls.Add(this.btnPhieuNhap);
            this.panel1.Controls.Add(this.btnChiTietPhieuNhap);
            this.panel1.Location = new System.Drawing.Point(2, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 552);
            this.panel1.TabIndex = 3;
            // 
            // panelUserControl
            // 
            this.panelUserControl.Controls.Add(this.userControlPHIEUNHAP1);
            this.panelUserControl.Controls.Add(this.userControlNHACUNGCAP1);
            this.panelUserControl.Controls.Add(this.userControlCHITIETPHIEUNHAP1);
            this.panelUserControl.Location = new System.Drawing.Point(240, 3);
            this.panelUserControl.Name = "panelUserControl";
            this.panelUserControl.Size = new System.Drawing.Size(856, 558);
            this.panelUserControl.TabIndex = 4;
            // 
            // userControlPHIEUNHAP1
            // 
            this.userControlPHIEUNHAP1.Location = new System.Drawing.Point(3, 3);
            this.userControlPHIEUNHAP1.Name = "userControlPHIEUNHAP1";
            this.userControlPHIEUNHAP1.Size = new System.Drawing.Size(856, 558);
            this.userControlPHIEUNHAP1.TabIndex = 2;
            // 
            // userControlNHACUNGCAP1
            // 
            this.userControlNHACUNGCAP1.Location = new System.Drawing.Point(3, 3);
            this.userControlNHACUNGCAP1.Name = "userControlNHACUNGCAP1";
            this.userControlNHACUNGCAP1.Size = new System.Drawing.Size(856, 558);
            this.userControlNHACUNGCAP1.TabIndex = 1;
            // 
            // userControlCHITIETPHIEUNHAP1
            // 
            this.userControlCHITIETPHIEUNHAP1.Location = new System.Drawing.Point(3, 3);
            this.userControlCHITIETPHIEUNHAP1.Name = "userControlCHITIETPHIEUNHAP1";
            this.userControlCHITIETPHIEUNHAP1.Size = new System.Drawing.Size(856, 558);
            this.userControlCHITIETPHIEUNHAP1.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1108, 573);
            this.Controls.Add(this.panelUserControl);
            this.Controls.Add(this.panel1);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.panel1.ResumeLayout(false);
            this.panelUserControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnNhaCungCap;
        private System.Windows.Forms.Button btnPhieuNhap;
        private System.Windows.Forms.Button btnChiTietPhieuNhap;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelUserControl;
        private UserControlPHIEUNHAP userControlPHIEUNHAP1;
        private UserControlNHACUNGCAP userControlNHACUNGCAP1;
        private UserControlCHITIETPHIEUNHAP userControlCHITIETPHIEUNHAP1;
    }
}
namespace NONGSANXANH
{
    partial class UserControlCHITIETPHIEUNHAP
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtIDPN = new System.Windows.Forms.TextBox();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.dataGridViewChiTiet = new System.Windows.Forms.DataGridView();
            this.txtSoPhieu = new System.Windows.Forms.TextBox();
            this.tenHangHoaColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.soLuongNhapColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ngaySanXuatColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.giaNhapColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hanSuDungColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblTongTien = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewChiTiet)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(305, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(250, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "CHI TIẾT PHIẾU NHẬP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(217, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "IDPN";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(168, 137);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 24);
            this.label3.TabIndex = 2;
            this.label3.Text = "SỐ PHIẾU";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // txtIDPN
            // 
            this.txtIDPN.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIDPN.Location = new System.Drawing.Point(320, 77);
            this.txtIDPN.Name = "txtIDPN";
            this.txtIDPN.Size = new System.Drawing.Size(235, 29);
            this.txtIDPN.TabIndex = 3;
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(172, 487);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(86, 35);
            this.buttonDelete.TabIndex = 4;
            this.buttonDelete.Text = "DELETE";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(369, 487);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(86, 35);
            this.buttonSave.TabIndex = 5;
            this.buttonSave.Text = "SAVE";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(577, 487);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(86, 35);
            this.buttonClose.TabIndex = 6;
            this.buttonClose.Text = "CLOSE";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // dataGridViewChiTiet
            // 
            this.dataGridViewChiTiet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewChiTiet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tenHangHoaColumn,
            this.soLuongNhapColumn,
            this.ngaySanXuatColumn,
            this.giaNhapColumn,
            this.hanSuDungColumn});
            this.dataGridViewChiTiet.Location = new System.Drawing.Point(140, 200);
            this.dataGridViewChiTiet.Name = "dataGridViewChiTiet";
            this.dataGridViewChiTiet.Size = new System.Drawing.Size(544, 222);
            this.dataGridViewChiTiet.TabIndex = 7;
            // 
            // txtSoPhieu
            // 
            this.txtSoPhieu.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSoPhieu.Location = new System.Drawing.Point(320, 134);
            this.txtSoPhieu.Name = "txtSoPhieu";
            this.txtSoPhieu.Size = new System.Drawing.Size(235, 29);
            this.txtSoPhieu.TabIndex = 8;
            // 
            // tenHangHoaColumn
            // 
            this.tenHangHoaColumn.HeaderText = "Tên hàng hóa";
            this.tenHangHoaColumn.Name = "tenHangHoaColumn";
            // 
            // soLuongNhapColumn
            // 
            this.soLuongNhapColumn.HeaderText = "Số lượng nhập";
            this.soLuongNhapColumn.Name = "soLuongNhapColumn";
            // 
            // ngaySanXuatColumn
            // 
            this.ngaySanXuatColumn.HeaderText = "Ngày sản xuất";
            this.ngaySanXuatColumn.Name = "ngaySanXuatColumn";
            // 
            // giaNhapColumn
            // 
            this.giaNhapColumn.HeaderText = "Giá nhập";
            this.giaNhapColumn.Name = "giaNhapColumn";
            // 
            // hanSuDungColumn
            // 
            this.hanSuDungColumn.HeaderText = "Hạn sử dụng";
            this.hanSuDungColumn.Name = "hanSuDungColumn";
            // 
            // lblTongTien
            // 
            this.lblTongTien.AutoSize = true;
            this.lblTongTien.Location = new System.Drawing.Point(628, 436);
            this.lblTongTien.Name = "lblTongTien";
            this.lblTongTien.Size = new System.Drawing.Size(35, 13);
            this.lblTongTien.TabIndex = 9;
            this.lblTongTien.Text = "label4";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(546, 436);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "TỔNG TIỀN";
            // 
            // UserControlCHITIETPHIEUNHAP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblTongTien);
            this.Controls.Add(this.txtSoPhieu);
            this.Controls.Add(this.dataGridViewChiTiet);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.txtIDPN);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "UserControlCHITIETPHIEUNHAP";
            this.Size = new System.Drawing.Size(856, 558);
            this.Load += new System.EventHandler(this.UserControlCHITIETPHIEUNHAP_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewChiTiet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtIDPN;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.DataGridView dataGridViewChiTiet;
        private System.Windows.Forms.DataGridViewComboBoxColumn tenHangHoaColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn soLuongNhapColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ngaySanXuatColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn giaNhapColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn hanSuDungColumn;
        private System.Windows.Forms.TextBox txtSoPhieu;
        private System.Windows.Forms.Label lblTongTien;
        private System.Windows.Forms.Label label4;
    }
}

using NONGSANXANH.Controller;
using NONGSANXANH.Model;
using NONGSANXANH.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NONGSANXANH
{
    public partial class UserControlCHITIETPHIEUNHAP : UserControl,IView
    {
        HangHoaController hangHoaController = new HangHoaController();
        PhieuNhapController phieuNhapController = new PhieuNhapController();
        ChiTietPhieuNhapController chiTietPhieuNhapController = new ChiTietPhieuNhapController();
        public UserControlCHITIETPHIEUNHAP()
        {
            InitializeComponent();
            LoadId();
            SetupDataGridView();
            this.Load += UserControlCHITIETPHIEUNHAP_Load;
            buttonSave.Click += buttonSave_Click;
            buttonDelete.Click += buttonDelete_Click;
            buttonClose.Click += buttonClose_Click;
            dataGridViewChiTiet.CellValueChanged += dataGridViewChiTiet_CellValueChanged;
        }
        public void LoadId()
        {
            var allEntries = phieuNhapController.GetAll(); // Replace with an existing method
            var idMax = allEntries.Max(x => x.IDPN);
            txtIDPN.Text = (idMax + 1).ToString(); // Automatically assign next ID
        }

        private void SetupDataGridView()
        {
            // Cột Tên hàng hóa
            var tenHangHoaColumn = new DataGridViewComboBoxColumn();
            tenHangHoaColumn.HeaderText = "Tên hàng hóa";
            tenHangHoaColumn.Name = "tenHangHoa";
            hangHoaController.Load();
            tenHangHoaColumn.DataSource = hangHoaController.Items.Cast<HangHoaModel>().ToList();
            tenHangHoaColumn.DisplayMember = "TenHangHoa";
            tenHangHoaColumn.ValueMember = "MaHangHoa";
            dataGridViewChiTiet.Columns.Add(tenHangHoaColumn);

            // Cột Số lượng nhập
            var soLuongNhapColumn = new DataGridViewTextBoxColumn();
            soLuongNhapColumn.HeaderText = "Số lượng nhập";
            soLuongNhapColumn.Name = "soLuongNhap";
            dataGridViewChiTiet.Columns.Add(soLuongNhapColumn);

            // Cột Ngày sản xuất
            var ngaySanXuatColumn = new DataGridViewTextBoxColumn();
            ngaySanXuatColumn.HeaderText = "Ngày sản xuất";
            ngaySanXuatColumn.Name = "ngaySanXuat";
            dataGridViewChiTiet.Columns.Add(ngaySanXuatColumn);

            // Cột Giá nhập
            var giaNhapColumn = new DataGridViewTextBoxColumn();
            giaNhapColumn.HeaderText = "Giá nhập";
            giaNhapColumn.Name = "giaNhap";
            dataGridViewChiTiet.Columns.Add(giaNhapColumn);

            // Cột Hạn sử dụng
            var hanSuDungColumn = new DataGridViewTextBoxColumn();
            hanSuDungColumn.HeaderText = "Hạn sử dụng";
            hanSuDungColumn.Name = "hanSuDung";
            dataGridViewChiTiet.Columns.Add(hanSuDungColumn);

            // Cài đặt chế độ tự động điều chỉnh cột
            dataGridViewChiTiet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void dataGridViewChiTiet_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra nếu cột thay đổi là Số lượng nhập hoặc Giá nhập
            if (e.ColumnIndex == dataGridViewChiTiet.Columns["soLuongNhap"].Index ||
                e.ColumnIndex == dataGridViewChiTiet.Columns["giaNhap"].Index)
            {
                decimal tongTien = 0;

                // Duyệt qua từng dòng trong DataGridView để tính tổng tiền
                foreach (DataGridViewRow row in dataGridViewChiTiet.Rows)
                {
                    if (row.Cells["soLuongNhap"].Value != null && row.Cells["giaNhap"].Value != null)
                    {
                        if (int.TryParse(row.Cells["soLuongNhap"].Value.ToString(), out int soLuong) &&
                            decimal.TryParse(row.Cells["giaNhap"].Value.ToString(), out decimal giaNhap))
                        {
                            tongTien += soLuong * giaNhap;
                        }
                    }
                }

                // Cập nhật label tổng tiền
                lblTongTien.Text = $"Tổng tiền: {tongTien.ToString("C0", CultureInfo.GetCultureInfo("vi-VN"))}";
            }
        }
        private void UserControlCHITIETPHIEUNHAP_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        public void SetDataToText(object item)
        {
            throw new NotImplementedException();
        }

        public IModel GetDataFromText()
        {
            throw new NotImplementedException();
        }

        public void clearDataFromText()
        {
            throw new NotImplementedException();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            // Get receipt number
            var soPhieu = txtSoPhieu.Text;
            if (string.IsNullOrEmpty(soPhieu))
            {
                MessageBox.Show("Vui lòng nhập Số Phiếu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Other information
            var phieuNhap = new PhieuNhapModel
            {
                IDPN = int.Parse(txtIDPN.Text),
                NgayNhap = dateTimePickerNgayNhap.Value,
                SoPhieu = soPhieu // Assuming PhieuNhapModel has a SoPhieu property
            };

            // Validation
            if (phieuNhapController.Create(phieuNhap))
            {
                foreach (DataGridViewRow row in dataGridViewChiTiet.Rows)
                {
                    if (row.IsNewRow) continue;

                    var maHangHoa = row.Cells["tenHangHoa"].Value;
                    var soLuongNhap = row.Cells["soLuongNhap"].Value;
                    var giaNhap = row.Cells["giaNhap"].Value;
                    var ngaySanXuat = row.Cells["ngaySanXuat"].Value;
                    var hanSuDung = row.Cells["hanSuDung"].Value;

                    if (maHangHoa == null || soLuongNhap == null || giaNhap == null || ngaySanXuat == null || hanSuDung == null)
                    {
                        MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }

                    if (int.TryParse(maHangHoa.ToString(), out int maHangHoaInt) &&
                        int.TryParse(soLuongNhap.ToString(), out int soLuong) &&
                        decimal.TryParse(giaNhap.ToString(), out decimal gia) &&
                        DateTime.TryParse(ngaySanXuat.ToString(), out DateTime ngaySX) &&
                        DateTime.TryParse(hanSuDung.ToString(), out DateTime hanSD))
                    {
                        var chiTietPhieuNhap = new ChiTietPhieuNhapModel
                        {
                            
                            MaHangHoa = maHangHoaInt,
                            SoLuongNhap = soLuong,
                            GiaNhap = gia,
                            NgaySanXuat = ngaySX,
                            HangSuDung = hanSD
                        };

                        chiTietPhieuNhapController.Create(chiTietPhieuNhap);
                    }
                }

                MessageBox.Show("Lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Không thể lưu phiếu nhập!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtIDPN.Text, out int id))
            {
                chiTietPhieuNhapController.Delete(id);
                MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Dispose(); // Close form
        }
    }
}

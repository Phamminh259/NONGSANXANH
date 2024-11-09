using NONGSANXANH.Model;
using NONGSANXANH.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NONGSANXANH.Controller;
using NONGSANXANH.Model;

namespace NONGSANXANH
{
    public partial class UserControlPHIEUNHAP : UserControl,IView
    {
        private PhieuNhapController _controller = new PhieuNhapController();
        public UserControlPHIEUNHAP()
        {
            InitializeComponent();
            LoadDataToDataGridView();
        }
        private void LoadDataToDataGridView()
        {
            if (_controller.Load())
            {
                dataGridViewPHIEUNHAP.AutoGenerateColumns = false;
                dataGridViewPHIEUNHAP.Columns.Clear();

                dataGridViewPHIEUNHAP.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "SoPhieu",
                    Name = "SoPhieu",
                    HeaderText = "Số Phiếu",
                    Width = 100,
                    DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft }
                });

                dataGridViewPHIEUNHAP.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "MaNCC",
                    Name = "MaNCC",
                    HeaderText = "Mã Nhà Cung Cấp",
                    Width = 150,
                    DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft }
                });

                dataGridViewPHIEUNHAP.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "NgayNhap",
                    Name = "NgayNhap",
                    HeaderText = "Ngày Nhập",
                    Width = 150,
                    DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft }
                });

                dataGridViewPHIEUNHAP.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridViewPHIEUNHAP.DataSource = _controller.Items.Cast<PhieuNhapModel>().ToList();
            }
            else
            {
                MessageBox.Show("Không có dữ liệu để hiển thị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void clearDataFromText()
        {
            txtSoPhieu.Clear();
            txtMaNCC.Clear();
            dateNgayNhap.Value = DateTime.Now;
        }

        public IModel GetDataFromText()
        {
            int soPhieu;
            if (!int.TryParse(txtSoPhieu.Text, out soPhieu))
            {
                MessageBox.Show("Số phiếu không hợp lệ. Vui lòng nhập lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null; // Return null or handle the error as appropriate
            }

            string maNCC = txtMaNCC.Text;
            DateTime ngayNhap = dateNgayNhap.Value;

            return new PhieuNhapModel(soPhieu, maNCC, ngayNhap);
        }

        public void SetDataToText(object item)
        {
            if (item is PhieuNhapModel phieuNhap)
            {
                txtSoPhieu.Text = phieuNhap.SoPhieu.ToString();  // Convert int to string
                txtMaNCC.Text = phieuNhap.MaNCC;
                dateNgayNhap.Value = phieuNhap.NgayNhap;
            }
            else
            {
                MessageBox.Show("Dữ liệu không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridViewPHIEUNHAP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridViewPHIEUNHAP.Rows[e.RowIndex];

                // Ensure SoPhieu is parsed correctly as an integer
                int soPhieu = int.Parse(selectedRow.Cells["SoPhieu"].Value?.ToString() ?? "0");

                // Create PhieuNhapModel using the constructor
                var phieuNhap = new PhieuNhapModel(
                    soPhieu,  // SoPhieu as int
                    selectedRow.Cells["MaNCC"].Value?.ToString(),  // MaNCC as string
                    DateTime.Parse(selectedRow.Cells["NgayNhap"].Value?.ToString())  // NgayNhap as DateTime
                );

                SetDataToText(phieuNhap);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu TextBox chưa có giá trị SoPhieu, lấy số phiếu tiếp theo
            if (string.IsNullOrEmpty(txtSoPhieu.Text))
            {
                txtSoPhieu.Text = _controller.GetNextSoPhieu().ToString();
            }

            var phieuNhap = (PhieuNhapModel)GetDataFromText();

            // Kiểm tra dữ liệu hợp lệ
            if (!_controller.IsValid(phieuNhap))
            {
                MessageBox.Show("Dữ liệu không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra nếu SoPhieu đã tồn tại trong CSDL
            if (phieuNhap.SoPhieu != 0 && _controller.IsSoPhieuExists(phieuNhap.SoPhieu)) // Kiểm tra SoPhieu có tồn tại không
            {
                // Kiểm tra điều kiện MACC phải giống với MANCC trong bảng NHACUNGCAP
                if (!_controller.IsMaNCCExist(phieuNhap.MaNCC))
                {
                    MessageBox.Show("Mã nhà cung cấp không tồn tại trong hệ thống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Nếu SoPhieu đã tồn tại, thì cập nhật phiếu nhập
                if (_controller.Update(phieuNhap)) // Cập nhật
                {
                    MessageBox.Show("Cập nhật phiếu nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataToDataGridView(); // Cập nhật lại danh sách sau khi cập nhật
                    txtSoPhieu.Text = _controller.GetNextSoPhieu().ToString(); // Cập nhật lại số phiếu tiếp theo vào TextBox
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi cập nhật phiếu nhập.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else // Nếu có giá trị SoPhieu, thì thêm mới phiếu nhập
            {
                // Kiểm tra điều kiện MACC phải giống với MANCC trong bảng NHACUNGCAP
                if (!_controller.IsMaNCCExist(phieuNhap.MaNCC))
                {
                    MessageBox.Show("Mã nhà cung cấp không tồn tại trong hệ thống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Nếu SoPhieu không có giá trị (tức là phiếu mới, SoPhieu tự động tăng trong CSDL)
                if (_controller.Create(phieuNhap)) // Thêm mới
                {
                    MessageBox.Show("Thêm mới phiếu nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataToDataGridView(); // Cập nhật lại danh sách sau khi thêm
                    txtSoPhieu.Text = _controller.GetNextSoPhieu().ToString(); // Cập nhật lại số phiếu tiếp theo vào TextBox
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi thêm mới phiếu nhập.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewPHIEUNHAP.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa các phiếu nhập đã chọn?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    bool success = true; // Biến kiểm tra nếu có lỗi khi xóa
                    string deletedSoPhieuList = ""; // Biến để lưu các SoPhieu đã xóa

                    // Lặp qua các dòng được chọn
                    foreach (DataGridViewRow row in dataGridViewPHIEUNHAP.SelectedRows)
                    {
                        string soPhieu = row.Cells["SoPhieu"].Value.ToString();

                        if (!string.IsNullOrEmpty(soPhieu))
                        {
                            // Xóa phiếu nhập
                            if (_controller.Delete(soPhieu))
                            {
                                deletedSoPhieuList += soPhieu + "\n"; // Thêm vào danh sách phiếu đã xóa
                            }
                            else
                            {
                                success = false; // Nếu có lỗi trong quá trình xóa, đổi giá trị thành false
                            }
                        }
                    }

                    // Hiển thị thông báo chung
                    if (success)
                    {
                        MessageBox.Show($"Xóa thành công các phiếu nhập:\n{deletedSoPhieuList}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi xảy ra trong quá trình xóa. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    // Cập nhật lại danh sách
                    LoadDataToDataGridView();

                    // Xóa dữ liệu trong các TextBox sau khi xóa phiếu nhập
                    ClearTextBoxes(); // Gọi phương thức để xóa dữ liệu trong các TextBox
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn ít nhất một phiếu nhập để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void ClearTextBoxes()
        {
            // Dọn dẹp dữ liệu trong các TextBox
            txtSoPhieu.Clear();
            txtMaNCC.Clear();

            // Đặt giá trị của DateTimePicker thành DateTime.Today (hoặc có thể dùng DateTime.Now nếu bạn muốn ngày và giờ hiện tại)
            dateNgayNhap.Value = DateTime.Today;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void UserControlPHIEUNHAP_Load(object sender, EventArgs e)
        {
            txtSoPhieu.Text = _controller.GetNextSoPhieu().ToString();
        }
    }
}

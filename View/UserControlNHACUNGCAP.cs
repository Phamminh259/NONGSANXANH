using NONGSANXANH.Controller;
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

namespace NONGSANXANH
{
    public partial class UserControlNHACUNGCAP : UserControl,IView
    {
        NHACUNGCAPController _controller = new NHACUNGCAPController();
        public UserControlNHACUNGCAP()
        {
            InitializeComponent();
            LoadDataToDataGridView();

        }
        private void LoadDataToDataGridView()
        {
            if (_controller.Load())
            {
                dataGridViewNHACUNGCAP.AutoGenerateColumns = false;
                dataGridViewNHACUNGCAP.Columns.Clear();

                // Add columns for the DataGridView, including the new fields
                dataGridViewNHACUNGCAP.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "MaNCC",
                    Name = "MaNCC",
                    HeaderText = "Mã Nhà Cung Cấp",
                    Width = 100,
                    DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft }
                });

                dataGridViewNHACUNGCAP.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "TenNCC",
                    Name = "TenNCC",
                    HeaderText = "Tên Nhà Cung Cấp",
                    Width = 200,
                    DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft }
                });

                dataGridViewNHACUNGCAP.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "DiaChi",
                    Name = "DiaChi",
                    HeaderText = "Địa Chỉ",
                    Width = 200,
                    DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft }
                });

                dataGridViewNHACUNGCAP.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "DienThoai",
                    Name = "DienThoai",
                    HeaderText = "Điện Thoại",
                    Width = 120,
                    DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft }
                });

                // Add new columns for STK (Số Tài Khoản) and TenNH (Tên Ngân Hàng)
                dataGridViewNHACUNGCAP.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "STK",
                    Name = "STK",
                    HeaderText = "Số Tài Khoản",
                    Width = 150,
                    DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft }
                });

                dataGridViewNHACUNGCAP.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "TenNH",
                    Name = "TenNH",
                    HeaderText = "Tên Ngân Hàng",
                    Width = 150,
                    DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft }
                });

                dataGridViewNHACUNGCAP.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dataGridViewNHACUNGCAP.DataSource = null;
                dataGridViewNHACUNGCAP.DataSource = _controller.Items.Cast<NHACUNGCAPModel>().ToList();
            }
            else
            {
                MessageBox.Show("Không có dữ liệu để hiển thị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void clearDataFromText()
        {
            throw new NotImplementedException();
        }

        public IModel GetDataFromText()
        {
            return new NHACUNGCAPModel
            {
                MaNCC = txtMaNCC.Text,
                TenNCC = txtTenNCC.Text,
                DiaChi = txtDiaChi.Text,
                DienThoai = txtDienThoai.Text,
                STK = txtSTK.Text,  // Get new field STK
                TenNH = txtTenNH.Text // Get new field TenNH
            };
        }

        public void SetDataToText(object item)
        {
            if (item is NHACUNGCAPModel nhaCungCap)
            {
                txtMaNCC.Text = nhaCungCap.MaNCC;
                txtTenNCC.Text = nhaCungCap.TenNCC;
                txtDiaChi.Text = nhaCungCap.DiaChi;
                txtDienThoai.Text = nhaCungCap.DienThoai;
                txtSTK.Text = nhaCungCap.STK;  // Set new field STK
                txtTenNH.Text = nhaCungCap.TenNH; // Set new field TenNH
            }
            else
            {
                MessageBox.Show("Dữ liệu không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridViewNHACUNGCAP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridViewNHACUNGCAP.Rows[e.RowIndex];

                // Create NHACUNGCAPModel object from the selected row
                NHACUNGCAPModel nhaCungCap = new NHACUNGCAPModel
                {
                    MaNCC = selectedRow.Cells["MaNCC"].Value?.ToString(),
                    TenNCC = selectedRow.Cells["TenNCC"].Value?.ToString(),
                    DiaChi = selectedRow.Cells["DiaChi"].Value?.ToString(),
                    DienThoai = selectedRow.Cells["DienThoai"].Value?.ToString(),
                    STK = selectedRow.Cells["STK"].Value?.ToString(),
                    TenNH = selectedRow.Cells["TenNH"].Value?.ToString()
                };

                SetDataToText(nhaCungCap);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        { // Kiểm tra có dòng nào được chọn không
            if (dataGridViewNHACUNGCAP.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa các nhà cung cấp đã chọn?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Duyệt qua các dòng đã chọn và xóa
                    foreach (DataGridViewRow row in dataGridViewNHACUNGCAP.SelectedRows)
                    {
                        // Lấy mã NCC từ dòng đã chọn
                        string maNCC = row.Cells["MaNCC"].Value.ToString();  // Thay "MaNCC" bằng tên cột đúng

                        // In ra maNCC để kiểm tra giá trị
                        Console.WriteLine("Đang xóa nhà cung cấp với mã NCC: " + maNCC);

                        if (!string.IsNullOrEmpty(maNCC))
                        {
                            bool isDeleted = _controller.Delete(maNCC);

                            if (isDeleted)
                            {
                                MessageBox.Show($"Xóa nhà cung cấp {maNCC} thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show($"Không tìm thấy nhà cung cấp {maNCC} để xóa hoặc có lỗi xảy ra.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }

                    // Tải lại dữ liệu sau khi xóa
                    LoadDataToDataGridView();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn ít nhất một nhà cung cấp để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            // Nếu không có mã NCC (tạo mới), làm mờ txtMaNCC
            if (string.IsNullOrEmpty(txtMaNCC.Text))
            {
                txtMaNCC.Enabled = false;  // Hoặc txtMaNCC.ReadOnly = true;
            }

            // Tiến hành xử lý lưu dữ liệu như bình thường
            var nhaCungCap = (NHACUNGCAPModel)GetDataFromText();

            if (!_controller.IsValid(nhaCungCap))
            {
                MessageBox.Show("Dữ liệu không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(txtMaNCC.Text)) // Tạo mới nhà cung cấp
            {
                bool isCreated = _controller.Create(nhaCungCap);

                if (isCreated)
                {
                    MessageBox.Show("Thêm mới nhà cung cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataToDataGridView();  // Tải lại dữ liệu trong DataGridView
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi thêm mới nhà cung cấp.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else // Cập nhật nhà cung cấp
            {
                bool isUpdated = _controller.Update(nhaCungCap);

                if (isUpdated)
                {
                    MessageBox.Show("Cập nhật nhà cung cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataToDataGridView();  // Tải lại dữ liệu trong DataGridView
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi cập nhật nhà cung cấp.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void UserControlNHACUNGCAP_Load(object sender, EventArgs e)
        {
           
        }
    }
}

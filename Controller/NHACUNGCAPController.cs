using NONGSANXANH.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NONGSANXANH.Model;
using System.Windows.Forms;
using NONGSANXANH.View;

namespace NONGSANXANH.Controller
{
    public class NHACUNGCAPController : IController
    {
        private string connectionString = "Data Source=DESKTOP-PU3ER6F\\SQLEXPRESS;Initial Catalog=QuanLyBanHangNet;Integrated Security=True;TrustServerCertificate=True";
        public List<IModel> Items { get; private set; } = new List<IModel>();

        public NHACUNGCAPController()
        {
            Load();

        }

        // Load tất cả các nhà cung cấp từ cơ sở dữ liệu
        public bool Load()
        {
            try
            {
                Items.Clear();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM NHACUNGCAP", conn);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Items.Add(new NHACUNGCAPModel(
                            reader["maNCC"].ToString(),
                            reader["tenNCC"].ToString(),
                            reader["diaChi"].ToString(),
                            reader["dienThoai"].ToString(),
                            reader["STK"].ToString(),
                            reader["tenNH"].ToString()));
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        // Load nhà cung cấp theo id
        public bool Load(object id)
        {
            try
            {
                Items.Clear();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM NHACUNGCAP WHERE maNCC = @maNCC", conn);
                    command.Parameters.AddWithValue("@maNCC", id);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        Items.Add(new NHACUNGCAPModel(
                            reader["maNCC"].ToString(),
                            reader["tenNCC"].ToString(),
                            reader["diaChi"].ToString(),
                            reader["dienThoai"].ToString(),
                            reader["STK"].ToString(),
                            reader["tenNH"].ToString()));
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        // Tạo mới nhà cung cấp
        public bool Create(IModel model)
        {
            try
            {
                NHACUNGCAPModel nhacungcap = model as NHACUNGCAPModel;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Kiểm tra tên nhà cung cấp đã tồn tại trong cơ sở dữ liệu chưa
                    SqlCommand checkCommand = new SqlCommand(
                        "SELECT COUNT(*) FROM NHACUNGCAP WHERE TenNCC = @tenNCC", conn);
                    checkCommand.Parameters.AddWithValue("@tenNCC", nhacungcap.TenNCC);

                    int count = (int)checkCommand.ExecuteScalar();

                    // Nếu tên nhà cung cấp đã tồn tại, trả về false và thông báo lỗi
                    if (count > 0)
                    {
                        MessageBox.Show("Tên nhà cung cấp đã tồn tại. Vui lòng nhập tên khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    // Nếu tên nhà cung cấp chưa tồn tại, thực hiện thêm mới
                    SqlCommand command = new SqlCommand(
                        "INSERT INTO NHACUNGCAP (tenNCC, diaChi, dienThoai, STK, tenNH) VALUES (@tenNCC, @diaChi, @dienThoai, @STK, @tenNH)", conn);

                    command.Parameters.AddWithValue("@tenNCC", nhacungcap.TenNCC);
                    command.Parameters.AddWithValue("@diaChi", nhacungcap.DiaChi);
                    command.Parameters.AddWithValue("@dienThoai", nhacungcap.DienThoai);
                    command.Parameters.AddWithValue("@STK", nhacungcap.STK);
                    command.Parameters.AddWithValue("@tenNH", nhacungcap.TenNH);

                    command.ExecuteNonQuery();
                }

                // Cập nhật lại danh sách nhà cung cấp sau khi thêm mới thành công
                Load();
                MessageBox.Show("Thêm mới nhà cung cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra khi thêm mới nhà cung cấp: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Cập nhật nhà cung cấp
        public bool Update(IModel model)
        {
            try
            {
                NHACUNGCAPModel nhacungcap = model as NHACUNGCAPModel;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(
                        "UPDATE NHACUNGCAP SET tenNCC = @tenNCC, diaChi = @diaChi, dienThoai = @dienThoai, STK = @STK, tenNH = @tenNH WHERE maNCC = @maNCC", conn);
                    command.Parameters.AddWithValue("@maNCC", nhacungcap.MaNCC);
                    command.Parameters.AddWithValue("@tenNCC", nhacungcap.TenNCC);
                    command.Parameters.AddWithValue("@diaChi", nhacungcap.DiaChi);
                    command.Parameters.AddWithValue("@dienThoai", nhacungcap.DienThoai);
                    command.Parameters.AddWithValue("@STK", nhacungcap.STK);
                    command.Parameters.AddWithValue("@tenNH", nhacungcap.TenNH);
                    command.ExecuteNonQuery();
                }
                Load();  // Cập nhật lại danh sách
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        // Xóa nhà cung cấp
        public bool Delete(object id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("DELETE FROM NHACUNGCAP WHERE maNCC = @maNCC", conn);
                    command.Parameters.AddWithValue("@maNCC", id);
                    command.ExecuteNonQuery();
                }
                Load();  // Cập nhật lại danh sách
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        // Đọc thông tin nhà cung cấp theo id
        public IModel Read(object id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM NHACUNGCAP WHERE maNCC = @maNCC", conn);
                    command.Parameters.AddWithValue("@maNCC", id);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        return new NHACUNGCAPModel(
                            reader["maNCC"].ToString(),
                            reader["tenNCC"].ToString(),
                            reader["diaChi"].ToString(),
                            reader["dienThoai"].ToString(),
                            reader["STK"].ToString(),
                            reader["tenNH"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        // Kiểm tra xem nhà cung cấp có tồn tại theo id
        public bool IsExist(object id)
        {
            //try
            //{
            //    using (SqlConnection conn = new SqlConnection(connectionString))
            //    {
            //        conn.Open();
            //        SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM NHACUNGCAP WHERE maNCC = @maNCC", conn);
            //        command.Parameters.AddWithValue("@maNCC", id);
            //        int count = (int)command.ExecuteScalar();
            //        return count > 0;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //    return false;
            //}
            return true;
        }

        // Kiểm tra xem nhà cung cấp có tồn tại trong danh sách
        public bool IsExist(IModel model)
        {
            //NHACUNGCAPModel nhacungcap = model as NHACUNGCAPModel;
            //return IsExist(nhacungcap?.MaNCC);
            return true;
        }
        public bool IsValue(NHACUNGCAPModel nhaCungCap)
        {
            if (string.IsNullOrEmpty(nhaCungCap.TenNCC))
            {
                MessageBox.Show("Tên NCC không được để trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrEmpty(nhaCungCap.DiaChi))
            {
                MessageBox.Show("Địa chỉ NCC không được để trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrEmpty(nhaCungCap.DienThoai))
            {
                MessageBox.Show("Số điện thoại NCC không được để trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!long.TryParse(nhaCungCap.DienThoai, out _))
            {
                MessageBox.Show("Số điện thoại phải là một chuỗi số hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        public bool IsValid(NHACUNGCAPModel nhaCungCap)
        {

            if (nhaCungCap == null)
            {
                MessageBox.Show("Đối tượng nhà cung cấp không được null.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(nhaCungCap.TenNCC))
            {
                MessageBox.Show("Tên nhà cung cấp không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(nhaCungCap.DiaChi))
            {
                MessageBox.Show("Địa chỉ nhà cung cấp không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(nhaCungCap.DienThoai))
            {
                MessageBox.Show("Số điện thoại nhà cung cấp không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!long.TryParse(nhaCungCap.DienThoai, out _))
            {
                MessageBox.Show("Số điện thoại phải là một chuỗi số hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        public bool IsValue(string columnName, object value)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = $"SELECT COUNT(*) FROM NHACUNGCAP WHERE {columnName} = @value";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@value", value);
                int count = (int)cmd.ExecuteScalar();

                return count > 0; // Nếu đã tồn tại, trả về true
            }
        }
        public bool Update(NHACUNGCAPModel nhaCungCap)
        {
            try
            {
                // Kiểm tra dữ liệu hợp lệ
                if (!IsValid(nhaCungCap))
                {
                    return false;
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Kiểm tra xem TenNCC đã tồn tại trong hệ thống chưa (chỉ kiểm tra với những dòng khác mã NCC)
                    string checkQuery = "SELECT COUNT(*) FROM NHACUNGCAP WHERE TenNCC = @TenNCC AND MaNCC != @MaNCC";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@TenNCC", nhaCungCap.TenNCC);
                    checkCmd.Parameters.AddWithValue("@MaNCC", nhaCungCap.MaNCC);  // Chỉ kiểm tra những bản ghi khác mã NCC

                    int count = (int)checkCmd.ExecuteScalar();

                    // Nếu TenNCC đã tồn tại trong hệ thống
                    if (count > 0)
                    {
                        MessageBox.Show("Tên nhà cung cấp đã tồn tại. Vui lòng nhập tên khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    // Cập nhật thông tin nhà cung cấp
                    string updateQuery = "UPDATE NHACUNGCAP SET TenNCC = @TenNCC, DiaChi = @DiaChi, DienThoai = @DienThoai, STK = @STK, tenNH = @tenNH WHERE MaNCC = @MaNCC";
                    SqlCommand updateCmd = new SqlCommand(updateQuery, conn);

                    // Thêm tham số cho các trường cần cập nhật
                    updateCmd.Parameters.AddWithValue("@TenNCC", nhaCungCap.TenNCC);
                    updateCmd.Parameters.AddWithValue("@DiaChi", nhaCungCap.DiaChi);
                    updateCmd.Parameters.AddWithValue("@DienThoai", nhaCungCap.DienThoai);
                    updateCmd.Parameters.AddWithValue("@STK", nhaCungCap.STK);
                    updateCmd.Parameters.AddWithValue("@tenNH", nhaCungCap.TenNH);
                    updateCmd.Parameters.AddWithValue("@MaNCC", nhaCungCap.MaNCC); // Sử dụng MaNCC để xác định bản ghi cần cập nhật

                    int rowsAffected = updateCmd.ExecuteNonQuery();

                    // Kiểm tra nếu có dòng bị ảnh hưởng (tức là bản ghi đã được cập nhật)
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Cập nhật nhà cung cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy nhà cung cấp với mã NCC đã nhập.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"Có lỗi SQL khi cập nhật nhà cung cấp: {sqlEx.Message}", "Lỗi SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra khi cập nhật nhà cung cấp: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Lấy mã nhà cung cấp tiếp theo
        // Method to get the next MaNCC
       





    }
}
using NONGSANXANH.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NONGSANXANH.Controller
{
    internal class ChiTietPhieuNhapController : IController
    {
        private List<IModel> _items;
        private readonly string _connectionString = "Data Source=DESKTOP-PU3ER6F\\SQLEXPRESS;Initial Catalog=QuanLyBanHangNet;Integrated Security=True;TrustServerCertificate=True";

        public ChiTietPhieuNhapController()
        {
            _items = new List<IModel>();
        }

        public List<IModel> Items => _items;

        // Tạo mới chi tiết phiếu nhập
        public bool Create(IModel model)
        {
            try
            {
                if (model is ChiTietPhieuNhapModel chiTietPhieuNhap)
                {
                    // Kiểm tra mã hàng hóa có tồn tại trong bảng HangHoa
                    if (!CheckMaHangHoa(chiTietPhieuNhap.MaHangHoa))
                    {
                        MessageBox.Show("Mã hàng hóa không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    // Kiểm tra số phiếu nhập có hợp lệ
                    if (!CheckSoPhieu(chiTietPhieuNhap.MaPhieuNhap))
                    {
                        MessageBox.Show("Số phiếu không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();
                        string query = "INSERT INTO CHITIETPHIEUNHAP (maPhieuNhap, maHangHoa, soLuongNhap, giaNhap, ngaySanXuat, hangSuDung) " +
                                       "VALUES (@maPhieuNhap, @maHangHoa, @soLuongNhap, @giaNhap, @ngaySanXuat, @hangSuDung)";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@maPhieuNhap", chiTietPhieuNhap.MaPhieuNhap);
                            command.Parameters.AddWithValue("@maHangHoa", chiTietPhieuNhap.MaHangHoa);
                            command.Parameters.AddWithValue("@soLuongNhap", chiTietPhieuNhap.SoLuongNhap);
                            command.Parameters.AddWithValue("@giaNhap", chiTietPhieuNhap.GiaNhap ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@ngaySanXuat", chiTietPhieuNhap.NgaySanXuat ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@hangSuDung", chiTietPhieuNhap.HangSuDung ?? (object)DBNull.Value);

                            int rowsAffected = command.ExecuteNonQuery();
                            return rowsAffected > 0;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo chi tiết phiếu nhập: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Cập nhật chi tiết phiếu nhập
        public bool Update(IModel model)
        {
            try
            {
                if (model is ChiTietPhieuNhapModel chiTietPhieuNhap)
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();
                        string query = "UPDATE CHITIETPHIEUNHAP SET maPhieuNhap = @maPhieuNhap, maHangHoa = @maHangHoa, " +
                                       "soLuongNhap = @soLuongNhap, giaNhap = @giaNhap, ngaySanXuat = @ngaySanXuat, hangSuDung = @hangSuDung " +
                                       "WHERE IDPN = @IDPN";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@IDPN", chiTietPhieuNhap.IDPN);
                            command.Parameters.AddWithValue("@maPhieuNhap", chiTietPhieuNhap.MaPhieuNhap);
                            command.Parameters.AddWithValue("@maHangHoa", chiTietPhieuNhap.MaHangHoa);
                            command.Parameters.AddWithValue("@soLuongNhap", chiTietPhieuNhap.SoLuongNhap);
                            command.Parameters.AddWithValue("@giaNhap", chiTietPhieuNhap.GiaNhap ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@ngaySanXuat", chiTietPhieuNhap.NgaySanXuat ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@hangSuDung", chiTietPhieuNhap.HangSuDung ?? (object)DBNull.Value);

                            int rowsAffected = command.ExecuteNonQuery();
                            return rowsAffected > 0;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật chi tiết phiếu nhập: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Xóa chi tiết phiếu nhập
        public bool Delete(object id)
        {
            try
            {
                if (id is int idPn)
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();
                        string query = "DELETE FROM CHITIETPHIEUNHAP WHERE IDPN = @IDPN";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@IDPN", idPn);
                            int rowsAffected = command.ExecuteNonQuery();
                            return rowsAffected > 0;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa chi tiết phiếu nhập: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Đọc thông tin chi tiết phiếu nhập theo ID
        public IModel Read(object id)
        {
            try
            {
                if (id is int idPn)
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();
                        string query = "SELECT IDPN, maPhieuNhap, maHangHoa, soLuongNhap, giaNhap, ngaySanXuat, hangSuDung FROM CHITIETPHIEUNHAP WHERE IDPN = @IDPN";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@IDPN", idPn);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    return new ChiTietPhieuNhapModel
                                    {
                                        IDPN = reader.GetInt32(0),
                                        MaPhieuNhap = reader.GetInt32(1),
                                        MaHangHoa = reader.GetInt32(2),
                                        SoLuongNhap = reader.GetInt32(3),
                                        GiaNhap = reader.IsDBNull(4) ? (decimal?)null : reader.GetDecimal(4),
                                        NgaySanXuat = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5),
                                        HangSuDung = reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6)
                                    };
                                }
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đọc thông tin chi tiết phiếu nhập: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        // Kiểm tra mã hàng hóa có tồn tại trong bảng HangHoa
        private bool CheckMaHangHoa(int maHangHoa)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(1) FROM HANGHOA WHERE maHangHoa = @maHangHoa";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@maHangHoa", maHangHoa);
                    return (int)command.ExecuteScalar() > 0;
                }
            }
        }

        // Kiểm tra số phiếu nhập có hợp lệ
        private bool CheckSoPhieu(int maPhieuNhap)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(1) FROM PHIEUNHAP WHERE maPhieuNhap = @maPhieuNhap";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@maPhieuNhap", maPhieuNhap);
                    return (int)command.ExecuteScalar() > 0;
                }
            }
        }

        // Lấy IDPN tiếp theo
        public int GetNextId()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "SELECT ISNULL(MAX(IDPN), 0) + 1 FROM CHITIETPHIEUNHAP";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        return (int)command.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy ID lớn nhất: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1; // Trả về -1 nếu có lỗi
            }
        }

        public bool Load()
        {
            try
            {
                _items.Clear(); // Xóa danh sách hiện tại

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "SELECT IDPN, maPhieuNhap, maHangHoa, soLuongNhap, giaNhap, ngaySanXuat, hangSuDung FROM CHITIETPHIEUNHAP";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var chiTietPhieuNhap = new ChiTietPhieuNhapModel
                                {
                                    IDPN = reader.GetInt32(0),
                                    MaPhieuNhap = reader.GetInt32(1),
                                    MaHangHoa = reader.GetInt32(2),
                                    SoLuongNhap = reader.GetInt32(3),
                                    GiaNhap = reader.IsDBNull(4) ? (decimal?)null : reader.GetDecimal(4),
                                    NgaySanXuat = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5),
                                    HangSuDung = reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6)
                                };
                                _items.Add(chiTietPhieuNhap); // Thêm vào danh sách
                            }
                        }
                    }
                }
                return true; // Thành công
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // Thất bại
            }
        }

        public bool Load(object id)
        {
            try
            {
                if (id is int idPn)
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();
                        string query = "SELECT IDPN, maPhieuNhap, maHangHoa, soLuongNhap, giaNhap, ngaySanXuat, hangSuDung FROM CHITIETPHIEUNHAP WHERE IDPN = @IDPN";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@IDPN", idPn);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    var chiTietPhieuNhap = new ChiTietPhieuNhapModel
                                    {
                                        IDPN = reader.GetInt32(0),
                                        MaPhieuNhap = reader.GetInt32(1),
                                        MaHangHoa = reader.GetInt32(2),
                                        SoLuongNhap = reader.GetInt32(3),
                                        GiaNhap = reader.IsDBNull(4) ? (decimal?)null : reader.GetDecimal(4),
                                        NgaySanXuat = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5),
                                        HangSuDung = reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6)
                                    };
                                    _items.Clear();
                                    _items.Add(chiTietPhieuNhap); // Thêm vào danh sách
                                    return true;
                                }
                            }
                        }
                    }
                }
                return false; // Nếu ID không hợp lệ
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải chi tiết phiếu nhập: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // Thất bại
            }
        }

        public bool IsExist(object id)
        {
            try
            {
                if (id is int idPn)
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();
                        string query = "SELECT COUNT(1) FROM CHITIETPHIEUNHAP WHERE IDPN = @IDPN";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@IDPN", idPn);
                            int count = (int)command.ExecuteScalar();
                            return count > 0; // Nếu có bản ghi thì trả về true
                        }
                    }
                }
                return false; // Nếu ID không hợp lệ
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi kiểm tra sự tồn tại: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // Thất bại
            }
        }

        public bool IsExist(IModel model)
        {
            try
            {
                if (model is ChiTietPhieuNhapModel chiTietPhieuNhap)
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();
                        string query = "SELECT COUNT(1) FROM CHITIETPHIEUNHAP WHERE maPhieuNhap = @maPhieuNhap AND maHangHoa = @maHangHoa";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@maPhieuNhap", chiTietPhieuNhap.MaPhieuNhap);
                            command.Parameters.AddWithValue("@maHangHoa", chiTietPhieuNhap.MaHangHoa);
                            int count = (int)command.ExecuteScalar();
                            return count > 0; // Nếu có bản ghi thì trả về true
                        }
                    }
                }
                return false; // Nếu không phải kiểu ChiTietPhieuNhapModel
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi kiểm tra sự tồn tại: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // Thất bại
            }
        }


    }
}

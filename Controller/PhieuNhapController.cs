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
    public class PhieuNhapController : IController
    {
        private string connectionString = "Data Source=DESKTOP-PU3ER6F\\SQLEXPRESS;Initial Catalog=QuanLyBanHangNet;Integrated Security=True;TrustServerCertificate=True";
        public List<IModel> Items { get; private set; } = new List<IModel>();

        public PhieuNhapController()
        {
            Load();
        }

        // Load tất cả các phiếu nhập từ cơ sở dữ liệu
        public bool Load()
        {
            try
            {
                Items.Clear();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM PHIEUNHAP", conn);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Items.Add(new PhieuNhapModel(
                            Convert.ToInt32(reader["SoPhieu"]),    // Chuyển đổi SoPhieu thành số nguyên
                            reader["MaNCC"].ToString(),
                            DateTime.Parse(reader["NgayNhap"].ToString())
                        ));
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        // Load phiếu nhập theo số phiếu
        public bool Load(object id)
        {
            try
            {
                Items.Clear();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM PHIEUNHAP WHERE SoPhieu = @SoPhieu", conn);
                    command.Parameters.AddWithValue("@SoPhieu", id);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        Items.Add(new PhieuNhapModel(
                            Convert.ToInt32(reader["SoPhieu"]),
                            reader["MaNCC"].ToString(),
                            DateTime.Parse(reader["NgayNhap"].ToString())));
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        // Tạo mới phiếu nhập
        public bool Create(IModel model)
        {
            try
            {
                PhieuNhapModel phieuNhap = model as PhieuNhapModel;

                // Kiểm tra dữ liệu đầu vào (MaNCC và NgayNhap)
                if (!IsValid(phieuNhap))
                {
                    return false; // Nếu không hợp lệ, không tiếp tục
                }

                // Mở kết nối đến CSDL
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(
                        "INSERT INTO PHIEUNHAP (MaNCC, NgayNhap) VALUES (@MaNCC, @NgayNhap)", conn);
                    command.Parameters.AddWithValue("@MaNCC", phieuNhap.MaNCC);
                    command.Parameters.AddWithValue("@NgayNhap", phieuNhap.NgayNhap);

                    // Thực thi câu lệnh SQL
                    command.ExecuteNonQuery();
                }

                // Cập nhật lại danh sách phiếu nhập
                Load();

                // Thông báo thành công
                MessageBox.Show("Thêm mới phiếu nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (Exception ex)
            {
                // Thông báo lỗi nếu có ngoại lệ
                MessageBox.Show($"Có lỗi xảy ra khi thêm mới phiếu nhập: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public bool IsMaNCCExist(string maNCC)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM NHACUNGCAP WHERE MaNCC = @MaNCC", conn);
                    command.Parameters.AddWithValue("@MaNCC", maNCC);

                    int count = (int)command.ExecuteScalar();
                    return count > 0; // Nếu count > 0, MaNCC tồn tại trong bảng NHACUNGCAP
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
       public bool IsSoPhieuExists(int soPhieu)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Sử dụng câu lệnh SQL để kiểm tra xem SoPhieu có tồn tại trong bảng PHIEUNHAP không
                    SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM PHIEUNHAP WHERE SoPhieu = @SoPhieu", conn);
                    command.Parameters.AddWithValue("@SoPhieu", soPhieu);

                    // Thực thi câu lệnh SQL và kiểm tra số lượng phiếu nhập có cùng SoPhieu
                    int count = (int)command.ExecuteScalar();

                    return count > 0; // Nếu count > 0, SoPhieu đã tồn tại trong bảng PHIEUNHAP
                }
            }
            catch (Exception ex)
            {
                // Ghi lại thông báo lỗi nếu có ngoại lệ xảy ra
                Console.WriteLine(ex.Message);
                return false; // Nếu có lỗi, trả về false
            }
        }
       

        // Cập nhật phiếu nhập
        public bool Update(IModel model)
        {
            try
            {
                PhieuNhapModel phieuNhap = model as PhieuNhapModel;

                // Mở kết nối đến CSDL
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(
                        "UPDATE PHIEUNHAP SET MaNCC = @MaNCC, NgayNhap = @NgayNhap WHERE SoPhieu = @SoPhieu", conn);
                    command.Parameters.AddWithValue("@SoPhieu", phieuNhap.SoPhieu);  // Phải có giá trị hợp lệ để cập nhật
                    command.Parameters.AddWithValue("@MaNCC", phieuNhap.MaNCC);
                    command.Parameters.AddWithValue("@NgayNhap", phieuNhap.NgayNhap);

                    // Thực thi câu lệnh SQL
                    command.ExecuteNonQuery();
                }

                // Cập nhật lại danh sách phiếu nhập
                Load();

                // Thông báo thành công
                MessageBox.Show("Cập nhật phiếu nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (Exception ex)
            {
                // Thông báo lỗi nếu có ngoại lệ
                MessageBox.Show($"Có lỗi xảy ra khi cập nhật phiếu nhập: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Xóa phiếu nhập
        public bool Delete(object id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("DELETE FROM PHIEUNHAP WHERE SoPhieu = @SoPhieu", conn);
                    command.Parameters.AddWithValue("@SoPhieu", id);

                    command.ExecuteNonQuery();
                }

                Load(); // Cập nhật lại danh sách phiếu nhập
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        // Đọc thông tin phiếu nhập theo số phiếu
        public IModel Read(object id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM PHIEUNHAP WHERE SoPhieu = @SoPhieu", conn);
                    command.Parameters.AddWithValue("@SoPhieu", id);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        return new PhieuNhapModel(
                            Convert.ToInt32(reader["SoPhieu"]),
                            reader["MaNCC"].ToString(),
                            DateTime.Parse(reader["NgayNhap"].ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        // Kiểm tra xem phiếu nhập có tồn tại theo số phiếu
        public bool IsExist(object id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM PHIEUNHAP WHERE SoPhieu = @SoPhieu", conn);
                    command.Parameters.AddWithValue("@SoPhieu", id);

                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        // Kiểm tra dữ liệu đầu vào của phiếu nhập
        public bool IsValid(PhieuNhapModel phieuNhap)
        {

            // Kiểm tra nếu phieuNhap là null
            if (phieuNhap == null)
            {
                MessageBox.Show("Dữ liệu phiếu nhập không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Kiểm tra MaNCC không để trống
            if (string.IsNullOrEmpty(phieuNhap.MaNCC))
            {
                MessageBox.Show("Mã nhà cung cấp không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Kiểm tra NgayNhap không hợp lệ
            if (phieuNhap.NgayNhap == DateTime.MinValue)
            {
                MessageBox.Show("Ngày nhập không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Không kiểm tra SoPhieu vì có thể để trống hoặc nhập tùy ý

            return true;
        }

        public bool IsExist(IModel model)
        {

            try
            {
                PhieuNhapModel phieuNhap = model as PhieuNhapModel;
                if (phieuNhap == null) return false;

                // Kiểm tra sự tồn tại của SoPhieu trong CSDL
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM PHIEUNHAP WHERE SoPhieu = @SoPhieu", conn);
                    command.Parameters.AddWithValue("@SoPhieu", phieuNhap.SoPhieu);

                    int count = (int)command.ExecuteScalar();
                    return count > 0; // Nếu count > 0, phiếu nhập tồn tại
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public int GetNextSoPhieu()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("SELECT ISNULL(MAX(SoPhieu), 0) + 1 FROM PHIEUNHAP", conn);
                    int nextSoPhieu = (int)command.ExecuteScalar();
                    return nextSoPhieu;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 1;  // Trả về 1 nếu không có phiếu nào trong bảng
            }
        }

    }
}

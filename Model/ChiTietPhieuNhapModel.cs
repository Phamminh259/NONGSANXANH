using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NONGSANXANH.Model
{
    internal class ChiTietPhieuNhapModel : IModel
    {
        public int IDPN { get; set; } // Khóa chính
        public int MaPhieuNhap { get; set; } // Mã phiếu nhập, liên kết tới bảng PHIEUNHAP
        public int MaHangHoa { get; set; } // Mã hàng hóa
        public int SoLuongNhap { get; set; } // Số lượng nhập
        public decimal? GiaNhap { get; set; } // Giá nhập
        public DateTime? NgaySanXuat { get; set; } // Ngày sản xuất của hàng hóa
        public DateTime? HangSuDung { get; set; } // Hạn sử dụng của hàng hóa

        // Hàm tạo mặc định
        public ChiTietPhieuNhapModel() { }

        // Hàm tạo với các tham số
        public ChiTietPhieuNhapModel(int idPn, int maPhieuNhap, int maHangHoa, int soLuongNhap, decimal? giaNhap, DateTime? ngaySanXuat, DateTime? hangSuDung)
        {
            IDPN = idPn;
            MaPhieuNhap = maPhieuNhap;
            MaHangHoa = maHangHoa;
            SoLuongNhap = soLuongNhap;
            GiaNhap = giaNhap;
            NgaySanXuat = ngaySanXuat;
            HangSuDung = hangSuDung;
        }
    }
}